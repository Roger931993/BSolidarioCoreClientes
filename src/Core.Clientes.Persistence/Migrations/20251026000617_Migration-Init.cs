using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Clientes.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MigrationInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "api_log_cliente_header",
                columns: table => new
                {
                    api_log_cliente_header_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    request_method = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    request_url = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    response_code = table.Column<int>(type: "int", nullable: true),
                    id_tracking = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_api_log_cliente_header", x => x.api_log_cliente_header_id);
                });

            migrationBuilder.CreateTable(
                name: "cliente",
                columns: table => new
                {
                    cliente_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    primer_nombre = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    segundo_nombre = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    apellido_paterno = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    apellido_materno = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    identificacion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    username = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    estado = table.Column<int>(type: "int", nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fecha_modificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cliente", x => x.cliente_id);
                });

            migrationBuilder.CreateTable(
                name: "api_log_cliente_detail",
                columns: table => new
                {
                    api_log_cliente_detail_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    api_log_cliente_header_id = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    status_code = table.Column<int>(type: "int", nullable: true),
                    type_process = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    data_message = table.Column<string>(type: "text", maxLength: 300, nullable: true),
                    process_component = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_api_log_cliente_detail", x => x.api_log_cliente_detail_id);
                    table.ForeignKey(
                        name: "fk_cliente_header_detail",
                        column: x => x.api_log_cliente_header_id,
                        principalTable: "api_log_cliente_header",
                        principalColumn: "api_log_cliente_header_id");
                });

            migrationBuilder.CreateTable(
                name: "contacto",
                columns: table => new
                {
                    contacto_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cliente_id = table.Column<int>(type: "int", nullable: true),
                    descripcion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    email = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    telefono = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    estado = table.Column<int>(type: "int", nullable: true),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fecha_modificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contacto", x => x.contacto_id);
                    table.ForeignKey(
                        name: "FK_contacto_cliente_cliente_id",
                        column: x => x.cliente_id,
                        principalTable: "cliente",
                        principalColumn: "cliente_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_api_log_cliente_detail_api_log_cliente_header_id",
                table: "api_log_cliente_detail",
                column: "api_log_cliente_header_id");

            migrationBuilder.CreateIndex(
                name: "IX_contacto_cliente_id",
                table: "contacto",
                column: "cliente_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "api_log_cliente_detail");

            migrationBuilder.DropTable(
                name: "contacto");

            migrationBuilder.DropTable(
                name: "api_log_cliente_header");

            migrationBuilder.DropTable(
                name: "cliente");
        }
    }
}
