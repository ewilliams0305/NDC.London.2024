using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeerSender.Web.Migrations.Read_contextMigrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Box_statuses",
                columns: table => new
                {
                    Aggregate_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number_of_bottles = table.Column<int>(type: "int", nullable: false),
                    Shipment_status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Box_statuses", x => x.Aggregate_id);
                });

            migrationBuilder.CreateTable(
                name: "Projection_checkpoints",
                columns: table => new
                {
                    Projection_name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Event_version = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projection_checkpoints", x => x.Projection_name);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Box_statuses");

            migrationBuilder.DropTable(
                name: "Projection_checkpoints");
        }
    }
}
