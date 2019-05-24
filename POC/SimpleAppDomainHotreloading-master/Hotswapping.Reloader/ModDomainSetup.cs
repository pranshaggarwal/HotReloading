using System;

namespace Hotswapping.Reloader
{
    public static class ModDomainSetup
    {
        public static AppDomainSetup Default => new AppDomainSetup
        {
            ApplicationBase = Environment.CurrentDirectory,
            DisallowCodeDownload = true,
            ConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile,
            ShadowCopyFiles = true.ToString().ToLowerInvariant(),
            LoaderOptimization = LoaderOptimization.MultiDomainHost
        };
    }
}