using System;
using System.Collections.Generic;

namespace HotReloading.Test.TestCodes
{
    public partial class PublicMethodTestClass : IInstanceClass
    {
        private Dictionary<string, Delegate> instanceMethods;

        public virtual Dictionary<string, Delegate> InstanceMethods
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

        public static void UpdateStaticMethod()
        {
            var @delegate = CodeChangeHandler.GetMethodDelegate(typeof(PublicMethodTestClass), nameof(UpdateStaticMethod));

            if (@delegate != null)
            {
                @delegate.DynamicInvoke();
                return;
            }

            Tracker.Call("default");
        }

        public static void AddedStaticMethodAndCalledFromSameClass1()
        {
            var @delegate = CodeChangeHandler.GetMethodDelegate(typeof(PublicMethodTestClass), nameof(AddedStaticMethodAndCalledFromSameClass1));

            if (@delegate != null)
            {
                @delegate.DynamicInvoke();
                return;
            }
        }

        public void UpdateInstanceMethod()
        {
            Delegate instanceMethod = GetInstanceMethod(nameof(UpdateInstanceMethod));
            if ((object)instanceMethod != null)
            {
                instanceMethod.DynamicInvoke(this);
                return;
            }

            Tracker.Call("default");
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
