using Microsoft.EntityFrameworkCore.Migrations;

namespace ETZ.Lending.Persistence.Migrations
{
    public partial class AddProductIsLentProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLent",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLent",
                table: "Products");
        }
    }
}
