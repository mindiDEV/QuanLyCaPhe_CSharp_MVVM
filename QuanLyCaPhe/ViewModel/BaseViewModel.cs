using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace QuanLyCaPhe.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool ConfirmDialog(string message)
        {
            MessageBoxResult result = MessageBox.Show(message, "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes ? true : false;
        }

        public bool WarningDialogs(string message)
        {
            MessageBoxResult res = MessageBox.Show(message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            return res == MessageBoxResult.OK ? true : false;
        }

        public bool isSymbol(string str)
        {
            return Regex.IsMatch(str, @"^[a-zA-Z0-9 ]*$");
        }

        public bool isSymbolAndNumber(string str)
        {
            return Regex.IsMatch(str, @"^[a-zA-Z]+$");
        }

        /*  ^ start of the string
            [] character set
            + one time or the more
            $ end of the string
       */
    }

    internal class RelayCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;

        public RelayCommand(Predicate<T> canExecute, Action<T> execute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _canExecute = canExecute;
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            try
            {
                return _canExecute == null ? true : _canExecute((T)parameter);
            }
            catch
            {
                return true;
            }
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}