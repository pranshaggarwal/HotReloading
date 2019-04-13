using System;
using System.Linq;
using HotReloading.Core;
using HotReloading.Core.Statements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StatementConverter.Extensions;

namespace StatementConverter.StatementInterpreter
{
    internal class ElementAccessStatementInterpreter : IStatementInterpreter
    {
        private StatementInterpreterHandler statementInterpreterHandler;
        private ElementAccessExpressionSyntax elementAccessExpressionSyntax;
        private readonly Microsoft.CodeAnalysis.SemanticModel semanticModel;

        public ElementAccessStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler, ElementAccessExpressionSyntax elementAccessExpressionSyntax, Microsoft.CodeAnalysis.SemanticModel semanticModel)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.elementAccessExpressionSyntax = elementAccessExpressionSyntax;
            this.semanticModel = semanticModel;
        }

        public Statement GetStatement()
        {
            var statement = new ElementAccessStatement();

            statement.Type = GetClassType();
            statement.Array = statementInterpreterHandler.GetStatement(elementAccessExpressionSyntax.Expression);

            statement.Indexes = elementAccessExpressionSyntax.ArgumentList.Arguments.Select(x => statementInterpreterHandler.GetStatement(x)).ToList();

            return statement;
        }

        private ClassType GetClassType()
        {
            var symbolInfo = semanticModel.GetSymbolInfo(elementAccessExpressionSyntax.Expression);

            switch (symbolInfo.Symbol)
            {
                case IFieldSymbol fs:
                    return fs.Type.GetClassType();
                case IPropertySymbol ps:
                    return ps.Type.GetClassType();
                case ILocalSymbol ls:
                    return ls.Type.GetClassType();
                case IParameterSymbol paraS:
                    return paraS.Type.GetClassType();
                case INamedTypeSymbol nts:
                    return nts.GetClassType();
                default:
                    throw new NotSupportedException($"{symbolInfo.Symbol.GetType()} is not supported yet.");
            }
        }
    }
}