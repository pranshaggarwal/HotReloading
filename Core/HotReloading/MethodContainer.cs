using System;
using CSharpExpressions.Microsoft.CSharp;
using HotReloading.Core;
using StatementConverter.ExpressionInterpreter;

namespace HotReloading
{
    public class MethodContainer : IMethodContainer
    {
        public Method Method { get; set; }
        private Delegate @delegate;
        private CSharpLamdaExpression expression;
        private ExpressionInterpreterHandler interpreter;

        public MethodContainer(Method method)
        {
            Method = method;
            interpreter = new ExpressionInterpreterHandler(method);
        }

        public CSharpLamdaExpression GetExpression()
        {
            if (expression == null)
                expression = interpreter.GetLamdaExpression();

            return expression;
        }

        public Delegate GetDelegate()
        {
            if (@delegate != null)
                return @delegate;

            if (expression == null)
                expression = interpreter.GetLamdaExpression();

            @delegate = expression.Compile();
            return @delegate;
        }
    }
}