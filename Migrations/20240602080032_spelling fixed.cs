using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo_App_Api.Migrations
{
    /// <inheritdoc />
    public partial class spellingfixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompetedDate",
                table: "Todos",
                newName: "CompletedDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompletedDate",
                table: "Todos",
                newName: "CompetedDate");
        }
    }
}
