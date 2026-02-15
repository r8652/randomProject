using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exe1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate52 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Prize",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Prize");
        }
    }
}
