using Microsoft.EntityFrameworkCore.Migrations;

namespace request_scheduler.Migrations
{
    public partial class RemoveSendFrequencyFromMauticForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SendFrequency",
                table: "MauticFormRequests");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SendFrequency",
                table: "MauticFormRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
