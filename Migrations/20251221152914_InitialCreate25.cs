using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exe1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_User_UserId",
                table: "Basket");

            migrationBuilder.DropIndex(
                name: "IX_Basket_UserId",
                table: "Basket");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Basket");

            migrationBuilder.CreateIndex(
                name: "IX_Basket_User_id",
                table: "Basket",
                column: "User_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_User_User_id",
                table: "Basket",
                column: "User_id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_User_User_id",
                table: "Basket");

            migrationBuilder.DropIndex(
                name: "IX_Basket_User_id",
                table: "Basket");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Basket",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Basket_UserId",
                table: "Basket",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_User_UserId",
                table: "Basket",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
