using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solforb.Orders.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SetUniqOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Orders_Number_ProviderId",
                table: "Orders",
                columns: new[] { "Number", "ProviderId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_Number_ProviderId",
                table: "Orders");
        }
    }
}
