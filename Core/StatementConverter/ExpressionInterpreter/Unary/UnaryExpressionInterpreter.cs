using System;
using System.Linq.Expressions;
using HotReloading.Syntax.Statements;

namespace StatementConverter.ExpressionInterpreter
{
    internal class UnaryExpressionInterpreter : IExpressionInterpreter
    {
        private ExpressionInterpreterHandler expressionInterpreterHandler;
        private UnaryStatement unaryStatement;

        public UnaryExpressionInterpreter(ExpressionInterpreterHandler expressionInterpreterHandler, UnaryStatement unaryStatement)
        {
            this.expressionInterpreterHandler = expressionInterpreterHandler;
            this.unaryStatement = unaryStatement;
        }

        public Expression GetExpression()
        {
            switch(unaryStatement.Operator)
            {
                case UnaryOperand.PostDecrementAssign:
                    return Expression.PostDecrementAssign(expressionInterpreterHandler
                                                    .GetExpression(
                                                        unaryStatement.Operand));
                case UnaryOperand.PostIncrementAssign:
                    return Expression.PostIncrementAssign(expressionInterpreterHandler
                                                    .GetExpression(
                                                        unaryStatement.Operand));
                case UnaryOperand.PreDecrementAssign:
                    return Expression.PreDecrementAssign(expressionInterpreterHandler
                                                    .GetExpression(
                                                        unaryStatement.Operand));
                case UnaryOperand.PreIncrementAssign:
                    return Expression.PreIncrementAssign(expressionInterpreterHandler
                                                    .GetExpression(
                                                        unaryStatement.Operand));
                case UnaryOperand.Not:
                    return Expression.Not(expressionInterpreterHandler.
                                            GetExpression(
                                                unaryStatement.Operand));
                case UnaryOperand.OnesComplement:
                    var operand = expressionInterpreterHandler
                                                    .GetExpression(
                                                        unaryStatement.Operand);
                    if(operand.Type.IsEnum)
                    {
                        var operand1 = Expression.Convert(operand, Enum.GetUnderlyingType(operand.Type));
                        return Expression.OnesComplement(operand1);
                    }
                    return Expression.OnesComplement(operand);
                case UnaryOperand.UnaryMinus:
                    return Expression.Negate(expressionInterpreterHandler
                                                    .GetExpression(
                                                        unaryStatement.Operand));
                case UnaryOperand.UnaryPlus:
                    return expressionInterpreterHandler
                                                    .GetExpression(
                                                        unaryStatement.Operand);
                default:
                    throw new NotSupportedException(unaryStatement.Operand + " is not suppoted unary statement");
            }
        }
    }
}