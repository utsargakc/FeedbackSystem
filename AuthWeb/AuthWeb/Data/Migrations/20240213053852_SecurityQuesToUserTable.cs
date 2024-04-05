using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class SecurityQuesToUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SecurityQuestions");

            migrationBuilder.AddColumn<string>(
                name: "SecurityAns1",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecurityAns2",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecurityAns3",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecurityQn1",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecurityQn2",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecurityQn3",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecurityAns1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SecurityAns2",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SecurityAns3",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SecurityQn1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SecurityQn2",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SecurityQn3",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "SecurityQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SecurityAns1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecurityAns2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecurityAns3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecurityQn1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecurityQn2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecurityQn3 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecurityQuestions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SecurityQuestions_UserId",
                table: "SecurityQuestions",
                column: "UserId");
        }
    }
}
