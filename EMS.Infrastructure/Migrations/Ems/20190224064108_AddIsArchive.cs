using Microsoft.EntityFrameworkCore.Migrations;

namespace EMS.Infrastructure.Migrations.Ems
{
    public partial class AddIsArchive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchive",
                schema: "ems",
                table: "locations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchive",
                schema: "ems",
                table: "itemTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_consumeParts_PartId",
                schema: "ems",
                table: "consumeParts",
                column: "PartId");

            migrationBuilder.AddForeignKey(
                name: "FK_consumeParts_parts_PartId",
                schema: "ems",
                table: "consumeParts",
                column: "PartId",
                principalSchema: "ems",
                principalTable: "parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_consumeParts_parts_PartId",
                schema: "ems",
                table: "consumeParts");

            migrationBuilder.DropIndex(
                name: "IX_consumeParts_PartId",
                schema: "ems",
                table: "consumeParts");

            migrationBuilder.DropColumn(
                name: "IsArchive",
                schema: "ems",
                table: "locations");

            migrationBuilder.DropColumn(
                name: "IsArchive",
                schema: "ems",
                table: "itemTypes");
        }
    }
}
