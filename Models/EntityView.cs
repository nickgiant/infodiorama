using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace infodiorama.Models
{
    public class EntityView
    {

        public EntityView(string NameIn, string CaptionIn, string TableIn, string SqlViewSelectIn, string SqlViewWhereIn, string PrimaryKeyIn,List<EntityField> FieldListIn, List<Record[]> RecordIn)
        {
            Name = NameIn;
            Caption = CaptionIn;
            Table = TableIn;
            SqlViewSelect = SqlViewSelectIn;
            SqlViewWhere = SqlViewWhereIn;
            PrimaryKey = PrimaryKeyIn;
            FieldList = FieldListIn;
            Record = RecordIn;
        }
        public string Name { get; set; }
        public string Caption { get; set; }
        public string Table { get; set; }
        public string SqlViewSelect { get; set; }
        public string SqlViewWhere { get; set; }
        public string PrimaryKey { get; set; }
        public List<EntityField> FieldList { get; set; }
        public List<Record[]> Record { get; set; }
    }
}
