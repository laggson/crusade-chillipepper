using FWA2.Core.Mvvm;
using GalaSoft.MvvmLight.Messaging;
using System;

namespace FWA2.Core.ViewModels
{
   public class MainViewModel : ObservableObject
   {
      #region Properties
      private DateTime selectedDate = DateTime.Today;

      public DateTime SelectedDate
      {
         get
         {
            return selectedDate;
         }
         set
         {
            if (value.Year == selectedDate.Year && value.Month == selectedDate.Month)
               return;
            
            Messenger.Default.Send(new PropertyChangedMessage<DateTime>(selectedDate, value, nameof(SelectedDate)));
            selectedDate = value;
            NotifyPropertyChanged(nameof(SelectedDate));
         }
      }
      #endregion
   }
}
