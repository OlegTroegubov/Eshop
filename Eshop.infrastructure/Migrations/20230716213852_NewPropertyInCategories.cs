#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Eshop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewPropertyInCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLastInHierarchy",
                table: "ProductCategories",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLastInHierarchy",
                table: "ProductCategories");
        }
    }
}
