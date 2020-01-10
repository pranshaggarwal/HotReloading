using HotReloading.Syntax.Statements;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StatementConverter.Extensions;

namespace StatementConverter.StatementInterpreter
{
    internal class ArrayCreationStatementInterpreter : IStatementInterpreter
    {
        private StatementInterpreterHandler statementInterpreterHandler;
        private ArrayCreationExpressionSyntax arrayCreationExpressionSyntax;
        private readonly Microsoft.CodeAnalysis.SemanticModel semanticModel;

        public ArrayCreationStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler, ArrayCreationExpressionSyntax arrayCreationExpressionSyntax, Microsoft.CodeAnalysis.SemanticModel semanticModel)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.arrayCreationExpressionSyntax = arrayCreationExpressionSyntax;
            this.semanticModel = semanticModel;
        }

        public IStatementCSharpSyntax GetStatement()
        {
            if(arrayCreationExpressionSyntax.Initializer == null)
            {
                var statement = new ArrayCreationStatement();
                statement.Type = arrayCreationExpressionSyntax.Type.ElementType.GetClassType(semanticModel);
                statement.Bounds = new System.Collections.Generic.List<IStatementCSharpSyntax>();
                foreach(var rank in arrayCreationExpressionSyntax.Type.RankSpecifiers)
                {
                    foreach(var size in rank.Sizes)
                    {
                        statement.Bounds.Add(statementInterpreterHandler.GetStatement(size));
                    }
                }

                return statement;
            }

            return statementInterpreterHandler.GetStatement(arrayCreationExpressionSyntax.Initializer)
                ;
        }
    }
}