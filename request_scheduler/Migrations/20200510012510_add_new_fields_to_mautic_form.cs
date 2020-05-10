using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace request_scheduler.Migrations
{
    public partial class add_new_fields_to_mautic_form : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "MauticFormRequests");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "MauticFormRequests",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DestinyAddress",
                table: "MauticFormRequests",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "MauticFormRequests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "MauticFormRequests");

            migrationBuilder.DropColumn(
                name: "DestinyAddress",
                table: "MauticFormRequests");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "MauticFormRequests");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "MauticFormRequests",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
