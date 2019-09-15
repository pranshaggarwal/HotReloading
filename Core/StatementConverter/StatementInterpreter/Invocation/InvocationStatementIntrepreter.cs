using System;
using System.Collections.Generic;
using System.Linq;
using HotReloading.Core.Statements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StatementConverter.Extensions;

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

            if (method is DelegateIdentifierStatement delegateMethodMemberStatement)
            {
                return new DelegateInvocationStatement
                {
                    Delegate = delegateMethodMemberStatement,
                    Arguments = ies.ArgumentList.Arguments.Select(x => statementInterpreterHandler.GetStatement(x))
                };
            }

            if (method is NameOfStatement)
            {
                //nameof()
                var arguments1 = ies.ArgumentList.Arguments.Select(x => statementInterpreterHandler.GetStatement(x));
                return new ConstantStatement(arguments1.Cast<IdentifierStatement>().First().Name);
            }

            var invocationStatement = new InvocationStatement();

            var methodSymbolInfo = semanticModel.GetSymbolInfo(ies.Expression);

            var arguments = new List<Statement>();

            var symbol = methodSymbolInfo.Symbol;

            if (methodSymbolInfo.CandidateReason == CandidateReason.OverloadResolutionFailure)
            {
                symbol = semanticModel.ResolveOverload(methodSymbolInfo, ies.Expression);
            }

            if (symbol is IMethodSymbol methodSymbol)
            {
                for (int i = 0; i < methodSymbol.Parameters.Length; i++)
                {
                    var parameter = methodSymbol.Parameters[i];
                    var argumentSyntax = ies.ArgumentList.Arguments.FirstOrDefault(x => x.NameColon != null && x.NameColon.Name.Identifier.ValueText == parameter.Name);

                    if (!parameter.IsOptional)
                    {
                        if (argumentSyntax == null)
                            arguments.Add(statementInterpreterHandler.GetStatement(ies.ArgumentList.Arguments[i]));
                        else
                            arguments.Add(statementInterpreterHandler.GetStatement(argumentSyntax));
                    }
                    else
                    {
                        if (argumentSyntax == null)
                        {
                            if (ies.ArgumentList.Arguments.Count <= i)
                            {
                                //use default value
                                arguments.Add(new ConstantStatement(parameter.ExplicitDefaultValue));
                            }
                            else
                                arguments.Add(statementInterpreterHandler.GetStatement(ies.ArgumentList.Arguments[i]));
                        }
                        else
                            arguments.Add(statementInterpreterHandler.GetStatement(argumentSyntax));
                    }
                }
                invocationStatement.ParametersSignature = methodSymbol.Parameters.Select(x => x.Type.GetHrType()).ToArray();
            }
            else
            {
                arguments.AddRange(ies.ArgumentList.Arguments.Select(x => statementInterpreterHandler.GetStatement(x)));
            }

            invocationStatement.Method = method as MethodMemberStatement;
            invocationStatement.Arguments.AddRange(arguments);

            return invocationStatement;
        }
    }
}