using FWA.Core.Mvvm;
using System.Reflection;

namespace FWA.Core.ViewModels
{
   public class AboutViewModel : ObservableObject
   {
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

      public AboutViewModel()
      {
         RetrieveVersion();
      }

      private void RetrieveVersion()
      {
         var version = Assembly.GetExecutingAssembly().GetName().Version;
         Version = version.Major + "." + version.Minor + "." + version.Build;  
      }
   }
}
