using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MS.CoachSystem.Repository.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CoachingResources");

            migrationBuilder.AddColumn<string>(
                name: "StudentID",
                table: "CoachingResources",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentID",
                table: "CoachingResources");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CoachingResources",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
