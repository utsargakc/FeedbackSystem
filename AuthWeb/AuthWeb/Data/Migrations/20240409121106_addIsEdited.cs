using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class addIsEdited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "isEdited",
                table: "Responses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isEdited",
                table: "Responses");
        }
    }
}
