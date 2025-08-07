using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class AddMerchantCategoryTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "merchant_categories_main",
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
                    table.PrimaryKey("PK_merchant_categories_main", x => x.Id);
                    table.ForeignKey(
                        name: "FK_merchant_categories_main_images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "images",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "merchant_category_mappings",
                columns: table => new
                {
                    MerchantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_merchant_category_mappings", x => new { x.MerchantId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_merchant_category_mappings_merchant_categories_main_Categor~",
                        column: x => x.CategoryId,
                        principalTable: "merchant_categories_main",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_merchant_category_mappings_merchants_MerchantId",
                        column: x => x.MerchantId,
                        principalTable: "merchants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_merchant_categories_main_ImageId",
                table: "merchant_categories_main",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_merchant_category_mappings_CategoryId",
                table: "merchant_category_mappings",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "merchant_category_mappings");

            migrationBuilder.DropTable(
                name: "merchant_categories_main");
        }
    }
}
