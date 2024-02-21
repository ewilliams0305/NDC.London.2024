﻿// <auto-generated />
using System;
using BeerSender.Web.Event_stream;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BeerSender.Web.Migrations
{
    [DbContext(typeof(Event_context))]
    [Migration("20240220235634_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BeerSender.Web.Event_stream.Event_model", b =>
                {
                    b.Property<Guid>("Aggregate_id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Sequence_nr")
                        .HasColumnType("int");

                    b.Property<string>("Event_payload")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("varchar");

                    b.Property<string>("Event_type")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar");

                    b.Property<byte[]>("Row_version")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Aggregate_id", "Sequence_nr");

                    b.ToTable("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
