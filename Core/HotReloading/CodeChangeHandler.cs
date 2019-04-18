using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using CSharpExpressions.Microsoft.CSharp;
using HotReloading.Core;
using Microsoft.CSharp.Expressions;
using StatementConverter.ExpressionInterpreter;

namespace HotReloading
{
    public static class CodeChangeHandler
    {
        public static List<IInstanceClass> instanceClasses = new List<IInstanceClass>();

        public static void HandleCodeChange(CodeChange codeChange)
        {
            foreach (var method in codeChange.Methods)
            {
                HandleMethodChange(method);
            }
        }

        public static Dictionary<Type, List<IMethodContainer>> Methods { get; private set; } = new Dictionary<Type, List<IMethodContainer>>();

        private static void HandleMethodChange(Method method)
        {
            if(!Methods.ContainsKey(method.ParentType))
            {
                Methods.Add(method.ParentType, new List<IMethodContainer>());
            }

            var methods = Methods[method.ParentType];

            var methodKey = GetMethodKey(method);

            var existingMethod = methods.SingleOrDefault(x => GetMethodKey(x.Method) == methodKey);

            if(existingMethod != null)
            {
                methods.Remove(existingMethod);
            }

            MethodContainer container = new MethodContainer(method);
            methods.Add(container);

            if (!method.IsStatic)
            {
                foreach (var list in instanceClasses.Where(x => x.GetType() == method.ParentType))
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

        public static string GetMethodKey(string methodName, params string[] parameterTypes)
        {
            string key = methodName;

            foreach (var parameter in parameterTypes)
            {
                key += $"`{(parameter)}";
            }

            return key;
        }

        private static string GetMethodKey(Method method)
        {
            return GetMethodKey(method.Name, method.Parameters.Select(x => x.Type.Key).ToArray());
        }

        public static Delegate GetMethodDelegate(Type parentType, string key)
        {
            Debug.WriteLine("GetMethodDelegate called");

            if(Methods.ContainsKey(parentType))
                return Methods[parentType].SingleOrDefault(x => GetMethodKey(x.Method) == key).GetDelegate();
            return null;
        }

        public static CSharpLamdaExpression GetMethod(Type @class, string key)
        {
            if(Methods.ContainsKey(@class))
            {
                var method = Methods[@class].SingleOrDefault(x => GetMethodKey(x.Method) == key);
                if (method != null)
                    return method.GetExpression();
            }

            return null;
        }

        public static Dictionary<string, Delegate> GetInitialInstanceMethods(IInstanceClass instanceClass)
        {
            if(!instanceClasses.Contains(instanceClass))
                instanceClasses.Add(instanceClass);
            var type = instanceClass.GetType();
            var instanceMethods = new Dictionary<string, Delegate>();

            if (Methods.ContainsKey(type))
                foreach (var instanceMethod in Methods[type])
                    instanceMethods.Add(GetMethodKey(instanceMethod.Method), instanceMethod.GetDelegate());

            return instanceMethods;
        }
    }
}