using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OTS.Infrastructure.Migrations
{
    public partial class AddedColumnTicketNoinTicketTableandRenmamecolumnnlastUpdatedDateofConversationTabletoLastUpdatedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lastUpdatedDate",
                table: "Conversation",
                newName: "LastUpdatedDate");

            migrationBuilder.AddColumn<string>(
                name: "TicketNo",
                table: "Ticket",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketNo",
                table: "Ticket");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedDate",
                table: "Conversation",
                newName: "lastUpdatedDate");
        }
    }
}
