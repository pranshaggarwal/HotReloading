using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using HotReloading.Syntax.Statements;

namespace StatementConverter.ExpressionInterpreter
{
    internal class SwitchExpressionInterpreter : IExpressionInterpreter
    {
        private ExpressionInterpreterHandler expressionInterpreterHandler;
        private SwitchStatement switchStatement;

        public SwitchExpressionInterpreter(ExpressionInterpreterHandler expressionInterpreterHandler, SwitchStatement switchStatement)
        {
            this.expressionInterpreterHandler = expressionInterpreterHandler;
            this.switchStatement = switchStatement;
        }

        public Expression GetExpression()
        {
            var switchValue = expressionInterpreterHandler.GetExpression(switchStatement.SwitchValue);

            var cases = new List<System.Linq.Expressions.SwitchCase>();

            foreach(var switchCase in switchStatement.Cases)
            {
                var body = expressionInterpreterHandler.GetExpression(switchCase.Body);
                var tests = switchCase.Tests.Select(x => expressionInterpreterHandler.GetExpression(x));
                cases.Add(Expression.SwitchCase(body, tests));
            }

            if (switchStatement.Default != null)
            {
                var defaultBody = expressionInterpreterHandler.GetExpression(switchStatement.Default)
                    ;

                return Expression.Switch(switchValue, defaultBody, cases.ToArray());
            }


            return Expression.Switch(switchValue, cases.ToArray());
        }
    }
}