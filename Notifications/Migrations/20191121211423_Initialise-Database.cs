using Microsoft.EntityFrameworkCore.Migrations;

namespace Notifications.Migrations
{
    public partial class InitialiseDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationSetting",
                columns: table => new
                {
                    Uid = table.Column<string>(nullable: false),
                    Daily = table.Column<bool>(nullable: false),
                    Mentions = table.Column<bool>(nullable: false),
                    Replies = table.Column<bool>(nullable: false),
                    NotificationInterval = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationSetting", x => x.Uid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationSetting");
        }
    }
}
