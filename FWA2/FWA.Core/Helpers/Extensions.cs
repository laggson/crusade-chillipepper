using System;
using System.ComponentModel;
using System.Reflection;

namespace FWA.Core.Helpers
{
   public static class Extensions
   {
      /// <summary>
      /// Die ältere Version einer Übersetzungsmethode für Beschreibungen von Enum-Werten.
      /// </summary>
      /// <param name="value">Der Wert des Enums, der Übersetzt werden soll.</param>
      /// <returns></returns>
      public static string Description(this Enum value)
      {
         var enumType = value.GetType();
         var field = enumType.GetField(value.ToString());
         var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute),
                                                    false);
         return attributes.Length == 0
             ? value.ToString()
             : ((DescriptionAttribute)attributes[0]).Description;
      }

      /// <summary>
      /// Die neuere Version einer Übersetzungsmethode für Beschreibungen von Enum-Werten.
      /// </summary>
      /// <param name="enumValue">Der Wert des Enums, der Übersetzt werden soll.</param>
      /// <returns></returns>
      public static string GetDescription(this Enum enumValue)
      {
         FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

         DescriptionAttribute[] attributes =
             (DescriptionAttribute[])fi.GetCustomAttributes(
             typeof(DescriptionAttribute),
             false);

         if (attributes != null &&
             attributes.Length > 0)
            return attributes[0].Description;
         else
            return enumValue.ToString();
      }
   }
}
