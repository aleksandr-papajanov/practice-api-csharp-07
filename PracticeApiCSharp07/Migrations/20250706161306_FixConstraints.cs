using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeApiCSharp07.Migrations
{
    /// <inheritdoc />
    public partial class FixConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Movie_Year",
                table: "Movie");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Actor_BirthYear",
                table: "Actor");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Movie_Year",
                table: "Movie",
                sql: "Year >= 1888");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Actor_BirthYear",
                table: "Actor",
                sql: "BirthYear >= 1835");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Movie_Year",
                table: "Movie");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Actor_BirthYear",
                table: "Actor");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Movie_Year",
                table: "Movie",
                sql: "Year > 1888");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Actor_BirthYear",
                table: "Actor",
                sql: "BirthYear > 1835");
        }
    }
}
