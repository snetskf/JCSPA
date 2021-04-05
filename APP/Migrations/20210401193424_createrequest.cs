using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APP.Migrations
{
    public partial class createrequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BillofMaterials",
                table: "SPAHeader",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CFTTeam",
                table: "SPAHeader",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Creater",
                table: "SPAHeader",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "SPAHeader",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DateRequired",
                table: "SPAHeader",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeviationInstruction",
                table: "SPAHeader",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Guid",
                table: "SPAHeader",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModelDescription",
                table: "SPAHeader",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModelandDescription",
                table: "SPAHeader",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PackagingRequirements",
                table: "SPAHeader",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QtyRequired",
                table: "SPAHeader",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SpecialAssemblyInstr",
                table: "SPAHeader",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "SPAHeader",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusCode",
                table: "SPAHeader",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestRequirements",
                table: "SPAHeader",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillofMaterials",
                table: "SPAHeader");

            migrationBuilder.DropColumn(
                name: "CFTTeam",
                table: "SPAHeader");

            migrationBuilder.DropColumn(
                name: "Creater",
                table: "SPAHeader");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "SPAHeader");

            migrationBuilder.DropColumn(
                name: "DateRequired",
                table: "SPAHeader");

            migrationBuilder.DropColumn(
                name: "DeviationInstruction",
                table: "SPAHeader");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "SPAHeader");

            migrationBuilder.DropColumn(
                name: "ModelDescription",
                table: "SPAHeader");

            migrationBuilder.DropColumn(
                name: "ModelandDescription",
                table: "SPAHeader");

            migrationBuilder.DropColumn(
                name: "PackagingRequirements",
                table: "SPAHeader");

            migrationBuilder.DropColumn(
                name: "QtyRequired",
                table: "SPAHeader");

            migrationBuilder.DropColumn(
                name: "SpecialAssemblyInstr",
                table: "SPAHeader");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "SPAHeader");

            migrationBuilder.DropColumn(
                name: "StatusCode",
                table: "SPAHeader");

            migrationBuilder.DropColumn(
                name: "TestRequirements",
                table: "SPAHeader");
        }
    }
}
