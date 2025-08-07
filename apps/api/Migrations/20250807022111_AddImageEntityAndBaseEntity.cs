using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class AddImageEntityAndBaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MerchantLogoId",
                table: "merchants",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Filename = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Url = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Width = table.Column<int>(type: "integer", nullable: true),
                    Height = table.Column<int>(type: "integer", nullable: true),
                    Size = table.Column<int>(type: "integer", nullable: true),
                    MimeType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_images_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_merchants_MerchantLogoId",
                table: "merchants",
                column: "MerchantLogoId");

            migrationBuilder.CreateIndex(
                name: "IX_images_UserId",
                table: "images",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_merchants_images_MerchantLogoId",
                table: "merchants",
                column: "MerchantLogoId",
                principalTable: "images",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_merchants_images_MerchantLogoId",
                table: "merchants");

            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropIndex(
                name: "IX_merchants_MerchantLogoId",
                table: "merchants");

            migrationBuilder.DropColumn(
                name: "MerchantLogoId",
                table: "merchants");
        }
    }
}
