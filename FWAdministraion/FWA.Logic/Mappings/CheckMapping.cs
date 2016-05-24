using FluentNHibernate.Mapping;
using FWA.Logic.Storage;

namespace FWA.Logic.Mappings
{
    /// <summary>
    /// Wird von NHibernate zum Zugriff auf die Datenbank 'Check' benötigt. Sollte vom Nutzer nicht verwendet werden
    /// </summary>
    public class CheckMapping : ClassMap<Check>
    {
        /// <summary>
        /// Ich bin ein Konstruktor
        /// </summary>
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
