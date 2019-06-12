using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Windows.Input;

namespace Ide.Core.Mvvm
{
    public abstract class DelegateCommandBase : ICommand
    {
        private SynchronizationContext _synchronizationContext;
        private readonly HashSet<string> _observedPropertiesExpressions = new HashSet<string>();

        protected DelegateCommandBase()
        {
            _synchronizationContext = SynchronizationContext.Current;
        }

        public virtual event EventHandler CanExecuteChanged;

        protected virtual void OnCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                if (_synchronizationContext != null && _synchronizationContext != SynchronizationContext.Current)
                    _synchronizationContext.Post((o) => handler.Invoke(this, EventArgs.Empty), null);
                else
                    handler.Invoke(this, EventArgs.Empty);
            }
        }

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }

        void ICommand.Execute(object parameter)
        {
            Execute(parameter);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute(parameter);
        }

        protected abstract void Execute(object parameter);

        protected abstract bool CanExecute(object parameter);

        protected internal void ObservesPropertyInternal<T>(Expression<Func<T>> propertyExpression)
        {
            if (_observedPropertiesExpressions.Contains(propertyExpression.ToString()))
            {
                throw new ArgumentException($"{propertyExpression.ToString()} is already being observed.",
                    nameof(propertyExpression));
            }
            else
            {
                _observedPropertiesExpressions.Add(propertyExpression.ToString());
                PropertyObserver.Observes(propertyExpression, RaiseCanExecuteChanged);
            }
        }
    }
}
