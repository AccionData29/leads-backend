#pragma warning disable CA1861
using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Leads.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "leads");

            migrationBuilder.CreateTable(
                name: "advisors",
                schema: "leads",
                columns: table => new
                {
                    AsesorId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    PuntoVentaId = table.Column<long>(type: "bigint", nullable: false),
                    EmpresaId = table.Column<long>(type: "bigint", nullable: false),
                    CapacidadDiariaLeads = table.Column<int>(type: "integer", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_advisors", x => x.AsesorId);
                });

            migrationBuilder.CreateTable(
                name: "conversations",
                schema: "leads",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConversacionId = table.Column<string>(type: "text", nullable: false),
                    LeadId = table.Column<long>(type: "bigint", nullable: false),
                    Canal = table.Column<string>(type: "text", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conversations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                schema: "leads",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpresaId = table.Column<long>(type: "bigint", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    TelefonoNormalizado = table.Column<string>(type: "text", nullable: true),
                    EmailNormalizado = table.Column<string>(type: "text", nullable: true),
                    Ciudad = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "lead_assignments",
                schema: "leads",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LeadId = table.Column<long>(type: "bigint", nullable: false),
                    AsesorId = table.Column<long>(type: "bigint", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lead_assignments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lead_enrichments",
                schema: "leads",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LeadId = table.Column<long>(type: "bigint", nullable: false),
                    ModeloInteres = table.Column<string>(type: "text", nullable: true),
                    CuotaInicial = table.Column<decimal>(type: "numeric", nullable: true),
                    FormaPago = table.Column<string>(type: "text", nullable: true),
                    Intencion = table.Column<string>(type: "text", nullable: true),
                    Objecion = table.Column<string>(type: "text", nullable: true),
                    SolicitoCita = table.Column<bool>(type: "boolean", nullable: false),
                    SolicitoCotizacion = table.Column<bool>(type: "boolean", nullable: false),
                    Confianza = table.Column<decimal>(type: "numeric(5,4)", precision: 5, scale: 4, nullable: false),
                    Provider = table.Column<string>(type: "text", nullable: false),
                    PipelineVersion = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lead_enrichments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lead_scores",
                schema: "leads",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LeadId = table.Column<long>(type: "bigint", nullable: false),
                    HistoricalProbability = table.Column<decimal>(type: "numeric(5,4)", precision: 5, scale: 4, nullable: false),
                    FinalScore = table.Column<decimal>(type: "numeric(5,4)", precision: 5, scale: 4, nullable: false),
                    Priority = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ReasonsJson = table.Column<string>(type: "text", nullable: false),
                    ModelVersion = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lead_scores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "leads",
                schema: "leads",
                columns: table => new
                {
                    LeadId = table.Column<long>(type: "bigint", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Canal = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    EmpresaId = table.Column<long>(type: "bigint", nullable: false),
                    PuntoVentaId = table.Column<long>(type: "bigint", nullable: false),
                    NombreCliente = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Telefono = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Ciudad = table.Column<string>(type: "text", nullable: true),
                    ModeloInteresTexto = table.Column<string>(type: "text", nullable: true),
                    EstadoGestion = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    FechaPrimerContacto = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Campania = table.Column<string>(type: "text", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: true),
                    TelefonoNormalizado = table.Column<string>(type: "text", nullable: true),
                    EmailNormalizado = table.Column<string>(type: "text", nullable: true),
                    CiudadNormalizada = table.Column<string>(type: "text", nullable: true),
                    CanalNormalizado = table.Column<string>(type: "text", nullable: true),
                    ModeloTextoNormalizado = table.Column<string>(type: "text", nullable: true),
                    MotorcycleSku = table.Column<string>(type: "text", nullable: true),
                    ModelMatchConfidence = table.Column<decimal>(type: "numeric(5,4)", precision: 5, scale: 4, nullable: true),
                    ModelMatchMethod = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leads", x => x.LeadId);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                schema: "leads",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConversationId = table.Column<long>(type: "bigint", nullable: false),
                    SenderType = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    SentAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "motorcycles",
                schema: "leads",
                columns: table => new
                {
                    Sku = table.Column<string>(type: "text", nullable: false),
                    Marca = table.Column<string>(type: "text", nullable: false),
                    Linea = table.Column<string>(type: "text", nullable: false),
                    Cilindraje = table.Column<int>(type: "integer", nullable: false),
                    Segmento = table.Column<string>(type: "text", nullable: false),
                    PrecioLista = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    PuntosVentaDisponibles = table.Column<int>(type: "integer", nullable: false),
                    UnidadesDisponibles = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_motorcycles", x => x.Sku);
                });

            migrationBuilder.CreateTable(
                name: "pipeline_runs",
                schema: "leads",
                columns: table => new
                {
                    RunId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    PipelineVersion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RecordsRead = table.Column<int>(type: "integer", nullable: false),
                    RecordsProcessed = table.Column<int>(type: "integer", nullable: false),
                    RecordsFailed = table.Column<int>(type: "integer", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FinishedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SummaryJson = table.Column<string>(type: "text", nullable: true),
                    Error = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pipeline_runs", x => x.RunId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_advisors_EmpresaId_PuntoVentaId_Activo",
                schema: "leads",
                table: "advisors",
                columns: new[] { "EmpresaId", "PuntoVentaId", "Activo" });

            migrationBuilder.CreateIndex(
                name: "IX_conversations_ConversacionId",
                schema: "leads",
                table: "conversations",
                column: "ConversacionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_conversations_LeadId",
                schema: "leads",
                table: "conversations",
                column: "LeadId");

            migrationBuilder.CreateIndex(
                name: "IX_customers_EmpresaId_EmailNormalizado",
                schema: "leads",
                table: "customers",
                columns: new[] { "EmpresaId", "EmailNormalizado" });

            migrationBuilder.CreateIndex(
                name: "IX_customers_EmpresaId_TelefonoNormalizado",
                schema: "leads",
                table: "customers",
                columns: new[] { "EmpresaId", "TelefonoNormalizado" });

            migrationBuilder.CreateIndex(
                name: "IX_lead_assignments_LeadId_AssignedAt",
                schema: "leads",
                table: "lead_assignments",
                columns: new[] { "LeadId", "AssignedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_lead_enrichments_LeadId_CreatedAt",
                schema: "leads",
                table: "lead_enrichments",
                columns: new[] { "LeadId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_lead_scores_LeadId_CreatedAt",
                schema: "leads",
                table: "lead_scores",
                columns: new[] { "LeadId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_leads_EmpresaId_PuntoVentaId",
                schema: "leads",
                table: "leads",
                columns: new[] { "EmpresaId", "PuntoVentaId" });

            migrationBuilder.CreateIndex(
                name: "IX_leads_FechaRegistro",
                schema: "leads",
                table: "leads",
                column: "FechaRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_leads_MotorcycleSku",
                schema: "leads",
                table: "leads",
                column: "MotorcycleSku");

            migrationBuilder.CreateIndex(
                name: "IX_messages_ConversationId",
                schema: "leads",
                table: "messages",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_motorcycles_Marca_Linea",
                schema: "leads",
                table: "motorcycles",
                columns: new[] { "Marca", "Linea" });

            migrationBuilder.CreateIndex(
                name: "IX_pipeline_runs_StartedAt",
                schema: "leads",
                table: "pipeline_runs",
                column: "StartedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "advisors",
                schema: "leads");

            migrationBuilder.DropTable(
                name: "conversations",
                schema: "leads");

            migrationBuilder.DropTable(
                name: "customers",
                schema: "leads");

            migrationBuilder.DropTable(
                name: "lead_assignments",
                schema: "leads");

            migrationBuilder.DropTable(
                name: "lead_enrichments",
                schema: "leads");

            migrationBuilder.DropTable(
                name: "lead_scores",
                schema: "leads");

            migrationBuilder.DropTable(
                name: "leads",
                schema: "leads");

            migrationBuilder.DropTable(
                name: "messages",
                schema: "leads");

            migrationBuilder.DropTable(
                name: "motorcycles",
                schema: "leads");

            migrationBuilder.DropTable(
                name: "pipeline_runs",
                schema: "leads");
        }
    }
}
