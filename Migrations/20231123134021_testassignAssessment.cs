using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdministratorSystem.Migrations
{
    /// <inheritdoc />
    public partial class testassignAssessment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaximumMark",
                table: "ModuleAssessment",
                newName: "MaxMark");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxMark",
                table: "ModuleAssessment",
                newName: "MaximumMark");
        }
    }
}
