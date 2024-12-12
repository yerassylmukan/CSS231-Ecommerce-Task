using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_CatalogItems_CatalogItemId",
                table: "Reviews");

            migrationBuilder.AlterColumn<int>(
                name: "CatalogItemId",
                table: "Reviews",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_CatalogItems_CatalogItemId",
                table: "Reviews",
                column: "CatalogItemId",
                principalTable: "CatalogItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_CatalogItems_CatalogItemId",
                table: "Reviews");

            migrationBuilder.AlterColumn<int>(
                name: "CatalogItemId",
                table: "Reviews",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_CatalogItems_CatalogItemId",
                table: "Reviews",
                column: "CatalogItemId",
                principalTable: "CatalogItems",
                principalColumn: "Id");
        }
    }
}
