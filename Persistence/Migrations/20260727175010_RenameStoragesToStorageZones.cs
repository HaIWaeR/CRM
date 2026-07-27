using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameStoragesToStorageZones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockItems_Storages_StorageZoneId",
                table: "StockItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Storages_Warehouses_WarehouseId",
                table: "Storages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Storages",
                table: "Storages");

            migrationBuilder.RenameTable(
                name: "Storages",
                newName: "StorageZones");

            migrationBuilder.RenameIndex(
                name: "IX_Storages_WarehouseId",
                table: "StorageZones",
                newName: "IX_StorageZones_WarehouseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StorageZones",
                table: "StorageZones",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockItems_StorageZones_StorageZoneId",
                table: "StockItems",
                column: "StorageZoneId",
                principalTable: "StorageZones",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_StorageZones_Warehouses_WarehouseId",
                table: "StorageZones",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockItems_StorageZones_StorageZoneId",
                table: "StockItems");

            migrationBuilder.DropForeignKey(
                name: "FK_StorageZones_Warehouses_WarehouseId",
                table: "StorageZones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StorageZones",
                table: "StorageZones");

            migrationBuilder.RenameTable(
                name: "StorageZones",
                newName: "Storages");

            migrationBuilder.RenameIndex(
                name: "IX_StorageZones_WarehouseId",
                table: "Storages",
                newName: "IX_Storages_WarehouseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Storages",
                table: "Storages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockItems_Storages_StorageZoneId",
                table: "StockItems",
                column: "StorageZoneId",
                principalTable: "Storages",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Storages_Warehouses_WarehouseId",
                table: "Storages",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
