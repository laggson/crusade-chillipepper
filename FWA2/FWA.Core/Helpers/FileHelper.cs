using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FWA.Core.Helpers
{
   public static class FileHelper
   {
      private static readonly string FolderPath;
      private static readonly string SettingsFilePath;

      public static Dictionary<Setting, object> Settings;

      static FileHelper()
      {
         FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Laggson Softworks\FWA";
         SettingsFilePath = FolderPath + "\\settings.cfg";

         ReadSettings();
      }

      /// <summary>
      /// Liest die Einstellungen aus der Textdatei aus und setzt <see cref="Settings"/> auf den gefundenen Wert.
      /// </summary>
      private static Dictionary<Setting, object> ReadSettings()
      {
         var dictionary = new Dictionary<Setting, object>();

         if (!Directory.Exists(FolderPath))
            Directory.CreateDirectory(FolderPath);

         string[] lines = new string[0];

         if (File.Exists(SettingsFilePath))
            lines = File.ReadAllLines(SettingsFilePath);

         if (!lines.Any())
            lines = new[] { "HinweisGesehen|false" };

         foreach (var line in lines)
         {
            var currentLine = line.Split('|');
            Setting setting;

            if (currentLine.Length != 2 || !Enum.TryParse(currentLine[0], out setting))
               continue;

            dictionary.Add(setting, currentLine[1]);
         }

         Settings = dictionary;
         return dictionary;
      }

      /// <summary>
      /// Schreibt den aktuellen Wert von <see cref="Settings"/> in die Einstellungs-Textdatei.
      /// </summary>
      public static void WriteSettings()
      {
         List<string> text = new List<string>();

         foreach (var setting in Settings)
         {
            text.Add(setting.Key + "|" + setting.Value);
         }

         File.WriteAllLines(SettingsFilePath, text);
      }
   }

   public enum Setting
   {
      HinweisGesehen = 0
   }
}
