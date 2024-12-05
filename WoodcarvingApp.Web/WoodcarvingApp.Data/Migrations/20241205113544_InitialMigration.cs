using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WoodcarvingApp.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CityName = table.Column<string>(type: "NVARCHAR(20)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Woodcarvers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "NVARCHAR(20)", nullable: false),
                    LastName = table.Column<string>(type: "NVARCHAR(20)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Woodcarvers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WoodTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WoodTypeName = table.Column<string>(type: "NVARCHAR(20)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hardness = table.Column<string>(type: "NVARCHAR(20)", nullable: false),
                    Color = table.Column<string>(type: "NVARCHAR(20)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WoodTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exhibitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExhibitionName = table.Column<string>(type: "NVARCHAR(20)", nullable: false),
                    Address = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exhibitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exhibitions_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Woodcarvings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "NVARCHAR(40)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    WoodcarverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WoodTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Woodcarvings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Woodcarvings_WoodTypes_WoodTypeId",
                        column: x => x.WoodTypeId,
                        principalTable: "WoodTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Woodcarvings_Woodcarvers_WoodcarverId",
                        column: x => x.WoodcarverId,
                        principalTable: "Woodcarvers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WoodcarvingExhibition",
                columns: table => new
                {
                    WoodcarvingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExhibitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WoodcarvingExhibition", x => new { x.WoodcarvingId, x.ExhibitionId });
                    table.ForeignKey(
                        name: "FK_WoodcarvingExhibition_Exhibitions_ExhibitionId",
                        column: x => x.ExhibitionId,
                        principalTable: "Exhibitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WoodcarvingExhibition_Woodcarvings_WoodcarvingId",
                        column: x => x.WoodcarvingId,
                        principalTable: "Woodcarvings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exhibitions_CityId",
                table: "Exhibitions",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_WoodcarvingExhibition_ExhibitionId",
                table: "WoodcarvingExhibition",
                column: "ExhibitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Woodcarvings_WoodcarverId",
                table: "Woodcarvings",
                column: "WoodcarverId");

            migrationBuilder.CreateIndex(
                name: "IX_Woodcarvings_WoodTypeId",
                table: "Woodcarvings",
                column: "WoodTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WoodcarvingExhibition");

            migrationBuilder.DropTable(
                name: "Exhibitions");

            migrationBuilder.DropTable(
                name: "Woodcarvings");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "WoodTypes");

            migrationBuilder.DropTable(
                name: "Woodcarvers");
        }
    }
}
