using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeApiCSharp07.Migrations
{
    /// <inheritdoc />
    public partial class FixActorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieActor_Reviews_ActorId",
                table: "MovieActor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Actor");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_Name",
                table: "Actor",
                newName: "IX_Actor_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actor",
                table: "Actor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieActor_Actor_ActorId",
                table: "MovieActor",
                column: "ActorId",
                principalTable: "Actor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieActor_Actor_ActorId",
                table: "MovieActor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actor",
                table: "Actor");

            migrationBuilder.RenameTable(
                name: "Actor",
                newName: "Reviews");

            migrationBuilder.RenameIndex(
                name: "IX_Actor_Name",
                table: "Reviews",
                newName: "IX_Reviews_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieActor_Reviews_ActorId",
                table: "MovieActor",
                column: "ActorId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
