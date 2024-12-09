using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WoodcarvingApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedWoodcarvingExhibitionsDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WoodcarvingExhibition_Exhibitions_ExhibitionId",
                table: "WoodcarvingExhibition");

            migrationBuilder.DropForeignKey(
                name: "FK_WoodcarvingExhibition_Woodcarvings_WoodcarvingId",
                table: "WoodcarvingExhibition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WoodcarvingExhibition",
                table: "WoodcarvingExhibition");

            migrationBuilder.RenameTable(
                name: "WoodcarvingExhibition",
                newName: "WoodcarvingExhibitions");

            migrationBuilder.RenameIndex(
                name: "IX_WoodcarvingExhibition_ExhibitionId",
                table: "WoodcarvingExhibitions",
                newName: "IX_WoodcarvingExhibitions_ExhibitionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WoodcarvingExhibitions",
                table: "WoodcarvingExhibitions",
                columns: new[] { "WoodcarvingId", "ExhibitionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WoodcarvingExhibitions_Exhibitions_ExhibitionId",
                table: "WoodcarvingExhibitions",
                column: "ExhibitionId",
                principalTable: "Exhibitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WoodcarvingExhibitions_Woodcarvings_WoodcarvingId",
                table: "WoodcarvingExhibitions",
                column: "WoodcarvingId",
                principalTable: "Woodcarvings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WoodcarvingExhibitions_Exhibitions_ExhibitionId",
                table: "WoodcarvingExhibitions");

            migrationBuilder.DropForeignKey(
                name: "FK_WoodcarvingExhibitions_Woodcarvings_WoodcarvingId",
                table: "WoodcarvingExhibitions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WoodcarvingExhibitions",
                table: "WoodcarvingExhibitions");

            migrationBuilder.RenameTable(
                name: "WoodcarvingExhibitions",
                newName: "WoodcarvingExhibition");

            migrationBuilder.RenameIndex(
                name: "IX_WoodcarvingExhibitions_ExhibitionId",
                table: "WoodcarvingExhibition",
                newName: "IX_WoodcarvingExhibition_ExhibitionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WoodcarvingExhibition",
                table: "WoodcarvingExhibition",
                columns: new[] { "WoodcarvingId", "ExhibitionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WoodcarvingExhibition_Exhibitions_ExhibitionId",
                table: "WoodcarvingExhibition",
                column: "ExhibitionId",
                principalTable: "Exhibitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WoodcarvingExhibition_Woodcarvings_WoodcarvingId",
                table: "WoodcarvingExhibition",
                column: "WoodcarvingId",
                principalTable: "Woodcarvings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
