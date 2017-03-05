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

      public static string Get(string url)
      {
         return Get(new Uri(url));
      }
      public static string Get(Uri url)
      {
         string text;

         using (var client = new WebClient())
         {
            text = client.DownloadString(url);
         }
         
         return text;
      }

      public static void GetFile(string url, string fileName)
      {
         using (var client = new WebClient())
         {
            client.DownloadFile(url, fileName);
         }
      }
   }
}
