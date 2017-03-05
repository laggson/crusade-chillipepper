namespace FWA.Wpf
{
   /// <summary>
   /// Interaktionslogik für PruefungsWindow.xaml
   /// </summary>
   public partial class PruefungWindow
   {
      public PruefungWindow()
      {
         InitializeComponent();
      }

      private void Abbrechen_Click(object sender, System.Windows.RoutedEventArgs e)
      {
         DialogResult = false;
         Close();
      }

      private void Fertig_Click(object sender, System.Windows.RoutedEventArgs e)
      {
         DialogResult = true;
         Close();
      }
   }
}