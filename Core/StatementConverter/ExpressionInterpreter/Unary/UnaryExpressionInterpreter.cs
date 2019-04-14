using System;
using System.Linq.Expressions;
using HotReloading.Core.Statements;

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
                    return Expression.OnesComplement(expressionInterpreterHandler
                                                    .GetExpression(
                                                        unaryStatement.Operand));
                default:
                    throw new NotSupportedException(unaryStatement.Operand + " is not suppoted unary statement");
            }
        }
    }
}