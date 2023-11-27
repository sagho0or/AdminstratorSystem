using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdministratorSystem.Migrations
{
    /// <inheritdoc />
    public partial class studentstatementModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModuleId",
                table: "StudentAssessments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssessments_ModuleId",
                table: "StudentAssessments",
                column: "ModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAssessments_Module_ModuleId",
                table: "StudentAssessments",
                column: "ModuleId",
                principalTable: "Module",
                principalColumn: "ModuleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssessments_Module_ModuleId",
                table: "StudentAssessments");

            migrationBuilder.DropIndex(
                name: "IX_StudentAssessments_ModuleId",
                table: "StudentAssessments");

            migrationBuilder.DropColumn(
                name: "ModuleId",
                table: "StudentAssessments");
        }
    }
}
