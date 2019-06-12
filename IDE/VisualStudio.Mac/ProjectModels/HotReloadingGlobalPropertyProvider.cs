using System;
using System.Collections.Generic;
using System.Globalization;
using Log;
using MonoDevelop.Ide;
using MonoDevelop.Projects.MSBuild;
using PubSub.Extension;
using VisualStudio.Mac.Events;

namespace VisualStudio.Mac.ProjectModels
{
    public class HotReloadingGlobalPropertyProvider : IMSBuildGlobalPropertyProvider
    {
        Lazy<Dictionary<string, string>> properties = new Lazy<Dictionary<string, string>>(CreateProperties);

        public event EventHandler GlobalPropertiesChanged;

        public HotReloadingGlobalPropertyProvider()
        {
            this.Subscribe<EnableHotReloadingChangeEvent>(EnableHotReloadingChanged);
            this.Subscribe<AllowOverrideChangeEvent>(AllowOverrideChanged);
        }

        public IDictionary<string, string> GetGlobalProperties()
        {
            return properties.Value;
        }

        static Dictionary<string, string> CreateProperties()
        {
            var properties = new Dictionary<string, string>();
            properties.Add(Settings.EnabledHotReloadingKey, Settings.Enabled.ToString(CultureInfo.InvariantCulture));
            properties.Add(Settings.AllowOverrideKey, Settings.AllowOverride.ToString(CultureInfo.InvariantCulture));
            return properties;
        }

        private void EnableHotReloadingChanged(EnableHotReloadingChangeEvent obj)
        {
            var newValue = Settings.Enabled.ToString(CultureInfo.InvariantCulture);
            if (properties.Value.ContainsKey(Settings.EnabledHotReloadingKey))
            {
                properties.Value[Settings.EnabledHotReloadingKey] = newValue;
            }
            else
                properties.Value.Add(Settings.EnabledHotReloadingKey, newValue);

            NotifyGlobalPropertyChanged();
        }

        private void AllowOverrideChanged(AllowOverrideChangeEvent obj)
        {
            var newValue = Settings.AllowOverride.ToString(CultureInfo.InvariantCulture);
            if (properties.Value.ContainsKey(Settings.AllowOverrideKey))
            {
                properties.Value[Settings.AllowOverrideKey] = newValue;
            }
            else
                properties.Value.Add(Settings.AllowOverrideKey, newValue);

            NotifyGlobalPropertyChanged();
        }

        private static void NotifyGlobalPropertyChanged()
        {
            //Bug: Can you use below code due to this issue: https://github.com/mono/monodevelop/issues/7486
            //GlobalPropertiesChanged?.Invoke(this, EventArgs.Empty);

            //Workarround for above issue
            try
            {
                foreach (var project in IdeApp.Workspace.GetAllProjects())
                {
                    project.ShutdownProjectBuilder();
                }
            }
            catch (Exception ex)
            {
                Logger.Current.WriteError(ex);
            }
        }
    }
}
