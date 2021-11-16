using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SGSWeb.EF.Migrations
{
    public partial class BounsFirstMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "depTbls",
                columns: table => new
                {
                    DepID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepNAme = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_depTbls", x => x.DepID);
                });

            migrationBuilder.CreateTable(
                name: "bonusTbls",
                columns: table => new
                {
                    EmpID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    JDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sarlary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Performanse = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DepID = table.Column<int>(type: "int", nullable: false),
                    depTblDepID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bonusTbls", x => x.EmpID);
                    table.ForeignKey(
                        name: "FK_bonusTbls_depTbls_depTblDepID",
                        column: x => x.depTblDepID,
                        principalTable: "depTbls",
                        principalColumn: "DepID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bonusTbls_depTblDepID",
                table: "bonusTbls",
                column: "depTblDepID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bonusTbls");

            migrationBuilder.DropTable(
                name: "depTbls");
        }
    }
}
