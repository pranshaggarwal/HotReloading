using System;
using System.Collections.Generic;
using HotReloading.Core;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace StatementConverter
{
    public static class Helper
    {
        public static AccessModifier GetAccessModifer(SyntaxTokenList modifiers)
        {
            if (modifiers.Any(SyntaxKind.PublicKeyword))
                return AccessModifier.Public;
            if (modifiers.Any(SyntaxKind.InternalKeyword) &&
                modifiers.Any(SyntaxKind.ProtectedKeyword))
                return AccessModifier.ProtectedInternal;
            if (modifiers.Any(SyntaxKind.InternalKeyword))
                return AccessModifier.Internal;
            if (modifiers.Any(SyntaxKind.ProtectedKeyword))
                return AccessModifier.Protected;
            return AccessModifier.Private;
        }
    }
}
