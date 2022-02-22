using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeTechRepair.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Addresses");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "SupportTickets",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "SupportTickets",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "SupportTickets");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "SupportTickets");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Addresses",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Addresses",
                type: "float",
                nullable: true);
        }
    }
}
