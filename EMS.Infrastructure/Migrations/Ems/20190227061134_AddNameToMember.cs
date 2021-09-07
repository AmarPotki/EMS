using Microsoft.EntityFrameworkCore.Migrations;

namespace EMS.Infrastructure.Migrations.Ems
{
    public partial class AddNameToMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ems",
                table: "members",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ems",
                table: "members");
        }
    }
}
