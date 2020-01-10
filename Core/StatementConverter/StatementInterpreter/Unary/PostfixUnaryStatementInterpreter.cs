using System;
using HotReloading.Syntax.Statements;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StatementConverter.StatementInterpreter
{
    internal class PostfixUnaryStatementInterpreter : IStatementInterpreter
    {
        private StatementInterpreterHandler statementInterpreterHandler;
        private PostfixUnaryExpressionSyntax postfixUnaryExpressionSyntax;

        public PostfixUnaryStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler, PostfixUnaryExpressionSyntax postfixUnaryExpressionSyntax)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.postfixUnaryExpressionSyntax = postfixUnaryExpressionSyntax;
        }

        public IStatementCSharpSyntax GetStatement()
        {
            var statement = new UnaryStatement
            {
                Operand = statementInterpreterHandler.GetStatement(postfixUnaryExpressionSyntax.Operand)
            };

            switch(postfixUnaryExpressionSyntax.Kind())
            {
                case Microsoft.CodeAnalysis.CSharp.SyntaxKind.PostDecrementExpression:
                    statement.Operator = UnaryOperand.PostDecrementAssign;
                    break;
                case Microsoft.CodeAnalysis.CSharp.SyntaxKind.PostIncrementExpression:
                    statement.Operator = UnaryOperand.PostIncrementAssign;
                    break;
                default:
                    throw new NotSupportedException(postfixUnaryExpressionSyntax.Kind() + " is not supported unary operation");
            }
            return statement;
        }
    }
}