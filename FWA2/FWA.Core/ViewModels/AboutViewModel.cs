using FWA.Core.Mvvm;
using System.Reflection;

namespace FWA.Core.ViewModels
{
   /// <summary>
   /// Stellt das ViewModel für den About-Dialog dar.
   /// </summary>
   public class AboutViewModel : ObservableObject
   {
      #region Properties
      private string version;
      public string Version
      {
         get { return version; }
         set
         {
            version = value;
            NotifyPropertyChanged(nameof(Version));
         }
      }
      #endregion

      /// <summary>
      /// Erstellt eine neue Instanz der <see cref="AboutViewModel"/>-Klasse.
      /// </summary>
      public AboutViewModel()
      {
         RetrieveVersion();
      }

      /// <summary>
      /// Setzt <see cref="Version"/> auf die aktuelle Version der Bibliothek.
      /// </summary>
      private void RetrieveVersion()
      {
         var version = Assembly.GetExecutingAssembly().GetName().Version;
         Version = version.Major + "." + version.Minor + "." + version.Build;  
      }
   }
}
