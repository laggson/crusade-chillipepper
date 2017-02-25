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
   }
}
