using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Hotswapping.ModApi.Marshalling
{
    public class DomainMediator : MarshalByRefObject
    {
        public void LoadAssembly(string dllPath)
        {
            Assembly.LoadFrom(Path.GetFullPath(dllPath));
        }

        public List<IMod> GetMods()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
                .Where(t => typeof(IMod).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract &&
                            t != typeof(ModMarshallingWrapper))
                .Select(t =>
                {
                    var mod = Activator.CreateInstance(t);
                    var marshaller = (IMod)Activator.CreateInstance(typeof(ModMarshallingWrapper), mod);
                    return marshaller;
                }).ToList();
        }

        public object CreateInstance(Type type)
        {
            var mod = Activator.CreateInstance(type);
            var marshaller = (IMod)Activator.CreateInstance(typeof(ModMarshallingWrapper), mod);
            return marshaller;
        }
    }
}