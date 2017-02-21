using FWA2.Core.Helpers;
using FWA2.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FWA2.Core.ViewModels
{
   public class ListViewModel : ObservableObject
   {
      #region Properties

      private List<Gegenstand> _gegenstaende;
      public List<Gegenstand> Gegenstaende
      {
         get
         {
            return _gegenstaende;
         }
         private set
         {
            _gegenstaende = value;
            NotifyPropertyChanged(nameof(Gegenstaende));
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

      #endregion

      public ListViewModel()
      {
         IsLoading = true;
         LoadItems();
      }

      public void LoadItems()
      {
         Task.Run(() =>
         {
            DBAuthentication.Create("hs", System.Text.Encoding.UTF8.GetBytes("Vivendi2016"));

            Gegenstaende = DBAuthentication.Instance.GetAlleGegenstaende();

            IsLoading = false;
         });
      }

      public ICommand ListItemDoubleClicked => new DelegateCommand(ItemDoubleClicked);

      private void ItemDoubleClicked()
      {
         if (SelectedItem == null)
            return;
      }
   }
}
