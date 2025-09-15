using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolFees.API.Migrations
{
    /// <inheritdoc />
    public partial class AddInstitucionTipoPago : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdInstitucion",
                table: "TipoPago",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Institucion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdTipoInstitucion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institucion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Institucion_TipoInstitucion_IdTipoInstitucion",
                        column: x => x.IdTipoInstitucion,
                        principalTable: "TipoInstitucion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TipoPago_IdInstitucion",
                table: "TipoPago",
                column: "IdInstitucion");

            migrationBuilder.CreateIndex(
                name: "IX_Institucion_IdTipoInstitucion",
                table: "Institucion",
                column: "IdTipoInstitucion");

            migrationBuilder.AddForeignKey(
                name: "FK_TipoPago_Institucion_IdInstitucion",
                table: "TipoPago",
                column: "IdInstitucion",
                principalTable: "Institucion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TipoPago_Institucion_IdInstitucion",
                table: "TipoPago");

            migrationBuilder.DropTable(
                name: "Institucion");

            migrationBuilder.DropIndex(
                name: "IX_TipoPago_IdInstitucion",
                table: "TipoPago");

            migrationBuilder.DropColumn(
                name: "IdInstitucion",
                table: "TipoPago");
        }
    }
}
