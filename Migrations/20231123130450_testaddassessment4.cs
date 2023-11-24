using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdministratorSystem.Migrations
{
    /// <inheritdoc />
    public partial class testaddassessment4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assessment_Module_ModuleId",
                table: "Assessment");

            migrationBuilder.AlterColumn<int>(
                name: "ModuleId",
                table: "Assessment",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Assessment_Module_ModuleId",
                table: "Assessment",
                column: "ModuleId",
                principalTable: "Module",
                principalColumn: "ModuleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assessment_Module_ModuleId",
                table: "Assessment");

            migrationBuilder.AlterColumn<int>(
                name: "ModuleId",
                table: "Assessment",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Assessment_Module_ModuleId",
                table: "Assessment",
                column: "ModuleId",
                principalTable: "Module",
                principalColumn: "ModuleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
