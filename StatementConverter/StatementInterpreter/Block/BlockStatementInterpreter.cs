using HotReloading.Core.Statements;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StatementConverter.StatementInterpreter
{
    internal class BlockStatementInterpreter : IStatementInterpreter
    {
        private readonly StatementInterpreterHandler statementInterpreterHandler;
        private BlockSyntax blockSyntax;

        public BlockStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler, BlockSyntax blockSyntax)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.blockSyntax = blockSyntax;
        }

        public Statement GetStatement()
        {
            var block = new Block();
            foreach(var statementSyntax in blockSyntax.Statements)
            {
                var statement = statementInterpreterHandler.GetStatement(statementSyntax);
                if (statement is MultiStatement multiStatement)
                    block.Statements.AddRange(multiStatement.Statements);
                else
                    block.Statements.Add(statement);
            }

            return block;
        }
    }
}