using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdministratorSystem.Migrations
{
    /// <inheritdoc />
    public partial class testaddassessment3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "AssessmentId",
                table: "StudentAssessments",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssessments_Assessment_AssessmentId",
                table: "StudentAssessments");

            migrationBuilder.DropIndex(
                name: "IX_StudentAssessments_AssessmentId",
                table: "StudentAssessments");

            migrationBuilder.AlterColumn<string>(
                name: "AssessmentId",
                table: "StudentAssessments",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "AssessmentId1",
                table: "StudentAssessments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

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
    }
}
