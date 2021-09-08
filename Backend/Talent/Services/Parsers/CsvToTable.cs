using System.Collections.Generic;
using System.Data;
using System.IO;
using Talent.Models;
using Talent.Services.Interfaces;

namespace Talent.Services.Parsers
{
    public class CsvToTable : ICsvToTable
    {
        public DataTable ConvertCsvToDataTable(CsvFile csvFile)
        {
            var dataTable = new DataTable();
            using var streamReader = new StreamReader(csvFile.FormFile.OpenReadStream());
            var firstRow = streamReader.ReadLine()?.Split(csvFile.Delimiter);
            var headers = ExtractHeaders(firstRow, csvFile.HasHeader);
            AddColumns(dataTable, headers);
            while (!streamReader.EndOfStream)
            {
                var rows = streamReader.ReadLine()?.Split(csvFile.Delimiter);
                AddRows(dataTable, rows, firstRow.Length);
            }
            return dataTable;
        }
        
        private string[] ExtractHeaders(string[] firstRow, bool hasHeader)
        {
            var headers = new List<string>();
            if (hasHeader)
            {
                return firstRow;
            }
            for (var i = 0; i < firstRow.Length; i++)
            {
                headers.Add($"field-{i}");
            }
            return headers.ToArray();
        }

        private void AddColumns(DataTable dataTable, string[] headers)
        {
            foreach (var header in headers)
            {
                dataTable.Columns.Add(header);
            }
        }

        private void AddRows(DataTable dataTable, string[] rows, int rowLength)
        {
            var dataRow = dataTable.NewRow();
            for (var i = 0; i < rowLength; i++)
            {
                dataRow[i] = rows[i];
            }
            dataTable.Rows.Add(dataRow);
        }
    }
}