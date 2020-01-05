using System;
using HotReloading.Core.Statements;
using HotReloading.Syntax.Statements;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StatementConverter.StatementInterpreter
{
    internal class MemberBindingStatementInterpreter : IStatementInterpreter
    {
        private StatementInterpreterHandler statementInterpreterHandler;
        private MemberBindingExpressionSyntax memberBindingExpression;

        public MemberBindingStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler, MemberBindingExpressionSyntax memberBindingExpression)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.memberBindingExpression = memberBindingExpression;
        }

        public IStatementCSharpSyntax GetStatement()
        {
            var conditionalAccess = GetConditionalAccessExpressionSyntax(memberBindingExpression);
            var parent = statementInterpreterHandler.GetStatement(conditionalAccess.Expression);

            return statementInterpreterHandler.GetStatementInterpreter(memberBindingExpression.Name, parent);
        }

        private ConditionalAccessExpressionSyntax GetConditionalAccessExpressionSyntax(Microsoft.CodeAnalysis.SyntaxNode parent)
        {
            if (parent == null)
                return null;

            if (parent.Parent is ConditionalAccessExpressionSyntax conditionalAccess)
            {
                if (!conditionalAccess.ToString().StartsWith("." + memberBindingExpression.Name, StringComparison.Ordinal))
                    return conditionalAccess;
            }

            return GetConditionalAccessExpressionSyntax(parent.Parent);
        }
    }
}