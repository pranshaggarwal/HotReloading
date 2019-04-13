using System.Linq;
using HotReloading.Core.Statements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StatementConverter.StatementInterpreter
{
    public class InvocationStatementIntrepreter : IStatementInterpreter
    {
        private readonly InvocationExpressionSyntax ies;
        private readonly SemanticModel semanticModel;
        private readonly StatementInterpreterHandler statementInterpreterHandler;

        public InvocationStatementIntrepreter(StatementInterpreterHandler statementInterpreterHandler,
            SemanticModel semanticModel, InvocationExpressionSyntax ies)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.semanticModel = semanticModel;
            this.ies = ies;
        }

        public Statement GetStatement()
        {
            var method = statementInterpreterHandler.GetStatement(ies.Expression);
            var arguments = ies.ArgumentList.Arguments.Select(x => statementInterpreterHandler.GetStatement(x));

            if(method is NameOfStatement)
            {
                return new ConstantStatement(arguments.Cast<IdentifierStatement>().First().Name);
            }

            var invocationStatement = new InvocationStatement();

            invocationStatement.Method = method as MethodMemberStatement;
            invocationStatement.Arguments.AddRange(arguments);

            return invocationStatement;
        }
    }
}