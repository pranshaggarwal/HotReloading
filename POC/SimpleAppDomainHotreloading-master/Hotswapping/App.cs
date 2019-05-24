using Hotswapping.ModApi;
using Hotswapping.Module1;
using System;
using System.Collections.Generic;

namespace Hotswapping
{
    [Serializable]
    public class App : IMod
    {
        public List<IMod> Mods { get; set; }
        public void Init()
        {
            Mods = new List<IMod>()
            {
                new AnotherMod()
            };

            foreach(var mod in Mods)
            {
                mod.Init();
            }
        }
    }
}