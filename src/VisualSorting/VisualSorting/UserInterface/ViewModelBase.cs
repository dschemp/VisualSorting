using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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


        public ApplicationCommand(Action<object> executeAction, Predicate<object> canExecuteAction = null, bool async = false)
        {
            _innerCommand = canExecuteAction == null ?
                new Lazy<RelayCommand>(() => async ? new RelayAsyncCommand(executeAction) : new RelayCommand(executeAction)) :
                new Lazy<RelayCommand>(() => async ? new RelayAsyncCommand(executeAction, canExecuteAction) : new RelayCommand(executeAction, canExecuteAction));
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
            add { _innerCommand.Value.CanExecuteChanged += value; }
            remove { _innerCommand.Value.CanExecuteChanged -= value; }
        }


        public void OnCanExecuteChanged()
        {
            _innerCommand.Value.OnCanExecuteChanged();
        }
    }

    public class RelayCommand : ICommand
    {
        protected Action<object> execute;

        private Predicate<object> canExecute;

        private event EventHandler CanExecuteChangedInternal;

        public RelayCommand(Action<object> execute)
            : this(execute, DefaultCanExecute)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException("execute");
            this.canExecute = canExecute ?? throw new ArgumentNullException("canExecute");
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                this.CanExecuteChangedInternal += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
                this.CanExecuteChangedInternal -= value;
            }
        }

        public virtual bool CanExecute(object parameter)
        {
            return this.canExecute != null && this.canExecute(parameter);
        }

        public virtual void Execute(object parameter)
        {
            this.execute(parameter);
        }

        public void OnCanExecuteChanged()
        {
            EventHandler handler = this.CanExecuteChangedInternal;
            if (handler != null)
            {
                //DispatcherHelper.BeginInvokeOnUIThread(() => handler.Invoke(this, EventArgs.Empty));
                handler.Invoke(this, EventArgs.Empty);
            }
        }

        public void Destroy()
        {
            this.canExecute = _ => false;
            this.execute = _ => { return; };
        }

        private static bool DefaultCanExecute(object parameter)
        {
            return true;
        }
    }

    public class RelayAsyncCommand : RelayCommand
    {
        private bool isExecuting = false;

        public event EventHandler Started;

        public event EventHandler Ended;

        public bool IsExecuting
        {
            get { return this.isExecuting; }
        }

        public RelayAsyncCommand(Action<object> execute, Predicate<object> canExecute)
            : base(execute, canExecute)
        {
        }

        public RelayAsyncCommand(Action<object> execute)
            : base(execute)
        {
        }

        public override Boolean CanExecute(object parameter)
        {
            return ((base.CanExecute(parameter)) && (!this.isExecuting));
        }

        public override void Execute(object parameter)
        {
            try
            {
                this.isExecuting = true;
                this.Started?.Invoke(this, EventArgs.Empty);

                Task task = Task.Factory.StartNew(() =>
                {
                    try
                    {
                        this.execute(parameter);
                    }
                    catch (Exception ex)
                    {
                    }
                });
                task.ContinueWith(t =>
                {
                    this.OnRunWorkerCompleted(EventArgs.Empty);
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                this.OnRunWorkerCompleted(new RunWorkerCompletedEventArgs(null, ex, true));
            }
        }

        private void OnRunWorkerCompleted(EventArgs e)
        {
            this.isExecuting = false;
            this.Ended?.Invoke(this, e);
        }
    }
}
