using System;
using System.Linq;
using HotReloading.Core;
using HotReloading.Core.Statements;
using HotReloading.Syntax;
using HotReloading.Syntax.Statements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StatementConverter.Extensions;
using Type = HotReloading.Syntax.HrType;

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

        public IStatementCSharpSyntax GetStatement()
        {
            var statement = new ElementAccessStatement();

            statement.Type = GetHrType();
            statement.Array = statementInterpreterHandler.GetStatement(elementAccessExpressionSyntax.Expression);

            statement.Indexes = elementAccessExpressionSyntax.ArgumentList.Arguments.Select(x => statementInterpreterHandler.GetStatement(x)).ToList();

            return statement;
        }

        private BaseHrType GetHrType()
        {
            var symbolInfo = semanticModel.GetSymbolInfo(elementAccessExpressionSyntax.Expression);

            switch (symbolInfo.Symbol)
            {
                case IFieldSymbol fs:
                    return fs.Type.GetHrType();
                case IPropertySymbol ps:
                    return ps.Type.GetHrType();
                case ILocalSymbol ls:
                    return ls.Type.GetHrType();
                case IParameterSymbol paraS:
                    return paraS.Type.GetHrType();
                case INamedTypeSymbol nts:
                    return nts.GetHrType();
                default:
                    throw new NotSupportedException($"{symbolInfo.Symbol.GetType()} is not supported yet.");
            }
        }
    }
}