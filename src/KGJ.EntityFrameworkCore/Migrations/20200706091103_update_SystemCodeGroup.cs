using Microsoft.EntityFrameworkCore.Migrations;

namespace KGJ.Migrations
{
    public partial class update_SystemCodeGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "SystemCodes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "SystemCodeGroups",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "SystemCodes");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "SystemCodeGroups");
        }
    }
}
