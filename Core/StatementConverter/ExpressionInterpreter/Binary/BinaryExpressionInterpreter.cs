using System;
using System.Linq.Expressions;
using System.Reflection;
using HotReloading.Core.Statements;
using HotReloading.Syntax;

namespace StatementConverter.ExpressionInterpreter
{
    internal class BinaryExpressionInterpreter : IExpressionInterpreter
    {
        private readonly ExpressionInterpreterHandler expressionInterpreterHandler;
        private BinaryStatement binaryStatement;

        public BinaryExpressionInterpreter(ExpressionInterpreterHandler expressionInterpreterHandler, BinaryStatement binaryStatement)
        {
            this.expressionInterpreterHandler = expressionInterpreterHandler;
            this.binaryStatement = binaryStatement;
        }

        public Expression GetExpression()
        {
            var left = binaryStatement.Left;
            if (left is DelegateIdentifierStatement delegateIdentifier)
                left = delegateIdentifier.Target;
            if (left is StaticEventMemberStatement ||
                left is InstanceEventMemberStatement)
            {
                return AssignEventExpression(left);
            }
            return GetExpression(expressionInterpreterHandler.GetExpression(binaryStatement.Left),
                    expressionInterpreterHandler.GetExpression(binaryStatement.Right), binaryStatement.Operand);
        }

        private Expression AssignEventExpression(IStatementCSharpSyntax left)
        {
            var bindingFlags = left is StaticEventMemberStatement ?
                                BindingFlags.Static : BindingFlags.Instance;
            bindingFlags |= ((MemberAccessStatement)left).AccessModifier == AccessModifier.Public ?
                BindingFlags.Public : BindingFlags.NonPublic;

            EventInfo @event;

            Expression eventHandlerTarget = Expression.Constant(null);

            if (left is InstanceEventMemberStatement instanceEvent)
            {
                eventHandlerTarget = expressionInterpreterHandler.GetExpression(instanceEvent.Parent);
                @event = eventHandlerTarget.Type.GetEvent(instanceEvent.Name, bindingFlags);
            }
            else
            {
                var staticEvent = (StaticEventMemberStatement)left;
                var parentType = (Type)staticEvent.ParentType;
                @event = parentType.GetEvent(staticEvent.Name, bindingFlags);
            }

            var right = expressionInterpreterHandler.GetExpression(binaryStatement.Right);

            MethodInfo handlerMethod;
            if (binaryStatement.Operand == BinaryOperand.AddAssign)
                handlerMethod = @event.GetType().GetMethod("AddEventHandler");
            else
                handlerMethod = @event.GetType().GetMethod("RemoveEventHandler");

            return Expression.Call(Expression.Constant(@event), handlerMethod, eventHandlerTarget, right);
        }

        private Expression GetExpression(Expression left, Expression right, BinaryOperand operand)
        {
            var strType = typeof(string);
            var convertedLeft = left;
            var convertedRight = right;
            if (left.Type != right.Type)
            {
                if (left.Type == strType || right.Type == strType)
                {
                    var toStringMethod = typeof(Convert).GetMethod("ToString", new Type[] { typeof(object) });

                    if (left.Type == strType)
                        convertedRight = Expression.Call(null, toStringMethod,
                            Expression.Convert(right, typeof(object)));
                    else
                        convertedLeft = Expression.Call(null, toStringMethod,
                            Expression.Convert(left, typeof(object)));
                }
                else
                {
                    if (IsShiftOperator(operand))
                    {
                        convertedRight = Expression.Convert(right, typeof(int));
                    }
                    else if (IsAssignOperator(operand))
                    {
                        convertedRight = Expression.Convert(right, left.Type);
                    }
                    else if (operand != BinaryOperand.Coalesce)
                    {
                        convertedLeft = ConvertType(left, left.Type, right.Type);
                        convertedRight = ConvertType(right, right.Type, left.Type);
                    }
                }
            }
            else if (!IsAssignOperator(operand) && IsByteType(left.Type) && IsByteType(right.Type))
            {
                convertedLeft = Expression.Convert(left, typeof(int));
                convertedRight = Expression.Convert(right, typeof(int));
            }

            switch (operand)
            {
                case BinaryOperand.Add:
                    if (left.Type == strType || right.Type == strType)
                    {
                        var concatMethod = strType.GetMethod("Concat", new[] { typeof(object[]) });

                        var array = Expression.NewArrayInit(typeof(object),
                            Expression.Convert(left, typeof(object)),
                            Expression.Convert(right, typeof(object)));
                        return Expression.Call(null, concatMethod, array);
                    }
                    return Expression.Add(convertedLeft, convertedRight);
                case BinaryOperand.Sub:
                    return Expression.Subtract(convertedLeft, convertedRight);
                case BinaryOperand.Multiply:
                    return Expression.Multiply(convertedLeft, convertedRight);
                case BinaryOperand.Divide:
                    return Expression.Divide(convertedLeft, convertedRight);
                case BinaryOperand.Modulo:
                    return Expression.Modulo(convertedLeft, convertedRight);
                case BinaryOperand.Equal:
                    return Expression.Equal(convertedLeft, convertedRight);
                case BinaryOperand.NotEqual:
                    return Expression.NotEqual(convertedLeft, convertedRight);
                case BinaryOperand.And:
                    return Expression.AndAlso(convertedLeft, convertedRight);
                case BinaryOperand.Or:
                    return Expression.OrElse(convertedLeft, convertedRight);
                case BinaryOperand.LessThan:
                    return Expression.LessThan(convertedLeft, convertedRight);
                case BinaryOperand.LessThanEqual:
                    return Expression.LessThanOrEqual(convertedLeft, convertedRight);
                case BinaryOperand.GreaterThan:
                    return Expression.GreaterThan(convertedLeft, convertedRight);
                case BinaryOperand.GreaterThanEqual:
                    return Expression.GreaterThanOrEqual(convertedLeft, convertedRight);
                case BinaryOperand.BitwiseAnd:
                    if (left.Type.IsEnum)
                    {
                        var left1 = Expression.Convert(left, Enum.GetUnderlyingType(left.Type));
                        var right1 = Expression.Convert(right, Enum.GetUnderlyingType(right.Type));
                        var orExpression = Expression.And(left1, right1);
                        return Expression.Convert(orExpression, Enum.GetUnderlyingType(left.Type));
                    }
                    return Expression.And(convertedLeft, convertedRight);
                case BinaryOperand.BitwiseOr:
                    if (left.Type.IsEnum)
                    {
                        var left1 = Expression.Convert(left, Enum.GetUnderlyingType(left.Type));
                        var right1 = Expression.Convert(right, Enum.GetUnderlyingType(right.Type));
                        var orExpression = Expression.Or(left1, right1);
                        return Expression.Convert(orExpression, Enum.GetUnderlyingType(left.Type));
                    }
                    return Expression.Or(convertedLeft, convertedRight);
                case BinaryOperand.Xor:
                    if (left.Type.IsEnum)
                    {
                        var left1 = Expression.Convert(left, Enum.GetUnderlyingType(left.Type));
                        var right1 = Expression.Convert(right, Enum.GetUnderlyingType(right.Type));
                        var orExpression = Expression.ExclusiveOr(left1, right1);
                        return Expression.Convert(orExpression, Enum.GetUnderlyingType(left.Type));
                    }
                    return Expression.ExclusiveOr(convertedLeft, convertedRight);
                case BinaryOperand.LeftShift:
                    return Expression.LeftShift(convertedLeft, convertedRight);
                case BinaryOperand.RightShift:
                    return Expression.RightShift(convertedLeft, convertedRight);
                case BinaryOperand.Coalesce:
                    return Expression.Coalesce(convertedLeft, convertedRight);
            }

            switch (operand)
            {
                case BinaryOperand.Assign:
                    return Expression.Assign(left, convertedRight);
                case BinaryOperand.AddAssign:
                    if (left.Type == strType || right.Type == strType)
                    {
                        var concatMethod = strType.GetMethod("Concat", new[] { typeof(object[]) });

                        var array = Expression.NewArrayInit(typeof(object),
                            Expression.Convert(left, typeof(object)),
                            Expression.Convert(right, typeof(object)));
                        var addExpression = Expression.Call(null, concatMethod, array);
                        return Expression.Assign(left, addExpression);
                    }
                    else if (IsByteType(left.Type))
                    {
                        var addExpression = GetExpression(convertedLeft, convertedRight, BinaryOperand.Add);
                        return Expression.Assign(left, Expression.Convert(addExpression, left.Type));
                    }
                    if (left.Type.IsSubclassOf(typeof(MulticastDelegate)))
                    {
                        //Delegate.Combine
                        var delegateCombine = typeof(Delegate).GetMethod("Combine", new Type[] { typeof(Delegate), typeof(Delegate) });
                        return Expression.Assign(left,
                            Expression.Convert(Expression.Call(null,
                                delegateCombine, left, convertedRight),
                                left.Type));
                    }
                    return Expression.AddAssign(left, convertedRight);
                case BinaryOperand.SubAssign:
                    if (IsByteType(left.Type))
                    {
                        var addExpression = GetExpression(convertedLeft, convertedRight, BinaryOperand.Sub);
                        return Expression.Assign(left, Expression.Convert(addExpression, left.Type));
                    }
                    if (left.Type.IsSubclassOf(typeof(MulticastDelegate)))
                    {
                        //Delegate.Remove
                        var delegateCombine = typeof(Delegate).GetMethod("Remove", new Type[] { typeof(Delegate), typeof(Delegate) });
                        return Expression.Assign(left,
                            Expression.Convert(Expression.Call(null,
                                delegateCombine, left, convertedRight),
                                left.Type));
                    }
                    return Expression.SubtractAssign(left, convertedRight);
                case BinaryOperand.MultiplyAssign:
                    if (IsByteType(left.Type))
                    {
                        var addExpression = GetExpression(convertedLeft, convertedRight, BinaryOperand.Multiply);
                        return Expression.Assign(left, Expression.Convert(addExpression, left.Type));
                    }
                    return Expression.MultiplyAssign(left, convertedRight);
                case BinaryOperand.DivideAssign:
                    if (IsByteType(left.Type))
                    {
                        var addExpression = GetExpression(convertedLeft, convertedRight, BinaryOperand.Divide);
                        return Expression.Assign(left, Expression.Convert(addExpression, left.Type));
                    }
                    return Expression.DivideAssign(left, convertedRight);
                case BinaryOperand.ModuloAssign:
                    if (IsByteType(left.Type))
                    {
                        var addExpression = GetExpression(convertedLeft, convertedRight, BinaryOperand.Modulo);
                        return Expression.Assign(left, Expression.Convert(addExpression, left.Type));
                    }
                    return Expression.ModuloAssign(left, convertedRight);
                case BinaryOperand.BitwiseAndAssign:
                    if (left.Type.IsEnum)
                    {
                        var left1 = Expression.Convert(left, Enum.GetUnderlyingType(left.Type));
                        var right1 = Expression.Convert(convertedRight, Enum.GetUnderlyingType(convertedRight.Type));
                        var orExpression = Expression.And(left1, right1);
                        return Expression.Assign(left, Expression.Convert(orExpression, left.Type));
                    }
                    if (IsByteType(left.Type))
                    {
                        var addExpression = GetExpression(convertedLeft, convertedRight, BinaryOperand.BitwiseAndAssign);
                        return Expression.Assign(left, Expression.Convert(addExpression, left.Type));
                    }
                    return Expression.AndAssign(left, convertedRight);
                case BinaryOperand.BitwiseOrAssign:
                    if (left.Type.IsEnum)
                    {
                        var left1 = Expression.Convert(left, Enum.GetUnderlyingType(left.Type));
                        var right1 = Expression.Convert(convertedRight, Enum.GetUnderlyingType(convertedRight.Type));
                        var orExpression = Expression.Or(left1, right1);
                        return Expression.Assign(left, Expression.Convert(orExpression, left.Type));
                    }
                    if (IsByteType(left.Type))
                    {
                        var addExpression = GetExpression(convertedLeft, convertedRight, BinaryOperand.BitwiseOr);
                        return Expression.Assign(left, Expression.Convert(addExpression, left.Type));
                    }
                    return Expression.OrAssign(left, convertedRight);
                case BinaryOperand.XorAssign:
                    if (left.Type.IsEnum)
                    {
                        var left1 = Expression.Convert(left, Enum.GetUnderlyingType(left.Type));
                        var right1 = Expression.Convert(convertedRight, Enum.GetUnderlyingType(convertedRight.Type));
                        var orExpression = Expression.ExclusiveOr(left1, right1);
                        return Expression.Assign(convertedLeft, Expression.Convert(orExpression, left.Type));
                    }
                    if (IsByteType(left.Type))
                    {
                        var addExpression = GetExpression(convertedLeft, convertedRight, BinaryOperand.Xor);
                        return Expression.Assign(left, Expression.Convert(addExpression, left.Type));
                    }
                    return Expression.ExclusiveOrAssign(left, convertedRight);
                case BinaryOperand.LeftShiftAssign:
                    return Expression.LeftShiftAssign(left, convertedRight);
                case BinaryOperand.RightShiftAssign:
                    return Expression.RightShiftAssign(left, convertedRight);
                default:
                    throw new NotSupportedException($"{binaryStatement.Operand} is not supported yet");
            }
        }

        private bool IsByteType(Type type)
        {
            if (type == typeof(byte) ||
                type == typeof(char) ||
                type == typeof(sbyte))
                return true;
            return false;
        }

        private bool IsAssignOperator(BinaryOperand operand)
        {
            if (operand == BinaryOperand.Assign ||
               operand == BinaryOperand.AddAssign ||
               operand == BinaryOperand.SubAssign ||
               operand == BinaryOperand.MultiplyAssign ||
               operand == BinaryOperand.DivideAssign ||
               operand == BinaryOperand.ModuloAssign ||
               operand == BinaryOperand.BitwiseOrAssign ||
               operand == BinaryOperand.BitwiseAndAssign ||
               operand == BinaryOperand.XorAssign)
                return true;
            return false;
        }

        private bool IsShiftOperator(BinaryOperand operand)
        {
            if (operand == BinaryOperand.RightShift ||
                operand == BinaryOperand.RightShiftAssign ||
                operand == BinaryOperand.LeftShift ||
                operand == BinaryOperand.LeftShiftAssign)
                return true;
            return false;
        }

        private Expression ConvertType(Expression expressionToConvert, Type fromType, Type toType)
        {
            var precedentType = GetTypePrecendence(fromType) > GetTypePrecendence(toType) ?
                                        fromType : toType;
            return Expression.Convert(expressionToConvert, precedentType);
        }

        private int GetTypePrecendence(Type type)
        {
            if (type == typeof(Byte))
                return 1;
            if (type == typeof(Char))
                return 2;
            if (type == typeof(SByte))
                return 3;
            if (type == typeof(Int16))
                return 4;
            if (type == typeof(UInt16))
                return 5;
            if (type == typeof(Int32))
                return 6;
            if (type == typeof(UInt32))
                return 7;
            if (type == typeof(Int64))
                return 8;
            if (type == typeof(UInt64))
                return 9;
            if (type == typeof(Single))
                return 10;
            if (type == typeof(Double))
                return 11;
            if (type == typeof(Decimal))
                return 12;
            if (type == typeof(Object))
                return int.MaxValue;
            return -1;
        }
    }
}