using System;
using HotReloading.Core.Statements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StatementConverter.Extensions;

namespace StatementConverter.StatementInterpreter
{
    internal class TypeOfStatementInterpreter : IStatementInterpreter
    {
        private SemanticModel semanticModel;
        private TypeOfExpressionSyntax typeOfExpressionSyntax;

        public TypeOfStatementInterpreter(SemanticModel semanticModel, TypeOfExpressionSyntax typeOfExpressionSyntax)
        {
            this.semanticModel = semanticModel;
            this.typeOfExpressionSyntax = typeOfExpressionSyntax;
        }

        public IStatementCSharpSyntax GetStatement()
        {
            var typeSymbol = (INamedTypeSymbol)semanticModel.GetSymbolInfo(typeOfExpressionSyntax.Type).Symbol;
            return new ConstantStatement((Type)(typeSymbol.GetHrType()));
        }
    }
}