using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreatev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockItems_Materials_MaterialEntityId",
                table: "StockItems");

            migrationBuilder.DropForeignKey(
                name: "FK_StockItems_Products_ProductEntityId",
                table: "StockItems");

            migrationBuilder.DropForeignKey(
                name: "FK_StockItems_Storages_StorageZoneEntityId",
                table: "StockItems");

            migrationBuilder.DropIndex(
                name: "IX_StockItems_MaterialEntityId",
                table: "StockItems");

            migrationBuilder.DropIndex(
                name: "IX_StockItems_ProductEntityId",
                table: "StockItems");

            migrationBuilder.DropIndex(
                name: "IX_StockItems_StorageZoneEntityId",
                table: "StockItems");

            migrationBuilder.DropColumn(
                name: "MaterialEntityId",
                table: "StockItems");

            migrationBuilder.DropColumn(
                name: "ProductEntityId",
                table: "StockItems");

            migrationBuilder.DropColumn(
                name: "StorageZoneEntityId",
                table: "StockItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MaterialEntityId",
                table: "StockItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductEntityId",
                table: "StockItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StorageZoneEntityId",
                table: "StockItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockItems_MaterialEntityId",
                table: "StockItems",
                column: "MaterialEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_StockItems_ProductEntityId",
                table: "StockItems",
                column: "ProductEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_StockItems_StorageZoneEntityId",
                table: "StockItems",
                column: "StorageZoneEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockItems_Materials_MaterialEntityId",
                table: "StockItems",
                column: "MaterialEntityId",
                principalTable: "Materials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockItems_Products_ProductEntityId",
                table: "StockItems",
                column: "ProductEntityId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockItems_Storages_StorageZoneEntityId",
                table: "StockItems",
                column: "StorageZoneEntityId",
                principalTable: "Storages",
                principalColumn: "Id");
        }
    }
}
