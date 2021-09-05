using System.Text;
using Microsoft.Data.SqlClient;
using Talent.Services.Interfaces;

namespace Talent.Models
{
    public class CsvDownloader : ICsvDownloader
    {
        private readonly ISqlHandler _sqlHandler;

        public CsvDownloader(ISqlHandler sqlHandler)
        {
            _sqlHandler = sqlHandler;
        }
        public string DownloadCsv(SqlConnection connection, string tableName, CsvFile csvFile)
        {
            var query = $"Select * from {tableName}";
            var dataReader = _sqlHandler.ExecuteReader(connection, query);
            var csvString = WriteToFile(dataReader, csvFile);
            dataReader.Close();
            _sqlHandler.CloseConnection(connection);
            return csvString;
        }

        private string WriteToFile(SqlDataReader dataReader, CsvFile csvFile)
        {
            var output = new object[dataReader.FieldCount];
            var result = new StringBuilder();
            if (csvFile.HasHeader)
            {
                result.Append(WriteHeaders(dataReader, csvFile.Delimiter));
            }
            while (dataReader.Read())
            {
                dataReader.GetValues(output);
                result.Append(string.Join(csvFile.Delimiter, output));
                result.Append('\n');
            }
            return result.ToString();
        }

        private string WriteHeaders(SqlDataReader dataReader, string delimiter)
        {
            var output = new object[dataReader.FieldCount];
            for (var i = 0; i < dataReader.FieldCount; i++)
            {
                output[i] = dataReader.GetName(i);
            }
            return string.Join(delimiter, output) + '\n';
        }
    }
}