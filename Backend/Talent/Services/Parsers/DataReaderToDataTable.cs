using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Talent.Services.Parsers
{
    public class DataReaderToDataTable
    {
        public DataTable ConvertToDataTable(SqlDataReader dataReader, string tableName)
        {
            var resultTable = new DataTable();
            for (var i = 0; i < dataReader.FieldCount; i++)
                resultTable.Columns.Add(dataReader.GetName(i));
            while (dataReader.Read())
            {
                var newRow = resultTable.NewRow();
                for (var i = 0; i < dataReader.FieldCount; i++)
                    newRow[i] = dataReader[dataReader.GetName(i)];
                resultTable.Rows.Add(newRow);
            }
            resultTable.TableName = tableName;
            return resultTable;
        }
    }
}