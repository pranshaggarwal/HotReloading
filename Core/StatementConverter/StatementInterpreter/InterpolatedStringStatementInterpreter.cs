using System;
using System.Collections.Generic;
using System.Linq;
using HotReloading.Syntax;
using HotReloading.Syntax.Statements;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StatementConverter.StatementInterpreter
{
    public class InterpolatedStringStatementInterpreter : IStatementInterpreter
    {
        private StatementInterpreterHandler statementInterpreterHandler;
        private InterpolatedStringExpressionSyntax interpolatedStringExpressionSyntax;

        public InterpolatedStringStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler, InterpolatedStringExpressionSyntax interpolatedStringExpressionSyntax)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.interpolatedStringExpressionSyntax = interpolatedStringExpressionSyntax;
        }

        public IStatementCSharpSyntax GetStatement()
        {
            return GetStatement(interpolatedStringExpressionSyntax.Contents.ToList());
        }

        private IStatementCSharpSyntax GetStatement(List<InterpolatedStringContentSyntax> contents)
        {
            if (contents.Count == 0)
                return new ConstantStatement("");

            if (contents.Count == 1)
            {
                return GetStatement(contents[0]);
            }

            var binaryStatement = new BinaryStatement();
            binaryStatement.Operand = BinaryOperand.Add;

            binaryStatement.Left = GetStatement(contents[0]);
            binaryStatement.Right = GetStatement(contents.Skip(1).ToList());
            return binaryStatement;
        }

        private IStatementCSharpSyntax GetStatement(InterpolatedStringContentSyntax interpolatedStringContentSyntax)
        {
            if (interpolatedStringContentSyntax is InterpolationSyntax interpolationSyntax)
                return statementInterpreterHandler.GetStatement(interpolationSyntax.Expression);
            else
            {
                var stringTextSyntax = (InterpolatedStringTextSyntax)interpolatedStringContentSyntax;

                return new ConstantStatement(stringTextSyntax.TextToken.Text);
            }
        }
    }
}