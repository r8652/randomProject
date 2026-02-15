using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exe1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate66 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_User_UserId",
                table: "Basket");

            migrationBuilder.DropColumn(
                name: "User_id",
                table: "Basket");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Basket",
                newName: "Userid");

            migrationBuilder.RenameIndex(
                name: "IX_Basket_UserId",
                table: "Basket",
                newName: "IX_Basket_Userid");

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_User_Userid",
                table: "Basket",
                column: "Userid",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_User_Userid",
                table: "Basket");

            migrationBuilder.RenameColumn(
                name: "Userid",
                table: "Basket",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Basket_Userid",
                table: "Basket",
                newName: "IX_Basket_UserId");

            migrationBuilder.AddColumn<int>(
                name: "User_id",
                table: "Basket",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
