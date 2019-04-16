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
            var methodKey = CodeChangeHandler.GetMethodKey(nameof(UpdateStaticMethod));
            var @delegate = CodeChangeHandler.GetMethodDelegate(typeof(PublicMethodTestClass), methodKey);

            if (@delegate != null)
            {
                @delegate.DynamicInvoke();
                return;
            }

            Tracker.Call("default");
        }

        public static void AddedStaticMethodAndCalledFromSameClass1()
        {
            var methodKey = CodeChangeHandler.GetMethodKey(nameof(AddedStaticMethodAndCalledFromSameClass1));
            var @delegate = CodeChangeHandler.GetMethodDelegate(typeof(PublicMethodTestClass), methodKey);

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

        public void AddedInstanceMethodAndCalledFromSameClass()
        {
            Delegate instanceMethod = GetInstanceMethod(nameof(AddedInstanceMethodAndCalledFromSameClass));
            if ((object)instanceMethod != null)
            {
                instanceMethod.DynamicInvoke(this);
                return;
            }
        }

        public void AddedStaticMethodAndCalledFromInstanceMethod()
        {
            Delegate instanceMethod = GetInstanceMethod(nameof(AddedStaticMethodAndCalledFromInstanceMethod));
            if ((object)instanceMethod != null)
            {
                instanceMethod.DynamicInvoke(this);
                return;
            }
        }

        public static void MethodOverload()
        {
            var methodKey = CodeChangeHandler.GetMethodKey(nameof(MethodOverload));
            var @delegate = CodeChangeHandler.GetMethodDelegate(typeof(PublicMethodTestClass), nameof(MethodOverload));

            if (@delegate != null)
            {
                @delegate.DynamicInvoke();
                return;
            }
        }

        public static void MethodOverload(string str)
        {
            var methodKey = CodeChangeHandler.GetMethodKey(nameof(MethodOverload), typeof(string).FullName);
            var @delegate = CodeChangeHandler.GetMethodDelegate(typeof(PublicMethodTestClass), methodKey);

            if (@delegate != null)
            {
                @delegate.DynamicInvoke(str);
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
