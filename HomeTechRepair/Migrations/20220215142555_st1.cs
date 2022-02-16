using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeTechRepair.Migrations
{
    public partial class st1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SupportTicketId",
                table: "ReciptMasters",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ReciptMasters_SupportTicketId",
                table: "ReciptMasters",
                column: "SupportTicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReciptMasters_SupportTickets_SupportTicketId",
                table: "ReciptMasters",
                column: "SupportTicketId",
                principalTable: "SupportTickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReciptMasters_SupportTickets_SupportTicketId",
                table: "ReciptMasters");

            migrationBuilder.DropIndex(
                name: "IX_ReciptMasters_SupportTicketId",
                table: "ReciptMasters");

            migrationBuilder.DropColumn(
                name: "SupportTicketId",
                table: "ReciptMasters");
        }
    }
}
