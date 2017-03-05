using System;
using System.Windows;

namespace FWA.Wpf
{
   /// <summary>
   /// Interaktionslogik für "App.xaml"
   /// </summary>
   public partial class App : Application
   {
      [STAThread]
      public static void Main(string[] args)
      {
#if !DEBUG
         CheckForUpdates();
#endif

         var app = new App();
         app.InitializeComponent();
         app.Run();
      }

      /// <summary>
      /// Prüft, ob eine neuere Version des Programms verfügbar ist und installiert diese nach Rückfrage.
      /// </summary>
      private static void CheckForUpdates()
      {
         if (!Core.Helpers.Update.NeuereVerfuegbar())
            return;

         var result = MessageBox.Show("Es ist eine neuere Version verfügbar. Möchten sie jetzt aktualisieren?",
            "Neue Version verfügbar", MessageBoxButton.YesNo, MessageBoxImage.Asterisk);

         if (result != MessageBoxResult.Yes)
            return;

         Core.Helpers.Update.Do();
         Environment.Exit(0);
      }
   }
}
