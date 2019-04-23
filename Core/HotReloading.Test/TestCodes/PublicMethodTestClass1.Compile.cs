using System;
using System.Collections.Generic;
using HotReloading.Core;

namespace HotReloading.Test.TestCodes
{
    public partial class PublicMethodTestClass1 : IInstanceClass
    {
        private Dictionary<string, Delegate> instanceMethods;

        public virtual Dictionary<string, Delegate> InstanceMethods
        {
            get
            {
                if (instanceMethods == null)
                {
                    instanceMethods = Runtime.GetInitialInstanceMethods(this);
                }
                return instanceMethods;
            }
        }

        public static void AddedStaticMethodAndCalledFromAnotherClass()
        {
            var methodKey = Runtime.GetMethodKey(nameof(AddedStaticMethodAndCalledFromAnotherClass));
            var @delegate = Runtime.GetMethodDelegate(typeof(PublicMethodTestClass1), methodKey);

            if (@delegate != null)
            {
                @delegate.DynamicInvoke();
                return;
            }
        }

        public void AddedInstanceMethodAndCalledFromAnotherClass()
        {
            Delegate instanceMethod = GetInstanceMethod(nameof(AddedInstanceMethodAndCalledFromAnotherClass));
            if ((object)instanceMethod != null)
            {
                instanceMethod.DynamicInvoke(this);
                return;
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
