using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using HotReloading.Syntax.Statements;

namespace StatementConverter.ExpressionInterpreter
{
    internal class TryExpressionInterpreter : IExpressionInterpreter
    {
        private readonly ExpressionInterpreterHandler expressionInterpreterHandler;
        private TryStatement tryStatement;
        private readonly List<ParameterExpression> scopedLocalVariable;

        public TryExpressionInterpreter(ExpressionInterpreterHandler expressionInterpreterHandler, TryStatement tryStatement, List<ParameterExpression> scopedLocalVariable)
        {
            this.expressionInterpreterHandler = expressionInterpreterHandler;
            this.tryStatement = tryStatement;
            this.scopedLocalVariable = scopedLocalVariable;
        }

        public Expression GetExpression()
        {
            var tryBlock = tryStatement.TryBlock == null ? 
                null : expressionInterpreterHandler.GetExpression(tryStatement.TryBlock);
            var finallyBlock = tryStatement.Finally == null ?
                null : expressionInterpreterHandler.GetExpression(tryStatement.Finally);

            var catches = new List<CatchBlock>();

            foreach(var catchStatement in tryStatement.Catches)
            {
                if (catchStatement.Variable != null)
                {
                    var variable = (ParameterExpression)expressionInterpreterHandler.GetExpression(catchStatement.Variable);
                    scopedLocalVariable.Add(variable);
                    var blockExpression = expressionInterpreterHandler.GetExpression(catchStatement.Block);
                    var filterExpression = catchStatement.Filter == null ?
                        null : expressionInterpreterHandler.GetExpression(catchStatement.Filter);
                    scopedLocalVariable.Remove(variable);

                    catches.Add(Expression.Catch(variable, blockExpression, filterExpression));
                }
                else
                {
                    var catchVariableType = catchStatement.Type != null ?
                                                (Type)catchStatement.Type : typeof(Exception);
                    var blockExpression = expressionInterpreterHandler.GetExpression(catchStatement.Block);
                    var filterExpression = catchStatement.Filter == null ?
                        null : expressionInterpreterHandler.GetExpression(catchStatement.Filter);

                    catches.Add(Expression.Catch(catchVariableType, blockExpression, filterExpression));
                }
            }

            return Expression.TryCatchFinally(tryBlock, finallyBlock, catches.ToArray());
        }
    }
}