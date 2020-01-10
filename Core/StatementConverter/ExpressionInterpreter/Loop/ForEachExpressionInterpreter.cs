using System;
using System.Linq.Expressions;
using HotReloading.Syntax.Statements;
using Microsoft.CSharp.Expressions;

namespace StatementConverter.ExpressionInterpreter
{
    internal class ForEachExpressionInterpreter : IExpressionInterpreter
    {
        private ExpressionInterpreterHandler expressionInterpreterHandler;
        private ForEachStatement forEachStatement;
        private readonly System.Collections.Generic.List<ParameterExpression> scopedLocalVariable;
        private LabelTarget brk;
        private LabelTarget cont;

        public ForEachExpressionInterpreter(ExpressionInterpreterHandler expressionInterpreterHandler, ForEachStatement forEachStatement, System.Collections.Generic.List<ParameterExpression> scopedLocalVariable, LabelTarget brk, LabelTarget cont)
        {
            this.expressionInterpreterHandler = expressionInterpreterHandler;
            this.forEachStatement = forEachStatement;
            this.scopedLocalVariable = scopedLocalVariable;
            this.brk = brk;
            this.cont = cont;
        }

        public Expression GetExpression()
        {
            var variable = expressionInterpreterHandler.GetExpression(forEachStatement.Variable) as ParameterExpression;
            var collection = expressionInterpreterHandler.GetExpression(forEachStatement.Collection);

            scopedLocalVariable.Add(variable);
            var body = expressionInterpreterHandler.GetExpression(forEachStatement.Body);
            scopedLocalVariable.Remove(variable);
            return CSharpExpression.ForEach(variable, collection, body, brk, cont);
        }
    }
}