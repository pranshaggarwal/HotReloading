using System;
using HotReloading.Core.Statements;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StatementConverter.StatementInterpreter
{
    internal class PrefixUnaryStatementInterpreter : IStatementInterpreter
    {
        private StatementInterpreterHandler statementInterpreterHandler;
        private PrefixUnaryExpressionSyntax prefixUnaryExpressionSyntax;

        public PrefixUnaryStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler, PrefixUnaryExpressionSyntax prefixUnaryExpressionSyntax)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.prefixUnaryExpressionSyntax = prefixUnaryExpressionSyntax;
        }

        public Statement GetStatement()
        {
            var statement = new UnaryStatement
            {
                Operand = statementInterpreterHandler.GetStatement(prefixUnaryExpressionSyntax.Operand)
            };

            switch (prefixUnaryExpressionSyntax.Kind())
            {
                case Microsoft.CodeAnalysis.CSharp.SyntaxKind.PreDecrementExpression:
                    statement.Operator = UnaryOperand.PreDecrementAssign;
                    break;
                case Microsoft.CodeAnalysis.CSharp.SyntaxKind.PreIncrementExpression:
                    statement.Operator = UnaryOperand.PreIncrementAssign;
                    break;
                case Microsoft.CodeAnalysis.CSharp.SyntaxKind.LogicalNotExpression:
                    statement.Operator = UnaryOperand.Not;
                    break;
                case Microsoft.CodeAnalysis.CSharp.SyntaxKind.BitwiseNotExpression:
                    statement.Operator = UnaryOperand.OnesComplement;
                    break;
                case Microsoft.CodeAnalysis.CSharp.SyntaxKind.UnaryMinusExpression:
                    statement.Operator = UnaryOperand.UnaryMinus;
                    break;
                case Microsoft.CodeAnalysis.CSharp.SyntaxKind.UnaryPlusExpression:
                    statement.Operator = UnaryOperand.UnaryPlus;
                    break;
                default:
                    throw new NotSupportedException(prefixUnaryExpressionSyntax.Kind() + " is not supported unary operation");
            }
            return statement;
        }
    }
}