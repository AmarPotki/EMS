using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EMS.Infrastructure.Migrations.Ems
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ems");

            migrationBuilder.CreateSequence(
                name: "faultseq",
                schema: "ems",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "faultTypeseq",
                schema: "ems",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "fixUnitseq",
                schema: "ems",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "flowseq",
                schema: "ems",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "itemTypeseq",
                schema: "ems",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "locationseq",
                schema: "ems",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "memeberseq",
                schema: "ems",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "vielitrequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vielitrequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "faultStatus",
                schema: "ems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_faultStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "faultTypes",
                schema: "ems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    IsArchive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_faultTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "fixUnits",
                schema: "ems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ItemTypeId = table.Column<int>(nullable: false),
                    Owner = table.Column<string>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fixUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "itemTypes",
                schema: "ems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itemTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_itemTypes_itemTypes_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "ems",
                        principalTable: "itemTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "locations",
                schema: "ems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_locations_locations_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "ems",
                        principalTable: "locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "members",
                schema: "ems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    IdentityGuid = table.Column<string>(nullable: false),
                    FixUnitId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_members_fixUnits_FixUnitId",
                        column: x => x.FixUnitId,
                        principalSchema: "ems",
                        principalTable: "fixUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "faults",
                schema: "ems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    FaultStatusId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    FaultTypeId = table.Column<int>(nullable: false),
                    ItemTypeId = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    OwnerId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_faults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_faults_faultStatus_FaultStatusId",
                        column: x => x.FaultStatusId,
                        principalSchema: "ems",
                        principalTable: "faultStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_faults_faultTypes_FaultTypeId",
                        column: x => x.FaultTypeId,
                        principalSchema: "ems",
                        principalTable: "faultTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_faults_itemTypes_ItemTypeId",
                        column: x => x.ItemTypeId,
                        principalSchema: "ems",
                        principalTable: "itemTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_faults_locations_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "ems",
                        principalTable: "locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "flows",
                schema: "ems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    From_FixUnitId = table.Column<int>(nullable: false),
                    From_FixUnitName = table.Column<string>(nullable: true),
                    To_FixUnitId = table.Column<int>(nullable: false),
                    To_FixUnitName = table.Column<string>(nullable: true),
                    Time = table.Column<DateTimeOffset>(nullable: false),
                    FaultId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_flows_faults_FaultId",
                        column: x => x.FaultId,
                        principalSchema: "ems",
                        principalTable: "faults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_faults_FaultStatusId",
                schema: "ems",
                table: "faults",
                column: "FaultStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_faults_FaultTypeId",
                schema: "ems",
                table: "faults",
                column: "FaultTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_faults_ItemTypeId",
                schema: "ems",
                table: "faults",
                column: "ItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_faults_LocationId",
                schema: "ems",
                table: "faults",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_flows_FaultId",
                schema: "ems",
                table: "flows",
                column: "FaultId");

            migrationBuilder.CreateIndex(
                name: "IX_itemTypes_ParentId",
                schema: "ems",
                table: "itemTypes",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_locations_ParentId",
                schema: "ems",
                table: "locations",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_members_FixUnitId",
                schema: "ems",
                table: "members",
                column: "FixUnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vielitrequests");

            migrationBuilder.DropTable(
                name: "flows",
                schema: "ems");

            migrationBuilder.DropTable(
                name: "members",
                schema: "ems");

            migrationBuilder.DropTable(
                name: "faults",
                schema: "ems");

            migrationBuilder.DropTable(
                name: "fixUnits",
                schema: "ems");

            migrationBuilder.DropTable(
                name: "faultStatus",
                schema: "ems");

            migrationBuilder.DropTable(
                name: "faultTypes",
                schema: "ems");

            migrationBuilder.DropTable(
                name: "itemTypes",
                schema: "ems");

            migrationBuilder.DropTable(
                name: "locations",
                schema: "ems");

            migrationBuilder.DropSequence(
                name: "faultseq",
                schema: "ems");

            migrationBuilder.DropSequence(
                name: "faultTypeseq",
                schema: "ems");

            migrationBuilder.DropSequence(
                name: "fixUnitseq",
                schema: "ems");

            migrationBuilder.DropSequence(
                name: "flowseq",
                schema: "ems");

            migrationBuilder.DropSequence(
                name: "itemTypeseq",
                schema: "ems");

            migrationBuilder.DropSequence(
                name: "locationseq",
                schema: "ems");

            migrationBuilder.DropSequence(
                name: "memeberseq",
                schema: "ems");
        }
    }
}
