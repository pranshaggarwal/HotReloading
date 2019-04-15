using System;
using CSharpExpressions.Microsoft.CSharp;

namespace StatementConverter
{
    public class CodeChangeHandler
    {
        public static Func<Type, string, CSharpLamdaExpression> GetMethod;
    }
}
