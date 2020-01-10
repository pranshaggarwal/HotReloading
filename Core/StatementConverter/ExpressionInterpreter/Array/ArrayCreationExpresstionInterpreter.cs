using System;
using System.Linq;
using System.Linq.Expressions;
using HotReloading.Syntax;
using HotReloading.Syntax.Statements;
using Microsoft.CSharp.Expressions;

namespace StatementConverter.ExpressionInterpreter
{
    internal class ArrayCreationExpresstionInterpreter : IExpressionInterpreter
    {
        private readonly ExpressionInterpreterHandler expressionInterpreterHandler;
        private ArrayCreationStatement arrayCreationStatement;

        public ArrayCreationExpresstionInterpreter(ExpressionInterpreterHandler expressionInterpreterHandler, ArrayCreationStatement arrayCreationStatement)
        {
            this.expressionInterpreterHandler = expressionInterpreterHandler;
            this.arrayCreationStatement = arrayCreationStatement;
        }

        public Expression GetExpression()
        {
            if (arrayCreationStatement.Initializers == null)
            {
                var bounds = arrayCreationStatement.Bounds.Select(x => expressionInterpreterHandler.GetExpression(x)
                ).ToList();
                return Expression.NewArrayBounds(arrayCreationStatement.Type, bounds);
            }
            else
            {
                var bounds = arrayCreationStatement.Bounds.Cast<ConstantStatement>().Select(x =>
                {
                    return Convert.ToInt32(x.Value);
                }).ToArray();

                var initializers = arrayCreationStatement.Initializers.Select(x =>
                                        expressionInterpreterHandler.GetExpression(x));
                if (bounds.Length == 1)
                    return Expression.NewArrayInit(arrayCreationStatement.Type, initializers);

                return CSharpExpression.NewMultidimensionalArrayInit(arrayCreationStatement.Type, bounds, initializers);
            }
        }
    }
}