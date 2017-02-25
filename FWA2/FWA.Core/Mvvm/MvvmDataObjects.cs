using System;
using System.ComponentModel;
using System.Windows.Input;

namespace FWA.Core.Mvvm
{
   public class ObservableObject : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;

      protected void NotifyPropertyChanged(string propertyName)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
   }

   public class DelegateCommand : ICommand
   {
      private Action _action;
#pragma warning disable CS0067
      public event EventHandler CanExecuteChanged;
#pragma warning restore CS0067

      public DelegateCommand(Action action)
      {
         _action = action;
      }

      public bool CanExecute(object parameter)
      {
         return true;
      }

      public void Execute(object parameter)
      {
         _action();
      }
   }
}
