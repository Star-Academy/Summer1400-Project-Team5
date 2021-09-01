using Microsoft.SqlServer.Management.Smo;

namespace Talent.Models.DatabaseModels
{
    public class TableRecord
    {
        public string value { get; }
        public DataType valueType { get; }

        public TableRecord(string value, DataType valueType)
        {
            this.value = value;
            this.valueType = valueType;
        }

        public override string ToString()
        {
            return $"'{value}'";
        }
    }
}