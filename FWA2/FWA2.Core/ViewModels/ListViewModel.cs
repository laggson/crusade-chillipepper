using FWA2.Core.Helpers;
using FWA2.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using FWA2.Core.Mvvm;
using GalaSoft.MvvmLight.Messaging;
using System;

namespace FWA2.Core.ViewModels
{
   public class ListViewModel : ObservableObject
   {
      #region Properties

      private List<Gegenstand> _alleGegenstaende;
      public List<Gegenstand> AlleGegenstaende
      {
         get { return _alleGegenstaende; }
         set
         {
            _alleGegenstaende = value;
            RefreshFilter();
         }
      }
      
      private List<Gegenstand> _filterGegenstaende;
      public List<Gegenstand> FilterGegenstaende
      {
         get
         {
            return _filterGegenstaende;
         }
         private set
         {
            _filterGegenstaende = value;
            NotifyPropertyChanged(nameof(FilterGegenstaende));
         }
      }

      private Gegenstand selectedItem;
      public Gegenstand SelectedItem
      {
         get
         {
            return selectedItem;
         }
         set
         {
            selectedItem = value;
            NotifyPropertyChanged(nameof(SelectedItem));
         }
      }

      private bool _isLoading;
      public bool IsLoading
      {
         get
         {
            return _isLoading;
         }
         set
         {
            _isLoading = value;
            NotifyPropertyChanged(nameof(IsLoading));
         }
      }

      private DateTime SelectedDate;

      public ICommand ListItemDoubleClicked => new DelegateCommand(OpenPruefung);
      #endregion

      public ListViewModel()
      {
         RegisterEvents();
         IsLoading = true;
         LoadItems();
      }

      /// <summary>
      /// Registriert die benötigten Events beim Galasoft-Messenger
      /// </summary>
      private void RegisterEvents()
      {
         Messenger.Default.Register<PropertyChangedMessage<DateTime>>(this, OnDateChanged);
      }

      /// <summary>
      /// Wird aufgerufen, wenn sich im MainWindow das ausgewählte Datum ändert.
      /// </summary>
      /// <param name="message">Die Nachricht, die empfangen wurde.</param>
      private void OnDateChanged(PropertyChangedMessage<DateTime> message)
      {
         SelectedDate = message.NewValue;
      }

      public void RefreshFilter()
      {
         // TODO: Bei Gegenstaende nur die anzeigen, die im ausgewählten Monat geprüft werden müssen.
         FilterGegenstaende = AlleGegenstaende;
      }

      /// <summary>
      /// Meldet sich mit dem Nutzer 'hs' an und lädt alle Gegenstände aus der Datenbank, damit sie im Dialog angezeigt werden.
      /// </summary>
      public void LoadItems()
      {
         Task.Run(() =>
         {
            // TODO: nicht hartkodieren.
            DBAuthentication.Create("hs", System.Text.Encoding.UTF8.GetBytes("Vivendi2016"));

            AlleGegenstaende = DBAuthentication.Instance.GetAlleGegenstaende();

            IsLoading = false;
         });

         // Anderen VMs bescheid geben, dass ich da bin.
         Messenger.Default.Send(new NotificationMessage(this, "Bereit"));
      }

      /// <summary>
      /// Wird ausgelöst, wenn ein Item doppelt geklickt wurde und soll den Dialog öffnen.
      /// </summary>
      private void OpenPruefung()
      {
         if (SelectedItem == null)
            return;
      }
   }
}
