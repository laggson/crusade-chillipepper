using Laggson.Common;
using System.Diagnostics;
using System.Reflection;

namespace FWA.Core.Helpers
{
   public static class Update
   {
      private static readonly string Name = "FWA";
      private static AssemblyName CurrentAssembly;

      /// <summary>
      /// Prüft, ob für die FWA eine neuere Version verfügbar ist.
      /// </summary>
      /// <returns></returns>
      public static bool NeuereVerfuegbar()
      {
         CurrentAssembly = Assembly.GetExecutingAssembly().GetName();

         try
         {
            bool neuereVerfuegbar = UpdateHelper.IsNewVersionAvailable(Name, CurrentAssembly.Version);
            return neuereVerfuegbar;
         }
         catch (System.Net.WebException)
         {
            return false;
         }
      }

      /// <summary>
      /// Lädt den Installer der aktuellsten Version herunter und startet diesen.
      /// </summary>
      public static void Do()
      {
         var serverPath = Http.Get("http://h2608125.stratoserver.net:5000/api/files/FWA");
         serverPath = serverPath.Replace("\\\\", "/");

         if (serverPath.StartsWith("[\""))
            serverPath = serverPath.Substring(2, serverPath.Length - 4);

         serverPath = "http://h2608125.stratoserver.net:5000/Content" + serverPath;

         var localPath = @"C:\Temp\FWA Updater.exe";
         Http.GetFile(serverPath, localPath);

         LaunchFile(localPath);
      }

      /// <summary>
      /// Startet die angegebene Datei als eingenen Prozess.
      /// </summary>
      /// <param name="path"></param>
      private static void LaunchFile(string path)
      {
         Process process = new Process();
         process.StartInfo.FileName = path;

         process.Start();
      }
   }
}
