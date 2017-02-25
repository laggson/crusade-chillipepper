using FWA.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FWA.Core.Helpers
{
   public class CsvImport
   {
      public static List<Gegenstand> GetFiles(string folderPath)
      {
         var data = new List<Gegenstand>();

         foreach (var file in Directory.GetFiles(folderPath))
         {
            if (!file.EndsWith(".csv"))
               continue;

            data.AddRange(FileToDevices(file));
         }

         return data;
      }
             
      public static List<Gegenstand> FileToDevices(string filePath)
      {
         var data = new List<Gegenstand>();

         foreach (var line in File.ReadAllLines(filePath, Encoding.UTF8))
         {
            var device = GetDeviceModel(line);

            if(device != null)
               data.Add(device);
         }

         return data;
      }
             
      public static Gegenstand GetDeviceModel(string csv)
      {
         var line = csv.Split(new []{ ';'}, StringSplitOptions.None);

         if (!line.Any(c => !string.IsNullOrEmpty(c)))
            return null;

         if (line.Length < 4 || string.IsNullOrEmpty(line[1]))
            throw new ArgumentException("Die Zeile '" + string.Join(";", line) + "' ist ungültig.");
         
         var device = new Gegenstand
         {
            Bezeichnung = line[1],
            InvNummer = line[2],
            BrauchtPruefkarte = bool.Parse(line[3]),
            ArtDerPruefung = line[5],
            Kommentar = line[6]
         };

         return device;
      }
   }
}
