using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingPartnerAPI.Migrations
{
    /// <inheritdoc />
    public partial class Blood_Group_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MyProperty",
                table: "Employees",
                newName: "BloodGroup");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BloodGroup",
                table: "Employees",
                newName: "MyProperty");
        }
    }
}
