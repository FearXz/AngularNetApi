using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AngularNetApi.Migrations
{
    /// <inheritdoc />
    public partial class changenameweekday : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JoinStoreOpeningDayId",
                table: "JoinStoreWeekDays",
                newName: "JoinStoreWeekDayId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JoinStoreWeekDayId",
                table: "JoinStoreWeekDays",
                newName: "JoinStoreOpeningDayId");
        }
    }
}
