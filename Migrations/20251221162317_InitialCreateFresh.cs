using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exe1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateFresh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketPrize");

            migrationBuilder.AddColumn<int>(
                name: "PrizeId",
                table: "Basket",
                type: "int",
                nullable: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_Prize_PrizeId",
                table: "Basket");

            migrationBuilder.DropIndex(
                name: "IX_Basket_PrizeId",
                table: "Basket");

            migrationBuilder.DropColumn(
                name: "PrizeId",
                table: "Basket");

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
        }
    }
}
