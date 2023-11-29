using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdministratorSystem.Migrations
{
    /// <inheritdoc />
    public partial class optionalModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSelected",
                table: "StudentModule",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSelected",
                table: "StudentModule");
        }
    }
}
