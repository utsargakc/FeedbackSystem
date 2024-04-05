using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class recurrenceFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "dayOfMonth",
                table: "Topics",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "recurrenceType",
                table: "Topics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "repeatingNumber",
                table: "Topics",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "selectedDays",
                table: "Topics",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dayOfMonth",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "recurrenceType",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "repeatingNumber",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "selectedDays",
                table: "Topics");
        }
    }
}
