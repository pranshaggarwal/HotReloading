using System;
using System.Collections.Generic;
using System.Linq;
using HotReloading.Core;
using HotReloading.Core.Statements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StatementConverter.Extensions;

namespace StatementConverter.StatementInterpreter
{
    public class StatementInterpreterHandler
    {
        private readonly MethodDeclarationSyntax declarationSyntax;
        public readonly List<LocalVariableDeclaration> scopedLocalVariableDeclarations;
        private readonly SemanticModel semanticModel;
        private readonly ParameterInterpreter parameterInterpreter;
        private List<Parameter> parameters;

        public StatementInterpreterHandler(MethodDeclarationSyntax declarationSyntax, SemanticModel semanticModel)
        {
            this.declarationSyntax = declarationSyntax;
            this.semanticModel = semanticModel;

            parameterInterpreter = new ParameterInterpreter(declarationSyntax, semanticModel);

            scopedLocalVariableDeclarations = new List<LocalVariableDeclaration>();
        }

        public Statement GetStatement(CSharpSyntaxNode syntax)
        {
            Statement statement = null;
            switch (syntax)
            {
                case LocalDeclarationStatementSyntax localDeclarationStatementSyntax:
                    statement = new LocalVariableStatementIntrepreter(this,
                        localDeclarationStatementSyntax).GetStatement();
                    return statement;
                case VariableDeclarationSyntax variableDeclarationSyntax:
                    statement = new VariableDeclarationStatementInterpreter(this, semanticModel, scopedLocalVariableDeclarations,
                        variableDeclarationSyntax).GetStatement();
                    return statement;
                case ExpressionStatementSyntax expressionStatementSyntax:
                    statement = new ExpressionStatementIntrepreter(this, expressionStatementSyntax).GetStatement();
                    return statement;
                case InvocationExpressionSyntax ies:
                    statement = new InvocationStatementIntrepreter(this, semanticModel, ies).GetStatement();
                    return statement;
                case AssignmentExpressionSyntax aes:
                    statement = new AssignmentStatementInterpreter(this, aes).GetStatement();
                    return statement;
                case LiteralExpressionSyntax les:
                    statement = new LiteralStatementInterpreter(les, semanticModel).GetStatement();
                    return statement;
                case IdentifierNameSyntax ins:
                    statement = new IdentifierNameStatementInterpreter(ins, semanticModel,
                        parameters).GetStatement();
                    return statement;
                case EqualsValueClauseSyntax evcs:
                    statement = new EqualsClauseStatementInterpreter(evcs, this).GetStatement();
                    return statement;
                case ObjectCreationExpressionSyntax oces:
                    statement = new ObjectCreationStatementInterpreter(this, semanticModel, oces).GetStatement();
                    return statement;
                case ArgumentSyntax argumentSyntax:
                    statement = new ArgumentStatementInterpreter(this, argumentSyntax).GetStatement();
                    return statement;
                case MemberAccessExpressionSyntax memberAccessExpressionSyntax:
                    statement = new MemberAccessStatementInterpreter(this, memberAccessExpressionSyntax).GetStatement();
                    return statement;
                case ThisExpressionSyntax thisExpressionSyntax:
                    statement = new ThisStatementInterpreter(thisExpressionSyntax, semanticModel).GetStatement();
                    return statement;
                case BinaryExpressionSyntax binaryExpressionSyntax:
                    statement = new BinaryStatementInterpreter(this, semanticModel, binaryExpressionSyntax).GetStatement();
                    return statement;
                case ConditionalExpressionSyntax conditionalExpressionSyntax:
                    statement = new ConditionalStatementInterpreter(this, conditionalExpressionSyntax).GetStatement();
                    return statement;
                case IfStatementSyntax ifStatementSyntax:
                    statement = new IfStatementInterpreter(this, ifStatementSyntax).GetStatement();
                    return statement;
                case ElseClauseSyntax elseClauseSyntax:
                    statement = new ElseClauseStatementInterpreter(this, elseClauseSyntax).GetStatement();
                    return statement;
                case BlockSyntax blockSyntax:
                    statement = new BlockStatementInterpreter(this, blockSyntax).GetStatement();
                    return statement;
                case ArrowExpressionClauseSyntax arrowExpression:
                    statement = new ArrowStatementInterpreter(this, arrowExpression).GetStatement();
                    return statement;
                case DefaultExpressionSyntax defaultExpressionSyntax:
                    statement = new DefaultStatementInterpreter(semanticModel, defaultExpressionSyntax).GetStatement();
                    return statement;
                case TypeOfExpressionSyntax typeOfExpressionSyntax:
                    statement = new TypeOfStatementInterpreter(semanticModel, typeOfExpressionSyntax).GetStatement();
                    return statement;
                case IsPatternExpressionSyntax patternExpressionSyntax:
                    statement = new IsPatternStatementInterpreter(this, patternExpressionSyntax).GetStatement();
                    return statement;
                case SwitchStatementSyntax switchStatementSyntax:
                    statement = new SwitchStatementInterpreter(this, switchStatementSyntax).GetStatement();
                    return statement;
                case BreakStatementSyntax breakStatementSyntax:
                    statement = new BreakStatementInterpreter(breakStatementSyntax).GetStatement();
                    return statement;
                case ContinueStatementSyntax continueStatementSyntax:
                    statement = new ContinueStatementInterpreter().GetStatement();
                    return statement;
                case AwaitExpressionSyntax awaitExpressionSyntax:
                    statement = new AwaitStatementInterpreter(this, awaitExpressionSyntax).GetStatement();
                    return statement;
                case ReturnStatementSyntax returnStatementSyntax:
                    statement = new ReturnStatementInterpreter(this, returnStatementSyntax).GetStatement();
                    return statement;
                case PostfixUnaryExpressionSyntax postfixUnaryExpressionSyntax:
                    statement = new PostfixUnaryStatementInterpreter(this, postfixUnaryExpressionSyntax).GetStatement();
                    return statement;
                case PrefixUnaryExpressionSyntax prefixUnaryExpressionSyntax:
                    statement = new PrefixUnaryStatementInterpreter(this, prefixUnaryExpressionSyntax).GetStatement();
                    return statement;
                case CastExpressionSyntax castExpressionSyntax:
                    statement = new CastStatementInterpreter(this, castExpressionSyntax, semanticModel).GetStatement();
                    return statement;
                case ArrayCreationExpressionSyntax arrayCreationExpressionSyntax:
                    statement = new ArrayCreationStatementInterpreter(this, arrayCreationExpressionSyntax, semanticModel).GetStatement();
                    return statement;
                case InitializerExpressionSyntax initializerExpressionSyntax:
                    statement = new InitializerStatementInterpreter(this, initializerExpressionSyntax, semanticModel).GetStatement();
                    return statement;
                case ElementAccessExpressionSyntax elementAccessExpressionSyntax:
                    statement = new ElementAccessStatementInterpreter(this, elementAccessExpressionSyntax, semanticModel).GetStatement();
                    return statement;
                case WhileStatementSyntax whileStatementSyntax:
                    statement = new WhileStatementInterpreter(this, whileStatementSyntax).GetStatement();
                    return statement;
                case DoStatementSyntax doStatementSyntax:
                    statement = new DoStatementInterpreter(this, doStatementSyntax).GetStatement();
                    return statement;
                case ForStatementSyntax forStatementSyntax:
                    statement = new ForStatementInterpreter(this, forStatementSyntax).GetStatement();
                    return statement;
                case ForEachStatementSyntax forEachStatementSyntax:
                    statement = new ForEachStatementInterpreter(this, forEachStatementSyntax, semanticModel).GetStatement();
                    return statement;
                case UsingStatementSyntax usingStatementSyntax:
                    statement = new UsingStatementInterpreter(this, usingStatementSyntax).GetStatement();
                    return statement;
                case BaseExpressionSyntax baseExpressionSyntax:
                    statement = new BaseStatementInterpreter(baseExpressionSyntax).GetStatement();
                    return statement;
                case ParenthesizedExpressionSyntax parenthesizedExpressionSyntax:
                    statement = new ParenthesizedStatementInterpreter(this, parenthesizedExpressionSyntax).GetStatement();
                    return statement;
                case InterpolatedStringExpressionSyntax interpolatedStringExpressionSyntax:
                    statement = new InterpolatedStringStatementInterpreter(this, interpolatedStringExpressionSyntax).GetStatement();
                    return statement;
                case TryStatementSyntax tryStatementSyntax:
                    statement = new TryStatementInterpreter(this, tryStatementSyntax).GetStatement();
                    return statement;
                case CatchClauseSyntax catchClauseSyntax:
                    statement = new CatchStatementInterpreter(this, catchClauseSyntax, semanticModel).GetStatement();
                    return statement;
                case ThrowStatementSyntax throwStatementSyntax:
                    statement = new ThrowStatementInterpreter(this, throwStatementSyntax).GetStatement();
                    return statement;
                case FinallyClauseSyntax finallyClauseSyntax:
                    statement = new FinallyStatementInterpreter(this, finallyClauseSyntax).GetStatement();
                    return statement;
                default:
                    throw new NotImplementedException(syntax.GetType() + " is not supported yet");
            }
        }

        public Statement GetStatementInterpreter(SimpleNameSyntax simpleNameSyntax, Statement parent)
        {
            switch (simpleNameSyntax)
            {
                case IdentifierNameSyntax ins:
                    var statement = new IdentifierNameStatementInterpreter(ins, semanticModel,
                        parameters, parent).GetStatement();
                    return statement;
                default:
                    throw new NotImplementedException(simpleNameSyntax.GetType() + " is not supported yet");
            }
        }

        public Method GetMethod()
        {
            var newMethodData = new Method();
            newMethodData.Name = declarationSyntax.Identifier.Text;

            newMethodData.AccessModifier = GetModifier();
            newMethodData.IsAsync = declarationSyntax.Modifiers.Any(SyntaxKind.AsyncKeyword);
            newMethodData.ReturnType = GetReturnType();
            var parentNamedType = (INamedTypeSymbol) semanticModel.GetDeclaredSymbol(declarationSyntax.Parent);
            newMethodData.ParentType = parentNamedType.GetClassType();
            newMethodData.IsStatic = declarationSyntax.Modifiers.Any(SyntaxKind.StaticKeyword);

            parameters = parameterInterpreter.GetParameters();
            newMethodData.Parameters = parameters;

            newMethodData.Block = new Block();

            var nodes = declarationSyntax.DescendantNodes();

            var firstStatement = nodes.FirstOrDefault(x => x is BlockSyntax ||
                                        x is ArrowExpressionClauseSyntax);

            if (firstStatement is BlockSyntax blockSyntax)
            {
                var blockStatement = GetStatement(blockSyntax);

                scopedLocalVariableDeclarations.Clear();

                newMethodData.Block.Statements.Add(blockStatement);
            }
            else if (firstStatement is ArrowExpressionClauseSyntax arrowExpression)
            {
                var statement = GetStatement(arrowExpression);
                newMethodData.Block.Statements.Add(statement);
            }
            else
            {
                throw new Exception("Unabel to find first statement");
            }


            return newMethodData;
        }

        private ClassType GetReturnType()
        {
            var returnType = semanticModel.GetTypeInfo(declarationSyntax.ReturnType);

            return returnType.GetClassType();
        }

        private AccessModifier GetModifier()
        {
            if (declarationSyntax.Modifiers.Any(SyntaxKind.PublicKeyword))
                return AccessModifier.Public;
            if (declarationSyntax.Modifiers.Any(SyntaxKind.InternalKeyword) &&
                declarationSyntax.Modifiers.Any(SyntaxKind.ProtectedKeyword))
                return AccessModifier.ProtectedInternal;
            if (declarationSyntax.Modifiers.Any(SyntaxKind.InternalKeyword))
                return AccessModifier.Internal;
            if (declarationSyntax.Modifiers.Any(SyntaxKind.ProtectedKeyword))
                return AccessModifier.Protected;
            return AccessModifier.Private;
        }
    }
}