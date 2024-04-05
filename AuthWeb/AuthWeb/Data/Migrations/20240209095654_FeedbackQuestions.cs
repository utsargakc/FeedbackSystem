using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class FeedbackQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecurityQn3",
                table: "Questions",
                newName: "FeedbackQn3");

            migrationBuilder.RenameColumn(
                name: "SecurityQn2",
                table: "Questions",
                newName: "FeedbackQn2");

            migrationBuilder.RenameColumn(
                name: "SecurityQn1",
                table: "Questions",
                newName: "FeedbackQn1");

            migrationBuilder.RenameColumn(
                name: "SecurityAns3",
                table: "Questions",
                newName: "FeedbackAns3");

            migrationBuilder.RenameColumn(
                name: "SecurityAns2",
                table: "Questions",
                newName: "FeedbackAns2");

            migrationBuilder.RenameColumn(
                name: "SecurityAns1",
                table: "Questions",
                newName: "FeedbackAns1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FeedbackQn3",
                table: "Questions",
                newName: "SecurityQn3");

            migrationBuilder.RenameColumn(
                name: "FeedbackQn2",
                table: "Questions",
                newName: "SecurityQn2");

            migrationBuilder.RenameColumn(
                name: "FeedbackQn1",
                table: "Questions",
                newName: "SecurityQn1");

            migrationBuilder.RenameColumn(
                name: "FeedbackAns3",
                table: "Questions",
                newName: "SecurityAns3");

            migrationBuilder.RenameColumn(
                name: "FeedbackAns2",
                table: "Questions",
                newName: "SecurityAns2");

            migrationBuilder.RenameColumn(
                name: "FeedbackAns1",
                table: "Questions",
                newName: "SecurityAns1");
        }
    }
}
