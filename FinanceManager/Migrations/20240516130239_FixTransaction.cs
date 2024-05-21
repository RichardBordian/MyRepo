using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManager.Migrations
{
    /// <inheritdoc />
    public partial class FixTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Storages_StorageId",
                table: "Transactions");

            migrationBuilder.AlterColumn<int>(
                name: "StorageId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Storages_StorageId",
                table: "Transactions",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Storages_StorageId",
                table: "Transactions");

            migrationBuilder.AlterColumn<int>(
                name: "StorageId",
                table: "Transactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Storages_StorageId",
                table: "Transactions",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id");
        }
    }
}
