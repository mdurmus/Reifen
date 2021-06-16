using Microsoft.EntityFrameworkCore.Migrations;

namespace reifen.Migrations
{
    public partial class PersonalManagerFeatureAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isManager",
                table: "Personals",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isManager",
                table: "Personals");
        }
    }
}
