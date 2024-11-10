using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservas.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Local",
                columns: table => new
                {
                    IdLocal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Acessibilidade = table.Column<bool>(type: "bit", nullable: false),
                    EstacionamentoDisponivel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cep = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Local", x => x.IdLocal);
                });

            migrationBuilder.CreateTable(
                name: "Participantes",
                columns: table => new
                {
                    IdParticipante = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participantes", x => x.IdParticipante);
                });

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    EventoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrecoIngresso = table.Column<double>(type: "float", nullable: false),
                    LocalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.EventoId);
                    table.ForeignKey(
                        name: "FK_Eventos_Local_LocalId",
                        column: x => x.LocalId,
                        principalTable: "Local",
                        principalColumn: "IdLocal",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventoParticipantes",
                columns: table => new
                {
                    EventoId = table.Column<int>(type: "int", nullable: false),
                    ParticipanteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventoParticipantes", x => new { x.EventoId, x.ParticipanteId });
                    table.ForeignKey(
                        name: "FK_EventoParticipantes_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "EventoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventoParticipantes_Participantes_ParticipanteId",
                        column: x => x.ParticipanteId,
                        principalTable: "Participantes",
                        principalColumn: "IdParticipante",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    ReservaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventoId = table.Column<int>(type: "int", nullable: false),
                    ParticipanteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.ReservaId);
                    table.ForeignKey(
                        name: "FK_Reservas_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "EventoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservas_Participantes_ParticipanteId",
                        column: x => x.ParticipanteId,
                        principalTable: "Participantes",
                        principalColumn: "IdParticipante",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventoParticipantes_ParticipanteId",
                table: "EventoParticipantes",
                column: "ParticipanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_LocalId",
                table: "Eventos",
                column: "LocalId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_EventoId",
                table: "Reservas",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_ParticipanteId",
                table: "Reservas",
                column: "ParticipanteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventoParticipantes");

            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "Participantes");

            migrationBuilder.DropTable(
                name: "Local");
        }
    }
}
