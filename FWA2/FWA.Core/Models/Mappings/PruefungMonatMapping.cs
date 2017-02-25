using FluentNHibernate.Mapping;

namespace FWA.Core.Models.Mappings
{
   public class ZeitraumMapping : ClassMap<Zeitraum>
   {
      public ZeitraumMapping()
      {
         Id(x => x.Id).Column("Id").GeneratedBy.Identity();
         Map(x => x.Januar).Column("Januar");
         Map(x => x.Februar).Column("Februar");
         Map(x => x.Maerz).Column("Maerz");
         Map(x => x.April).Column("April");
         Map(x => x.Mai).Column("Mai");
         Map(x => x.Juni).Column("Juni");
         Map(x => x.Juli).Column("Juli");
         Map(x => x.August).Column("August");
         Map(x => x.September).Column("September");
         Map(x => x.Oktober).Column("Oktober");
         Map(x => x.November).Column("November");
         Map(x => x.Dezember).Column("Dezember");
      }
   }
}
