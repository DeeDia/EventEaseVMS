using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventEaseVMS.Migrations
{
    /// <inheritdoc />
    public partial class SeedVenuesAndEventTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EventTypes",
                columns: new[] { "EventTypeId", "Description", "TypeName" },
                values: new object[,]
                {
                    { 1, null, "Wedding" },
                    { 2, null, "Conference" },
                    { 3, null, "Birthday Party" },
                    { 4, null, "Corporate Event" },
                    { 5, null, "Concert" },
                    { 6, null, "Graduation" },
                    { 7, null, "Baby Shower" }
                });

            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "VenueId", "Capacity", "Description", "ImageUrl", "IsActive", "Location", "VenueName" },
                values: new object[,]
                {
                    { 1, 200, null, null, true, "Johannesburg", "Shepstone Gardens" },
                    { 2, 500, null, null, true, "Fourways", "Montecasino" },
                    { 3, 1000, null, null, true, "Sandton", "Sandton Convention Centre" },
                    { 4, 150, null, null, true, "Muldersdrift", "Glenburn Lodge" },
                    { 5, 300, null, null, true, "Muldersdrift", "Avianto Estate" },
                    { 6, 250, null, null, true, "Bryanston", "The Forum Bryanston" },
                    { 7, 800, null, null, true, "North West", "Sun City Resort" },
                    { 8, 400, null, null, true, "Stellenbosch", "Spier Wine Farm" },
                    { 9, 180, null, null, true, "Umhlanga", "The Oyster Box" },
                    { 10, 1200, null, null, true, "Cape Town", "CTICC" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueId",
                keyValue: 10);
        }
    }
}
