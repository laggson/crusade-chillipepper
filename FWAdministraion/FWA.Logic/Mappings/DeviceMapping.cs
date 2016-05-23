using FluentNHibernate.Mapping;
using FWA.Logic.Storage;

namespace FWA.Logic.Mappings
{
    public class DeviceMapping : ClassMap<Device>
    {
        public DeviceMapping()
        {
            Id(x => x.ID).Column("DeviceID").GeneratedBy.Native();
            Map(x => x.Name).Column("DeviceName");
            Map(x => x.InvNumber).Column("DeviceInvNumber");
            Map(x => x.AnnualChecks).Column("DeviceAnnualChecks");
            //Map(x => x.MonthsToCheck).Column("DeviceMonthsToCheck");
            Map(x => x.NeedsCheckcard).Column("DeviceNeedsCheckcard");
            Map(x => x.KindOfCheck).Column("DeviceKindOfCheck");
            Map(x => x.Comment).Column("DeviceComment");
        }
    }
}
