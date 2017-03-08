using FWA.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace FWA.Wpf
{
   /// <summary>
   /// Interaktionslogik für PruefungsWindow.xaml
   /// </summary>
   public partial class PruefungWindow
   {
      public static IEnumerable<string> ZustandValues => Enum.GetValues(typeof(Zustand)).Cast<Zustand>().Select(z => z.ToString());//.GetDescription());

      public PruefungWindow()
      {
         InitializeComponent();
         CbZustand.ItemsSource = ZustandValues;
         PruefungGrid.SelectedIndex = 0;
         //CbZustand.SelectedItemBinding = ZustandValues.ElementAt(1);
      }

      private void Abbrechen_Click(object sender, System.Windows.RoutedEventArgs e)
      {
         DialogResult = false;
         Close();
      }

      private void Fertig_Click(object sender, System.Windows.RoutedEventArgs e)
      {
         ViewModel.Fertig();
         DialogResult = true;
         Close();
      }

      private void DataGrid_GotFocus(object sender, System.Windows.RoutedEventArgs e)
      {
         // Lookup for the source to be DataGridCell
         if (e.OriginalSource.GetType() == typeof(DataGridCell))
         {
            // Starts the Edit on the row;
            DataGrid grd = (DataGrid)sender;
            grd.BeginEdit(e);
         }
      }
   }
}