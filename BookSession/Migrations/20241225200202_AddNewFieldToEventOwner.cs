using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookSession.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFieldToEventOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EventOwner",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventOwner",
                table: "Events");
        }
    }
}
