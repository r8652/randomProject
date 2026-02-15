using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exe1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate753 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_Prize_PrizeId",
                table: "Basket");

            migrationBuilder.AlterColumn<int>(
                name: "PrizeId",
                table: "Basket",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_Prize_PrizeId",
                table: "Basket",
                column: "PrizeId",
                principalTable: "Prize",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_Prize_PrizeId",
                table: "Basket");

            migrationBuilder.AlterColumn<int>(
                name: "PrizeId",
                table: "Basket",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_Prize_PrizeId",
                table: "Basket",
                column: "PrizeId",
                principalTable: "Prize",
                principalColumn: "Id");
        }
    }
}
