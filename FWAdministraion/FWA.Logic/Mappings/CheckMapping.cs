using FluentNHibernate.Mapping;
using FWA.Logic.Storage;

namespace FWA.Logic.Mappings
{
    public class CheckMapping : ClassMap<Check>
    {
        public CheckMapping()
        {
            Id(x => x.ID).GeneratedBy.Native();
            References(x => x.Device);
            References(x => x.Tester);
            Map(x => x.DateChecked);
            Map(x => x.CheckType);
            Map(x => x.Lack);
            Map(x => x.Comment);
        }
    }
}
