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
            Id(x => x.ID).GeneratedBy.Native();
            Map(x => x.DeviceID);
            Map(x => x.TesterID);
            Map(x => x.DateChecked);
            Map(x => x.CheckType);
            Map(x => x.Lack);
            Map(x => x.Comment);
        }
    }
}
