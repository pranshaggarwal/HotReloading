using System;
using HotReloading.Syntax.Statements;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StatementConverter.StatementInterpreter
{
    public class EqualsClauseStatementInterpreter : IStatementInterpreter
    {
        private readonly EqualsValueClauseSyntax equalsValueClauseSyntax;
        private readonly StatementInterpreterHandler statementInterpreterHandler;

        public EqualsClauseStatementInterpreter(EqualsValueClauseSyntax equalsValueClauseSyntax,
            StatementInterpreterHandler statementInterpreterHandler)
        {
            this.equalsValueClauseSyntax = equalsValueClauseSyntax;
            this.statementInterpreterHandler = statementInterpreterHandler;
        }

        public IStatementCSharpSyntax GetStatement()
        {
            var assignmentStatement = new BinaryStatement();

            if (equalsValueClauseSyntax.Parent is VariableDeclaratorSyntax vds)
                assignmentStatement.Left = new LocalIdentifierStatement
                {
                    Name = vds.Identifier.Text
                };
            else
                throw new NotSupportedException(equalsValueClauseSyntax.Parent.GetType() + " is not supported yet");

            assignmentStatement.Right = statementInterpreterHandler
                .GetStatement(equalsValueClauseSyntax.Value);

            assignmentStatement.Operand = BinaryOperand.Assign;

            return assignmentStatement;
        }
    }
}