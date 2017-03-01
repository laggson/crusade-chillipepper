using FWA.Core.Models;
using FWA.Core.Mvvm;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;
using System;

namespace FWA.Wpf
{
   /// <summary>
   /// Interaktionslogik für MainWindow.xaml
   /// </summary>
   public sealed partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
         RegisterEvents();
      }

      /// <summary>
      /// Registriert sich beim MVVM-Messenger für die angegebenen Message-Typen
      /// </summary>
      private void RegisterEvents()
      {
         Messenger.Default.Register<ErrorMessage>(this, OnErroReceived);
         Messenger.Default.Register<LoginMessage>(this, OnloginChanged);
         Messenger.Default.Register<RequestDialogOpenMessage>(this, OnDialogOpenRequest);
      }
      void OnloginChanged(LoginMessage msg)
      {
         Focus();
      }

      private void OnDialogOpenRequest(RequestDialogOpenMessage msg)
      {
         Window window = null;

         switch (msg.Dialog)
         {
            case Dialog.AboutWindow:
               window = new AboutWindow();
               break;
            case Dialog.LoginWindow:
               window = new LoginWindow();
               break;
            case Dialog.PruefungWindow:
               window = new PruefungsWindow();
               break;
            default:
               return;
         }

         window.Owner = this;
         window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
         window.ShowDialog();
      }

      /// <summary>
      /// Wird aufgerufen, wenn im Programm eine Ausnahme abgefangen wird, und zeigt eine entsprechende Meldung.
      /// </summary>
      /// <param name="message"></param>
      private void OnErroReceived(ErrorMessage message)
      {
         this.ShowMessageAsync(message.Header, message.Message);
      }

      /*private void ShowCsvDlg()
      {
         if (!CommonFileDialog.IsPlatformSupported)
            return;

         var dlg = new CommonOpenFileDialog();
         dlg.IsFolderPicker = true;
         dlg.Multiselect = false;
         var result = dlg.ShowDialog();

         if (result != CommonFileDialogResult.Ok)
            return;

         var data = Core.Helpers.CsvImport.GetFiles(dlg.FileName);
      }*/

      /// <summary>
      /// Zeigt modal den About-Dialog an, der die aktuelle Version und die verwendeten Libraries enthält.
      /// </summary>
      private void OpenAboutWindow()
      {
         var aboutWindow = new AboutWindow { Owner = this }.ShowDialog();
      }

      private void MiAbout_Click(object sender, RoutedEventArgs e)
      {
         OpenAboutWindow();
      }

      protected override void OnContentRendered(EventArgs e)
      {
         base.OnContentRendered(e);
         OnDialogOpenRequest(new RequestDialogOpenMessage(Dialog.LoginWindow));
      }
   }
}
