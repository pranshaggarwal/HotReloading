using System;
using System.Linq;
using System.Linq.Expressions;
using HotReloading.Core.Statements;

namespace StatementConverter.ExpressionInterpreter
{
    internal class ElementAccessExpressionInterpreter : IExpressionInterpreter
    {
        private ExpressionInterpreterHandler expressionInterpreterHandler;
        private ElementAccessStatement arrayAccessStatement;

        public ElementAccessExpressionInterpreter(ExpressionInterpreterHandler expressionInterpreterHandler, ElementAccessStatement arrayAccessStatement)
        {
            this.expressionInterpreterHandler = expressionInterpreterHandler;
            this.arrayAccessStatement = arrayAccessStatement;
        }

        public Expression GetExpression()
        {
            var parent = expressionInterpreterHandler.GetExpression(arrayAccessStatement.Array);
            var indexes = arrayAccessStatement.Indexes.Select(x => expressionInterpreterHandler.GetExpression(x));

            var parentType = (Type)arrayAccessStatement.Type;
            if (parentType.IsArray)
            {
                return Expression.ArrayAccess(parent, indexes);
            }

            var indexer = parentType.GetProperties().First(x => x.GetIndexParameters().Length == indexes.Count());

            return Expression.MakeIndex(parent, indexer, indexes);
        }
    }
}