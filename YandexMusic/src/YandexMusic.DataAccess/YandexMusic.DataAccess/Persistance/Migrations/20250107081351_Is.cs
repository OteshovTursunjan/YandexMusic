using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YandexMusic.Migrations
{
    /// <inheritdoc />
    public partial class Is : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Card_Types_Card_TypeId",
                table: "Cards");

            migrationBuilder.RenameColumn(
                name: "Card_TypeId",
                table: "Cards",
                newName: "CardTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Cards_Card_TypeId",
                table: "Cards",
                newName: "IX_Cards_CardTypeId");

            migrationBuilder.AlterColumn<string>(
                name: "Card_Number",
                table: "Cards",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

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

            migrationBuilder.RenameColumn(
                name: "CardTypeId",
                table: "Cards",
                newName: "Card_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Cards_CardTypeId",
                table: "Cards",
                newName: "IX_Cards_Card_TypeId");

            migrationBuilder.AlterColumn<int>(
                name: "Card_Number",
                table: "Cards",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Card_Types_Card_TypeId",
                table: "Cards",
                column: "Card_TypeId",
                principalTable: "Card_Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
