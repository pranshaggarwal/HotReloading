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
        private static List<IInstanceClass> instanceClasses = new List<IInstanceClass>();

        public static Dictionary<Type, Dictionary<string, CSharpLamdaExpression>> PrivateInstanceMethods =
            new Dictionary<Type, Dictionary<string, CSharpLamdaExpression>>();

        public static Dictionary<Type, Dictionary<string, CSharpLamdaExpression>> PublicInstanceMethods =
            new Dictionary<Type, Dictionary<string, CSharpLamdaExpression>>();

        public static Dictionary<Type, Dictionary<string, CSharpLamdaExpression>> InternalInstanceMethods =
            new Dictionary<Type, Dictionary<string, CSharpLamdaExpression>>();

        public static Dictionary<Type, Dictionary<string, CSharpLamdaExpression>> ProtectedInstanceMethods =
            new Dictionary<Type, Dictionary<string, CSharpLamdaExpression>>();

        public static Dictionary<Type, Dictionary<string, CSharpLamdaExpression>> ProtectedInternalInstanceMethods =
            new Dictionary<Type, Dictionary<string, CSharpLamdaExpression>>();

        public static void HandleCodeChange(CodeChange codeChange)
        {
            foreach (var method in codeChange.Methods)
            {
                HandleMethodChange(method);
            }
        }

        public static Dictionary<Type, List<MethodContainer>> Methods { get; private set; } = new Dictionary<Type, List<MethodContainer>>();

        private static void HandleMethodChange(Method method)
        {
            if(!Methods.ContainsKey(method.ParentType))
            {
                Methods.Add(method.ParentType, new List<MethodContainer>());
            }

            var methods = Methods[method.ParentType];

            var existingMethod = methods.FirstOrDefault(x => x.Method.Name == method.Name);

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
                    if (list.InstanceMethods.ContainsKey(method.Name))
                    {
                        list.InstanceMethods[method.Name] = container.GetDelegate();
                    }
                    else
                        list.InstanceMethods.Add(method.Name, container.GetDelegate());
                }
            }
        }

        public static Delegate GetMethodDelegate(Type parentType, string methodName)
        {
            Debug.WriteLine("GetMethodDelegate called");

            if(Methods.ContainsKey(parentType))
                return Methods[parentType].FirstOrDefault(x => x.Method.Name == methodName).GetDelegate();
            return null;
        }

        private static void InstanceMethodRequest(Method methodRequest)
        {
            var instanceMethodDictionary = GetInstanceMethodDictionary(methodRequest);

            var expressionInterpreterHandler = new ExpressionInterpreterHandler(methodRequest);

            var instanceMethod = expressionInterpreterHandler.GetLamdaExpression();

            if (instanceMethodDictionary.ContainsKey(methodRequest.ParentType))
            {
                var membersDictionary = instanceMethodDictionary[methodRequest.ParentType];
                if (membersDictionary.ContainsKey(methodRequest.Name))
                    membersDictionary[methodRequest.Name] = instanceMethod;
                else
                    membersDictionary.Add(methodRequest.Name, instanceMethod);
            }
            else
            {
                var membersDictionary = new Dictionary<string, CSharpLamdaExpression>();
                membersDictionary.Add(methodRequest.Name, instanceMethod);
                instanceMethodDictionary.Add(methodRequest.ParentType, membersDictionary);
            }

            foreach(var list in instanceClasses.Where(x => x.GetType() == methodRequest.ParentType))
            {
                if (list.InstanceMethods.ContainsKey(methodRequest.Name))
                {
                    list.InstanceMethods[methodRequest.Name] = instanceMethod.Compile();
                }
                else
                    list.InstanceMethods.Add(methodRequest.Name, instanceMethod.Compile());
            }
        }

        public static Dictionary<string, CSharpLamdaExpression> GetInstanceMethods(Type type)
        {
            if (PublicInstanceMethods.ContainsKey(type))
                return PublicInstanceMethods[type];

            return new Dictionary<string, CSharpLamdaExpression>();
        }

        private static Dictionary<Type, Dictionary<string, CSharpLamdaExpression>> GetInstanceMethodDictionary(
            Method methodRequest)
        {
            switch (methodRequest.AccessModifier)
            {
                case AccessModifier.Private:
                    return PrivateInstanceMethods;
                case AccessModifier.Public:
                    return PublicInstanceMethods;
                case AccessModifier.Internal:
                    return InternalInstanceMethods;
                case AccessModifier.Protected:
                    return ProtectedInstanceMethods;
                case AccessModifier.ProtectedInternal:
                    return ProtectedInternalInstanceMethods;
                default:
                    throw new ArgumentOutOfRangeException("This access modifier is not supported");
            }
        }

        public static CSharpLamdaExpression GetMethod(Type @class, string name)
        {
            if(Methods.ContainsKey(@class))
            {
                var method = Methods[@class].FirstOrDefault(x => x.Method.Name == name);
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
                    instanceMethods.Add(instanceMethod.Method.Name, instanceMethod.GetDelegate());

            return instanceMethods;
        }
    }
}