﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WoodcarvingApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedImageUrlToExhibitionEntityModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Exhibitions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Exhibitions");
        }
    }
}
