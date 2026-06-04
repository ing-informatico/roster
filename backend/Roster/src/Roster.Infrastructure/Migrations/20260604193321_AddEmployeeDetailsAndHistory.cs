using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Roster.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeDetailsAndHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                schema: "roster",
                table: "empleado",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Facturable",
                schema: "roster",
                table: "empleado",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "JefeInmediato",
                schema: "roster",
                table: "empleado",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Modalidad",
                schema: "roster",
                table: "empleado",
                type: "character varying(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Moneda",
                schema: "roster",
                table: "empleado",
                type: "character varying(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "USD");

            migrationBuilder.AddColumn<string>(
                name: "Puesto",
                schema: "roster",
                table: "empleado",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SalarioActual",
                schema: "roster",
                table: "empleado",
                type: "numeric(12,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefono1",
                schema: "roster",
                table: "empleado",
                type: "character varying(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefono2",
                schema: "roster",
                table: "empleado",
                type: "character varying(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "empleado_bono",
                schema: "roster",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmpleadoId = table.Column<long>(type: "bigint", nullable: false),
                    Tipo = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Monto = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
                    Moneda = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false, defaultValue: "USD"),
                    OtorgadoEn = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empleado_bono", x => x.Id);
                    table.ForeignKey(
                        name: "FK_empleado_bono_empleado_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalSchema: "roster",
                        principalTable: "empleado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "empleado_evaluacion",
                schema: "roster",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmpleadoId = table.Column<long>(type: "bigint", nullable: false),
                    Periodo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Rate = table.Column<decimal>(type: "numeric(3,1)", nullable: false),
                    EscalaMax = table.Column<decimal>(type: "numeric(3,1)", nullable: false, defaultValue: 5m),
                    Comentario = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    EvaluadoEn = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empleado_evaluacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_empleado_evaluacion_empleado_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalSchema: "roster",
                        principalTable: "empleado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "empleado_historial_puesto",
                schema: "roster",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmpleadoId = table.Column<long>(type: "bigint", nullable: false),
                    Puesto = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Desde = table.Column<DateOnly>(type: "date", nullable: false),
                    Hasta = table.Column<DateOnly>(type: "date", nullable: true),
                    Motivo = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empleado_historial_puesto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_empleado_historial_puesto_empleado_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalSchema: "roster",
                        principalTable: "empleado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "empleado_historial_salario",
                schema: "roster",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmpleadoId = table.Column<long>(type: "bigint", nullable: false),
                    Salario = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
                    Moneda = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false, defaultValue: "USD"),
                    Desde = table.Column<DateOnly>(type: "date", nullable: false),
                    Hasta = table.Column<DateOnly>(type: "date", nullable: true),
                    Motivo = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empleado_historial_salario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_empleado_historial_salario_empleado_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalSchema: "roster",
                        principalTable: "empleado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_empleado_bono_EmpleadoId",
                schema: "roster",
                table: "empleado_bono",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_empleado_evaluacion_EmpleadoId",
                schema: "roster",
                table: "empleado_evaluacion",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_empleado_historial_puesto_EmpleadoId",
                schema: "roster",
                table: "empleado_historial_puesto",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_empleado_historial_salario_EmpleadoId",
                schema: "roster",
                table: "empleado_historial_salario",
                column: "EmpleadoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "empleado_bono",
                schema: "roster");

            migrationBuilder.DropTable(
                name: "empleado_evaluacion",
                schema: "roster");

            migrationBuilder.DropTable(
                name: "empleado_historial_puesto",
                schema: "roster");

            migrationBuilder.DropTable(
                name: "empleado_historial_salario",
                schema: "roster");

            migrationBuilder.DropColumn(
                name: "Direccion",
                schema: "roster",
                table: "empleado");

            migrationBuilder.DropColumn(
                name: "Facturable",
                schema: "roster",
                table: "empleado");

            migrationBuilder.DropColumn(
                name: "JefeInmediato",
                schema: "roster",
                table: "empleado");

            migrationBuilder.DropColumn(
                name: "Modalidad",
                schema: "roster",
                table: "empleado");

            migrationBuilder.DropColumn(
                name: "Moneda",
                schema: "roster",
                table: "empleado");

            migrationBuilder.DropColumn(
                name: "Puesto",
                schema: "roster",
                table: "empleado");

            migrationBuilder.DropColumn(
                name: "SalarioActual",
                schema: "roster",
                table: "empleado");

            migrationBuilder.DropColumn(
                name: "Telefono1",
                schema: "roster",
                table: "empleado");

            migrationBuilder.DropColumn(
                name: "Telefono2",
                schema: "roster",
                table: "empleado");
        }
    }
}
