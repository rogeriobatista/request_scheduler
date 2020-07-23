using Microsoft.EntityFrameworkCore.Migrations;

namespace request_scheduler.Migrations
{
    public partial class CreateScheduleConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CronId",
                table: "MauticFormRequests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CronId",
                table: "MauticFormRequests");
        }
    }
}
