using System;
using System.Collections.Generic;
using HotReloading.Core.Statements;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StatementConverter.StatementInterpreter
{
    public class AssignmentStatementInterpreter : IStatementInterpreter
    {
        private readonly AssignmentExpressionSyntax assignmentExpressionSyntax;
        private readonly StatementInterpreterHandler statementInterpreterHandler;

        public AssignmentStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler,
            AssignmentExpressionSyntax assignmentExpressionSyntax)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.assignmentExpressionSyntax = assignmentExpressionSyntax;
        }

        public Statement GetStatement()
        {
            var assignmentStatement = new BinaryStatement();

            assignmentStatement.Left = statementInterpreterHandler.GetStatement(assignmentExpressionSyntax.Left)
                ;

            assignmentStatement.Right = statementInterpreterHandler
                .GetStatement(assignmentExpressionSyntax.Right);

            assignmentStatement.Operand = GetOperand(assignmentExpressionSyntax.Kind());

            return assignmentStatement;
        }

        private BinaryOperand GetOperand(SyntaxKind syntaxKind)
        {
            switch (syntaxKind)
            {
                case SyntaxKind.SimpleAssignmentExpression:
                    return BinaryOperand.Assign;
                case SyntaxKind.AddAssignmentExpression:
                    return BinaryOperand.AddAssign;
                case SyntaxKind.SubtractAssignmentExpression:
                    return BinaryOperand.SubAssign;
                case SyntaxKind.MultiplyAssignmentExpression:
                    return BinaryOperand.MultiplyAssign;
                case SyntaxKind.DivideAssignmentExpression:
                    return BinaryOperand.DivideAssign;
                case SyntaxKind.ModuloAssignmentExpression:
                    return BinaryOperand.ModuloAssign;
                case SyntaxKind.AndAssignmentExpression:
                    return BinaryOperand.BitwiseAndAssign;
                case SyntaxKind.OrAssignmentExpression:
                    return BinaryOperand.BitwiseOrAssign;
                case SyntaxKind.ExclusiveOrAssignmentExpression:
                    return BinaryOperand.XorAssign;
                case SyntaxKind.LeftShiftAssignmentExpression:
                    return BinaryOperand.LeftShiftAssign;
                case SyntaxKind.RightShiftAssignmentExpression:
                    return BinaryOperand.RightShiftAssign;
                default:
                    throw new NotSupportedException($"{syntaxKind.GetType()} is not supported yet");
            }
        }
    }
}