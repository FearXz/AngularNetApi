using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AngularNetApi.Migrations
{
    /// <inheritdoc />
    public partial class addedOpDaysCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosingTime",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "OpeningTime",
                table: "Stores");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "OpeningDays",
                columns: table => new
                {
                    OpeningDaysId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DaysNumber = table.Column<int>(type: "int", nullable: false),
                    DaysName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpeningTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    ClosingTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    OpeningTime2 = table.Column<TimeSpan>(type: "time", nullable: true),
                    ClosingTime2 = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningDays", x => x.OpeningDaysId);
                });

            migrationBuilder.CreateTable(
                name: "JoinStoreCategory",
                columns: table => new
                {
                    JoinStoreCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinStoreCategory", x => x.JoinStoreCategoryId);
                    table.ForeignKey(
                        name: "FK_JoinStoreCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JoinStoreCategory_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JoinStoreOpeningDays",
                columns: table => new
                {
                    JoinStoreOpeningDaysId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    OpeningDaysId = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_JoinStoreCategory_CategoryId",
                table: "JoinStoreCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_JoinStoreCategory_StoreId",
                table: "JoinStoreCategory",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_JoinStoreOpeningDays_OpeningDaysId",
                table: "JoinStoreOpeningDays",
                column: "OpeningDaysId");

            migrationBuilder.CreateIndex(
                name: "IX_JoinStoreOpeningDays_StoreId",
                table: "JoinStoreOpeningDays",
                column: "StoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JoinStoreCategory");

            migrationBuilder.DropTable(
                name: "JoinStoreOpeningDays");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "OpeningDays");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ClosingTime",
                table: "Stores",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "OpeningTime",
                table: "Stores",
                type: "time",
                nullable: true);
        }
    }
}
