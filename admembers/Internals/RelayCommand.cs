using System;
using System.Windows.Input;

namespace ADMembers.Internals
{
    /// <summary>
    /// Relay command object
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Members
        readonly Func<Boolean> _canExecute;
        readonly Func<object, Boolean> _canExecuteWithArgs;
        readonly Action _execute;
        private Action<object> _executeWithArgs;
        #endregion
   
        #region Constructors
        /// <summary>
        /// Constructor taking the execute command
        /// </summary>
        /// <param name="execute">The execute command</param>
        public RelayCommand(Action execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<Object> execute)
            : this(execute, null)
        { 
        }
        
        /// <summary>
        /// Constructor taking execute and can execute commands
        /// </summary>
        /// <param name="execute">The execute command</param>
        /// <param name="canExecute">The can execute command</param>
        public RelayCommand(Action execute, Func<Boolean> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Constructor taking execute and can execute commands
        /// </summary>
        /// <param name="execute">The execute command</param>
        /// <param name="canExecute">The can execute command</param>
        public RelayCommand(Action<object> execute, Func<object, Boolean> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _executeWithArgs = execute;
            _canExecuteWithArgs = canExecute;
        }
        #endregion
   
        #region ICommand Members
        /// <summary>
        /// Event for CanExecute
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
   
                if (_canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {
   
                if (_canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }
   
        /// <summary>
        /// Checks if the command can execute
        /// </summary>
        /// <param name="parameter">N/A</param>
        /// <returns>true if the command can be executed</returns>
        public Boolean CanExecute(Object parameter)
        {
            if (null != _canExecuteWithArgs)
            {
                return _canExecuteWithArgs == null ? true : _canExecuteWithArgs(parameter);
            }
            else
            {
                return _canExecute == null ? true : _canExecute();
            }
        }
   
        /// <summary>
        /// Executs the command
        /// </summary>
        /// <param name="parameter">N/A</param>
        public void Execute(Object parameter)
        {
            if (null != _executeWithArgs)
                _executeWithArgs(parameter);
            else
                _execute();            
        }
        #endregion
    }

    
}
