using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "repair_orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_number = table.Column<string>(type: "varchar(50)", nullable: false),
                    device_model = table.Column<string>(type: "varchar(255)", nullable: false),
                    estimated_cost = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    is_urgent = table.Column<bool>(type: "boolean", nullable: false),
                    status = table.Column<string>(type: "varchar(50)", nullable: false),
                    estimated_completion_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_repair_orders", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_repair_orders_order_number",
                table: "repair_orders",
                column: "order_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "repair_orders");
        }
    }
}
