using HotReloading.Core.Statements;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StatementConverter.StatementInterpreter
{
    internal class IsPatternStatementInterpreter : IStatementInterpreter
    {
        private StatementInterpreterHandler statementInterpreterHandler;
        private IsPatternExpressionSyntax patternExpressionSyntax;

        public IsPatternStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler, IsPatternExpressionSyntax patternExpressionSyntax)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.patternExpressionSyntax = patternExpressionSyntax;
        }

        public IStatementCSharpSyntax GetStatement()
        {
            var statement = statementInterpreterHandler.GetStatement(patternExpressionSyntax.Expression);

            if(patternExpressionSyntax.Pattern is ConstantPatternSyntax constantPatternSyntax)
            {
                return new ObjectEqualStatement
                {
                    Left = statement,
                    Right = statementInterpreterHandler.GetStatement(constantPatternSyntax.Expression)
                };
            }

            return null;
        }
    }
}