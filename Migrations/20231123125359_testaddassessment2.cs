using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdministratorSystem.Migrations
{
    /// <inheritdoc />
    public partial class testaddassessment2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssessments_Assessment_AssessmentId",
                table: "StudentAssessments");

            migrationBuilder.DropIndex(
                name: "IX_StudentAssessments_AssessmentId",
                table: "StudentAssessments");

            migrationBuilder.AddColumn<int>(
                name: "AssessmentId1",
                table: "StudentAssessments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "AssessmentId",
                table: "ModuleAssessment",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "AssessmentId",
                table: "Module",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AssessmentId",
                table: "Assessment",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssessments_AssessmentId1",
                table: "StudentAssessments",
                column: "AssessmentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAssessments_Assessment_AssessmentId1",
                table: "StudentAssessments",
                column: "AssessmentId1",
                principalTable: "Assessment",
                principalColumn: "AssessmentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssessments_Assessment_AssessmentId1",
                table: "StudentAssessments");

            migrationBuilder.DropIndex(
                name: "IX_StudentAssessments_AssessmentId1",
                table: "StudentAssessments");

            migrationBuilder.DropColumn(
                name: "AssessmentId1",
                table: "StudentAssessments");

            migrationBuilder.AlterColumn<string>(
                name: "AssessmentId",
                table: "ModuleAssessment",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "AssessmentId",
                table: "Module",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AssessmentId",
                table: "Assessment",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssessments_AssessmentId",
                table: "StudentAssessments",
                column: "AssessmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAssessments_Assessment_AssessmentId",
                table: "StudentAssessments",
                column: "AssessmentId",
                principalTable: "Assessment",
                principalColumn: "AssessmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
