using FluentNHibernate.Mapping;

namespace FWA.Core.Models.Mappings
{
   public class PruefungMapping : ClassMap<Pruefung>
   {
      public PruefungMapping()
      {
         Id(x => x.Id).Column("Id").GeneratedBy.Identity();
         Map(x => x.Datum).Column("Datum");
         References(x => x.Gegenstand);
         Map(x => x.Kommentar).Column("Kommentar");
         Map(x => x.Mangel).Column("Mangel");
         References(x => x.Tester);
         Map(x => x.Zustand).Column("Zustand").CustomType<int>();
      }
   }
}
