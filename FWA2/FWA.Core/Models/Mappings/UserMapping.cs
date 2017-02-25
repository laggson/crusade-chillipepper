using FluentNHibernate.Mapping;

namespace FWA.Core.Models.Mappings
{
   public class UserMapping : ClassMap<User>
   {
      public UserMapping()
      {
         Id(x => x.Id).Column("Id").GeneratedBy.Identity();
         Map(x => x.Name).Column("Name");
         Map(x => x.EMail).Column("Email");
         Map(x => x.Hash).Column("PwHash");
         Map(x => x.Salt).Column("PwSalt");
         Map(x => x.AccountType).Column("Type").CustomType<int>();
      }
   }
}
