using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MS.CoachSystem.Repository.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionDuration",
                table: "CoachingSessions");

            migrationBuilder.AddColumn<string>(
                name: "SessionLocation",
                table: "CoachingSessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SessionStatus",
                table: "CoachingSessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionLocation",
                table: "CoachingSessions");

            migrationBuilder.DropColumn(
                name: "SessionStatus",
                table: "CoachingSessions");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "SessionDuration",
                table: "CoachingSessions",
                type: "time",
                nullable: true);
        }
    }
}
