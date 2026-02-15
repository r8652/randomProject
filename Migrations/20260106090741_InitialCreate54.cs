using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exe1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate54 : Migration
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

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrizeId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BasketId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.id);
                    table.ForeignKey(
                        name: "FK_Tickets_Basket_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Basket",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Prize_PrizeId",
                        column: x => x.PrizeId,
                        principalTable: "Prize",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Basket_PrizeId",
                table: "Basket",
                column: "PrizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_BasketId",
                table: "Tickets",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_PrizeId",
                table: "Tickets",
                column: "PrizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserId",
                table: "Tickets",
                column: "UserId");

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

            migrationBuilder.DropTable(
                name: "Tickets");

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
