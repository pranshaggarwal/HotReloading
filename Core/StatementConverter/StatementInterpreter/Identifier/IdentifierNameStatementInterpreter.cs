using System;
using System.Collections.Generic;
using System.Linq;
using HotReloading.Core;
using HotReloading.Core.Statements;
using HotReloading.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StatementConverter.Extensions;

namespace StatementConverter.StatementInterpreter
{
    public class IdentifierNameStatementInterpreter : IStatementInterpreter
    {
        private readonly IdentifierNameSyntax identifierNameSyntax;
        private readonly List<Parameter> parameters;
        private readonly IStatementCSharpSyntax parent;
        private readonly SemanticModel semanticModel;

        public IdentifierNameStatementInterpreter(IdentifierNameSyntax identifierNameSyntax,
            SemanticModel semanticModel,
            List<Parameter> parameters, IStatementCSharpSyntax parent = null)
        {
            this.identifierNameSyntax = identifierNameSyntax;
            this.semanticModel = semanticModel;
            this.parameters = parameters;
            this.parent = parent;
        }

        public IStatementCSharpSyntax GetStatement()
        {
            var varName = identifierNameSyntax.Identifier.Text;

            if (varName == "nameof")
                return new NameOfStatement();

            if (parent is BaseStatement)
            {
                //Find HotReloadingBaseCall method
                var callerMethod = GetCallingMethod(identifierNameSyntax);
                var callerSymbol = semanticModel.GetDeclaredSymbol(callerMethod);
                var methodSymbol = semanticModel.GetSymbolInfo(identifierNameSyntax).Symbol as IMethodSymbol;
                return new InstanceMethodMemberStatement
                {
                    Name = "HotReloadingBase_" + methodSymbol.Name,
                    ParentType = callerSymbol.ContainingType.GetHrType(),
                    Parent = parent ?? new ThisStatement(),
                    AccessModifier = AccessModifier.Private
                };
            }

            var symbolInfo = semanticModel.GetSymbolInfo(identifierNameSyntax);

            var symbol = symbolInfo.Symbol;

            if (symbol == null)
            {
                if (symbolInfo.CandidateReason == CandidateReason.OverloadResolutionFailure)
                {
                    //Resolve overload
                    symbol = semanticModel.ResolveOverload(symbolInfo, identifierNameSyntax);
                }
                else
                    throw new Exception($"Identifier {varName}: not able to find symbol for {symbolInfo.GetType()}. Candidate Reason: {symbolInfo.CandidateReason}");
            }
            var statement = GetStatement(symbol, varName);
            var typeInfo = semanticModel.GetTypeInfo(identifierNameSyntax);

            if (typeInfo.Type?.TypeKind == TypeKind.Delegate)
            {
                return new DelegateIdentifierStatement
                {
                    Target = statement,
                    Type = typeInfo.GetHrType()
                };
            }

            if (typeInfo.ConvertedType?.TypeKind == TypeKind.Delegate)
            {
                return new MethodPointerStatement
                {
                    Method = statement,
                    Type = typeInfo.ConvertedType.GetHrType()
                };
            }

            return statement;
        }



        private MethodDeclarationSyntax GetCallingMethod(SyntaxNode syntaxNode)
        {
            if (syntaxNode is MethodDeclarationSyntax)
                return syntaxNode as MethodDeclarationSyntax;

            if (syntaxNode.Parent == null)
                return null;

            return GetCallingMethod(syntaxNode.Parent);
        }

        private IStatementCSharpSyntax GetStatement(ISymbol symbol, string varName)
        {
            IStatementCSharpSyntax statement;
            switch (symbol)
            {
                case IFieldSymbol fs:
                    statement = GetStatement(fs, varName);
                    break;
                case IPropertySymbol ps:
                    statement = GetStatement(ps, varName);
                    break;
                case ILocalSymbol ls:
                    statement = GetStatement(ls, varName);
                    break;
                case IParameterSymbol paraS:
                    statement = GetStatement(paraS, varName);
                    break;
                case IMethodSymbol ms:
                    statement = GetStatement(ms, varName);
                    break;
                case INamedTypeSymbol nts:
                    statement = GetStatement(nts, varName);
                    break;
                case INamespaceSymbol nts:
                    statement = GetStatement(nts, varName);
                    break;
                case IEventSymbol es:
                    statement = GetStatement(es, varName);
                    break;
                default:
                    throw new NotSupportedException($"Identifier: {varName} - {symbol.GetType()} is not supported identifier yet.");
            }

            return statement;
        }

        private IStatementCSharpSyntax GetStatement(IFieldSymbol fs, string varName)
        {
            if (fs.IsStatic)
                return new StaticFieldMemberStatement
                {
                    Name = varName,
                    ParentType = fs.ContainingType.GetHrType(),
                    AccessModifier = GetAccessModifier(fs)
                };
            return new InstanceFieldMemberStatement
            {
                Name = varName,
                Parent = parent ?? new ThisStatement(),
                AccessModifier = GetAccessModifier(fs)
            };
        }

        private IStatementCSharpSyntax GetStatement(IPropertySymbol ps, string varName)
        {
            if (ps.IsStatic)
                return new StaticPropertyMemberStatement
                {
                    Name = varName,
                    ParentType = ps.ContainingType.GetHrType(),
                    AccessModifier = GetAccessModifier(ps)
                };
            return new InstancePropertyMemberStatement
            {
                Name = varName,
                Parent = parent ?? new ThisStatement(),
                AccessModifier = GetAccessModifier(ps)
            };
        }

        private IStatementCSharpSyntax GetStatement(IMethodSymbol ms, string varName)
        {
            if (ms.IsStatic)
                return new StaticMethodMemberStatement
                {
                    Name = ms.Name,
                    ParentType = ms.ContainingType.GetHrType(),
                    AccessModifier = GetAccessModifier(ms)
                };
            return new InstanceMethodMemberStatement
            {
                Name = ms.Name,
                ParentType = ms.ContainingType.GetHrType(),
                Parent = parent ?? new ThisStatement(),
                AccessModifier = GetAccessModifier(ms)
            };
        }

        private IStatementCSharpSyntax GetStatement(IEventSymbol es, string varName)
        {
            if (es.IsStatic)
                return new StaticEventMemberStatement
                {
                    Name = varName,
                    ParentType = es.ContainingType.GetHrType(),
                    AccessModifier = GetAccessModifier(es)
                };
            return new InstanceEventMemberStatement
            {
                Name = es.Name,
                ParentType = es.ContainingType.GetHrType(),
                Parent = parent ?? new ThisStatement(),
                AccessModifier = GetAccessModifier(es)
            };
        }

        private AccessModifier GetAccessModifier(ISymbol ms)
        {
            switch (ms.DeclaredAccessibility)
            {
                case Accessibility.Public:
                    return AccessModifier.Public;
                case Accessibility.Private:
                    return AccessModifier.Private;
                case Accessibility.Protected:
                    return AccessModifier.Protected;
                case Accessibility.Internal:
                    return AccessModifier.Internal;
                case Accessibility.ProtectedOrInternal:
                    return AccessModifier.ProtectedInternal;
                default:
                    throw new Exception("Accessibility is unknown");
            }
        }

        private static IStatementCSharpSyntax GetStatement(ILocalSymbol ls, string varName)
        {
            return new LocalIdentifierStatement
            {
                Name = varName,
                Type = ls.Type.GetHrType()
            };
        }

        private static IStatementCSharpSyntax GetStatement(IParameterSymbol paraS, string varName)
        {
            return new ParameterIdentifierStatement
            {
                Name = varName,
                Type = paraS.Type.GetHrType()
            };
        }

        private static IStatementCSharpSyntax GetStatement(INamedTypeSymbol ns, string varName)
        {
            return new NamedTypeStatement
            {
                Name = varName,
                Type = ns.GetHrType()
            };
        }

        private static IStatementCSharpSyntax GetStatement(INamespaceSymbol ns, string varName)
        {
            return new NamedTypeStatement
            {
                Name = varName,
                Type = null
            };
        }
    }
}