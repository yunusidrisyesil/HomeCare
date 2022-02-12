using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeTechRepair.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Appointments_SupportTicketId",
                table: "Appointments");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_SupportTicketId",
                table: "Appointments",
                column: "SupportTicketId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Appointments_SupportTicketId",
                table: "Appointments");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_SupportTicketId",
                table: "Appointments",
                column: "SupportTicketId");
        }
    }
}
