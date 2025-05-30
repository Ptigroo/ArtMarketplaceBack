using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace data.Migrations
{
    /// <inheritdoc />
    public partial class reviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReviewId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ReviewId",
                table: "Products",
                column: "ReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Reviews_ReviewId",
                table: "Products",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Reviews_ReviewId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Products_ReviewId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "Products");
        }
    }
}
