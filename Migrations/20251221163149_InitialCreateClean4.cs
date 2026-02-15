using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exe1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateClean4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_Prize_PrizeId",
                table: "Basket");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_manegers_manegerId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Prize_Category_CategoryId",
                table: "Prize");

            migrationBuilder.DropIndex(
                name: "IX_Category_manegerId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Basket_PrizeId",
                table: "Basket");

            migrationBuilder.DropColumn(
                name: "manegerId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "PrizeId",
                table: "Basket");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Prize",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "BasketPrize",
                columns: table => new
                {
                    OwonersId = table.Column<int>(type: "int", nullable: false),
                    YourCardsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketPrize", x => new { x.OwonersId, x.YourCardsId });
                    table.ForeignKey(
                        name: "FK_BasketPrize_Basket_OwonersId",
                        column: x => x.OwonersId,
                        principalTable: "Basket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketPrize_Prize_YourCardsId",
                        column: x => x.YourCardsId,
                        principalTable: "Prize",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketPrize_YourCardsId",
                table: "BasketPrize",
                column: "YourCardsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prize_Category_CategoryId",
                table: "Prize",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prize_Category_CategoryId",
                table: "Prize");

            migrationBuilder.DropTable(
                name: "BasketPrize");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Prize",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "manegerId",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrizeId",
                table: "Basket",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_manegerId",
                table: "Category",
                column: "manegerId");

            migrationBuilder.CreateIndex(
                name: "IX_Basket_PrizeId",
                table: "Basket",
                column: "PrizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_Prize_PrizeId",
                table: "Basket",
                column: "PrizeId",
                principalTable: "Prize",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_manegers_manegerId",
                table: "Category",
                column: "manegerId",
                principalTable: "manegers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prize_Category_CategoryId",
                table: "Prize",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
