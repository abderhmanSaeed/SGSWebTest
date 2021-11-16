using Microsoft.EntityFrameworkCore.Migrations;

namespace SGSWeb.EF.Migrations
{
    public partial class BounsTwoMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Bonus",
                table: "bonusTbls",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bonus",
                table: "bonusTbls");
        }
    }
}
