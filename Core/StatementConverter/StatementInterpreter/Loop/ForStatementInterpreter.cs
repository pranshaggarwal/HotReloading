using System;
using System.Collections.Generic;
using System.Linq;
using HotReloading.Core.Statements;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StatementConverter.StatementInterpreter
{
    internal class ForStatementInterpreter : IStatementInterpreter
    {
        private StatementInterpreterHandler statementInterpreterHandler;
        private ForStatementSyntax forStatementSyntax;

        public ForStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler, ForStatementSyntax forStatementSyntax)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.forStatementSyntax = forStatementSyntax;
        }

        public Statement GetStatement()
        {
            var block = new Block();
            var initializers = new List<Statement>();
            if (forStatementSyntax.Declaration != null)
            {
                var declaration = statementInterpreterHandler.GetStatement(forStatementSyntax.Declaration) as MultiStatement;
                foreach(var declarationStatement in declaration.Statements)
                {
                    if(declarationStatement is LocalVariableDeclaration)
                    {
                        block.Statements.Add(declarationStatement);
                    }
                    else
                    {
                        initializers.Add(declarationStatement);
                    }
                }
            }
            initializers.AddRange(forStatementSyntax.Initializers.Select(x => statementInterpreterHandler.GetStatement(x)).ToList());
            var condition = forStatementSyntax.Condition == null ? null : statementInterpreterHandler.GetStatement(forStatementSyntax.Condition);
            var iterators = forStatementSyntax.Incrementors.Select(x => statementInterpreterHandler.GetStatement(x)).ToList();
            var statement = statementInterpreterHandler.GetStatement(forStatementSyntax.Statement);
            var forStatement = new ForStatement
            {
                Initializers = initializers,
                Condition = condition,
                Iterators = iterators,
                Statement = statement
            };

            block.Statements.Add(forStatement);
            return block;
        }
    }
}