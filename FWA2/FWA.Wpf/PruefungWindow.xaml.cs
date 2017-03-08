using FWA.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace FWA.Wpf
{
   /// <summary>
   /// Stellt den Dialog dar, in dem die Prüfungen durchgeführt werden.
   /// </summary>
   public partial class PruefungWindow
   {
      /// <summary>
      /// Stellt alle Werte von <see cref="Zustand"/> als <see cref="string"/>-Aufzählung dar.
      /// </summary>
      public static IEnumerable<string> ZustandValues => Enum.GetValues(typeof(Zustand)).Cast<Zustand>().Select(z => z.ToString());

      /// <summary>
      /// Erstelllt eine neue Instanz der <see cref="PruefungWindow"/>-Klasse.
      /// </summary>
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
         ViewModel.OnPruefungFinished();
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