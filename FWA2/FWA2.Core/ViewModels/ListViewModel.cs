using FWA2.Core.Helpers;
using FWA2.Core.Models;
using System.Collections.Generic;

namespace FWA2.Core.ViewModels
{
   public class ListViewModel
   {
      public List<Gegenstand> Gegenstaende { get; private set; }

      public ListViewModel()
      {
         DBAuthentication.Create("hs", System.Text.Encoding.UTF8.GetBytes("Vivendi2016"));
         
         Gegenstaende = DBAuthentication.Instance.GetAlleGegenstaende();
      }
   }
}
