using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aeronaves.WebApi.Migrations
{
    public partial class MigrationSQLServerInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aeronave",
                columns: table => new
                {
                    AeronaveId = table.Column<Guid>(nullable: false),
                    Marca = table.Column<string>(nullable: true),
                    Modelo = table.Column<string>(nullable: true),
                    NroAsientos = table.Column<int>(nullable: false),
                    CapacidadCarga = table.Column<decimal>(nullable: false),
                    CapTanqueCombustible = table.Column<decimal>(nullable: false),
                    AereopuertoEstacionamiento = table.Column<string>(nullable: true),
                    EstadoAeronave = table.Column<string>(nullable: true),
                    AeronaveGuid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aeronave", x => x.AeronaveId);
                });

            migrationBuilder.CreateTable(
                name: "AeronaveAsientos",
                columns: table => new
                {
                    AeronaveAsientosId = table.Column<Guid>(nullable: false),
                    ClaseAsiento = table.Column<string>(nullable: true),
                    Ubicacion = table.Column<string>(nullable: true),
                    NroSilla = table.Column<int>(nullable: false),
                    EstadoAsiento = table.Column<string>(nullable: true),
                    AeronaveId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AeronaveAsientos", x => x.AeronaveAsientosId);
                    table.ForeignKey(
                        name: "FK_AeronaveAsientos_Aeronave_AeronaveId",
                        column: x => x.AeronaveId,
                        principalTable: "Aeronave",
                        principalColumn: "AeronaveId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AeronaveAsientos_AeronaveId",
                table: "AeronaveAsientos",
                column: "AeronaveId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AeronaveAsientos");

            migrationBuilder.DropTable(
                name: "Aeronave");
        }
    }
}
