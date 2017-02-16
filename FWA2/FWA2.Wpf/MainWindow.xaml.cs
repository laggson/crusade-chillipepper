using FWA.Logic;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows;

namespace FWA2.Wpf
{
   /// <summary>
   /// Interaktionslogik für MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      public MainWindow()
      {
         InitializeComponent();

         //ShowCsvDlg();
         UserTest();
      }

      private void UserTest()
      {
         var bytes = System.Text.Encoding.UTF8.GetBytes("Vivendi2016");

         //DBAuthentication.CreateNewUser("hs", "hermann.schmidt24@freenet.de", bytes);
         DBAuthentication.Create("hs", bytes);
      }

      private void ShowCsvDlg()
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
      }
   }
}
