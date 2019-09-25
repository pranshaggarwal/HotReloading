using System;
using HotReloading.Core.Statements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StatementConverter.StatementInterpreter
{
    internal class ConditionalAccessStatementInterpreter : IStatementInterpreter
    {
        private StatementInterpreterHandler statementInterpreterHandler;
        private ConditionalAccessExpressionSyntax conditionalAccessExpressionSyntax;

        public ConditionalAccessStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler, ConditionalAccessExpressionSyntax conditionalAccessExpressionSyntax)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.conditionalAccessExpressionSyntax = conditionalAccessExpressionSyntax;
        }

        public IStatementCSharpSyntax GetStatement()
        {
            var whenNotNull = statementInterpreterHandler.GetStatement(conditionalAccessExpressionSyntax.WhenNotNull);

            var condition = new BinaryStatement
            {
                Left = statementInterpreterHandler.GetStatement(conditionalAccessExpressionSyntax.Expression),
                Right = new ConstantStatement(null),
                Operand = BinaryOperand.NotEqual
            };

            //IsAssignment
            var parent = GetParent(conditionalAccessExpressionSyntax);
            if (parent is ArgumentSyntax ||
                parent is EqualsValueClauseSyntax)
            {
                return new ConditionalStatement
                {
                    Condition = condition,
                    IfTrue = whenNotNull,
                    IfFalse = new ConstantStatement(null)
                };
            }

            return new IfStatement
            {
                Condition = condition,
                IfTrue = whenNotNull
            };
        }

        private SyntaxNode GetParent(SyntaxNode syntaxNode)
        {
            if (syntaxNode.Parent is ConditionalAccessExpressionSyntax conditionalAccess)
                return GetParent(conditionalAccess);

            return syntaxNode.Parent;
        }
    }
}