using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace infodiorama.Models
{
    public class EntityField
    {
        public string Name { get; set; }
        public string Table { get; set; }
        public string Caption { get; set; }
        public string Type { get; set; }
        public int GroupOfComps { get; set; }
        public int LengthInDb { get; set; }
        public int LengthInUi { get; set; }
        public int RequiredOrSuggested { get; set; }
        public int VisibleOrEditable { get; set; }
        public int LookupType { get; set; }
        public string LookupName { get; set; }
        public string InitialValue { get; set; }
    }
}
