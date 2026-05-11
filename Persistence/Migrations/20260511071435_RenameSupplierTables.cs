using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameSupplierTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierMaterials_SupplierEntity_SupplierId",
                table: "SupplierMaterials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierEntity",
                table: "SupplierEntity");

            migrationBuilder.RenameTable(
                name: "SupplierEntity",
                newName: "Suppliers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierMaterials_Suppliers_SupplierId",
                table: "SupplierMaterials",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierMaterials_Suppliers_SupplierId",
                table: "SupplierMaterials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers");

            migrationBuilder.RenameTable(
                name: "Suppliers",
                newName: "SupplierEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierEntity",
                table: "SupplierEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierMaterials_SupplierEntity_SupplierId",
                table: "SupplierMaterials",
                column: "SupplierId",
                principalTable: "SupplierEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
