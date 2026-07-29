using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSupplierMaterialEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "SupplierMaterials",
                newName: "PriceUnit");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "SupplierMaterials",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PriceUnit",
                table: "SupplierMaterials",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "SupplierMaterials",
                newName: "Note");
        }
    }
}
