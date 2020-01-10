using System;
using System.Linq.Expressions;
using HotReloading.Syntax.Statements;
using Microsoft.CSharp.Expressions;

namespace StatementConverter.ExpressionInterpreter
{
    internal class WhileExpressionInterpreter : IExpressionInterpreter
    {
        private ExpressionInterpreterHandler expressionInterpreterHandler;
        private WhileStatement whileStatement;
        private readonly LabelTarget brk;
        private readonly LabelTarget cont;

        public WhileExpressionInterpreter(ExpressionInterpreterHandler expressionInterpreterHandler, WhileStatement whileStatement, LabelTarget brk, LabelTarget cont)
        {
            this.expressionInterpreterHandler = expressionInterpreterHandler;
            this.whileStatement = whileStatement;
            this.brk = brk;
            this.cont = cont;
        }

        public Expression GetExpression()
        {
            var condition = expressionInterpreterHandler.GetExpression(whileStatement.Conditional);
            var block = expressionInterpreterHandler.GetExpression(whileStatement.Statement);
            return CSharpExpression.While(condition, block, brk, cont);
        }
    }
}