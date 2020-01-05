using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using CSharpExpressions.Microsoft.CSharp;
using HotReloading.Core;
using HotReloading.Syntax;
using Microsoft.CSharp.Expressions;
using StatementConverter.ExpressionInterpreter;

namespace HotReloading
{
    public static class Runtime
    {
        public static void HandleCodeChange(CodeChange codeChange)
        {
            foreach (var method in codeChange.Methods)
            {
                HandleMethodChange(method);
            }

            foreach(var container in RuntimeMemory.Methods.SelectMany(x => 
                                                                x.Value).Where(x => 
                                                codeChange.Methods.Any(y => y == x.Method)))
            {
                UpdateInstanceMethod(container);
            }
        }

        private static void HandleMethodChange(Method method)
        {
            if (!RuntimeMemory.Methods.ContainsKey(method.ParentType))
            {
                RuntimeMemory.Methods.Add(method.ParentType, new List<IMethodContainer>());
            }

            var methods = RuntimeMemory.Methods[method.ParentType];

            var methodKey = Helper.GetMethodKey(method);

            var existingMethod = methods.SingleOrDefault(x => Helper.GetMethodKey(x.Method) == methodKey);

            if (existingMethod != null)
            {
                methods.Remove(existingMethod);
            }

            MethodContainer container = new MethodContainer(method);
            methods.Add(container);
        }

        private static void UpdateInstanceMethod(IMethodContainer container)
        {
            var methodKey = Helper.GetMethodKey(container.Method);
            if (!container.Method.IsStatic)
            {
                foreach (var list in RuntimeMemory.MemoryInstances.Where(x => x.GetType() == container.Method.ParentType))
                {
                    if (list.InstanceMethods.ContainsKey(methodKey))
                    {
                        list.InstanceMethods[methodKey] = container.GetDelegate();
                    }
                    else
                        list.InstanceMethods.Add(methodKey, container.GetDelegate());
                }
            }
        }

        public static Delegate GetMethodDelegate(System.Type parentType, string key)
        {
            if(RuntimeMemory.Methods.ContainsKey(parentType))
                return RuntimeMemory.Methods[parentType].SingleOrDefault(x => Helper.GetMethodKey(x.Method) == key)?.GetDelegate();
            return null;
        }

        public static Dictionary<string, Delegate> GetInitialInstanceMethods(IInstanceClass instanceClass)
        {
            if(!RuntimeMemory.MemoryInstances.Contains(instanceClass))
                RuntimeMemory.MemoryInstances.Add(instanceClass);
            var type = instanceClass.GetType();
            var instanceMethods = new Dictionary<string, Delegate>();

            if (RuntimeMemory.Methods.ContainsKey(type))
                foreach (var instanceMethod in RuntimeMemory.Methods[type])
                    instanceMethods.Add(Helper.GetMethodKey(instanceMethod.Method), instanceMethod.GetDelegate());

            return instanceMethods;
        }
    }
}