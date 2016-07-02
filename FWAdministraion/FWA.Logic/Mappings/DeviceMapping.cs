using FluentNHibernate.Mapping;
using FWA.Logic.Storage;

namespace FWA.Logic.Mappings
{
    /// <summary>
    /// Wird von NHibernate zum Zugriff auf die Datenbank 'Device' benötigt. Sollte vom Nutzer nicht verwendet werden
    /// </summary>
    public class DeviceMapping : ClassMap<Device>
    {
        /// <summary>
        /// Ich bin ein Konstruktor
        /// </summary>
        public DeviceMapping()
        {
            Id(x => x.ID).Column("DeviceID").GeneratedBy.Native();
            Map(x => x.Name).Column("DeviceName");
            Map(x => x.InvNumber).Column("DeviceInvNumber");
            Map(x => x.AnnualChecks).Column("DeviceAnnualChecks");
            Map(x => x.NeedsCheckcard).Column("DeviceNeedsCheckcard");
            Map(x => x.Januray).Column("DeviceJanuary");
            Map(x => x.February).Column("DeviceFebruary");
            Map(x => x.March).Column("DeviceMarch");
            Map(x => x.April).Column("DeviceApril");
            Map(x => x.May).Column("DeviceMay");
            Map(x => x.June).Column("DeviceJune");
            Map(x => x.July).Column("DeviceJuly");
            Map(x => x.August).Column("DeviceAugust");
            Map(x => x.September).Column("DeviceSeptember");
            Map(x => x.October).Column("DeviceOctober");
            Map(x => x.November).Column("DeviceNovember");
            Map(x => x.December).Column("DeviceDecember");
            Map(x => x.KindOfCheck).Column("DeviceKindOfCheck");
            Map(x => x.Comment).Column("DeviceComment");
        }
    }
}
