using FWA.Core.Models;
using FWA.Core.Mvvm;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Reflection;
using Update = Laggson.Common.UpdateHelper;

namespace FWA.Core.ViewModels
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

      private bool alleAnzeigen;
      public bool AlleAnzeigen
      {
         get { return alleAnzeigen; }
         set
         {
            Messenger.Default.Send(new PropertyChangedMessage<bool>(alleAnzeigen, value, nameof(AlleAnzeigen)));
            alleAnzeigen = value;
            CbDisabled = !alleAnzeigen;
            NotifyPropertyChanged(nameof(AlleAnzeigen));
         }
      }

      private bool cbDisabled;
      public bool CbDisabled
      {
         get { return cbDisabled; }
         set
         {
            cbDisabled = value;
            NotifyPropertyChanged(nameof(CbDisabled));
         }
      }
      #endregion

      /// <summary>
      /// Erstellt eine neue Instanz des <see cref="MainViewModel"/>.
      /// </summary>
      public MainViewModel()
      {
         AlleAnzeigen = false;
         RegisterEvents();
      }

      /// <summary>
      /// Registriert die Instanz beim Messenger für die benötigten Nachrichten.
      /// </summary>
      private void RegisterEvents()
      {
         Messenger.Default.Register<NotificationMessage>(this, NotificationReceived);

         // Nach dem Registrieren das Login-Fenster öffnen.
         //Messenger.Default.Send(new RequestDialogOpenMessage(Models.Dialog.LoginWindow));
         Messenger.Default.Send(new RequestDialogOpenMessage(Dialog.LoginWindow));
      }

      /// <summary>
      /// Wird aufgerufen, wenn eine nicht generische NotificationMessage empfangen wird. Zeigt aktuell, dass das <see cref="ListViewModel"/> bereit ist.
      /// </summary>
      /// <param name="message">Die empfangene Nachricht vom Typ <see cref="NotifyUserMessage"/></param>
      private void NotificationReceived(NotificationMessage message)
      {
         if(message.Sender is ListViewModel && message.Notification == "Bereit")
         {
            Messenger.Default.Send(new PropertyChangedMessage<DateTime>(default(DateTime), SelectedDate, nameof(SelectedDate)));
         }
      }
   }
}
