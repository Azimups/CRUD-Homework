using Microsoft.EntityFrameworkCore.Migrations;

namespace Fiorella.Migrations
{
    public partial class AddAboutAndAboutTextTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboutImg",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutImg", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AboutTexts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconText1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconText2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconText3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutTexts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutImg");

            migrationBuilder.DropTable(
                name: "AboutTexts");
        }
    }
}
