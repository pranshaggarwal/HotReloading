using System;
using System.Linq;
using HotReloading.Core;
using HotReloading.Core.Statements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StatementConverter.Extensions;

namespace StatementConverter.StatementInterpreter
{
    internal class AnonymousMethodStatementInterpreter : IStatementInterpreter
    {
        private StatementInterpreterHandler statementInterpreterHandler;
        private AnonymousMethodExpressionSyntax anonymousMethodExpressionSyntax;
        readonly Microsoft.CodeAnalysis.SemanticModel semanticModel;

        public AnonymousMethodStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler, AnonymousMethodExpressionSyntax anonymousMethodExpressionSyntax, Microsoft.CodeAnalysis.SemanticModel semanticModel)
        {
            this.semanticModel = semanticModel;
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.anonymousMethodExpressionSyntax = anonymousMethodExpressionSyntax;
        }

        public Statement GetStatement()
        {
            var lamdaStatement = new LamdaStatement();

            var parameters = anonymousMethodExpressionSyntax.ParameterList?.Parameters
                .Select(x => statementInterpreterHandler.GetStatement(x));
            lamdaStatement.Parameters = parameters?.Cast<Parameter>()?.ToArray() ?? new Parameter[0];

            lamdaStatement.Body = statementInterpreterHandler.GetStatement(anonymousMethodExpressionSyntax.Body);

            var typeInfo = semanticModel.GetTypeInfo(anonymousMethodExpressionSyntax);

            lamdaStatement.Type = typeInfo.ConvertedType.GetHrType();

            return lamdaStatement;
        }
    }
}