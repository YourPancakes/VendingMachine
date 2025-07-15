using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendingMachine.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddInsertedQuantityToCoins : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InsertedQuantity",
                table: "Coins",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsertedQuantity",
                table: "Coins");
        }
    }
}
