using Microsoft.EntityFrameworkCore.Migrations;

namespace EMS.Infrastructure.Migrations.Ems
{
    public partial class AddPartComplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "consumePartseq",
                schema: "ems",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "consumeParts",
                schema: "ems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    PartName = table.Column<string>(nullable: true),
                    PartId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    FaultId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_consumeParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_consumeParts_faults_FaultId",
                        column: x => x.FaultId,
                        principalSchema: "ems",
                        principalTable: "faults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_consumeParts_FaultId",
                schema: "ems",
                table: "consumeParts",
                column: "FaultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "consumeParts",
                schema: "ems");

            migrationBuilder.DropSequence(
                name: "consumePartseq",
                schema: "ems");
        }
    }
}
