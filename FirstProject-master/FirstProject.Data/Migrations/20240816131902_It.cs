using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class It : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StorageTransactions_Id_ShopId",
                table: "StorageTransactions",
                columns: new[] { "Id", "ShopId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StorageTransactions_Id_ShopId",
                table: "StorageTransactions");
        }
    }
}
