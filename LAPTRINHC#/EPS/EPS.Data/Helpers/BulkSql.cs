using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Data.Helpers
{
    public class BulkSql
    {
        private static IConfiguration configuration;
        private string _connString;

        static BulkSql()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configuration = builder.Build();
        }

        public BulkSql(string connName = "eHRM")
        {
            _connString = configuration.GetConnectionString(connName);
        }

        public void InsertData<T>(List<T> list, string tableName)
        {
            DataTable dt = new DataTable("MyTable");
            dt = ToDataTable(list);

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                using (SqlCommand command = new SqlCommand("", conn))
                {
                    try
                    {
                        conn.Open();

                        //Bulk insert into table
                        using (SqlBulkCopy bulkcopy = new SqlBulkCopy(conn))
                        {
                            bulkcopy.BulkCopyTimeout = 1800;
                            bulkcopy.DestinationTableName = tableName;
                            bulkcopy.WriteToServer(dt);
                            bulkcopy.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                        // Handle exception properly
                        throw ex;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        public void UpdateData<T>(List<T> list, string tableName, string pkName)
        {
            DataTable dt = new DataTable("MyTable");
            dt = ToDataTable(list);
            dt.PrimaryKey = new DataColumn[] { dt.Columns["pkName"] };
            var updateColumns = dt.Columns.OfType<DataColumn>().Where(x => x.ColumnName != pkName).Select(x => x.ColumnName);

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                using (SqlCommand command = new SqlCommand("", conn))
                {
                    try
                    {
                        conn.Open();

                        //Creating temp table on database
                        command.CommandText = SqlTableCreator.GetCreateFromDataTableSQL("#BulkTempTable", dt);
                        command.ExecuteNonQuery();

                        //Bulk insert into temp table
                        using (SqlBulkCopy bulkcopy = new SqlBulkCopy(conn))
                        {
                            bulkcopy.BulkCopyTimeout = 1800;
                            bulkcopy.DestinationTableName = "#BulkTempTable";
                            bulkcopy.WriteToServer(dt);
                            bulkcopy.Close();
                        }

                        // Updating destination table, and dropping temp table
                        command.CommandTimeout = 1800;
                        command.CommandText = "UPDATE T SET " + string.Join(",", updateColumns.Select(col => "T." + col + " = Temp." + col)) + " FROM " + tableName + " T INNER JOIN #BulkTempTable Temp ON T." + pkName + " = Temp." + pkName + "; DROP TABLE #BulkTempTable;";
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Handle exception properly
                        throw ex;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        public void DeleteData(List<int> listKey, string tableName, string keyName)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                using (SqlCommand command = new SqlCommand("", conn))
                {
                    try
                    {
                        conn.Open();

                        command.CommandTimeout = 1800;
                        command.CommandText = @"
WITH _IDS_ AS (
SELECT * FROM (VALUES " + string.Join(",", listKey.Select(x => "(" + x + ")")) + @") AS _IDS_ (Id)
)
DELETE T1
    FROM  " + tableName + @" AS T1
    INNER JOIN _IDS_ AS T2 ON T1." + keyName + @" = T2.Id
";
                        command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        // Handle exception properly
                        throw ex;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        private DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dt = new DataTable();
            for (int i = 0; i < properties.Count; i++)
            {
                PropertyDescriptor property = properties[i];
                var propertyType = Nullable.GetUnderlyingType(
                property.PropertyType) ?? property.PropertyType;
                if (propertyType.IsPrimitive || propertyType == typeof(DateTime) || propertyType == typeof(string) || propertyType == typeof(decimal))
                {
                    dt.Columns.Add(property.Name, propertyType);
                }
            }
            object[] values = new object[dt.Columns.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    values[i] = properties[dt.Columns[i].ColumnName].GetValue(item);
                }
                dt.Rows.Add(values);
            }
            return dt;
        }
    }
}
