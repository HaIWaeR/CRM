using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddWarehouse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockItems_Warehouse_WarehouseId",
                table: "StockItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Storages_Warehouse_WarehouseId",
                table: "Storages");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouse_Branches_BranchId",
                table: "Warehouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Warehouse",
                table: "Warehouse");

            migrationBuilder.RenameTable(
                name: "Warehouse",
                newName: "Warehouses");

            migrationBuilder.RenameIndex(
                name: "IX_Warehouse_BranchId",
                table: "Warehouses",
                newName: "IX_Warehouses_BranchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Warehouses",
                table: "Warehouses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockItems_Warehouses_WarehouseId",
                table: "StockItems",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Storages_Warehouses_WarehouseId",
                table: "Storages",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Branches_BranchId",
                table: "Warehouses",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockItems_Warehouses_WarehouseId",
                table: "StockItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Storages_Warehouses_WarehouseId",
                table: "Storages");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Branches_BranchId",
                table: "Warehouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Warehouses",
                table: "Warehouses");

            migrationBuilder.RenameTable(
                name: "Warehouses",
                newName: "Warehouse");

            migrationBuilder.RenameIndex(
                name: "IX_Warehouses_BranchId",
                table: "Warehouse",
                newName: "IX_Warehouse_BranchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Warehouse",
                table: "Warehouse",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockItems_Warehouse_WarehouseId",
                table: "StockItems",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Storages_Warehouse_WarehouseId",
                table: "Storages",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouse_Branches_BranchId",
                table: "Warehouse",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id");
        }
    }
}
