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

      /// <summary>
      /// Erstellt eine neue Instanz des <see cref="MainViewModel"/>.
      /// </summary>
      public MainViewModel()
      {
         RegisterEvents();
      }

      /// <summary>
      /// Registriert die Instanz beim Messenger für die benötigten Nachrichten.
      /// </summary>
      private void RegisterEvents()
      {
         Messenger.Default.Register<NotificationMessage>(this, NotificationReceived);
      }

      /// <summary>
      /// Wird aufgerufen, wenn eine nicht generische NotificationMessage empfangen wird. Zeigt aktuell, dass das <see cref="ListViewModel"/> bereit ist.
      /// </summary>
      /// <param name="message">Die empfangene Nachricht vom Typ <see cref="NotificationMessage"/></param>
      private void NotificationReceived(NotificationMessage message)
      {
         if(message.Sender is ListViewModel && message.Notification == "Bereit")
         {
            Messenger.Default.Send(new PropertyChangedMessage<DateTime>(default(DateTime), SelectedDate, nameof(SelectedDate)));
         }
      }
   }
}
