using Microsoft.EntityFrameworkCore.Migrations;

namespace APP.Migrations
{
    public partial class attachments_revised : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attachmentid",
                table: "SPAAttachments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Attachmentid",
                table: "SPAAttachments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
