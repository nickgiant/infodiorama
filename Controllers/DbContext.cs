using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using infodiorama.Models;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace infodiorama.Controllers
{
    public class DbContext
    {
       readonly private string connectionString = "Server=LAPTOP-6K7LN3AO\\SQLEXPRESS2017;Database=hr;Trusted_Connection=True;MultipleActiveResultSets=true";
        //string connectionString = Configuration["ConnectionStrings:DefaultConnection"]; //this.Configuration.GetConnectionString("DefaultConnection");  // 

        public List<EntityField> listFieldAccommodations = new List<EntityField>()
            {
                new EntityField {Name="id",Table="talent",Caption="id",Type="",GroupOfComps=0,LengthInDb=20,LengthInUi=10, RequiredOrSuggested=1,VisibleOrEditable=1, LookupType=1,LookupName="",InitialValue="ini"},
                new EntityField {Name="title",Table="talent",Caption="ονομασία",Type="",GroupOfComps=0,LengthInDb=20,LengthInUi=10, RequiredOrSuggested=1,VisibleOrEditable=1, LookupType=1,LookupName="",InitialValue="title"},
                new EntityField {Name="address",Table="talent",Caption="address διευ",Type="",GroupOfComps=0,LengthInDb=20,LengthInUi=10, RequiredOrSuggested=1,VisibleOrEditable=1, LookupType=1,LookupName="",InitialValue=""},
                new EntityField {Name="description",Table="talent",Caption="περιγραφή",Type="",GroupOfComps=0,LengthInDb=20,LengthInUi=10, RequiredOrSuggested=1,VisibleOrEditable=1, LookupType=1,LookupName="",InitialValue=""},
                              
            };
        EntityView entityView = null;


        public DbContext()
        {

             entityView = new EntityView("talents", "erg", "talent", "SELECT * FROM talent", "", "talent.id", listFieldAccommodations,null);
        }

        public EntityView GetEntityView()
        {

            return entityView;
        }
        
        public void Update(string FieldString, int id)
        {
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlupdate = $"UPDATE {entityView.Table} SET {FieldString} WHERE {entityView.PrimaryKey} LIKE {id}";
                Console.WriteLine("****** sqlupdate" + sqlupdate);


                                SqlCommand command = new SqlCommand(sqlupdate, connection);
                               command.CommandType = CommandType.Text;
                                connection.Open();
                                command.ExecuteNonQuery();// error
                                connection.Close();
                
            }
        }

        public Record[] GetEntityViewWithRecords(int id)
        {
            Record[] rec = new Record[1];

            if (entityView.SqlViewWhere.Equals(""))
                {
                    entityView.SqlViewWhere = $"WHERE {entityView.PrimaryKey} LIKE {id}";
                }
                else
                {

                }

                entityView.SqlViewSelect = entityView.SqlViewSelect + " " + entityView.SqlViewWhere;
                rec = GetRecords().ToList()[0];
           
            
            return rec;
        }

        public List<Record[]> GetRecords()
        {
           
            // string connectionString = Configuration["ConnectionStrings:DefaultConnection"]; //this.Configuration.GetConnectionString("DefaultConnection");  // 
            // Console.WriteLine(" connectionString:{0}", connectionString);
            List<Record[]> listRecord = new List<Record[]>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string sqlread = entityView.SqlViewSelect;
                SqlCommand command = new SqlCommand(sqlread, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                DataTable schemaTable = reader.GetSchemaTable();
                //int colcount = schemaTable.Columns.Count;
                int colcount = reader.GetColumnSchema().Count;
               // Console.WriteLine("   colcount:{0}", colcount);



                int r = 0;
                while (reader.Read())
                {
                    Record[] rec = new Record[colcount];
                    for (int c = 0; c < colcount; c++)   //while(reader.Read())
                    {
                    

                        Console.WriteLine("   rowcount:{0}  colcount{1}  GetName{2} c{3} r{4}", schemaTable.Rows.Count, colcount, reader.GetName(c), c,r);

                        rec[c] = new Record();
                        rec[c].Column = Convert.ToInt32(c);
                        //string s = Convert.ToString(reader.GetName(c));
                        rec[c].ColumnCaption = reader.GetName(c) as string;
                        rec[c].ColumnName = reader.GetName(c) as string;
                        rec[c].ColumnType = reader.GetDataTypeName(c) as string;//.GetFieldType(c) as string; // GetDataTypeName(c);                        

                        if (rec[c].ColumnType.Equals("int"))
                        {
                            rec[c].Value = Convert.ToInt32(reader[reader.GetName(c)]);
                        }
                        else
                        {
                            rec[c].Value = reader[reader.GetName(c)] as string;
                        }



                    }
                    listRecord.Add(rec);
                    r++;

                }

                connection.Close();
            }

            return listRecord;
    }



}
}
