using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YandexMusic.Migrations
{
    /// <inheritdoc />
    public partial class Musics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Musics",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "Musics");
        }
    }
}
