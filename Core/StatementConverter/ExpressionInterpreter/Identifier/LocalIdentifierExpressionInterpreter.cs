using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using HotReloading.Core.Statements;

namespace StatementConverter.ExpressionInterpreter
{
    internal class LocalIdentifierExpressionInterpreter : IExpressionInterpreter
    {
        private readonly LocalIdentifierStatement localIdentifierStatement;
        private readonly List<ParameterExpression> scopedLocalVariable;

        public LocalIdentifierExpressionInterpreter(LocalIdentifierStatement localIdentifierStatement,
            List<ParameterExpression> scopedLocalVariable)
        {
            this.localIdentifierStatement = localIdentifierStatement;
            this.scopedLocalVariable = scopedLocalVariable;
        }

        public Expression GetExpression()
        {
            return scopedLocalVariable.First(x => x.Name == localIdentifierStatement.Name);
        }
    }
}