using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using HotReloading.Syntax.Statements;

namespace StatementConverter.ExpressionInterpreter
{
    public class BlockExpressionInterpreter : IExpressionInterpreter
    {
        private readonly Block block;
        private readonly ExpressionInterpreterHandler interpreterHandler;
        private readonly List<ParameterExpression> scopedLocalVariable = new List<ParameterExpression>();

        public BlockExpressionInterpreter(ExpressionInterpreterHandler interpreterHandler,
            List<ParameterExpression> scopedLocalVariable, Block block)
        {
            this.interpreterHandler = interpreterHandler;
            this.scopedLocalVariable = scopedLocalVariable;
            this.block = block;
        }

        public Expression GetExpression()
        {
            var varDeclarationStatements = block.Statements.OfType<LocalVariableDeclaration>();

            var varDeclarationExpresions = varDeclarationStatements.Select(x =>
                interpreterHandler.GetExpression(x)).Cast<ParameterExpression>().ToList();

            scopedLocalVariable.AddRange(varDeclarationExpresions);

            var statementExpressions = block.Statements
                .Except(varDeclarationStatements)
                .Select(x => interpreterHandler.GetExpression(x)).ToList();
            scopedLocalVariable.RemoveAll(x => varDeclarationExpresions.Contains(x));
            return Expression.Block(varDeclarationExpresions, statementExpressions);
        }
    }
}