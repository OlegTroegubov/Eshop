#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Eshop.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixedCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder == null) throw new ArgumentNullException(nameof(migrationBuilder));
            migrationBuilder.AddColumn<int>(
                name: "ParentProductCategoryId",
                table: "ProductCategories",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ParentProductCategoryId",
                table: "ProductCategories",
                column: "ParentProductCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_ProductCategories_ParentProductCategoryId",
                table: "ProductCategories",
                column: "ParentProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_ProductCategories_ParentProductCategoryId",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_ParentProductCategoryId",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "ParentProductCategoryId",
                table: "ProductCategories");
        }
    }
}
