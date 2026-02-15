using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exe1.Migrations
{
    /// <inheritdoc />
    public partial class Idfgh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Prize",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ThewinnerId",
                table: "Prize",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prize_ThewinnerId",
                table: "Prize",
                column: "ThewinnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prize_User_ThewinnerId",
                table: "Prize",
                column: "ThewinnerId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prize_User_ThewinnerId",
                table: "Prize");

            migrationBuilder.DropIndex(
                name: "IX_Prize_ThewinnerId",
                table: "Prize");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Prize");

            migrationBuilder.DropColumn(
                name: "ThewinnerId",
                table: "Prize");
        }
    }
}
