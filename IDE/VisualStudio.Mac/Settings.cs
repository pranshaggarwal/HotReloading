using System;
using MonoDevelop.Core;

namespace VisualStudio.Mac
{
    public static class Settings
    {
        private const string EnabledHotReloadingKey = "EnableHotReloading";
        private const string AllowOverrideKey = "AllowOverride";

        public static bool Enabled
        {
            get => PropertyService.Get<bool>(EnabledHotReloadingKey, true);
            set => PropertyService.Set(EnabledHotReloadingKey, value);
        }

        public static bool AllowOverride
        {
            get => PropertyService.Get<bool>(AllowOverrideKey, true);
            set => PropertyService.Set(AllowOverrideKey, value);
        }
    }
}
