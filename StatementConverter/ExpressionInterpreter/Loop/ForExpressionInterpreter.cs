using System;
using System.Linq;
using System.Linq.Expressions;
using HotReloading.Core.Statements;
using Microsoft.CSharp.Expressions;

namespace StatementConverter.ExpressionInterpreter
{
    public class ForExpressionInterpreter  : IExpressionInterpreter
    {
        private ExpressionInterpreterHandler expressionInterpreterHandler;
        private ForStatement forStatement;
        private readonly LabelTarget brk;
        private readonly LabelTarget cont;

        public ForExpressionInterpreter(ExpressionInterpreterHandler expressionInterpreterHandler, ForStatement forStatement, LabelTarget brk, LabelTarget cont)
        {
            this.expressionInterpreterHandler = expressionInterpreterHandler;
            this.forStatement = forStatement;
            this.brk = brk;
            this.cont = cont;
        }

        public Expression GetExpression()
        {
            var initializers = forStatement.Initializers.Select(x => expressionInterpreterHandler.GetExpression(x)).Cast<BinaryExpression>().ToList();
            var condition = forStatement.Condition == null ? null : expressionInterpreterHandler.GetExpression(forStatement.Condition);
            var iterators = forStatement.Iterators.Select(x => expressionInterpreterHandler.GetExpression(x)).ToList();


            var statement = expressionInterpreterHandler.GetExpression(forStatement.Statement);
            return CSharpExpression.For(initializers, condition, iterators, statement, brk, cont);
        }
    }
}