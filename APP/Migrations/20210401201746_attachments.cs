using Microsoft.EntityFrameworkCore.Migrations;

namespace APP.Migrations
{
    public partial class attachments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SPAAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Attachmentid = table.Column<int>(type: "int", nullable: false),
                    uploadFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Filename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Creater = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    spaheaderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SPAAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SPAAttachments_SPAHeader_spaheaderId",
                        column: x => x.spaheaderId,
                        principalTable: "SPAHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SPAAttachments_spaheaderId",
                table: "SPAAttachments",
                column: "spaheaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SPAAttachments");
        }
    }
}
