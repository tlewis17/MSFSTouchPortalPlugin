using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MSFSTouchPortalPlugin_Generator.Configuration;
using MSFSTouchPortalPlugin_Generator.Interfaces;
using MSFSTouchPortalPlugin_Generator.Model;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using TouchPortalExtension.Attributes;

namespace MSFSTouchPortalPlugin_Generator {
  internal class GenerateEntry : IGenerateEntry {
    private readonly ILogger<GenerateEntry> _logger;
    private readonly IOptions<GeneratorOptions> _options;

    public GenerateEntry(ILogger<GenerateEntry> logger, IOptions<GeneratorOptions> options) {
      _logger = logger;
      _options = options;
    }

    public void Generate() {
      // Find assembly
      var a = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Where(a => a.Name == _options.Value.PluginName).FirstOrDefault();

      if (a == null) {
        throw new ArgumentNullException("Unable to load assembly for reflection.");
      }

      var assembly = Assembly.Load(a);

      var fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
      var version = fvi.FileVersion;

      // Setup Base Model
      var model = new Base {
        sdk = 2,
        version = int.Parse(version.Replace(".", "")),
        name = _options.Value.PluginName,
        id = _options.Value.PluginName
      };

      // Add Configuration
      // Add Plug Start Comand
      model.plugin_start_cmd = Path.Combine("%TP_PLUGIN_FOLDER%", "MSFS-TouchPortal-Plugin\\dist", "MSFSTouchPortalPlugin.exe");
      // Load asembly
      var _ = MSFSTouchPortalPlugin.Objects.AutoPilot.AutoPilot.AP_AIRSPEED_HOLD;

      var q = assembly.GetTypes().ToList();

      // Get all classes with the TouchPortalCategory
      var s = q.Where(t => t.CustomAttributes.Any(att => att.AttributeType == typeof(TouchPortalCategoryAttribute))).OrderBy(o => o.Name).ToList();

      // For each category, add to model
      s.ForEach(cat => {
        var att = (TouchPortalCategoryAttribute)Attribute.GetCustomAttribute(cat, typeof(TouchPortalCategoryAttribute));
        var category = new TouchPortalCategory {
          id = $"{_options.Value.PluginName}.{att.Id}",
          name = att.Name,
          imagepath = att.ImagePath
        };

        // Add actions
        var actions = cat.GetMembers().Where(t => t.CustomAttributes.Any(att => att.AttributeType == typeof(TouchPortalActionAttribute))).ToList();
        actions.ForEach(act => {
          var actionAttribute = (TouchPortalActionAttribute)Attribute.GetCustomAttribute(act, typeof(TouchPortalActionAttribute));
          var action = new TouchPortalAction {
            id = $"{category.id}.Action.{actionAttribute.Id}",
            name = actionAttribute.Name,
            prefix = actionAttribute.Prefix,
            type = actionAttribute.Type,
            description = actionAttribute.Description,
            tryInline = true,
            format = actionAttribute.Format,
          };

          // Has Choices
          var choiceAttributes = act.GetCustomAttributes<TouchPortalActionChoiceAttribute>()?.ToList();

          if (choiceAttributes.Count > 0) {
            for (int i = 0; i < choiceAttributes.Count; i++) {
              var data = new TouchPortalActionData {
                id = $"{action.id}.Data.{i}",
                type = "choice",
                label = "Action",
                defaultValue = choiceAttributes[i].DefaultValue,
                valueChoices = choiceAttributes[i].ChoiceValues
              };

              action.data.Add(data);
            }
            action.format = string.Format(action.format, action.data.Select(d => $"{{${d.id}$}}").ToArray());
          }

          category.actions.Add(action);
        });

        // Ordering
        category.actions = category.actions.OrderBy(c => c.name).ToList();

        // TODO: Non-Choice Types

        // TODO: Add states
        var states = cat.GetMembers().Where(t => t.CustomAttributes.Any(att => att.AttributeType == typeof(TouchPortalStateAttribute))).ToList();
        states.ForEach(state => {
          var stateAttribute = state.GetCustomAttribute<TouchPortalStateAttribute>();

          if (stateAttribute != null) {
            var newState = new TouchPortalState {
              id = $"{category.id}.State.{stateAttribute.Id}",
              type = stateAttribute.Type,
              description = $"{category.name} - {stateAttribute.Description}",
              defaultValue = stateAttribute.Default
            };

            category.states.Add(newState);
          }
        });

        // Ordering
        category.states = category.states.OrderBy(c => c.description).ToList();

        // TODO: Add events

        model.categories.Add(category);
      });

      // Ordering
      model.categories = model.categories.OrderBy(c => c.name).ToList();

      var context = new ValidationContext(model, null, null);
      var errors = new Collection<ValidationResult>();
      var isValid = Validator.TryValidateObject(model, context, errors, true);

      if (!isValid) {
        throw new AggregateException(
          errors.Select((e) => new ValidationException(e.ErrorMessage)));
      }

      var result = JsonConvert.SerializeObject(model, Formatting.Indented);
      File.WriteAllText(Path.Combine(_options.Value.TargetPath, "entry.tp"), result);
      _logger.LogInformation("entry.tp generated.");
    }
  }
}
