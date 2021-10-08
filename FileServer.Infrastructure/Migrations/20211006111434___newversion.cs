using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileServer.Infrastructure.Migrations
{
    public partial class __newversion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileInformation_FileLayer",
                schema: "dbo",
                table: "FileInfo");

            migrationBuilder.AddColumn<string>(
                name: "FileInformation_CreatedBy",
                schema: "dbo",
                table: "FileInfo",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FileInformation_CreatedDate",
                schema: "dbo",
                table: "FileInfo",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileInformation_CreatedBy",
                schema: "dbo",
                table: "FileInfo");

            migrationBuilder.DropColumn(
                name: "FileInformation_CreatedDate",
                schema: "dbo",
                table: "FileInfo");

            migrationBuilder.AddColumn<int>(
                name: "FileInformation_FileLayer",
                schema: "dbo",
                table: "FileInfo",
                type: "int",
                nullable: true);
        }
    }
}
