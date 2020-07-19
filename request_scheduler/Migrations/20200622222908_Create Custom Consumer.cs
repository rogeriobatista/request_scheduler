using Microsoft.EntityFrameworkCore.Migrations;

namespace request_scheduler.Migrations
{
    public partial class CreateCustomConsumer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Headers",
                table: "MauticFormRequests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Headers",
                table: "MauticFormRequests");
        }
    }
}
