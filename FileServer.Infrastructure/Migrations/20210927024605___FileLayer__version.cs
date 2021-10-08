using Microsoft.EntityFrameworkCore.Migrations;

namespace FileServer.Infrastructure.Migrations
{
    public partial class __FileLayer__version : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FileInformation_FileLayer",
                schema: "dbo",
                table: "FileInfo",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileInformation_FileLayer",
                schema: "dbo",
                table: "FileInfo");
        }
    }
}
