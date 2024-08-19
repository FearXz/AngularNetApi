using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AngularNetApi.Migrations
{
    /// <inheritdoc />
    public partial class changeEntitesBaseClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserProfiles",
                newName: "UserProfileId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CompanyProfiles",
                newName: "CompanyProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserProfileId",
                table: "UserProfiles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CompanyProfileId",
                table: "CompanyProfiles",
                newName: "Id");
        }
    }
}
