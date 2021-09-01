using System;
using System.Collections.Generic;
using System.IO;
using LumenWorks.Framework.IO.Csv;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Talent.Services.Interfaces;

namespace Talent.Services.Parsers
{
    public class CsvParser : IParser
    {
        private IFormFile _formFile;
        private bool _hasHeader;
        private char _delimiter;

        private const string DefaultColumnName = "field";

        public CsvParser(IFormFile formFile, bool hasHeader, char delimiter)
        {
            _formFile = formFile;
            _hasHeader = hasHeader;
            _delimiter = delimiter;
        }

        public Table ConvertToTable(Database database)
        {
            using (var csvReader = new CsvReader(new StreamReader(_formFile.OpenReadStream()), _hasHeader))
            {
                var headers = ExtractHeaders(csvReader);
                csvReader.ReadNextRecord();

                while (csvReader.ReadNextRecord())
                {

                }
            }
            throw new NotImplementedException();
        }

        private IEnumerable<string> ExtractHeaders(CsvReader csvReader)
        {
            if (_hasHeader)
                return csvReader.GetFieldHeaders();
            var headers = new List<string>();
            for (int i = 0; i < csvReader.FieldCount; i++)
                headers.Add($"{DefaultColumnName}{i}");
            return headers.ToArray();
        }
    }
}