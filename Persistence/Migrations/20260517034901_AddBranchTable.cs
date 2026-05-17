using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddBranchTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Branchs_BranchId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Branchs_BranchId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Branchs_BranchId",
                table: "Warehouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Branchs",
                table: "Branchs");

            migrationBuilder.RenameTable(
                name: "Branchs",
                newName: "Branches");

            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "Warehouses",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "Warehouses",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "Users",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "Users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "Tasks",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "SupplierMaterials",
                newName: "UpdatedAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Tasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "SupplierMaterials",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Branches",
                table: "Branches",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Branches_BranchId",
                table: "Orders",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Branches_BranchId",
                table: "Users",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id");

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
                name: "FK_Orders_Branches_BranchId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Branches_BranchId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Branches_BranchId",
                table: "Warehouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Branches",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "SupplierMaterials");

            migrationBuilder.RenameTable(
                name: "Branches",
                newName: "Branchs");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Warehouses",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Warehouses",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Users",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Users",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Tasks",
                newName: "DueDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "SupplierMaterials",
                newName: "Created");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Branchs",
                table: "Branchs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Branchs_BranchId",
                table: "Orders",
                column: "BranchId",
                principalTable: "Branchs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Branchs_BranchId",
                table: "Users",
                column: "BranchId",
                principalTable: "Branchs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Branchs_BranchId",
                table: "Warehouses",
                column: "BranchId",
                principalTable: "Branchs",
                principalColumn: "Id");
        }
    }
}
