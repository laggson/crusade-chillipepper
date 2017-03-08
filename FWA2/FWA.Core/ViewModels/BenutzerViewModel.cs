using FWA.Core.Helpers;
using FWA.Core.Models;
using FWA.Core.Mvvm;
using System;
using System.Linq;
using System.Security.Authentication;

namespace FWA.Core.ViewModels
{
   public class BenutzerViewModel : ObservableObject
   {
      #region Properties
      private bool nutzerEnabled = true;
      public bool NutzerEnabled
      {
         get { return nutzerEnabled; }
         set
         {
            nutzerEnabled = value;
            NotifyPropertyChanged(nameof(NutzerEnabled));
         }
      }

      private string[] accountTypes;
      public string[] AccountTypes
      {
         get { return accountTypes; }
         set
         {
            accountTypes = value;
            NotifyPropertyChanged(nameof(AccountTypes));
         }
      }

      private string selectedAccountType;

      public string SelectedAccountType
      {
         get { return selectedAccountType; }
         set
         {
            selectedAccountType = value;
            NotifyPropertyChanged(nameof(SelectedAccountType));
         }
      }

      #endregion

      public BenutzerViewModel()
      {
         AccountTypes = Enum.GetValues(typeof(AccountType)).Cast<AccountType>().Select(z => z.ToString()).ToArray();
      }

      public void CreateUser(string name, string mail, string pass1, string pass2)
      {
         if (pass1 != pass2)
            throw new InvalidCredentialException("Die angegebenen Passwörter stimmen nicht überein.");

         DBAuthentication.Instance.CreateOrAlterUser(name, mail, pass1);
      }
   }
}
