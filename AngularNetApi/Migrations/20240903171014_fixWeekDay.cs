using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AngularNetApi.Migrations
{
    /// <inheritdoc />
    public partial class fixWeekDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JoinStoreCategory_Category_CategoryId",
                table: "JoinStoreCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_JoinStoreCategory_Stores_StoreId",
                table: "JoinStoreCategory");

            migrationBuilder.DropTable(
                name: "JoinStoreOpeningDays");

            migrationBuilder.DropTable(
                name: "OpeningDays");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JoinStoreCategory",
                table: "JoinStoreCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "JoinStoreCategory",
                newName: "JoinStoreCategories");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_JoinStoreCategory_StoreId",
                table: "JoinStoreCategories",
                newName: "IX_JoinStoreCategories_StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_JoinStoreCategory_CategoryId",
                table: "JoinStoreCategories",
                newName: "IX_JoinStoreCategories_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JoinStoreCategories",
                table: "JoinStoreCategories",
                column: "JoinStoreCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "CategoryId");

            migrationBuilder.CreateTable(
                name: "WeekDays",
                columns: table => new
                {
                    WeekDayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeekDayNumber = table.Column<int>(type: "int", nullable: false),
                    WeekDayName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekDays", x => x.WeekDayId);
                });

            migrationBuilder.CreateTable(
                name: "JoinStoreWeekDays",
                columns: table => new
                {
                    JoinStoreOpeningDayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    WeekDayId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinStoreWeekDays", x => x.JoinStoreOpeningDayId);
                    table.ForeignKey(
                        name: "FK_JoinStoreWeekDays_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JoinStoreWeekDays_WeekDays_WeekDayId",
                        column: x => x.WeekDayId,
                        principalTable: "WeekDays",
                        principalColumn: "WeekDayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JoinStoreWeekDays_StoreId",
                table: "JoinStoreWeekDays",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_JoinStoreWeekDays_WeekDayId",
                table: "JoinStoreWeekDays",
                column: "WeekDayId");

            migrationBuilder.AddForeignKey(
                name: "FK_JoinStoreCategories_Categories_CategoryId",
                table: "JoinStoreCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JoinStoreCategories_Stores_StoreId",
                table: "JoinStoreCategories",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JoinStoreCategories_Categories_CategoryId",
                table: "JoinStoreCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_JoinStoreCategories_Stores_StoreId",
                table: "JoinStoreCategories");

            migrationBuilder.DropTable(
                name: "JoinStoreWeekDays");

            migrationBuilder.DropTable(
                name: "WeekDays");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JoinStoreCategories",
                table: "JoinStoreCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "JoinStoreCategories",
                newName: "JoinStoreCategory");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_JoinStoreCategories_StoreId",
                table: "JoinStoreCategory",
                newName: "IX_JoinStoreCategory_StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_JoinStoreCategories_CategoryId",
                table: "JoinStoreCategory",
                newName: "IX_JoinStoreCategory_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JoinStoreCategory",
                table: "JoinStoreCategory",
                column: "JoinStoreCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "CategoryId");

            migrationBuilder.CreateTable(
                name: "OpeningDays",
                columns: table => new
                {
                    OpeningDaysId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClosingTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    ClosingTime2 = table.Column<TimeSpan>(type: "time", nullable: true),
                    DaysName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DaysNumber = table.Column<int>(type: "int", nullable: false),
                    OpeningTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    OpeningTime2 = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningDays", x => x.OpeningDaysId);
                });

            migrationBuilder.CreateTable(
                name: "JoinStoreOpeningDays",
                columns: table => new
                {
                    JoinStoreOpeningDaysId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpeningDaysId = table.Column<int>(type: "int", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinStoreOpeningDays", x => x.JoinStoreOpeningDaysId);
                    table.ForeignKey(
                        name: "FK_JoinStoreOpeningDays_OpeningDays_OpeningDaysId",
                        column: x => x.OpeningDaysId,
                        principalTable: "OpeningDays",
                        principalColumn: "OpeningDaysId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JoinStoreOpeningDays_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JoinStoreOpeningDays_OpeningDaysId",
                table: "JoinStoreOpeningDays",
                column: "OpeningDaysId");

            migrationBuilder.CreateIndex(
                name: "IX_JoinStoreOpeningDays_StoreId",
                table: "JoinStoreOpeningDays",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_JoinStoreCategory_Category_CategoryId",
                table: "JoinStoreCategory",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JoinStoreCategory_Stores_StoreId",
                table: "JoinStoreCategory",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
