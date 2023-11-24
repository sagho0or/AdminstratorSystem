using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdministratorSystem.Migrations
{
    /// <inheritdoc />
    public partial class testaddassessment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaximumMark",
                table: "Assessment");

            migrationBuilder.AddColumn<string>(
                name: "AssessmentId",
                table: "Module",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ModuleAssessment",
                columns: table => new
                {
                    AssessmentId = table.Column<string>(type: "TEXT", nullable: false),
                    ModuleId = table.Column<int>(type: "INTEGER", nullable: false),
                    MaximumMark = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleAssessment", x => new { x.ModuleId, x.AssessmentId });
                    table.ForeignKey(
                        name: "FK_ModuleAssessment_Assessment_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessment",
                        principalColumn: "AssessmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModuleAssessment_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Module_AssessmentId",
                table: "Module",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleAssessment_AssessmentId",
                table: "ModuleAssessment",
                column: "AssessmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Module_Assessment_AssessmentId",
                table: "Module",
                column: "AssessmentId",
                principalTable: "Assessment",
                principalColumn: "AssessmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Module_Assessment_AssessmentId",
                table: "Module");

            migrationBuilder.DropTable(
                name: "ModuleAssessment");

            migrationBuilder.DropIndex(
                name: "IX_Module_AssessmentId",
                table: "Module");

            migrationBuilder.DropColumn(
                name: "AssessmentId",
                table: "Module");

            migrationBuilder.AddColumn<int>(
                name: "MaximumMark",
                table: "Assessment",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
