using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WoodcarvingApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedCityIdInWoodcarverClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CityId",
                table: "Woodcarvers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Woodcarvers_CityId",
                table: "Woodcarvers",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Woodcarvers_Cities_CityId",
                table: "Woodcarvers",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Woodcarvers_Cities_CityId",
                table: "Woodcarvers");

            migrationBuilder.DropIndex(
                name: "IX_Woodcarvers_CityId",
                table: "Woodcarvers");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Woodcarvers");
        }
    }
}
