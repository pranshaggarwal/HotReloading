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
    public class CodeChangeHandler
    {
        private static List<IInstanceClass> instanceClasses = new List<IInstanceClass>();

        public static Dictionary<Type, Dictionary<string, Delegate>> PrivateStaticMethods =
            new Dictionary<Type, Dictionary<string, Delegate>>();

        public static Dictionary<Type, Dictionary<string, Delegate>> PublicStaticMethods =
            new Dictionary<Type, Dictionary<string, Delegate>>();

        public static Dictionary<Type, Dictionary<string, Delegate>> InternalStaticMethods =
            new Dictionary<Type, Dictionary<string, Delegate>>();

        public static Dictionary<Type, Dictionary<string, Delegate>> ProtectedStaticMethods =
            new Dictionary<Type, Dictionary<string, Delegate>>();

        public static Dictionary<Type, Dictionary<string, Delegate>> ProtectedInternalStaticMethods =
            new Dictionary<Type, Dictionary<string, Delegate>>();

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

        private static void HandleMethodChange(Method method)
        {
            if (method.IsStatic)
                StaticMethodRequest(method);
            else
                InstanceMethodRequest(method);
        }

        private static void StaticMethodRequest(Method methodRequest)
        {
            var staticMethodDictionary = GetStaticMethodDictionary(methodRequest);

            var expressionInterpreterHandler = new ExpressionInterpreterHandler(methodRequest);

            var compiledMethod = expressionInterpreterHandler.GetLamdaExpression().Compile();

            if (staticMethodDictionary.ContainsKey(methodRequest.ParentType))
            {
                var membersDictionary = staticMethodDictionary[methodRequest.ParentType];
                if (membersDictionary.ContainsKey(methodRequest.Name))
                    membersDictionary[methodRequest.Name] = compiledMethod;
                else
                    membersDictionary.Add(methodRequest.Name, compiledMethod);
            }
            else
            {
                var membersDictionary = new Dictionary<string, Delegate>();
                membersDictionary.Add(methodRequest.Name, compiledMethod);
                staticMethodDictionary.Add(methodRequest.ParentType, membersDictionary);
            }
        }

        public static Delegate GetMethodDelegate(Type parentType, string methodName)
        {
            Debug.WriteLine("GetMethodDelegate called");
            if (PublicStaticMethods.ContainsKey(parentType) && PublicStaticMethods[parentType].ContainsKey(methodName))
                return PublicStaticMethods[parentType][methodName];

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

        private static Dictionary<Type, Dictionary<string, Delegate>> GetStaticMethodDictionary(Method methodRequest)
        {
            switch (methodRequest.AccessModifier)
            {
                case AccessModifier.Private:
                    return PrivateStaticMethods;
                case AccessModifier.Public:
                    return PublicStaticMethods;
                case AccessModifier.Internal:
                    return InternalStaticMethods;
                case AccessModifier.Protected:
                    return ProtectedStaticMethods;
                case AccessModifier.ProtectedInternal:
                    return ProtectedInternalStaticMethods;
                default:
                    throw new ArgumentOutOfRangeException("This access modifier is not supported");
            }
        }

        public static Dictionary<string, Delegate> GetInitialInstanceMethods(IInstanceClass instanceClass)
        {
            if(!instanceClasses.Contains(instanceClass))
                instanceClasses.Add(instanceClass);
            var type = instanceClass.GetType();
            var instanceMethods = new Dictionary<string, Delegate>();

            if (PublicInstanceMethods.ContainsKey(type))
                foreach (var instanceMethod in PublicInstanceMethods[type])
                    instanceMethods.Add(instanceMethod.Key, instanceMethod.Value.Compile());

            return instanceMethods;
        }
    }
}