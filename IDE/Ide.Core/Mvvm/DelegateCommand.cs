using System;
using System.Linq.Expressions;

namespace Ide.Core.Mvvm
{
    public class DelegateCommand : DelegateCommandBase
    {
        Action _executeMethod;
        Func<bool> _canExecuteMethod;

        public DelegateCommand(Action executeMethod)
            : this(executeMethod, () => true)
        {

        }

        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
            : base()
        {
            if (executeMethod == null || canExecuteMethod == null)
                throw new ArgumentNullException(nameof(executeMethod), "Neither the executeMethod nor the canExecuteMethod delegates can be null.");

            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        public void Execute()
        {
            _executeMethod();
        }

        public bool CanExecute()
        {
            return _canExecuteMethod();
        }

        protected override void Execute(object parameter)
        {
            Execute();
        }

        protected override bool CanExecute(object parameter)
        {
            return CanExecute();
        }

        public DelegateCommand ObservesProperty<T>(Expression<Func<T>> propertyExpression)
        {
            ObservesPropertyInternal(propertyExpression);
            return this;
        }

        public DelegateCommand ObservesCanExecute(Expression<Func<bool>> canExecuteExpression)
        {
            _canExecuteMethod = canExecuteExpression.Compile();
            ObservesPropertyInternal(canExecuteExpression);
            return this;
        }
    }
}
