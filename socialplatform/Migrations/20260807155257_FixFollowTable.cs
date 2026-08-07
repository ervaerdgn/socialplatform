using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace socialplatform.Migrations
{
    /// <inheritdoc />
    public partial class FixFollowTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "time",
                table: "Follows",
                newName: "zaman");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "zaman",
                table: "Follows",
                newName: "time");
        }
    }
}
