using System;
using System.Linq.Expressions;
using HotReloading.Syntax.Statements;
using Microsoft.CSharp.Expressions;

namespace StatementConverter.ExpressionInterpreter
{
    internal class DoWhileExpressionInterpreter : IExpressionInterpreter
    {
        private ExpressionInterpreterHandler expressionInterpreterHandler;
        private DoWhileStatement doWhileStatement;
        private readonly LabelTarget brk;
        private readonly LabelTarget cont;

        public DoWhileExpressionInterpreter(ExpressionInterpreterHandler expressionInterpreterHandler, DoWhileStatement doWhileStatement, LabelTarget brk, LabelTarget cont)
        {
            this.expressionInterpreterHandler = expressionInterpreterHandler;
            this.doWhileStatement = doWhileStatement;
            this.brk = brk;
            this.cont = cont;
        }

        public Expression GetExpression()
        {
            var condition = expressionInterpreterHandler.GetExpression(doWhileStatement.Conditional);
            var statement = expressionInterpreterHandler.GetExpression(doWhileStatement.Statement);

            return CSharpExpression.Do(statement, condition, brk, cont);
        }
    }
}