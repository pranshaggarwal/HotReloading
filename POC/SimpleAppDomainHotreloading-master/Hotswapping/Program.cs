using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Hotswapping.ModApi;
using Hotswapping.ModApi.Marshalling;
using Hotswapping.Module1;
using Hotswapping.Reloader;

namespace Hotswapping
{
    internal class Program
    {
        public static List<IMod> Mods;
        private static void Main(string[] args)
        {
            var manager = ModManager.Create();

            while (true)
            {
                Console.WriteLine("Waiting for input.. (press 'q' to quit)");
                var key = Console.ReadKey(true).KeyChar;
                if (key == 'q') return;
                manager.Reload();

                //var app = (IMod)ModContext.proxy.CreateInstance(typeof(App));
                //app.Init();
                PrintModules(manager);
            }
        }

        private static void PrintModules(ModManager manager)
        {
            Console.WriteLine("\tAll modules:");
            //foreach(var mod in Mods)
            //{
            //    mod.Init();
            //}
            foreach (var module in manager.GetModModules())
            {
                foreach (var mod in module.ModEntries)
                {
                    mod.Init();
                }
            }

            //foreach(var mod in new DomainMediator().GetMods())
            //{
            //    mod.Init();
            //}
        }
    }
}