using System;
using CSharpExpressions.Microsoft.CSharp;

namespace HotReloading.Core
{
    public interface IPropertyContainer
    {
        Property Property { get; }
        CSharpLamdaExpression GetterExpression { get; }
        Delegate GetterDelegate { get; }
        CSharpLamdaExpression SetterExpression { get; }
        Delegate SetterDelegate { get; }
    }
}