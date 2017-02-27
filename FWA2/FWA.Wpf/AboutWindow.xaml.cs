namespace FWA.Wpf
{
   /// <summary>
   /// Interaktionslogik für AboutWindow.xaml
   /// </summary>
   public partial class AboutWindow
   {
      public AboutWindow()
      {
         InitializeComponent();
      }

      private void Hyperlink_OnRequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
      {
         Core.Helpers.Http.OpenUrl(e.Uri);
      }

      private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
      {
         DialogResult = true;
         Close();
      }
   }
}
