using System;
using System.Linq;
using HotReloading.Core.Statements;
using HotReloading.Syntax.Statements;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StatementConverter.StatementInterpreter
{
    internal class UsingStatementInterpreter : IStatementInterpreter
    {
        private StatementInterpreterHandler statementInterpreterHandler;
        private UsingStatementSyntax usingStatementSyntax;

        public UsingStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler, UsingStatementSyntax usingStatementSyntax)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.usingStatementSyntax = usingStatementSyntax;
        }

        public IStatementCSharpSyntax GetStatement()
        {
            IStatementCSharpSyntax variable = null;
            IStatementCSharpSyntax resource = null;
            if (usingStatementSyntax.Declaration != null)
            {
                var declaration = statementInterpreterHandler.GetStatement(usingStatementSyntax.Declaration) as MultiStatement;
                variable = declaration.Statements.FirstOrDefault(x => x is LocalVariableDeclaration);
                resource = (declaration.Statements.FirstOrDefault(x => x is BinaryStatement) as BinaryStatement).Right;
            }
            else
            {
                resource = statementInterpreterHandler.GetStatement(usingStatementSyntax.Expression);
            }
            var body = statementInterpreterHandler.GetStatement(usingStatementSyntax.Statement);
            return new UsingStatement
            {
                Resource = resource,
                Body = body,
                Variable = variable
            };
        }
    }
}