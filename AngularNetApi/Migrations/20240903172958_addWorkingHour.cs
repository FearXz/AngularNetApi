using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AngularNetApi.Migrations
{
    /// <inheritdoc />
    public partial class addWorkingHour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkingHours",
                columns: table => new
                {
                    WorkingHourId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JoinStoreWeekDayId = table.Column<int>(type: "int", nullable: false),
                    ShiftOrder = table.Column<int>(type: "int", nullable: false),
                    OpeningTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    ClosingTime = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingHours", x => x.WorkingHourId);
                    table.ForeignKey(
                        name: "FK_WorkingHours_JoinStoreWeekDays_JoinStoreWeekDayId",
                        column: x => x.JoinStoreWeekDayId,
                        principalTable: "JoinStoreWeekDays",
                        principalColumn: "JoinStoreOpeningDayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHours_JoinStoreWeekDayId",
                table: "WorkingHours",
                column: "JoinStoreWeekDayId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkingHours");
        }
    }
}
