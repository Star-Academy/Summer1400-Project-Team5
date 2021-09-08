using Microsoft.EntityFrameworkCore.Migrations;

namespace Talent.Migrations
{
    public partial class DataSource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tableName",
                table: "TempDataSources",
                newName: "TableName");

            migrationBuilder.AddColumn<string>(
                name: "DatabaseName",
                table: "TempDataSources",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DatabaseName",
                table: "DataSources",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatabaseName",
                table: "TempDataSources");

            migrationBuilder.DropColumn(
                name: "DatabaseName",
                table: "DataSources");

            migrationBuilder.RenameColumn(
                name: "TableName",
                table: "TempDataSources",
                newName: "tableName");
        }
    }
}
