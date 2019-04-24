using System;
using System.Linq.Expressions;
using HotReloading.Core.Statements;

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
            return GetExpression(expressionInterpreterHandler.GetExpression(binaryStatement.Left),
                    expressionInterpreterHandler.GetExpression(binaryStatement.Right));
        }

        private Expression GetExpression(Expression left, Expression right)
        {
            switch(binaryStatement.Operand)
            {
                case BinaryOperand.Add:
                    var strType = typeof(string);
                    if(left.Type == strType || right.Type == strType)
                    {
                        var concatMethod = strType.GetMethod("Concat", new[] { typeof(object[]) });

                        var array = Expression.NewArrayInit(typeof(object), 
                            Expression.Convert(left, typeof(object)), 
                            Expression.Convert(right, typeof(object)));
                        return Expression.Call(null, concatMethod, array);
                    }
                    return Expression.Add(left, right);
                case BinaryOperand.Sub:
                    return Expression.Subtract(left, right);
                case BinaryOperand.Multiply:
                    return Expression.Multiply(left, right);
                case BinaryOperand.Divide:
                    return Expression.Divide(left, right);
                case BinaryOperand.Modulo:
                    return Expression.Modulo(left, right);
                case BinaryOperand.Equal:
                    return Expression.Equal(left, right);
                case BinaryOperand.NotEqual:
                    return Expression.NotEqual(left, right);
                case BinaryOperand.And:
                    return Expression.AndAlso(left, right);
                case BinaryOperand.Or:
                    return Expression.OrElse(left, right);
                case BinaryOperand.LessThan:
                    return Expression.LessThan(left, right);
                case BinaryOperand.LessThanEqual:
                    return Expression.LessThanOrEqual(left, right);
                case BinaryOperand.GreaterThan:
                    return Expression.GreaterThan(left, right);
                case BinaryOperand.GreaterThanEqual:
                    return Expression.GreaterThanOrEqual(left, right);
                case BinaryOperand.BitwiseAnd:
                    if (left.Type.IsEnum)
                    {
                        var left1 = Expression.Convert(left, Enum.GetUnderlyingType(left.Type));
                        var right1 = Expression.Convert(right, Enum.GetUnderlyingType(right.Type));
                        var orExpression = Expression.And(left1, right1);
                        return Expression.Convert(orExpression, Enum.GetUnderlyingType(left.Type));
                    }
                    return Expression.And(left, right);
                case BinaryOperand.BitwiseOr:
                    if(left.Type.IsEnum)
                    {
                        var left1 = Expression.Convert(left, Enum.GetUnderlyingType(left.Type));
                        var right1 = Expression.Convert(right, Enum.GetUnderlyingType(right.Type));
                        var orExpression = Expression.Or(left1, right1);
                        return Expression.Convert(orExpression, Enum.GetUnderlyingType(left.Type));
                    }
                    return Expression.Or(left, right);
                case BinaryOperand.Xor:
                    if (left.Type.IsEnum)
                    {
                        var left1 = Expression.Convert(left, Enum.GetUnderlyingType(left.Type));
                        var right1 = Expression.Convert(right, Enum.GetUnderlyingType(right.Type));
                        var orExpression = Expression.ExclusiveOr(left1, right1);
                        return Expression.Convert(orExpression, Enum.GetUnderlyingType(left.Type));
                    }
                    return Expression.ExclusiveOr(left, right);
                case BinaryOperand.LeftShift:
                    return Expression.LeftShift(left, right);
                case BinaryOperand.RightShift:
                    return Expression.RightShift(left, right);
                case BinaryOperand.Coalesce:
                    return Expression.Coalesce(left, right);
            }

            var convertedRight = right;

            if(left.Type != right.Type)
            {
                if(left.Type == typeof(string) && right.Type == typeof(char))
                {
                    var toStringMethod = typeof(char).GetMethod("ToString", new Type[] { });
                    convertedRight = Expression.Call(right, toStringMethod);
                }
                else
                    convertedRight = Expression.Convert(right, left.Type);
            }

            switch (binaryStatement.Operand)
            {
                case BinaryOperand.Assign:
                    return Expression.Assign(left, convertedRight);
                case BinaryOperand.AddAssign:
                    var strType = typeof(string);
                    if (left.Type == strType || right.Type == strType)
                    {
                        var concatMethod = strType.GetMethod("Concat", new[] { typeof(object[]) });

                        var array = Expression.NewArrayInit(typeof(object),
                            Expression.Convert(left, typeof(object)),
                            Expression.Convert(right, typeof(object)));
                        var addExpression = Expression.Call(null, concatMethod, array);
                        return Expression.Assign(left, addExpression);
                    }
                    return Expression.AddAssign(left, convertedRight);
                case BinaryOperand.SubAssign:
                    return Expression.SubtractAssign(left, convertedRight);
                case BinaryOperand.MultiplyAssign:
                    return Expression.MultiplyAssign(left, convertedRight);
                case BinaryOperand.DivideAssign:
                    return Expression.DivideAssign(left, convertedRight);
                case BinaryOperand.ModuloAssign:
                    return Expression.ModuloAssign(left, convertedRight);
                case BinaryOperand.BitwiseAndAssign:
                    if (left.Type.IsEnum)
                    {
                        var left1 = Expression.Convert(left, Enum.GetUnderlyingType(left.Type));
                        var right1 = Expression.Convert(convertedRight, Enum.GetUnderlyingType(convertedRight.Type));
                        var orExpression = Expression.AndAssign(left1, right1);
                        return Expression.Convert(orExpression, Enum.GetUnderlyingType(left.Type));
                    }
                    return Expression.AndAssign(left, convertedRight);
                case BinaryOperand.BitwiseOrAssign:
                    if (left.Type.IsEnum)
                    {
                        var left1 = Expression.Convert(left, Enum.GetUnderlyingType(left.Type));
                        var right1 = Expression.Convert(convertedRight, Enum.GetUnderlyingType(convertedRight.Type));
                        var orExpression = Expression.OrAssign(left1, right1);
                        return Expression.Convert(orExpression, Enum.GetUnderlyingType(left.Type));
                    }
                    return Expression.OrAssign(left, convertedRight);
                case BinaryOperand.XorAssign:
                    if (left.Type.IsEnum)
                    {
                        var left1 = Expression.Convert(left, Enum.GetUnderlyingType(left.Type));
                        var right1 = Expression.Convert(convertedRight, Enum.GetUnderlyingType(convertedRight.Type));
                        var orExpression = Expression.ExclusiveOrAssign(left1, right1);
                        return Expression.Convert(orExpression, Enum.GetUnderlyingType(left.Type));
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
    }
}