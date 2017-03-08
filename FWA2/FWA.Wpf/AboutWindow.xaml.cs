namespace FWA.Wpf
{
   /// <summary>
   /// Stellt das About-Fenster dar.
   /// </summary>
   public partial class AboutWindow
   {
      /// <summary>
      /// Erstellt eine neue Instanz der <see cref="AboutWindow"/>-Klasse.
      /// </summary>
      public AboutWindow()
      {
         InitializeComponent();
      }

      /// <summary>
      /// Wird aufgerufen, wenn einer der Hyperlinks gedrückt wird und öffnet den Browser mit der Url.
      /// </summary>
      private void Hyperlink_OnRequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
      {
         Core.Helpers.Http.OpenUrl(e.Uri);
      }

      /// <summary>
      /// Wird aufgerufen, wenn der "Schließen"-Knopf gedrückt wird.
      /// </summary>
      private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
      {
         DialogResult = true;
         Close();
      }
   }
}
