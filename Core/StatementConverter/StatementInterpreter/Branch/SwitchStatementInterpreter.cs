using System.Collections.Generic;
using System.Linq;
using HotReloading.Core.Statements;
using HotReloading.Syntax.Statements;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StatementConverter.StatementInterpreter
{
    internal class SwitchStatementInterpreter : IStatementInterpreter
    {
        private StatementInterpreterHandler statementInterpreterHandler;
        private SwitchStatementSyntax switchStatementSyntax;

        public SwitchStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler, SwitchStatementSyntax switchStatementSyntax)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.switchStatementSyntax = switchStatementSyntax;
        }

        public IStatementCSharpSyntax GetStatement()
        {
            var switchStatement = new SwitchStatement();

            switchStatement.SwitchValue = statementInterpreterHandler.GetStatement(switchStatementSyntax.Expression);

            var switchCases = new List<SwitchCase>();

            foreach(var section in switchStatementSyntax.Sections)
            {

                var block = new Block();

                foreach (var statementSyntax in section.Statements)
                {
                    if (statementSyntax is BreakStatementSyntax)
                        continue;

                    var statement = statementInterpreterHandler.GetStatement(statementSyntax);
                    if (statement is MultiStatement multiStatement)
                        block.Statements.AddRange(multiStatement.Statements);
                    else
                        block.Statements.Add(statement);
                }

                if (section.Labels.Count == 1 && section.Labels.Single() is DefaultSwitchLabelSyntax defaultSwitch)
                {
                    switchStatement.Default = block;
                }
                else
                {
                    var switchCase = new SwitchCase();

                    switchCase.Tests = section.Labels.Cast<CaseSwitchLabelSyntax>()
                    .Select(x => statementInterpreterHandler.GetStatement(x.Value)).ToArray();

                    switchCase.Body = block;

                    switchCases.Add(switchCase);
                }
            }

            switchStatement.Cases = switchCases.ToArray();

            return switchStatement;
        }
    }
}