using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OTS.Infrastructure.Migrations
{
    public partial class AddedColumnStatusinTicketTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Ticket");
        }
    }
}
