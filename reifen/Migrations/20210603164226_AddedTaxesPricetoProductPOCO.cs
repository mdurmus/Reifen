using Microsoft.EntityFrameworkCore.Migrations;

namespace reifen.Migrations
{
    public partial class AddedTaxesPricetoProductPOCO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SalesPrice",
                table: "Products",
                newName: "VKNetto");

            migrationBuilder.RenameColumn(
                name: "PurchasePrice",
                table: "Products",
                newName: "VKBrutto");

            migrationBuilder.AddColumn<double>(
                name: "EKBrutto",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "EKNetto",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EKBrutto",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "EKNetto",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "VKNetto",
                table: "Products",
                newName: "SalesPrice");

            migrationBuilder.RenameColumn(
                name: "VKBrutto",
                table: "Products",
                newName: "PurchasePrice");
        }
    }
}
