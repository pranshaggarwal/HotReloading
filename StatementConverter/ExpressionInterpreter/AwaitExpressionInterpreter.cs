using System.Linq.Expressions;
using HotReloading.Core.Statements;
using Microsoft.CSharp.Expressions;

namespace StatementConverter.ExpressionInterpreter
{
    internal class AwaitExpressionInterpreter : IExpressionInterpreter
    {
        private ExpressionInterpreterHandler expressionInterpreterHandler;
        private AwaitStatement awaitStatement;

        public AwaitExpressionInterpreter(ExpressionInterpreterHandler expressionInterpreterHandler, AwaitStatement awaitStatement)
        {
            this.expressionInterpreterHandler = expressionInterpreterHandler;
            this.awaitStatement = awaitStatement;
        }

        public Expression GetExpression()
        {
            var expression = expressionInterpreterHandler.GetExpression(awaitStatement.Statement);

            return CSharpExpression.Await(expression);
        }
    }
}