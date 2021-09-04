using System.IO;
using Microsoft.Data.SqlClient;
using Talent.Services.Interfaces;

namespace Talent.Models
{
    public class CsvLoader : ICsvLoader
    {
        private readonly ISqlHandler _sqlHandler;

        public CsvLoader(ISqlHandler sqlHandler)
        {
            _sqlHandler = sqlHandler;
        }
        public void LoadCsv(SqlConnection connection, string tableName, CsvFile csvFile)
        {
            var query = $"Select * from {tableName}";
            var dataReader = _sqlHandler.ExecuteReader(connection, query);
            var streamWriter = new StreamWriter(csvFile.FilePath);
            WriteToFile(dataReader, streamWriter, csvFile);
            streamWriter.Close();
            dataReader.Close();
            if (_sqlHandler.IsOpen(connection))
            {
                connection.Close();
            }
        }

        private void WriteToFile(SqlDataReader dataReader, StreamWriter streamWriter, CsvFile csvFile)
        {
            var output = new object[dataReader.FieldCount];
            if (csvFile.HasHeader)
            {
                WriteHeaders(dataReader, streamWriter, csvFile.Delimiter);
            }
            while (dataReader.Read())
            {
                dataReader.GetValues(output);
                streamWriter.WriteLine(string.Join(csvFile.Delimiter, output));
            }
        }

        private void WriteHeaders(SqlDataReader dataReader, StreamWriter streamWriter, char delimiter)
        {
            var output = new object[dataReader.FieldCount];
            for (var i = 0; i < dataReader.FieldCount; i++)
            {
                output[i] = dataReader.GetName(i);
            }
            streamWriter.WriteLine(string.Join(delimiter, output));
        }
    }
}