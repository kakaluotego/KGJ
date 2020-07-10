using Microsoft.EntityFrameworkCore.Migrations;

namespace KGJ.Migrations
{
    public partial class update_productCustomField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "ProductCustomFields",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "ProductCustomFields");
        }
    }
}
