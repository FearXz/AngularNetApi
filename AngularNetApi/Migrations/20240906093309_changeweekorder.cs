using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AngularNetApi.Migrations
{
    /// <inheritdoc />
    public partial class changeweekorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WeekDayNumber",
                table: "WeekDays",
                newName: "WeekDayCode");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "WeekDays",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "WeekDays");

            migrationBuilder.RenameColumn(
                name: "WeekDayCode",
                table: "WeekDays",
                newName: "WeekDayNumber");
        }
    }
}
