namespace Talent.Services.Interfaces
{
    public interface ISqlToJson
    {
        string ConvertSqlTableToJson(string tableName, int rowCount);

        string ConvertSqlTableToJson(string tableName);
    }
}