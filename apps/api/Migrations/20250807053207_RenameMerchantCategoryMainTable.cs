using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class RenameMerchantCategoryMainTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_merchant_category_mappings_merchant_categories_main_Categor~",
                table: "merchant_category_mappings");

            migrationBuilder.DropTable(
                name: "merchant_categories_main");

            migrationBuilder.CreateTable(
                name: "merchant_categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ImageId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_merchant_categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_merchant_categories_images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "images",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_merchant_categories_ImageId",
                table: "merchant_categories",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_merchant_category_mappings_merchant_categories_CategoryId",
                table: "merchant_category_mappings",
                column: "CategoryId",
                principalTable: "merchant_categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_merchant_category_mappings_merchant_categories_CategoryId",
                table: "merchant_category_mappings");

            migrationBuilder.DropTable(
                name: "merchant_categories");

            migrationBuilder.CreateTable(
                name: "merchant_categories_main",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImageId = table.Column<Guid>(type: "uuid", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_merchant_categories_main", x => x.Id);
                    table.ForeignKey(
                        name: "FK_merchant_categories_main_images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "images",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_merchant_categories_main_ImageId",
                table: "merchant_categories_main",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_merchant_category_mappings_merchant_categories_main_Categor~",
                table: "merchant_category_mappings",
                column: "CategoryId",
                principalTable: "merchant_categories_main",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
