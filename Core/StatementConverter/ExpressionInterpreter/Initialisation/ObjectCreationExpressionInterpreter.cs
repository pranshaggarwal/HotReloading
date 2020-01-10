using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using HotReloading.Syntax;
using HotReloading.Syntax.Statements;
using StatementConverter.Extensions;

namespace StatementConverter.ExpressionInterpreter
{
    internal class ObjectCreationExpressionInterpreter : IExpressionInterpreter
    {
        private readonly ExpressionInterpreterHandler expressionInterpreterHandler;
        private readonly ObjectCreationStatement creationStatement;

        public ObjectCreationExpressionInterpreter(ExpressionInterpreterHandler expressionInterpreterHandler, 
            ObjectCreationStatement creationStatement)
        {
            this.expressionInterpreterHandler = expressionInterpreterHandler;
            this.creationStatement = creationStatement;
        }

        public Expression GetExpression()
        {
            var instanceType = (Type)creationStatement.Type;

            var arguments = new List<Expression>();
            foreach(var argument in creationStatement.ArgumentList)
            {
                arguments.Add(expressionInterpreterHandler.GetExpression(argument));
            }

            var parameterTypes = creationStatement.ParametersSignature.Select(x => (Type)x).ToArray();

            var ctorInfo = instanceType.GetConstructor(parameterTypes);

            Expression[] convertedArguments = new Expression[arguments.Count];

            for (int i = 0; i < convertedArguments.Length; i++)
            {
                var argument = arguments[i];

                var parameter = ctorInfo.GetParameters()[i];

                if (parameter.ParameterType != argument.Type)
                {
                    convertedArguments[i] = Expression.Convert(argument, parameter.ParameterType);
                }
                else
                {
                    convertedArguments[i] = argument;
                }
            }

            var ctorExpression = Expression.New(ctorInfo, convertedArguments);

            if (creationStatement.Initializer.Any())
            {
                if(instanceType.IsGenericType)
                {
                    var list = typeof(List<>);
                    if (instanceType.GenericTypeArguments.Length == 1)
                    {
                        var genericList = list.MakeGenericType(instanceType.GenericTypeArguments);
                        if (genericList == instanceType)
                        {
                            var addMethod = genericList.GetMethod("Add");

                            List<ElementInit> initializers = new List<ElementInit>();

                            foreach (var element in creationStatement.Initializer)
                            {
                                var elementInit = Expression.ElementInit(addMethod, expressionInterpreterHandler.GetExpression(element)
                                    );

                                initializers.Add(elementInit);
                            }

                            return Expression.ListInit(ctorExpression, initializers);
                        }
                    }

                    var dic = typeof(Dictionary<,>);
                    if (instanceType.GenericTypeArguments.Length == 2)
                    {
                        var genericDic = dic.MakeGenericType(instanceType.GenericTypeArguments);
                        if (genericDic == instanceType)
                        {
                            var addMethod = genericDic.GetMethod("Add");

                            List<ElementInit> initializers = new List<ElementInit>();

                            foreach (var element in creationStatement.Initializer)
                            {
                                if (element is InitializerStatement initializerStatement)
                                {
                                    var initExpression = initializerStatement.Statements.Select(x =>
                                                            expressionInterpreterHandler.GetExpression(x));
                                    var elementInit = Expression.ElementInit(addMethod, initExpression);

                                    initializers.Add(elementInit);
                                }
                                else
                                {
                                    throw new NotSupportedException("This dictionary initialization is not supported");
                                }

                            }

                            return Expression.ListInit(ctorExpression, initializers);
                        }
                    }
                }

                var membersInitalizer = new List<MemberBinding>();
                foreach (var statement in creationStatement.Initializer.Cast<BinaryStatement>())
                {
                    if (statement.Operand != BinaryOperand.Assign)
                        throw new NotSupportedException("This operation is not supported");

                    string memberName = "";
                    if (statement.Left is InstanceFieldMemberStatement field)
                        memberName = field.Name;
                    else if (statement.Left is InstancePropertyMemberStatement property)
                        memberName = property.Name;
                    else
                        throw new NotSupportedException("This operation is not supported yet");

                    var bindingFlags = BindingFlags.Instance;
                    bindingFlags |= ((MemberAccessStatement)statement.Left).AccessModifier == AccessModifier.Public ?
                        BindingFlags.Public : BindingFlags.NonPublic;

                    var member = instanceType.GetMostSuitableMember(memberName, bindingFlags);
                    var rightExpression = expressionInterpreterHandler.GetExpression(statement.Right);

                    if(member is FieldInfo fi && fi.FieldType != rightExpression.Type)
                    {
                        rightExpression = Expression.Convert(rightExpression, fi.FieldType);
                    }
                    else if(member is PropertyInfo pi && pi.PropertyType != rightExpression.Type)
                    {
                        rightExpression = Expression.Convert(rightExpression, pi.PropertyType);
                    }

                    membersInitalizer.Add(Expression.Bind(member, rightExpression));
                }
                return Expression.MemberInit(ctorExpression, membersInitalizer);
            }
            return ctorExpression;
        }
    }
}