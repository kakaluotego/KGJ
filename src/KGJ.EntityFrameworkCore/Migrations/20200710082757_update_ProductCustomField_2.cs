using Microsoft.EntityFrameworkCore.Migrations;

namespace KGJ.Migrations
{
    public partial class update_ProductCustomField_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomFieldName",
                table: "ProductCustomFields");

            migrationBuilder.AddColumn<bool>(
                name: "IsRequired",
                table: "ProductCustomFields",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRequired",
                table: "ProductCustomFields");

            migrationBuilder.AddColumn<string>(
                name: "CustomFieldName",
                table: "ProductCustomFields",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
