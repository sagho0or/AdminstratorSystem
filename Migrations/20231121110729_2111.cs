using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdministratorSystem.Migrations
{
    /// <inheritdoc />
    public partial class _2111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Cohort_CohortId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "StudentAssesments");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CohortIdentifier",
                table: "Cohort");

            migrationBuilder.DropColumn(
                name: "Mark",
                table: "Assessment");

            migrationBuilder.RenameColumn(
                name: "Isrequired",
                table: "Module",
                newName: "IsRequired");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Course",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Assessment",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "MaxMark",
                table: "Assessment",
                newName: "MaximumMark");

            migrationBuilder.AlterColumn<int>(
                name: "CohortId",
                table: "Students",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CourseModule",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "INTEGER", nullable: false),
                    ModuleId = table.Column<int>(type: "INTEGER", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    IsRequired = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseModule", x => new { x.CourseId, x.ModuleId });
                    table.ForeignKey(
                        name: "FK_CourseModule_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseModule_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentAssessments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false),
                    AssessmentId = table.Column<string>(type: "TEXT", nullable: false),
                    Mark = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAssessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAssessments_Assessment_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessment",
                        principalColumn: "AssessmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentAssessments_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false),
                    CourseId = table.Column<int>(type: "INTEGER", nullable: false),
                    Mark = table.Column<int>(type: "INTEGER", nullable: false),
                    Result = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentCourse_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourse_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentModule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false),
                    ModuleId = table.Column<int>(type: "INTEGER", nullable: false),
                    Mark = table.Column<int>(type: "INTEGER", nullable: false),
                    Result = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentModule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentModule_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentModule_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseModule_ModuleId",
                table: "CourseModule",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssessments_AssessmentId",
                table: "StudentAssessments",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssessments_StudentId",
                table: "StudentAssessments",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourse_CourseId",
                table: "StudentCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourse_StudentId",
                table: "StudentCourse",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentModule_ModuleId",
                table: "StudentModule",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentModule_StudentId",
                table: "StudentModule",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Cohort_CohortId",
                table: "Students",
                column: "CohortId",
                principalTable: "Cohort",
                principalColumn: "CohortId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Cohort_CohortId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "CourseModule");

            migrationBuilder.DropTable(
                name: "StudentAssessments");

            migrationBuilder.DropTable(
                name: "StudentCourse");

            migrationBuilder.DropTable(
                name: "StudentModule");

            migrationBuilder.RenameColumn(
                name: "IsRequired",
                table: "Module",
                newName: "Isrequired");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Course",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Assessment",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "MaximumMark",
                table: "Assessment",
                newName: "MaxMark");

            migrationBuilder.AlterColumn<int>(
                name: "CohortId",
                table: "Students",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Students",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CohortIdentifier",
                table: "Cohort",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Mark",
                table: "Assessment",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StudentAssesments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AssessmentId = table.Column<string>(type: "TEXT", nullable: false),
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false),
                    Score = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAssesments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAssesments_Assessment_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessment",
                        principalColumn: "AssessmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentAssesments_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssesments_AssessmentId",
                table: "StudentAssesments",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssesments_StudentId",
                table: "StudentAssesments",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Cohort_CohortId",
                table: "Students",
                column: "CohortId",
                principalTable: "Cohort",
                principalColumn: "CohortId");
        }
    }
}
