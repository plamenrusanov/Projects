using Microsoft.EntityFrameworkCore.Migrations;

namespace Eventures.Data.Migrations
{
    public partial class NewTicketVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Row",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "Area",
                table: "Tickets",
                newName: "EventId");

            migrationBuilder.AlterColumn<string>(
                name: "EventId",
                table: "Tickets",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Adult",
                table: "Tickets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Child",
                table: "Tickets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_EventId",
                table: "Tickets",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Events_EventId",
                table: "Tickets",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Events_EventId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_EventId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Adult",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Child",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Tickets",
                newName: "Area");

            migrationBuilder.AlterColumn<string>(
                name: "Area",
                table: "Tickets",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Row",
                table: "Tickets",
                nullable: true);
        }
    }
}
