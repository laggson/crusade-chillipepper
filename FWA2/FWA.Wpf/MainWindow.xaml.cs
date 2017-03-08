﻿using FWA.Core.Models;
using FWA.Core.Mvvm;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using System;

namespace FWA.Wpf
{
   /// <summary>
   /// Stellt das Hauptfenster der Anwendung dar.
   /// </summary>
   public sealed partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
         RegisterEvents();
      }

      /// <summary>
      /// Registriert sich beim MVVM-Messenger für die angegebenen Message-Typen.
      /// </summary>
      private void RegisterEvents()
      {
         Messenger.Default.Register<ErrorMessage>(this, OnErroReceived);
         Messenger.Default.Register<MessageboxMessage>(this, OnMessageboxInvoked);
         Messenger.Default.Register<RequestDialogOpenMessage>(this, OnDialogOpenRequest);
      }

      /// <summary>
      /// Wird aufgerufen, wenn im Programm eine Ausnahme abgefangen wird, und zeigt eine entsprechende Meldung.
      /// </summary>
      /// <param name="message"></param>
      private void OnErroReceived(NotifyUserMessage message)
      {
         MessageBox.Show(this, message.Message, message.Header, MessageBoxButton.OK, MessageBoxImage.Error);
      }

      /// <summary>
      /// Wird aufgerufen, wenn eine <see cref="MessageboxMessage"/> empfangen wird und zeigt eine entsprechende <see cref="MessageBox"/>.
      /// </summary>
      /// <param name="message"></param>
      private void OnMessageboxInvoked(MessageboxMessage message)
      {
         MessageBoxButton button;
         MessageBoxImage image;

         switch (message.Buttons)
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

         switch (message.ImageType)
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
         MyDialogResult dialogResult = MyDialogResult.Ok;

         switch (result)
         {
            case MessageBoxResult.Yes:
               dialogResult = MyDialogResult.Ja;
               break;
            case MessageBoxResult.No:
               dialogResult = MyDialogResult.Nein;
               break;
            case MessageBoxResult.Cancel:
               dialogResult = MyDialogResult.Abbrechen;
               break;
         }

         Messenger.Default.Send(new MessageboxResponseMessage(dialogResult));
      }

      /// <summary>
      /// Wird aufgerufen, wenn der Dialog eine Nachricht zum Öffnen eines neuen Dialogs empfängt.
      /// </summary>
      /// <param name="msg">Die Nachricht, die den betroffenen Dialog enthält.</param>
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
               var pruefungsWindow =  new PruefungWindow();
               pruefungsWindow.ViewModel.Init(msg.Data as Gegenstand);
               window = pruefungsWindow;
               break;
            case Dialog.BenutzerWindow:
               window = new BenutzerWindow();
               break;
            default:
               return;
         }

         window.Owner = this;
         window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
         window.Closed += (o, args) => { Application.Current.MainWindow.Focus(); };
         window.ShowDialog();
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

      private void BenutzerAnlegen_Click(object sender, RoutedEventArgs e)
      {
         OnDialogOpenRequest(new RequestDialogOpenMessage(Dialog.BenutzerWindow));
      }
   }
}
