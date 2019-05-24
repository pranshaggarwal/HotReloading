using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hotswapping.Reloader
{
    public class ModManager
    {
        private ModManager()
        {
            Mods = new Dictionary<string, ModContext>();
        }

        private Dictionary<string, ModContext> Mods { get; }

        public static ModManager Create(bool loadAssemblies = false)
        {
            var manager = new ModManager();

            foreach (var dir in Directory.GetDirectories("Mods"))
            {
                var modDll = Directory.GetFiles(dir, "*.dll").First();
                var modStub = ModContext.Create(modDll);
                manager.Mods.Add(Path.GetFileName(dir), modStub);
            }

            if (loadAssemblies) manager.Reload();

            return manager;
        }

        public void Reload()
        {
            foreach (var mod in Mods)
            {
                mod.Value.Reload();
            }
        }

        public IEnumerable<ModContext> GetModModules()
        {
            return Mods.Values;
        }
    }
}