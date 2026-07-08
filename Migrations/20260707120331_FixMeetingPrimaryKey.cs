using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GACHSLApi.Migrations
{
    /// <inheritdoc />
    public partial class FixMeetingPrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Meetings",
                newName: "MeetingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MeetingId",
                table: "Meetings",
                newName: "Id");
        }
    }
}
