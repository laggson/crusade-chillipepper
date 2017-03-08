using FWA.Core.Models;
using FWA.Core.Mvvm;
using GalaSoft.MvvmLight.Messaging;

namespace FWA.Wpf
{
   /// <summary>
   /// Stellt einen Dialog zum Bearbeiten oder Erstellen von Benutzern dar.
   /// </summary>
   public partial class BenutzerWindow
   {
      public BenutzerWindow()
      {
         InitializeComponent();

         Messenger.Default.Register<MessageboxResponseMessage>(this, OnResponseReceived);
      }

      /// <summary>
      /// Result Ja: Überschreiben von Nutzer. Nein: Werte im Control ändern. Abbrechen schließt.
      /// </summary>
      /// <param name="obj"></param>
      private void OnResponseReceived(MessageboxResponseMessage obj)
      {
         switch(obj.Result)
         {
            case MyDialogResult.Ja:
               TryCreateOrUpdateUser();
               break;
            case MyDialogResult.Nein:
               Show();
               break;
            case MyDialogResult.Abbrechen:
               Close();
               break;
         }
      }

      /// <summary>
      /// Schließt den Dialog.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void Abbrechen_Click(object sender, System.Windows.RoutedEventArgs e)
      {
         Close();
      }

      /// <summary>
      /// Zeigt eine Meldung, falls das Kürzel schon existiert und erstellt sonst den Nutzer.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="args"></param>
      private void Ok_Click(object sender, System.Windows.RoutedEventArgs args)
      {
         string name = TxtName.Text;

         if (name == "" || TxtMail.Text == "" || TxtPw1.Password == "")
            return;

         Hide();

         if (ViewModel.ExistiertNutzer(name))
         {
            Messenger.Default.Send(new MessageboxMessage("Es existiert bereits ein Nutzer namens '" + name + "'. Möchten Sie den überschreiben?",
               ImageType.Warnung, Buttons.YesNoCancel));
         }
         else
         {
            TryCreateOrUpdateUser();
         }

      }

      /// <summary>
      /// Die eigentliche Update-Logik. Erstellt den Nutzer in der Datenbank, falls die beiden PWs gleich sind.
      /// </summary>
      private void TryCreateOrUpdateUser()
      {
         try
         {
            ViewModel.CreateUser(
               TxtName.Text,
               TxtMail.Text,
               TxtPw1.Password,
               TxtPw2.Password);

            Messenger.Default.Send(new MessageboxMessage("Der Nutzer '" + TxtName.Text + "' wurde erfolgreich eingetragen.", ImageType.Information, Buttons.Ok));
         }
         catch (System.Security.Authentication.InvalidCredentialException e)
         {
            Messenger.Default.Send(new MessageboxMessage(e.Message, ImageType.Fehler, Buttons.Ok));

            TxtPw1.Password = "";
            TxtPw2.Password = "";
            Show();
            TxtPw1.Focus();
         }
      }
   }
}
