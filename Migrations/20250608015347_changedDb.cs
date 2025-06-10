using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibrary.Migrations
{
    /// <inheritdoc />
    public partial class changedDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ebooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaperBooks",
                table: "PaperBooks");

            migrationBuilder.RenameTable(
                name: "PaperBooks",
                newName: "Books");

            migrationBuilder.AddColumn<string>(
                name: "MediaType",
                table: "Books",
                type: "TEXT",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "MediaType",
                table: "Books");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "PaperBooks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaperBooks",
                table: "PaperBooks",
                column: "id");

            migrationBuilder.CreateTable(
                name: "Ebooks",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Author = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ebooks", x => x.id);
                });
        }
    }
}
