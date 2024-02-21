using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeerSender.Web.Migrations.Read_contextMigrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Bottles",
                columns: table => new
                {
                    Bottle_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Bottles_sold = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Brewery = table.Column<string>(type: "varchar(2048)", maxLength: 2048, nullable: false),
                    Alcohol_percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Volume_in_ml = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bottles", x => x.Bottle_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Box_statuses",
                columns: table => new
                {
                    Aggregate_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Number_of_bottles = table.Column<int>(type: "int", nullable: false),
                    Shipment_status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Box_statuses", x => x.Aggregate_id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Projection_checkpoints",
                columns: table => new
                {
                    Projection_name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Event_version = table.Column<byte[]>(type: "binary(8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projection_checkpoints", x => x.Projection_name);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bottles");

            migrationBuilder.DropTable(
                name: "Box_statuses");

            migrationBuilder.DropTable(
                name: "Projection_checkpoints");
        }
    }
}
