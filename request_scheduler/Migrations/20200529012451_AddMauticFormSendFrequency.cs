using Microsoft.EntityFrameworkCore.Migrations;

namespace request_scheduler.Migrations
{
    public partial class AddMauticFormSendFrequency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SendFrequency",
                table: "MauticFormRequests",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SendFrequency",
                table: "MauticFormRequests");
        }
    }
}
