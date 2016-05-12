using FluentNHibernate.Mapping;
using FWA.Logic.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWA.Logic.Mappings
{
    public class UserMapping : ClassMap<User> 
    {
        public UserMapping()
        {
            Id(x => x.ID).Column("UserID").GeneratedBy.Native();
            Map(x => x.Name).Column("UserName");
            Map(x => x.EMail).Column("UserEMail");
            Map(x => x.Hash).Column("UserPwHash");
            Map(x => x.Salt).Column("UserSalt");
            Map(x => x.AccountType).Column("AccountTypeID").CustomType<int>();
        }
    }
}
