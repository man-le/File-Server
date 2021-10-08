using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileServer.Infrastructure.Migrations
{
    public partial class __initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "FileInfo",
                schema: "dbo",
                columns: table => new
                {
                    FileID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FileInformation_FileName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FileInformation_ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FileInformation_ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FileInformation_FileLocation = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RootFolderTenantID = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileInfo", x => x.FileID);
                    table.ForeignKey(
                        name: "FK_FileInfo_FileInfo_RootFolderTenantID",
                        column: x => x.RootFolderTenantID,
                        principalSchema: "dbo",
                        principalTable: "FileInfo",
                        principalColumn: "FileID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileInfo_RootFolderTenantID",
                schema: "dbo",
                table: "FileInfo",
                column: "RootFolderTenantID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileInfo",
                schema: "dbo");
        }
    }
}
