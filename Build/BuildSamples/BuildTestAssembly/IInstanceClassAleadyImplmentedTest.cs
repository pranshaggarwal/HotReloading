using System;
using System.Collections.Generic;
using HotReloading;

namespace BuildTestAssembly
{
    public class IInstanceClassAleadyImplmentedTest : IInstanceClass
    {
        private Dictionary<string, Delegate> instanceMethods;

        public Dictionary<string, Delegate> InstanceMethods
        {
            get
            {
                if (instanceMethods == null)
                {
                    instanceMethods = CodeChangeHandler.GetInitialInstanceMethods(this);
                }
                return instanceMethods;
            }
        }

        protected Delegate GetInstanceMethod(string methodName)
        {
            if (InstanceMethods.ContainsKey(methodName))
            {
                return InstanceMethods[methodName];
            }
            return null;
        }
    }
}
