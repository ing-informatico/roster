using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Roster.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "roster");

            migrationBuilder.CreateTable(
                name: "departamento",
                schema: "roster",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pais",
                schema: "roster",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "empleado",
                schema: "roster",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    NombreCompleto = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Correo = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    FechaIngreso = table.Column<DateOnly>(type: "date", nullable: true),
                    FechaNacimiento = table.Column<DateOnly>(type: "date", nullable: true),
                    Activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    PaisId = table.Column<short>(type: "smallint", nullable: true),
                    DepartamentoId = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empleado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_empleado_departamento_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalSchema: "roster",
                        principalTable: "departamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_empleado_pais_PaisId",
                        column: x => x.PaisId,
                        principalSchema: "roster",
                        principalTable: "pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_departamento_Nombre",
                schema: "roster",
                table: "departamento",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_empleado_Codigo",
                schema: "roster",
                table: "empleado",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_empleado_Correo",
                schema: "roster",
                table: "empleado",
                column: "Correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_empleado_DepartamentoId",
                schema: "roster",
                table: "empleado",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_empleado_PaisId",
                schema: "roster",
                table: "empleado",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_pais_Nombre",
                schema: "roster",
                table: "pais",
                column: "Nombre",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "empleado",
                schema: "roster");

            migrationBuilder.DropTable(
                name: "departamento",
                schema: "roster");

            migrationBuilder.DropTable(
                name: "pais",
                schema: "roster");
        }
    }
}
