﻿// <auto-generated />
using System;
using Aeronaves.WebApi.Persistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Aeronaves.WebApi.Migrations
{
    [DbContext(typeof(ContextoAeronave))]
    partial class ContextoAeronaveModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Aeronaves.WebApi.Modelo.Aeronave", b =>
                {
                    b.Property<Guid>("AeronaveId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AereopuertoEstacionamiento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("AeronaveGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("CapTanqueCombustible")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CapacidadCarga")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("EstadoAeronave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Marca")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Modelo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NroAsientos")
                        .HasColumnType("int");

                    b.HasKey("AeronaveId");

                    b.ToTable("Aeronave");
                });

            modelBuilder.Entity("Aeronaves.WebApi.Modelo.AeronaveAsientos", b =>
                {
                    b.Property<Guid>("AeronaveAsientosId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AeronaveId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClaseAsiento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EstadoAsiento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NroSilla")
                        .HasColumnType("int");

                    b.Property<string>("Ubicacion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AeronaveAsientosId");

                    b.HasIndex("AeronaveId");

                    b.ToTable("AeronaveAsientos");
                });

            modelBuilder.Entity("Aeronaves.WebApi.Modelo.AeronaveAsientos", b =>
                {
                    b.HasOne("Aeronaves.WebApi.Modelo.Aeronave", "Aeronave")
                        .WithMany("ListaAeronaveAsientos")
                        .HasForeignKey("AeronaveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
