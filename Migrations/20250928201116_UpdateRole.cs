using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolFees.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdInstitucion",
                table: "Role",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_IdInstitucion",
                table: "Role",
                column: "IdInstitucion");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Institucion_IdInstitucion",
                table: "Role",
                column: "IdInstitucion",
                principalTable: "Institucion",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role_Institucion_IdInstitucion",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Role_IdInstitucion",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "IdInstitucion",
                table: "Role");
        }
    }
}
