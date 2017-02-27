using System;

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
   }
}
