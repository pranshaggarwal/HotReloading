using System;
using System.Linq;
using HotReloading.Core;
using HotReloading.Syntax;
using HotReloading.Syntax.Statements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StatementConverter.Extensions;
using Type = HotReloading.Syntax.HrType;

namespace StatementConverter.StatementInterpreter
{
    internal class ElementbindingStatementInterpreter : IStatementInterpreter
    {
        private StatementInterpreterHandler statementInterpreterHandler;
        private ElementBindingExpressionSyntax elementBindingExpression;
        private ExpressionSyntax elementAccessExpression;
        private readonly SemanticModel semanticModel;

        public ElementbindingStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler, 
            ElementBindingExpressionSyntax elementBindingExpression,
            SemanticModel semanticModel)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.elementBindingExpression = elementBindingExpression;
            this.semanticModel = semanticModel;
        }

        public IStatementCSharpSyntax GetStatement()
        {
            var conditionalAccess = GetConditionalAccessExpressionSyntax(elementBindingExpression);
            elementAccessExpression = conditionalAccess.Expression;

            var statement = new ElementAccessStatement();

            statement.Type = GetHrType();
            statement.Array = statementInterpreterHandler.GetStatement(elementAccessExpression);

            statement.Indexes = elementBindingExpression.ArgumentList.Arguments.Select(x => statementInterpreterHandler.GetStatement(x)).ToList();

            return statement;
        }

        private ConditionalAccessExpressionSyntax GetConditionalAccessExpressionSyntax(SyntaxNode parent)
        {
            if (parent == null)
                return null;

            if(parent.Parent is ConditionalAccessExpressionSyntax conditionalAccess)
            {
                if(!conditionalAccess.ToString().StartsWith(elementBindingExpression.ArgumentList.ToString()))
                    return conditionalAccess;
            }

            return GetConditionalAccessExpressionSyntax(parent.Parent);
        }

        private BaseHrType GetHrType()
        {
            var symbolInfo = semanticModel.GetSymbolInfo(elementAccessExpression);

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