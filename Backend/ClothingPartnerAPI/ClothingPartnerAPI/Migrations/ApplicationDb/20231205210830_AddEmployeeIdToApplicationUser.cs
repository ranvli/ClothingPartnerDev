﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingPartnerAPI.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class AddEmployeeIdToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "AspNetUsers");
        }
    }
}
