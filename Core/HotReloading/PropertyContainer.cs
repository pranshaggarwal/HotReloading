using System;
using CSharpExpressions.Microsoft.CSharp;
using HotReloading.Core;
using StatementConverter.ExpressionInterpreter;

namespace HotReloading
{
    public class PropertyContainer : IPropertyContainer
    {
        public Property Property { get; }

        private ExpressionInterpreterHandler getterInterpreter;
        private ExpressionInterpreterHandler setterInterpreter;
        private CSharpLamdaExpression getterExpression;
        private Delegate getterDelegate;
        private CSharpLamdaExpression setterExpression;
        private Delegate setterDelegate;

        public CSharpLamdaExpression GetterExpression
        {
            get
            {
                if (getterInterpreter == null)
                    return null;
                if (getterExpression == null)
                    getterExpression = getterInterpreter.GetLamdaExpression();

                return getterExpression;
            }
        }

        public Delegate GetterDelegate
        {
            get
            {
                if (getterDelegate != null)
                    return getterDelegate;

                var exp = GetterExpression;

                if (exp == null)
                    return null;

                return exp.Compile();
            }
        }

        public CSharpLamdaExpression SetterExpression
        {
            get
            {
                if (setterInterpreter == null)
                    return null;
                if (setterExpression == null)
                    setterExpression = setterInterpreter.GetLamdaExpression();

                return setterExpression;
            }
        }

        public Delegate SetterDelegate
        {
            get
            {
                if (setterDelegate != null)
                    return setterDelegate;

                var exp = SetterExpression;

                if (exp == null)
                    return null;

                return exp.Compile();
            }
        }

        public PropertyContainer(Property property)
        {
            Property = property;

            getterInterpreter = property.Getter == null ? null :
                 new ExpressionInterpreterHandler(property.Getter);
            setterInterpreter = property.Setter == null ? null :
                 new ExpressionInterpreterHandler(property.Setter);
        }
    }
}