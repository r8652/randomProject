using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exe1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate58 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prize_Donors_DonerId",
                table: "Prize");

            migrationBuilder.AlterColumn<int>(
                name: "DonerId",
                table: "Prize",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "category_id",
                table: "Prize",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "price",
                table: "Prize",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Prize_Donors_DonerId",
                table: "Prize",
                column: "DonerId",
                principalTable: "Donors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prize_Donors_DonerId",
                table: "Prize");

            migrationBuilder.DropColumn(
                name: "category_id",
                table: "Prize");

            migrationBuilder.DropColumn(
                name: "price",
                table: "Prize");

            migrationBuilder.AlterColumn<int>(
                name: "DonerId",
                table: "Prize",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Prize_Donors_DonerId",
                table: "Prize",
                column: "DonerId",
                principalTable: "Donors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
