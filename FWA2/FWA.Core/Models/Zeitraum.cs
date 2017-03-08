using System.Collections.Generic;
using System.Linq;

namespace FWA.Core.Models
{
   public class Zeitraum
   {
      private int id;

      public virtual int Id
      {
         get { return id; }
         set { id = value; }
      }

      private bool januar;
      public virtual bool Januar
      {
         get { return januar; }
         set { januar = value; }
      }

      private bool februar;
      public virtual bool Februar
      {
         get { return februar; }
         set { februar = value; }
      }

      private bool maerz;
      public virtual bool Maerz
      {
         get { return maerz; }
         set { maerz = value; }
      }

      private bool april;
      public virtual bool April
      {
         get { return april; }
         set { april = value; }
      }

      private bool mai;
      public virtual bool Mai
      {
         get { return mai; }
         set { mai = value; }
      }

      private bool juni;
      public virtual bool Juni
      {
         get { return juni; }
         set { juni = value; }
      }

      private bool juli;
      public virtual bool Juli
      {
         get { return juli; }
         set { juli = value; }
      }

      private bool august;
      public virtual bool August
      {
         get { return august; }
         set { august = value; }
      }

      private bool september;
      public virtual bool September
      {
         get { return september; }
         set { september = value; }
      }

      private bool oktober;
      public virtual bool Oktober
      {
         get { return oktober; }
         set { oktober = value; }
      }

      private bool november;
      public virtual bool November
      {
         get { return november; }
         set { november = value; }
      }

      private bool dezember;
      public virtual bool Dezember
      {
         get { return dezember; }
         set { dezember = value; }
      }

      public override string ToString()
      {
         var aktivierte = ToDictionary().Values.Where(v => v);

         if (aktivierte.Count() <= 3)
            return string.Join(", ", aktivierte.Select(c => nameof(c)));

         return string.Join(", ", aktivierte.Select(c => nameof(c).Substring(0, 1)));
      }

      /// <summary>
      /// Fasst die Eigenschaften als Array zusammen, sortiert nach Monat des Jahres.
      /// </summary>
      /// <returns></returns>
      public virtual bool[] ToArray()
      {
         return new[]
         {
            Januar,
            Februar,
            Maerz,
            April,
            Mai,
            Juni,
            Juli,
            August,
            September,
            Oktober,
            November,
            Dezember
         };
      }

      /// <summary>
      /// Gibt den Wert des Monats am 1-basierten Index zurück (also z.b. 12 für Dezember).
      /// </summary>
      /// <param name="index"></param>
      /// <returns></returns>
      public virtual bool ValueAt(int index)
      {
         return ToArray()[index - 1];
      }

      /// <summary>
      /// Fasst die einzelnen Eigenschaften als <see cref="Dictionary{TKey, TValue}" zusammen, mit dem Namen als Key/>
      /// </summary>
      /// <returns></returns>
      public virtual Dictionary<string, bool> ToDictionary()
      {
         var dictionary = new Dictionary<string, bool>();
         dictionary.Add(nameof(Januar), Januar);
         dictionary.Add(nameof(Februar), Februar);
         dictionary.Add(nameof(Maerz), Maerz);
         dictionary.Add(nameof(April), April);
         dictionary.Add(nameof(Mai), Mai);
         dictionary.Add(nameof(Juni), Juni);
         dictionary.Add(nameof(Juli), Juli);
         dictionary.Add(nameof(August), August);
         dictionary.Add(nameof(September), September);
         dictionary.Add(nameof(Oktober), Oktober);
         dictionary.Add(nameof(November), November);
         dictionary.Add(nameof(Dezember), Dezember);

         return dictionary;
      }
   }
}
