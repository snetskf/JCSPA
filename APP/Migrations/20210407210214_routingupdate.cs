using Microsoft.EntityFrameworkCore.Migrations;

namespace APP.Migrations
{
    public partial class routingupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpaheaderidId",
                table: "SPARequestRouting",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SPARequestRouting_SpaheaderidId",
                table: "SPARequestRouting",
                column: "SpaheaderidId");

            migrationBuilder.AddForeignKey(
                name: "FK_SPARequestRouting_SPAHeader_SpaheaderidId",
                table: "SPARequestRouting",
                column: "SpaheaderidId",
                principalTable: "SPAHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SPARequestRouting_SPAHeader_SpaheaderidId",
                table: "SPARequestRouting");

            migrationBuilder.DropIndex(
                name: "IX_SPARequestRouting_SpaheaderidId",
                table: "SPARequestRouting");

            migrationBuilder.DropColumn(
                name: "SpaheaderidId",
                table: "SPARequestRouting");
        }
    }
}
