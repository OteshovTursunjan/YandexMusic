using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YandexMusic.Migrations
{
    /// <inheritdoc />
    public partial class CardTy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Card_Types_CardTypesId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_CardTypesId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "CardTypesId",
                table: "Cards");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardTypeId",
                table: "Cards",
                column: "CardTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Card_Types_CardTypeId",
                table: "Cards",
                column: "CardTypeId",
                principalTable: "Card_Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Card_Types_CardTypeId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_CardTypeId",
                table: "Cards");

            migrationBuilder.AddColumn<Guid>(
                name: "CardTypesId",
                table: "Cards",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardTypesId",
                table: "Cards",
                column: "CardTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Card_Types_CardTypesId",
                table: "Cards",
                column: "CardTypesId",
                principalTable: "Card_Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
