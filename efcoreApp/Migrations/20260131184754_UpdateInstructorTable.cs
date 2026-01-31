using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace efcoreApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInstructorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Instructors",
                newName: "InstructorLastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Instructors",
                newName: "InstructorFirstName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InstructorLastName",
                table: "Instructors",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "InstructorFirstName",
                table: "Instructors",
                newName: "FirstName");
        }
    }
}
