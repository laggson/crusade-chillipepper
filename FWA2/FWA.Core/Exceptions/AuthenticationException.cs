using System;
using System.Runtime.Serialization;

namespace FWA.Core.Exceptions
{
   /// <summary>
   /// Diese Ausnahme wird ausgelöst, falls die eingegebenen Nutzerdaten nicht in der Datenbank gefunden werden konnten
   /// </summary>
   [Serializable]
   public class AuthenticationException : Exception
   {
      /// <summary>
      /// Der eingegebene Name, der zu der fehlgeschlagenen Anmeldung führte
      /// </summary>
      public string Username { get; }

      /// <summary>
      /// Diese Nachricht beschreibt, woran die Anmeldung scheiterte
      /// </summary>
      public string FailReasonMessage { get; }

      /// <summary>
      /// Initialisiert eine neue Instanz der FWA.Logic.Exceptions.AuthenticationException mit einem eingegeben Nutzernamen und einer Nachricht
      /// </summary>
      /// <param name="username"></param>
      /// <param name="msg"></param>
      public AuthenticationException(string username, string msg)
          : base(string.Format("Anmeldung als Nutzer '{0}' fehlgeschlagen: {1}", username, msg))
      {
         Username = username;
         FailReasonMessage = msg;
      }

      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         base.GetObjectData(info, context);
      }
   }
}
