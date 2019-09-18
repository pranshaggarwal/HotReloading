using System;
using System.Collections.Generic;
using System.Linq;
using HotReloading.Core;
using HotReloading.Core.Statements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StatementConverter.Extensions;
using Type = HotReloading.Core.HrType;

namespace StatementConverter.StatementInterpreter
{
    internal class InitializerStatementInterpreter : IStatementInterpreter
    {
        private StatementInterpreterHandler statementInterpreterHandler;
        private InitializerExpressionSyntax initializerExpressionSyntax;
        private readonly Microsoft.CodeAnalysis.SemanticModel semanticModel;

        public InitializerStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler, InitializerExpressionSyntax initializerExpressionSyntax, Microsoft.CodeAnalysis.SemanticModel semanticModel)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.initializerExpressionSyntax = initializerExpressionSyntax;
            this.semanticModel = semanticModel;
        }

        public Statement GetStatement()
        {
            var kind = initializerExpressionSyntax.Kind();

            if(kind == Microsoft.CodeAnalysis.CSharp.SyntaxKind.ArrayInitializerExpression)
            {
                var statement = new ArrayCreationStatement();
                statement.Type = GetTypeSyntax(initializerExpressionSyntax);

                statement.Bounds = new List<Statement>();
                statement.Bounds.Add(new ConstantStatement(initializerExpressionSyntax.Expressions.Count));
                statement.Initializers = new List<Statement>();
                bool getInnerArrayLength = false;
                foreach (var expression in initializerExpressionSyntax.Expressions)
                {
                    var innerStatement = statementInterpreterHandler.GetStatement(expression);

                    if (innerStatement is ArrayCreationStatement arrayCreationStatement)
                    {
                        if (!getInnerArrayLength)
                        {
                            statement.Bounds.AddRange(
                                    arrayCreationStatement.Bounds.Select(x => x));
                            getInnerArrayLength = true;
                        }
                        statement.Initializers.AddRange(
                                arrayCreationStatement.Initializers.Select(x => x));
                    }
                    else
                    {
                        statement.Initializers.Add(innerStatement);
                    }
                }
                return statement;
            }
            else
            {
                var statement = new InitializerStatement();
                statement.Statements = initializerExpressionSyntax.Expressions.Select(x =>
                                        statementInterpreterHandler.GetStatement(x)).ToList();

                return statement;
            }
        }

        private BaseHrType GetTypeSyntax(SyntaxNode syntaxNode)
        {
            if (syntaxNode.Parent == null)
                return null;
            while(!(syntaxNode.Parent is VariableDeclarationSyntax || syntaxNode.Parent is ArrayCreationExpressionSyntax))
            {
                return GetTypeSyntax(syntaxNode.Parent);
            }

            var arrayCreationSyntax = syntaxNode.Parent as ArrayCreationExpressionSyntax;

            if(arrayCreationSyntax != null)
            {
                return arrayCreationSyntax.Type.ElementType.GetClassType(semanticModel);
            }

            var typeSyntax = ((VariableDeclarationSyntax)syntaxNode.Parent).Type;

            var typeSymbolInfo = semanticModel.GetSymbolInfo(typeSyntax);
            var arrayTypeSymbol = (IArrayTypeSymbol)typeSymbolInfo.Symbol;
            return arrayTypeSymbol.ElementType.GetHrType();
        }
    }
}