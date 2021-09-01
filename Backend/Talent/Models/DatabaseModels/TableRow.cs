using System.Collections.Generic;

namespace Talent.Models.DatabaseModels
{
    public class TableRow
    {
        public List<TableRecord> records;

        private const string DelimiterString = ", ";

        public TableRow()
        {
            records = new List<TableRecord>();
        }

        public void AddRecord(TableRecord record)
        {
            records.Add(record);
        }

        public override string ToString()
        {
            var resultString = "";
            for (var i = 0; i < records.Count; i++)
            {
                resultString += records[i].ToString();
                if (i != records.Count - 1)
                    resultString += DelimiterString;
            }
            return $"({resultString})";
        }
    }
}