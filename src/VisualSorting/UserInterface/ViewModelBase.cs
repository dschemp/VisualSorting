using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VisualSorting.UserInterface
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static ICommand CreateCommand(Action<object> execute, bool async = false)
        {
            return new ApplicationCommand(execute, null, async);
        }

        public static ICommand CreateCommand(Action<object> execute, Predicate<object> canExecute, bool async = false)
        {
            return new ApplicationCommand(execute, canExecute, async);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ApplicationCommand : ICommand
    {
        private readonly Lazy<RelayCommand> _innerCommand;

        public ApplicationCommand(Action<object> executeAction, Predicate<object> canExecuteAction = null,
            bool async = false)
        {
            _innerCommand = canExecuteAction == null
                ? new Lazy<RelayCommand>(() =>
                    async ? new RelayAsyncCommand(executeAction) : new RelayCommand(executeAction))
                : new Lazy<RelayCommand>(() =>
                    async
                        ? new RelayAsyncCommand(executeAction, canExecuteAction)
                        : new RelayCommand(executeAction, canExecuteAction));
        }

        public bool CanExecute(object parameter)
        {
            return _innerCommand.Value.CanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _innerCommand.Value.Execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => _innerCommand.Value.CanExecuteChanged += value;
            remove => _innerCommand.Value.CanExecuteChanged -= value;
        }

        public void OnCanExecuteChanged()
        {
            _innerCommand.Value.OnCanExecuteChanged();
        }
    }

    public class RelayCommand : ICommand
    {
        private Predicate<object> canExecute;
        protected Action<object> execute;

        public RelayCommand(Action<object> execute)
            : this(execute, DefaultCanExecute)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                CanExecuteChangedInternal += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
                CanExecuteChangedInternal -= value;
            }
        }

        public virtual bool CanExecute(object parameter)
        {
            return canExecute != null && canExecute(parameter);
        }

        public virtual void Execute(object parameter)
        {
            execute(parameter);
        }

        private event EventHandler CanExecuteChangedInternal;

        public void OnCanExecuteChanged()
        {
            var handler = CanExecuteChangedInternal;
            if (handler != null) handler.Invoke(this, EventArgs.Empty);
        }

        public void Destroy()
        {
            canExecute = _ => false;
            execute = _ => { };
        }

        private static bool DefaultCanExecute(object parameter)
        {
            return true;
        }
    }

    public class RelayAsyncCommand : RelayCommand
    {
        public RelayAsyncCommand(Action<object> execute, Predicate<object> canExecute)
            : base(execute, canExecute)
        {
        }

        public RelayAsyncCommand(Action<object> execute)
            : base(execute)
        {
        }

        public bool IsExecuting { get; private set; }

        public event EventHandler Started;

        public event EventHandler Ended;

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter) && !IsExecuting;
        }

        public override void Execute(object parameter)
        {
            try
            {
                IsExecuting = true;
                Started?.Invoke(this, EventArgs.Empty);

                var task = Task.Factory.StartNew(() =>
                {
                    try
                    {
                        execute(parameter);
                    }
                    catch (Exception)
                    {
                    }
                });
                task.ContinueWith(_ => OnRunWorkerCompleted(EventArgs.Empty),
                    TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                OnRunWorkerCompleted(new RunWorkerCompletedEventArgs(null, ex, true));
            }
        }

        private void OnRunWorkerCompleted(EventArgs e)
        {
            IsExecuting = false;
            Ended?.Invoke(this, e);
        }
    }
}