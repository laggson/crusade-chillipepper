using FluentNHibernate.Mapping;

namespace FWA.Core.Models.Mappings
{
   public class GegenstandMapping : ClassMap<Gegenstand>
   {
      public GegenstandMapping()
      {
         Id(x => x.Id).Column("Id").GeneratedBy.Identity();
         Map(x => x.Bezeichnung).Column("Name");
         Map(x => x.InvNummer).Column("InvNummer");
         Map(x => x.BrauchtPruefkarte).Column("Pruefkarte");
         Map(x => x.ArtDerPruefung).Column("Art");
         Map(x => x.Kommentar).Column("Kommentar");
         References(x => x.Zeitraum).Fetch.Join();
      }
   }
}
