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
      /// <summary>
      /// Sucht alle CSV-Dateien im angegebenen Ordner, und versucht, sie in eine Liste von Gegenständen umzuwandeln.
      /// </summary>
      /// <param name="folderPath">Der absolute Dateipfad zum gesuchten Ordner.</param>
      /// <returns></returns>
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

      /// <summary>
      /// Wandelt die angegebene CSV-Datei in eine Liste von Gegenständen um.
      /// </summary>
      /// <param name="filePath">Der absolute Pfad zur gesuchten CSV-Datei.</param>
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

      /// <summary>
      /// Wandelt eine einzelne Zeile einer CSV-Datei in einen <see cref="Gegenstand"/> um, falls die Daten gültig sind.
      /// </summary>
      /// <param name="csv">Die CSV-Zeile, die decodiert werden soll.</param>
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
