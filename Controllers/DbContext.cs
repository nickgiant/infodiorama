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

        //string connectionString = Configuration["ConnectionStrings:DefaultConnection"]; //this.Configuration.GetConnectionString("DefaultConnection");  // 
        
        public List<EntityField> listFieldAccommodations = new List<EntityField>()
            {
                new EntityField {Name="id",Table="Accommodations",Caption="id",Type="",GroupOfComps=0,LengthInDb=20,LengthInUi=10, RequiredOrSuggested=1,VisibleOrEditable=1, LookupType=1,LookupName="",InitialValue="ini"},
                new EntityField {Name="title",Table="Accommodations",Caption="ονομασία",Type="",GroupOfComps=0,LengthInDb=20,LengthInUi=10, RequiredOrSuggested=1,VisibleOrEditable=1, LookupType=1,LookupName="",InitialValue="title"},
                new EntityField {Name="adress",Table="Accommodations",Caption="address",Type="",GroupOfComps=0,LengthInDb=20,LengthInUi=10, RequiredOrSuggested=1,VisibleOrEditable=1, LookupType=1,LookupName="",InitialValue=""}
            };
        EntityView entityView = null;


        public DbContext()
        {

             entityView = new EntityView("accommodations", "οικίες", "Accommodations", "SELECT * FROM Accommodations", "", listFieldAccommodations);
        }

        public EntityView getEntityView()
        {
            return entityView;
        }
            public List<ReadOnlyRecord[]> GetRecords()
        {
            string connectionString = "Server=LAPTOP-6K7LN3AO\\SQLEXPRESS2017;Database=CozySmart;Trusted_Connection=True;MultipleActiveResultSets=true";
            // string connectionString = Configuration["ConnectionStrings:DefaultConnection"]; //this.Configuration.GetConnectionString("DefaultConnection");  // 
            // Console.WriteLine(" connectionString:{0}", connectionString);
            List<ReadOnlyRecord[]> listRecord = new List<ReadOnlyRecord[]>();

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
                    ReadOnlyRecord[] rec = new ReadOnlyRecord[colcount];
                    for (int c = 0; c < colcount; c++)   //while(reader.Read())
                    {
                    

                        Console.WriteLine("   rowcount:{0}  colcount{1}  GetName{2} c{3} r{4}", schemaTable.Rows.Count, colcount, reader.GetName(c), c,r);

                        rec[c] = new ReadOnlyRecord();
                        rec[c].Column = Convert.ToInt32(c);
                        //string s = Convert.ToString(reader.GetName(c));
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



               /* for (int r = 0; r < schemaTable.Rows.Count; r++)   //while(reader.Read())
                {
                    
                    Console.WriteLine("   rowcount:{0}", schemaTable.Rows.Count);
                    ReadOnlyRecord[] rec = new ReadOnlyRecord[colcount];
                    for (int c = 0; c < colcount; c++)
                    {
                        
                        Console.WriteLine("   rowcount:{0}  colcount{1}  GetName{2} c{3} r{4}", schemaTable.Rows.Count, colcount, reader.GetName(c), c,r);
                        rec[c].Value = reader.GetString(c);
                    }
                    listRecord.Add(rec);
                }*/

                connection.Close();
            }
            
        
            


            return listRecord;
    }



}
}
