using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Notifications.Migrations
{
    public partial class Addedlastupdatedfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "lastUpdated",
                table: "NotificationSetting",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lastUpdated",
                table: "NotificationSetting");
        }
    }
}
