using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lab6.Migrations
{
    /// <inheritdoc />
    public partial class AddIsIndieGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsIndie",
                table: "Games",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsIndie",
                table: "Games");
        }
    }
}
