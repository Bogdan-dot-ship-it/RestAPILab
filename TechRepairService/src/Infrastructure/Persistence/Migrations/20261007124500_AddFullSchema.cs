using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFullSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "device_id",
                table: "repair_orders",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "technician_id",
                table: "repair_orders",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    full_name = table.Column<string>(type: "varchar(150)", nullable: false),
                    phone = table.Column<string>(type: "varchar(20)", nullable: false),
                    email = table.Column<string>(type: "varchar(100)", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "repair_parts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    repair_order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    part_name = table.Column<string>(type: "varchar(150)", nullable: false),
                    cost = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_repair_parts", x => x.id);
                    table.ForeignKey(
                        name: "fk_repair_parts_repair_orders_repair_order_id",
                        column: x => x.repair_order_id,
                        principalTable: "repair_orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "technicians",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    full_name = table.Column<string>(type: "varchar(150)", nullable: false),
                    specialization = table.Column<string>(type: "varchar(100)", nullable: false),
                    phone = table.Column<string>(type: "varchar(20)", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_technicians", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "devices",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    client_id = table.Column<Guid>(type: "uuid", nullable: false),
                    brand = table.Column<string>(type: "varchar(100)", nullable: false),
                    model = table.Column<string>(type: "varchar(150)", nullable: false),
                    serial_number = table.Column<string>(type: "varchar(100)", nullable: false),
                    device_type = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_devices", x => x.id);
                    table.ForeignKey(
                        name: "fk_devices_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_repair_orders_device_id",
                table: "repair_orders",
                column: "device_id");

            migrationBuilder.CreateIndex(
                name: "ix_repair_orders_technician_id",
                table: "repair_orders",
                column: "technician_id");

            migrationBuilder.CreateIndex(
                name: "ix_clients_phone",
                table: "clients",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_devices_client_id",
                table: "devices",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_devices_serial_number",
                table: "devices",
                column: "serial_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_repair_parts_repair_order_id",
                table: "repair_parts",
                column: "repair_order_id");

            migrationBuilder.CreateIndex(
                name: "ix_technicians_phone",
                table: "technicians",
                column: "phone",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_repair_orders_devices_device_id",
                table: "repair_orders",
                column: "device_id",
                principalTable: "devices",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_repair_orders_technicians_technician_id",
                table: "repair_orders",
                column: "technician_id",
                principalTable: "technicians",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_repair_orders_devices_device_id",
                table: "repair_orders");

            migrationBuilder.DropForeignKey(
                name: "fk_repair_orders_technicians_technician_id",
                table: "repair_orders");

            migrationBuilder.DropTable(
                name: "devices");

            migrationBuilder.DropTable(
                name: "repair_parts");

            migrationBuilder.DropTable(
                name: "technicians");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropIndex(
                name: "ix_repair_orders_device_id",
                table: "repair_orders");

            migrationBuilder.DropIndex(
                name: "ix_repair_orders_technician_id",
                table: "repair_orders");

            migrationBuilder.DropColumn(
                name: "device_id",
                table: "repair_orders");

            migrationBuilder.DropColumn(
                name: "technician_id",
                table: "repair_orders");
        }
    }
}
