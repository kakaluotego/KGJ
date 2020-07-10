using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KGJ.Migrations
{
    public partial class add_Products : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    ProCode = table.Column<string>(maxLength: 50, nullable: false),
                    ProName = table.Column<string>(maxLength: 50, nullable: false),
                    ProAlias = table.Column<string>(maxLength: 50, nullable: true),
                    ImagePath = table.Column<string>(maxLength: 200, nullable: true),
                    ProClassification = table.Column<string>(maxLength: 50, nullable: true),
                    ProModel = table.Column<string>(maxLength: 50, nullable: false),
                    ProBrand = table.Column<string>(maxLength: 50, nullable: false),
                    ProPackage = table.Column<string>(maxLength: 50, nullable: true),
                    ProUnit = table.Column<string>(maxLength: 50, nullable: true),
                    ProDesc = table.Column<string>(maxLength: 500, nullable: true),
                    Size = table.Column<string>(maxLength: 50, nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Acreage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinNum = table.Column<int>(nullable: false),
                    ChargeStandard = table.Column<int>(nullable: true),
                    ChargeAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsMultiUnit = table.Column<bool>(nullable: false),
                    IsGenerateQR = table.Column<bool>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            
        }
    }
}
