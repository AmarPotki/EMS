using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EMS.Infrastructure.Migrations.Ems
{
    public partial class AddFaultResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "faultResultseq",
                schema: "ems",
                incrementBy: 10);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Assign_Time",
                schema: "ems",
                table: "faults",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Assign_UserDisplayName",
                schema: "ems",
                table: "faults",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Assign_UserId",
                schema: "ems",
                table: "faults",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FixUnitId",
                schema: "ems",
                table: "faults",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "faultResults",
                schema: "ems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    IsMoveTo = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Time = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    FixUnitId = table.Column<int>(nullable: false),
                    FaultId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_faultResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_faultResults_faults_FaultId",
                        column: x => x.FaultId,
                        principalSchema: "ems",
                        principalTable: "faults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_faultResults_FaultId",
                schema: "ems",
                table: "faultResults",
                column: "FaultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "faultResults",
                schema: "ems");

            migrationBuilder.DropSequence(
                name: "faultResultseq",
                schema: "ems");

            migrationBuilder.DropColumn(
                name: "Assign_Time",
                schema: "ems",
                table: "faults");

            migrationBuilder.DropColumn(
                name: "Assign_UserDisplayName",
                schema: "ems",
                table: "faults");

            migrationBuilder.DropColumn(
                name: "Assign_UserId",
                schema: "ems",
                table: "faults");

            migrationBuilder.DropColumn(
                name: "FixUnitId",
                schema: "ems",
                table: "faults");
        }
    }
}
