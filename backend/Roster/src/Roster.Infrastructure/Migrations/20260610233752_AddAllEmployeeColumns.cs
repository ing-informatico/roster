using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roster.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAllEmployeeColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CentroCosto",
                schema: "roster",
                table: "empleado",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CentroCostoUbicacion",
                schema: "roster",
                table: "empleado",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EquipoAsignado",
                schema: "roster",
                table: "empleado",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Genero",
                schema: "roster",
                table: "empleado",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdBeneficioHospAngeles",
                schema: "roster",
                table: "empleado",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdCentroCosto",
                schema: "roster",
                table: "empleado",
                type: "character varying(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdObsPoliza",
                schema: "roster",
                table: "empleado",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Manager",
                schema: "roster",
                table: "empleado",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModalidadCompensacion",
                schema: "roster",
                table: "empleado",
                type: "character varying(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumeroFila",
                schema: "roster",
                table: "empleado",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PadreOMadre",
                schema: "roster",
                table: "empleado",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Proyecto",
                schema: "roster",
                table: "empleado",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sdm",
                schema: "roster",
                table: "empleado",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Site",
                schema: "roster",
                table: "empleado",
                type: "character varying(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeamLead",
                schema: "roster",
                table: "empleado",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoSeguro",
                schema: "roster",
                table: "empleado",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CentroCosto",
                schema: "roster",
                table: "empleado");

            migrationBuilder.DropColumn(
                name: "CentroCostoUbicacion",
                schema: "roster",
                table: "empleado");

            migrationBuilder.DropColumn(
                name: "EquipoAsignado",
                schema: "roster",
                table: "empleado");

            migrationBuilder.DropColumn(
                name: "Genero",
                schema: "roster",
                table: "empleado");

            migrationBuilder.DropColumn(
                name: "IdBeneficioHospAngeles",
                schema: "roster",
                table: "empleado");

            migrationBuilder.DropColumn(
                name: "IdCentroCosto",
                schema: "roster",
                table: "empleado");

            migrationBuilder.DropColumn(
                name: "IdObsPoliza",
                schema: "roster",
                table: "empleado");

            migrationBuilder.DropColumn(
                name: "Manager",
                schema: "roster",
                table: "empleado");

            migrationBuilder.DropColumn(
                name: "ModalidadCompensacion",
                schema: "roster",
                table: "empleado");

            migrationBuilder.DropColumn(
                name: "NumeroFila",
                schema: "roster",
                table: "empleado");

            migrationBuilder.DropColumn(
                name: "PadreOMadre",
                schema: "roster",
                table: "empleado");

            migrationBuilder.DropColumn(
                name: "Proyecto",
                schema: "roster",
                table: "empleado");

            migrationBuilder.DropColumn(
                name: "Sdm",
                schema: "roster",
                table: "empleado");

            migrationBuilder.DropColumn(
                name: "Site",
                schema: "roster",
                table: "empleado");

            migrationBuilder.DropColumn(
                name: "TeamLead",
                schema: "roster",
                table: "empleado");

            migrationBuilder.DropColumn(
                name: "TipoSeguro",
                schema: "roster",
                table: "empleado");
        }
    }
}
