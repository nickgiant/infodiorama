using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace infodiorama.Models
{
    public class ReadOnlyRecord
    {

        public string ColumnName { get; set; }
        public string ColumnTable { get; set; }
        public string ColumnCaption { get; set; }
        public string ColumnType { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public object Value { get; set; }

    }
}
