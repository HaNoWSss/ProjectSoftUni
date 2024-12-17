using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WoodcarvingApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CityName", "ImageUrl", "IsDeleted" },
                values: new object[,]
                {
                    { new Guid("b2c8c39d-571a-4e0b-83d8-7f062083537e"), "Woodville", "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse2.mm.bing.net%2Fth%3Fid%3DOIP.JdS0gRMOKp50OdrdPtnkpwHaE8%26pid%3DApi&f=1&ipt=97ed82df0cb89ed0dbae5d680652f0513fbbf2df7d9f8f348584dece9e4b9abd&ipo=images", false },
                    { new Guid("c30fe708-a15c-42e8-b088-23d6ce4619fd"), "Carverton", "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse1.mm.bing.net%2Fth%3Fid%3DOIP.cXZECwOwvxCLTwpnK2l3TAHaE7%26pid%3DApi&f=1&ipt=a7083c6c3178beed48a1bfe3c62a86360491e732eb671367ba82cefb91ee78e8&ipo=images", false }
                });

            migrationBuilder.InsertData(
                table: "WoodTypes",
                columns: new[] { "Id", "Color", "Description", "Hardness", "ImageUrl", "IsDeleted", "WoodTypeName" },
                values: new object[,]
                {
                    { new Guid("3cc89bb7-bdd2-4e3b-bfc5-c62b95f6bccf"), "Brown", "Strong and durable wood.", "Hard", "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse4.mm.bing.net%2Fth%3Fid%3DOIP.43bK9jhL8WAmkOkIQ0kigQAAAA%26pid%3DApi&f=1&ipt=d6fe3a146fe401c9d507220a22fbd4e67589dd1ff882ba521587a4dfd15c2150&ipo=images", false, "Oak" },
                    { new Guid("8b644971-56b0-4caf-8055-3c5178512cbc"), "Light Brown", "Soft and easy to carve.", "Soft", "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse4.mm.bing.net%2Fth%3Fid%3DOIP.AhNSJ607gFDKq2tJVj04PAHaHa%26pid%3DApi&f=1&ipt=af59f17af437a8f74c8918e8efa275debb06f137b85caca46ea2d9f52b4206f7&ipo=images", false, "Pine" },
                    { new Guid("ed86f019-20de-4478-be30-ef28801fe50d"), "Light Yellow", "Smooth and fine grain.", "Hard", "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse4.mm.bing.net%2Fth%3Fid%3DOIP.3WqYsu_iHHF2EwQBMSo_2QHaFj%26pid%3DApi&f=1&ipt=588254c3f1bf1a405f58576b6ea909fe48941a6eb7e8595d1f0a5401639af129&ipo=images", false, "Maple" }
                });

            migrationBuilder.InsertData(
                table: "Exhibitions",
                columns: new[] { "Id", "Address", "CityId", "EndDate", "ExhibitionName", "ImageUrl", "IsDeleted", "StartDate" },
                values: new object[,]
                {
                    { new Guid("0cf6d5a3-1025-4fe1-ad94-a431f25bacd7"), "789 Timber Ln, Timbertown", new Guid("c30fe708-a15c-42e8-b088-23d6ce4619fd"), new DateTime(2025, 1, 12, 0, 19, 26, 592, DateTimeKind.Local).AddTicks(8988), "Wood Wonders", "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse1.mm.bing.net%2Fth%3Fid%3DOIP.gP6YVmx0me7chml0CfEjJQHaE7%26pid%3DApi&f=1&ipt=e4699c09783b072c48ec34083cff6c09ff9d3ff21d5c0e20b3448b8103515085&ipo=images", false, new DateTime(2025, 1, 2, 0, 19, 26, 592, DateTimeKind.Local).AddTicks(8985) },
                    { new Guid("bda37e99-eb32-4b8a-b28a-4cace7b19f24"), "123 Wood St, Woodville", new Guid("b2c8c39d-571a-4e0b-83d8-7f062083537e"), new DateTime(2024, 11, 28, 0, 19, 26, 592, DateTimeKind.Local).AddTicks(8959), "Carving Masterpieces", "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse4.mm.bing.net%2Fth%3Fid%3DOIP.5AIjLKw8FF0znP-xTYcqAgHaE8%26pid%3DApi&f=1&ipt=7860abb41f4893b00da021463660c566dfc0459e07b71cea469e9e3cebdb313d&ipo=images", false, new DateTime(2024, 11, 18, 0, 19, 26, 592, DateTimeKind.Local).AddTicks(8906) },
                    { new Guid("d28bb261-5ff3-432c-b1fd-162f0280c936"), "456 Forest Rd, Carverton", new Guid("c30fe708-a15c-42e8-b088-23d6ce4619fd"), new DateTime(2024, 12, 28, 0, 19, 26, 592, DateTimeKind.Local).AddTicks(8972), "Timber Treasures", "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse4.mm.bing.net%2Fth%3Fid%3DOIP.4uc16-_EvMHf3xL2hw07tAHaFc%26pid%3DApi&f=1&ipt=005208838b6ff8a3aa2ed3d7227f8f6a864861c9ca5bbe38885186cdb43c14ea&ipo=images", false, new DateTime(2024, 12, 8, 0, 19, 26, 592, DateTimeKind.Local).AddTicks(8969) }
                });

            migrationBuilder.InsertData(
                table: "Woodcarvers",
                columns: new[] { "Id", "Age", "CityId", "FirstName", "ImageUrl", "IsDeleted", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("78632fd1-22d1-4a9d-9120-1da7383d8632"), 40, new Guid("b2c8c39d-571a-4e0b-83d8-7f062083537e"), "John", "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse3.mm.bing.net%2Fth%3Fid%3DOIP.D0NryDHtkGfNWsDJGWhgIAHaEn%26pid%3DApi&f=1&ipt=9af9921628ca9667e11cfe3f94c0fd575e9a054f656f9f81513e5e93b82a299b&ipo=images", false, "Smith", "1234567890" },
                    { new Guid("bab54739-3aec-46c9-a56c-4038c2a409b8"), 50, new Guid("b2c8c39d-571a-4e0b-83d8-7f062083537e"), "Mike", "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse4.mm.bing.net%2Fth%3Fid%3DOIP.q2r57nZolKwjgkyxCrcwpAAAAA%26pid%3DApi&f=1&ipt=aa9dd349fa627c74a19608289fa26b7053f42851374ba7821e123824ffa6761f&ipo=images", false, "Brown", "1859346950" },
                    { new Guid("dd483604-dcec-4d01-9b5b-07543a563c4a"), 35, new Guid("c30fe708-a15c-42e8-b088-23d6ce4619fd"), "Jane", "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse1.mm.bing.net%2Fth%3Fid%3DOIP.wNqNYoHm96LzuJ1RHoqLNwHaEw%26pid%3DApi&f=1&ipt=8221c35ca919cf17ee6528b27d3405a2b17d96a70abf4bdb852dfb574e424162&ipo=images", false, "Doe", "9574868501" }
                });

            migrationBuilder.InsertData(
                table: "Woodcarvings",
                columns: new[] { "Id", "Description", "ImageUrl", "IsAvailable", "IsDeleted", "Title", "WoodTypeId", "WoodcarverId" },
                values: new object[,]
                {
                    { new Guid("02420c28-4001-472d-b491-f674c60443b3"), "An intricate tree design.", "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse2.mm.bing.net%2Fth%3Fid%3DOIP.7AIR0U_1vabQEcaWP39fKAHaFj%26pid%3DApi&f=1&ipt=8d109a1f95ee445a0815ceddf771b0d25c14fb05a1ee2147bf6ad2a599118d29&ipo=images", true, false, "Tree of Life", new Guid("3cc89bb7-bdd2-4e3b-bfc5-c62b95f6bccf"), new Guid("78632fd1-22d1-4a9d-9120-1da7383d8632") },
                    { new Guid("2a99a6a1-ab30-45f3-8d70-6d10fc679800"), "An eagle in flight.", "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse3.mm.bing.net%2Fth%3Fid%3DOIP.OUAMeKpuOr3HY0fYXokYXgHaIy%26pid%3DApi&f=1&ipt=cf537df6b293519170a2f05fc58ad58fdafc80afdf748e406ba2bf470728b3c6&ipo=images", true, false, "Eagle Soar", new Guid("3cc89bb7-bdd2-4e3b-bfc5-c62b95f6bccf"), new Guid("bab54739-3aec-46c9-a56c-4038c2a409b8") },
                    { new Guid("96224008-7927-4af8-a7f1-6260ff1f7b09"), "A carving of a mystical forest spirit.", "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse3.mm.bing.net%2Fth%3Fid%3DOIP.C4hztbq1A6ryriYBtGpuxwHaHa%26pid%3DApi&f=1&ipt=d0fb7717a71aa3a7afecc0e419519ecfc44ccd364cca8a86cfb89bd6b76aaab6&ipo=images", true, false, "Forest Spirit", new Guid("3cc89bb7-bdd2-4e3b-bfc5-c62b95f6bccf"), new Guid("78632fd1-22d1-4a9d-9120-1da7383d8632") },
                    { new Guid("daef10a0-76da-46c1-8e2d-c0f4b328688d"), "Abstract carving of a flowing river.", "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse3.mm.bing.net%2Fth%3Fid%3DOIP.RxwmJdt5sjf40KE4jB5MhAHaEK%26pid%3DApi&f=1&ipt=2f85a36c5768e5ec94f08599ca7857017d1bca99704f0af4fff233bc34dab831&ipo=images", true, false, "River Flow", new Guid("8b644971-56b0-4caf-8055-3c5178512cbc"), new Guid("78632fd1-22d1-4a9d-9120-1da7383d8632") },
                    { new Guid("e3877536-f9b7-40a8-9157-8656a1c0f79b"), "A mighty bear.", "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse4.mm.bing.net%2Fth%3Fid%3DOIP.DSkNji5zpPJG0ISXrzUgtAHaJ4%26pid%3DApi&f=1&ipt=2187f9344f88b67979b8289ea3466339cf13fdc1041538582cb7dd96e421b4a8&ipo=images", false, false, "Bear Strength", new Guid("3cc89bb7-bdd2-4e3b-bfc5-c62b95f6bccf"), new Guid("dd483604-dcec-4d01-9b5b-07543a563c4a") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Exhibitions",
                keyColumn: "Id",
                keyValue: new Guid("0cf6d5a3-1025-4fe1-ad94-a431f25bacd7"));

            migrationBuilder.DeleteData(
                table: "Exhibitions",
                keyColumn: "Id",
                keyValue: new Guid("bda37e99-eb32-4b8a-b28a-4cace7b19f24"));

            migrationBuilder.DeleteData(
                table: "Exhibitions",
                keyColumn: "Id",
                keyValue: new Guid("d28bb261-5ff3-432c-b1fd-162f0280c936"));

            migrationBuilder.DeleteData(
                table: "WoodTypes",
                keyColumn: "Id",
                keyValue: new Guid("ed86f019-20de-4478-be30-ef28801fe50d"));

            migrationBuilder.DeleteData(
                table: "Woodcarvings",
                keyColumn: "Id",
                keyValue: new Guid("02420c28-4001-472d-b491-f674c60443b3"));

            migrationBuilder.DeleteData(
                table: "Woodcarvings",
                keyColumn: "Id",
                keyValue: new Guid("2a99a6a1-ab30-45f3-8d70-6d10fc679800"));

            migrationBuilder.DeleteData(
                table: "Woodcarvings",
                keyColumn: "Id",
                keyValue: new Guid("96224008-7927-4af8-a7f1-6260ff1f7b09"));

            migrationBuilder.DeleteData(
                table: "Woodcarvings",
                keyColumn: "Id",
                keyValue: new Guid("daef10a0-76da-46c1-8e2d-c0f4b328688d"));

            migrationBuilder.DeleteData(
                table: "Woodcarvings",
                keyColumn: "Id",
                keyValue: new Guid("e3877536-f9b7-40a8-9157-8656a1c0f79b"));

            migrationBuilder.DeleteData(
                table: "WoodTypes",
                keyColumn: "Id",
                keyValue: new Guid("3cc89bb7-bdd2-4e3b-bfc5-c62b95f6bccf"));

            migrationBuilder.DeleteData(
                table: "WoodTypes",
                keyColumn: "Id",
                keyValue: new Guid("8b644971-56b0-4caf-8055-3c5178512cbc"));

            migrationBuilder.DeleteData(
                table: "Woodcarvers",
                keyColumn: "Id",
                keyValue: new Guid("78632fd1-22d1-4a9d-9120-1da7383d8632"));

            migrationBuilder.DeleteData(
                table: "Woodcarvers",
                keyColumn: "Id",
                keyValue: new Guid("bab54739-3aec-46c9-a56c-4038c2a409b8"));

            migrationBuilder.DeleteData(
                table: "Woodcarvers",
                keyColumn: "Id",
                keyValue: new Guid("dd483604-dcec-4d01-9b5b-07543a563c4a"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("b2c8c39d-571a-4e0b-83d8-7f062083537e"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("c30fe708-a15c-42e8-b088-23d6ce4619fd"));
        }
    }
}
