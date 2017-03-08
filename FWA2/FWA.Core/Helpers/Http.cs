using System;
using System.Net;

namespace FWA.Core.Helpers
{
   public static class Http
   {
      /// <summary>
      /// Öffnet den Standard-Webbrowser und ruft die angegebene Url auf.
      /// </summary>
      /// <param name="url">Die zu öffnende Url.</param>
      public static void OpenUrl(string url)
      {
         System.Diagnostics.Process.Start(url);
      }

      /// <summary>
      /// Öffnet den Standard-Webbrowser und ruft die angegebene Url auf.
      /// </summary>
      /// <param name="url">Die zu öffnende Url.</param>
      public static void OpenUrl(Uri url)
      {
         OpenUrl(url.ToString());
      }

      /// <summary>
      /// Macht einen synchronen Get-Request auf die angegebene Url und gibt den Text zurück.
      /// </summary>
      /// <param name="url">Die absolute Adresse der gesuchten Website.</param>
      /// <returns></returns>
      public static string Get(string url)
      {
         return Get(new Uri(url));
      }

      /// <summary>
      /// Macht einen synchronen Get-Request auf die angegebene Url und gibt den Text zurück.
      /// </summary>
      /// <param name="url">Die absolute Adresse der gesuchten Website.</param>
      /// <returns></returns>
      public static string Get(Uri url)
      {
         string text;

         using (var client = new WebClient())
         {
            text = client.DownloadString(url);
         }
         
         return text;
      }

      /// <summary>
      /// Lädt die angegebene Datei herunter und speichert sie lokal unter dem angegebenen Namen.
      /// </summary>
      /// <param name="url">Die Adresse der Datei im Internet.</param>
      /// <param name="fileName">Pfad und Name, unter denen die Datei lokal gespeichert werden soll.</param>
      public static void GetFile(string url, string fileName)
      {
         using (var client = new WebClient())
         {
            client.DownloadFile(url, fileName);
         }
      }
   }
}
