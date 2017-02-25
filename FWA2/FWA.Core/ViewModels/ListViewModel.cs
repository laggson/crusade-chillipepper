using FWA.Core.Helpers;
using FWA.Core.Models;
using FWA.Core.Mvvm;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FWA.Core.ViewModels
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
            RefreshFilter(SelectedDate);
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
         Messenger.Default.Register<PropertyChangedMessage<bool>>(this, OnAlleZeigenChanged);
      }

      private void OnAlleZeigenChanged(PropertyChangedMessage<bool> message)
      {
         switch (message.PropertyName)
         {
            case nameof(MainViewModel.AlleAnzeigen):
               RefreshFilter(message.NewValue ? default(DateTime) : SelectedDate);
               break;
         }
      }

      /// <summary>
      /// Wird aufgerufen, wenn sich im MainWindow das ausgewählte Datum ändert.
      /// </summary>
      /// <param name="message">Die Nachricht, die empfangen wurde.</param>
      private void OnDateChanged(PropertyChangedMessage<DateTime> message)
      {
         SelectedDate = message.NewValue;
         RefreshFilter(message.NewValue);
      }

      /// <summary>
      /// Aktualisiert die angezeigten Gegenstände der Liste mit dem aktuellen Datum.
      /// </summary>
      public void RefreshFilter(int month)
      {
         FilterGegenstaende = month <= 0
            ? AlleGegenstaende
            : AlleGegenstaende?.Where(g => g.Zeitraum != null &&
               g.Zeitraum.ToArray()[month]).ToList();
      }

      /// <summary>
      /// Aktualisiert die angezeigten Gegenstände der Liste mit dem aktuellen Datum.
      /// </summary>
      public void RefreshFilter(DateTime dateTime)
      {
         RefreshFilter(dateTime == default(DateTime) ? -1 : dateTime.Month - 1);
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
