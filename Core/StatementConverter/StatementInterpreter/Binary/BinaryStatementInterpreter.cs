using System;
using HotReloading.Core.Statements;
using HotReloading.Syntax.Statements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StatementConverter.Extensions;

namespace StatementConverter.StatementInterpreter
{
    internal class BinaryStatementInterpreter : IStatementInterpreter
    {
        private readonly StatementInterpreterHandler statementInterpreterHandler;
        private readonly Microsoft.CodeAnalysis.SemanticModel semanticModel;
        private BinaryExpressionSyntax binaryExpressionSyntax;

        public BinaryStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler, Microsoft.CodeAnalysis.SemanticModel semanticModel, BinaryExpressionSyntax binaryExpressionSyntax)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.semanticModel = semanticModel;
            this.binaryExpressionSyntax = binaryExpressionSyntax;
        }

        public IStatementCSharpSyntax GetStatement()
        {
            if (binaryExpressionSyntax.Kind() == SyntaxKind.IsExpression)
            {
                var statement = statementInterpreterHandler.GetStatement(binaryExpressionSyntax.Left);
                var typeSymbol = (INamedTypeSymbol)semanticModel.GetSymbolInfo(binaryExpressionSyntax.Right).Symbol;
                return new IsTypeStatement
                {
                    Statement = statement,
                    Type = typeSymbol.GetHrType()
                };
            }
            else if (binaryExpressionSyntax.Kind() == SyntaxKind.AsExpression)
            {
                var statement = statementInterpreterHandler.GetStatement(binaryExpressionSyntax.Left);
                var typeSymbol = (INamedTypeSymbol)semanticModel.GetSymbolInfo(binaryExpressionSyntax.Right).Symbol;
                return new AsStatement
                {
                    Statement = statement,
                    Type = typeSymbol.GetHrType()
                };
            }

            var binaryStatement = new BinaryStatement();
            binaryStatement.Left = statementInterpreterHandler.GetStatement(binaryExpressionSyntax.Left);
            binaryStatement.Right = statementInterpreterHandler.GetStatement(binaryExpressionSyntax.Right);

            binaryStatement.Operand = GetOperand(binaryExpressionSyntax.Kind());
            return binaryStatement;
        }

        private BinaryOperand GetOperand(SyntaxKind syntaxKind)
        {
            switch (syntaxKind)
            {
                case SyntaxKind.AddExpression:
                    return BinaryOperand.Add;
                case SyntaxKind.SubtractExpression:
                    return BinaryOperand.Sub;
                case SyntaxKind.MultiplyExpression:
                    return BinaryOperand.Multiply;
                case SyntaxKind.DivideExpression:
                    return BinaryOperand.Divide;
                case SyntaxKind.ModuloExpression:
                    return BinaryOperand.Modulo;
                case SyntaxKind.EqualsExpression:
                    return BinaryOperand.Equal;
                case SyntaxKind.NotEqualsExpression:
                    return BinaryOperand.NotEqual;
                case SyntaxKind.LogicalAndExpression:
                    return BinaryOperand.And;
                case SyntaxKind.LogicalOrExpression:
                    return BinaryOperand.Or;
                case SyntaxKind.GreaterThanExpression:
                    return BinaryOperand.GreaterThan;
                case SyntaxKind.GreaterThanOrEqualExpression:
                    return BinaryOperand.GreaterThanEqual;
                case SyntaxKind.LessThanExpression:
                    return BinaryOperand.LessThan;
                case SyntaxKind.LessThanOrEqualExpression:
                    return BinaryOperand.LessThanEqual;
                case SyntaxKind.BitwiseAndExpression:
                    return BinaryOperand.BitwiseAnd;
                case SyntaxKind.BitwiseOrExpression:
                    return BinaryOperand.BitwiseOr;
                case SyntaxKind.ExclusiveOrExpression:
                    return BinaryOperand.Xor;
                case SyntaxKind.LeftShiftExpression:
                    return BinaryOperand.LeftShift;
                case SyntaxKind.RightShiftExpression:
                    return BinaryOperand.RightShift;
                case SyntaxKind.CoalesceExpression:
                    return BinaryOperand.Coalesce;
                default:
                    throw new NotSupportedException($"{syntaxKind} is not supported yet");
            }
        }
    }
}