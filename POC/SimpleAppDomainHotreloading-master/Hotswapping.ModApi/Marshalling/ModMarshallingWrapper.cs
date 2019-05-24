using System;

namespace Hotswapping.ModApi.Marshalling
{
    public class ModMarshallingWrapper : MarshalByRefObject, IMod
    {
        private readonly IMod mod;

        public ModMarshallingWrapper(IMod mod)
        {
            this.mod = mod ?? throw new ArgumentNullException(nameof(mod));
        }

        public void Init()
        {
            mod.Init();
        }
    }
}