using FluentNHibernate.Mapping;
using FWA.Logic.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWA.Logic.Mappings
{
    public class CheckMapping : ClassMap<Check>
    {
        public CheckMapping()
        {
            Id(x => x.ID).Column("CheckID").GeneratedBy.Native();
            Map(x => x.Name).Column("CheckName");
            Map(x => x.InvNumber).Column("CheckInvNumber");
            Map(x => x.StringDate).Column("CheckDate");
            Map(x => x.WhoCheckedID).Column("DeviceUserID");
            Map(x => x.CheckType).Column("CheckTypeID");
            Map(x => x.Lack).Column("DeviceLack");
            Map(x => x.Comment).Column("DeviceComment");
        }
    }
}
