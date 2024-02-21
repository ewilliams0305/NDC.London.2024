﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeerSender.Web.Migrations
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
                name: "Events",
                columns: table => new
                {
                    Aggregate_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Sequence_nr = table.Column<int>(type: "int", nullable: false),
                    Event_type = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Event_payload = table.Column<string>(type: "varchar(2048)", maxLength: 2048, nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Row_version = table.Column<ulong>(type: "bigint unsigned", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => new { x.Aggregate_id, x.Sequence_nr });
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
