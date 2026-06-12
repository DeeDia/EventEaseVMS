using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventEaseVMS.Migrations
{
    /// <inheritdoc />
    public partial class SeedEventTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 1,
                columns: new[] { "Description", "TypeName" },
                values: new object[] { "Business conferences and professional summits", "Conference" });

            migrationBuilder.UpdateData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 2,
                columns: new[] { "Description", "TypeName" },
                values: new object[] { "Wedding ceremonies and receptions", "Wedding" });

            migrationBuilder.UpdateData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 3,
                columns: new[] { "Description", "TypeName" },
                values: new object[] { "Live music performances and shows", "Concert" });

            migrationBuilder.UpdateData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 4,
                columns: new[] { "Description", "TypeName" },
                values: new object[] { "Birthday celebrations and parties", "Birthday" });

            migrationBuilder.UpdateData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 5,
                columns: new[] { "Description", "TypeName" },
                values: new object[] { "Team events, award ceremonies, launches", "Corporate Function" });

            migrationBuilder.UpdateData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 6,
                columns: new[] { "Description", "TypeName" },
                values: new object[] { "Art shows, trade expos, displays", "Exhibition" });

            migrationBuilder.UpdateData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 7,
                columns: new[] { "Description", "TypeName" },
                values: new object[] { "Training sessions and interactive workshops", "Workshop" });

            migrationBuilder.InsertData(
                table: "EventTypes",
                columns: new[] { "EventTypeId", "Description", "TypeName" },
                values: new object[] { 8, "Formal dinners and fundraising galas", "Gala Dinner" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 8);

            migrationBuilder.UpdateData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 1,
                columns: new[] { "Description", "TypeName" },
                values: new object[] { null, "Wedding" });

            migrationBuilder.UpdateData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 2,
                columns: new[] { "Description", "TypeName" },
                values: new object[] { null, "Conference" });

            migrationBuilder.UpdateData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 3,
                columns: new[] { "Description", "TypeName" },
                values: new object[] { null, "Birthday Party" });

            migrationBuilder.UpdateData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 4,
                columns: new[] { "Description", "TypeName" },
                values: new object[] { null, "Corporate Event" });

            migrationBuilder.UpdateData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 5,
                columns: new[] { "Description", "TypeName" },
                values: new object[] { null, "Concert" });

            migrationBuilder.UpdateData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 6,
                columns: new[] { "Description", "TypeName" },
                values: new object[] { null, "Graduation" });

            migrationBuilder.UpdateData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 7,
                columns: new[] { "Description", "TypeName" },
                values: new object[] { null, "Baby Shower" });
        }
    }
}
