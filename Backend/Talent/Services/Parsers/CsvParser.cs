using System;
using System.Collections.Generic;
using System.IO;
using LumenWorks.Framework.IO.Csv;
using Microsoft.SqlServer.Management.Smo;
using Talent.Models;
using Talent.Services.Interfaces;

namespace Talent.Services.Parsers
{
    public class CsvParser : IParser
    {
        private readonly CsvFile _csvFile;
        private readonly SqlMiddleware _sqlMiddleware;

        private const string DefaultColumnName = "field";
        private readonly DataType _defaultDataType = DataType.Int;

        public CsvParser(CsvFile csvFile, SqlMiddleware sqlMiddleware)
        {
            _csvFile = csvFile;
            _sqlMiddleware = sqlMiddleware;
        }

        public Table ConvertToTable(Database database, string tableName)
        {
            var newTable = new Table(database, tableName);
            using (var csvReader = new CsvReader(new StreamReader(_csvFile.FormFile.OpenReadStream()), _csvFile.HasHeader))
            {
                InitializeTable(newTable, csvReader);
                while (csvReader.ReadNextRecord())
                    AddNewRecordToTable(newTable, csvReader);
            }
            newTable.Alter();
            return newTable;
        }

        private string[] ExtractHeaders(CsvReader csvReader)
        {
            if (_csvFile.HasHeader)
                return csvReader.GetFieldHeaders();
            var headers = new List<string>();
            for (var i = 0; i < csvReader.FieldCount; i++)
                headers.Add($"{DefaultColumnName}-{i}");
            return headers.ToArray();
        }

        private void InitializeTable(Table table, CsvReader csvReader)
        {
            var headers = ExtractHeaders(csvReader);
            var isFileEmpty = !csvReader.ReadNextRecord();
            for (var i = 0; i < csvReader.FieldCount; i++)
            {
                Column newColumn;
                newColumn = isFileEmpty ? new Column(table, headers[i], _defaultDataType) : _sqlMiddleware.GetColumnByInstance(table, headers[i], csvReader[i]);
                table.Columns.Add(newColumn);
            }
        }

        private void AddNewRecordToTable(Table table, CsvReader csvReader)
        {
            
        }
    }
}