using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdministratorSystem.Migrations
{
    /// <inheritdoc />
    public partial class cohort : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Students",
                newName: "StundetIdentifier");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Students",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Students",
                newName: "Score");

            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "Students",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Students",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "CohortId",
                table: "Students",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "StudentId");

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CourseIdentifier = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DurationInYears = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "Cohort",
                columns: table => new
                {
                    CohortId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CohortIdentifier = table.Column<int>(type: "INTEGER", nullable: false),
                    AcademicYear = table.Column<string>(type: "TEXT", nullable: false),
                    CourseId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cohort", x => x.CohortId);
                    table.ForeignKey(
                        name: "FK_Cohort_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    ModuleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ModuleCode = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Isrequired = table.Column<bool>(type: "INTEGER", nullable: false),
                    CourseId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.ModuleId);
                    table.ForeignKey(
                        name: "FK_Module_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assessment",
                columns: table => new
                {
                    AssessmentId = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false),
                    MaxMark = table.Column<int>(type: "INTEGER", nullable: false),
                    Mark = table.Column<int>(type: "INTEGER", nullable: true),
                    ModuleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessment", x => x.AssessmentId);
                    table.ForeignKey(
                        name: "FK_Assessment_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentAssesments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false),
                    AssessmentId = table.Column<string>(type: "TEXT", nullable: false),
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
                name: "IX_Students_CohortId",
                table: "Students",
                column: "CohortId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_ModuleId",
                table: "Assessment",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Cohort_CourseId",
                table: "Cohort",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Module_CourseId",
                table: "Module",
                column: "CourseId");

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
                name: "Cohort");

            migrationBuilder.DropTable(
                name: "StudentAssesments");

            migrationBuilder.DropTable(
                name: "Assessment");

            migrationBuilder.DropTable(
                name: "Module");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_CohortId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CohortId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "StundetIdentifier",
                table: "Students",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Score",
                table: "Students",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Students",
                newName: "LastName");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Students",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Students",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");
        }
    }
}
