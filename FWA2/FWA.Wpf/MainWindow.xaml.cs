using FWA.Core.Models;
using FWA.Core.Mvvm;
using GalaSoft.MvvmLight.Messaging;
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
         Messenger.Default.Register<NotifyUserMessage>(this, OnErroReceived);
         Messenger.Default.Register<MessageboxMessage>(this, OnMessageboxInvoked);
         //Messenger.Default.Register<ErrorMessage>(this, OnErroReceived); // kann vl weg, wenn der Messenger erkennt dass es derived ist
         Messenger.Default.Register<RequestDialogOpenMessage>(this, OnDialogOpenRequest);
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
         window.Closed += (o, args) => { Application.Current.MainWindow.Focus(); };
         window.ShowDialog();
      }

      /// <summary>
      /// Wird aufgerufen, wenn eine <see cref="MessageboxMessage"/> empfangen wird und zeigt eine entsprechende <see cref="MessageBox"/>.
      /// </summary>
      /// <param name="message"></param>
      void OnMessageboxInvoked(MessageboxMessage message)
      {
         MessageBoxButton button;
         MessageBoxImage image;

         switch(message.Buttons)
         {
            case Buttons.OkCancel:
               button = MessageBoxButton.OKCancel;
               break;
            case Buttons.YesNo:
               button = MessageBoxButton.YesNo;
               break;
            case Buttons.YesNoCancel:
               button = MessageBoxButton.YesNoCancel;
               break;
            default:
               button = MessageBoxButton.OK;
               break;
         }

         switch(message.ImageType)
         {
            case ImageType.Information:
               image = MessageBoxImage.Information;
               break;
            case ImageType.Warnung:
               image = MessageBoxImage.Warning;
               break;
            case ImageType.Fehler:
               image = MessageBoxImage.Error;
               break;
            default:
               image = MessageBoxImage.None;
               break;
         }

         var result = Dispatcher.Invoke(() => MessageBox.Show(this, message.Message, message.Header, button, image));
      }

      /// <summary>
      /// Wird aufgerufen, wenn im Programm eine Ausnahme abgefangen wird, und zeigt eine entsprechende Meldung.
      /// </summary>
      /// <param name="message"></param>
      private void OnErroReceived(NotifyUserMessage message)
      { 
         MessageBox.Show(this, message.Message, message.Header, MessageBoxButton.OK, MessageBoxImage.Error);
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
