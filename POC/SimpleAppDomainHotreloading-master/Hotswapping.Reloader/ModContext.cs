using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hotswapping.ModApi;
using Hotswapping.ModApi.Marshalling;

namespace Hotswapping.Reloader
{
    public class ModContext : IDisposable
    {
        public static AppDomain domain;
        private bool isDisposed;
        private List<IMod> modEntries;
        public static DomainMediator proxy;

        private ModContext(string fileName)
        {
            if (fileName == null) throw new ArgumentNullException(nameof(fileName));
            if (!File.Exists(fileName)) throw new FileNotFoundException("Cannot find .dll file for mod.", fileName);

            FileName = fileName;
            isDisposed = true; // No AppDomain until reload.
        }

        public ICollection<IMod> ModEntries =>
            modEntries == null || !modEntries.Any() ? (modEntries = proxy.GetMods()) : modEntries;

        public string FileName { get; }

        public void Dispose()
        {
            if (isDisposed) return;

            AppDomain.Unload(domain);
            domain = null;
            modEntries = null;
            isDisposed = true;
        }

        public static ModContext Create(string modDllPath)
        {
            var sandbox = new ModContext(modDllPath);

            return sandbox;
        }

        public void Reload()
        {
            Dispose();

            domain = AppDomain.CreateDomain(Directory.GetParent(FileName).Name, AppDomain.CurrentDomain.Evidence,
                ModDomainSetup.Default);
            proxy = (DomainMediator)domain.CreateInstanceAndUnwrap(typeof(DomainMediator).Assembly.FullName,
                typeof(DomainMediator).FullName);

            // Load the mod's assembly using the remote AppDomain
            proxy.LoadAssembly(FileName);
            proxy.LoadAssembly("Hotswapping.exe");

            isDisposed = false;
        }
    }
}