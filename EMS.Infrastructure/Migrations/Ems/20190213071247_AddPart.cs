using Microsoft.EntityFrameworkCore.Migrations;

namespace EMS.Infrastructure.Migrations.Ems
{
    public partial class AddPart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "partseq",
                schema: "ems",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "parts",
                schema: "ems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    IsArchive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "parts",
                schema: "ems");

            migrationBuilder.DropSequence(
                name: "partseq",
                schema: "ems");
        }
    }
}
