using FWA.Core.Exceptions;
using FWA.Core.Mvvm;
using GalaSoft.MvvmLight.Messaging;
using System;

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
      }

      private void Abbrechen_Click(object sender, System.Windows.RoutedEventArgs e)
      {
         Close();
      }

      private void Ok_Click(object sender, System.Windows.RoutedEventArgs args)
      {
         Hide();
         
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
