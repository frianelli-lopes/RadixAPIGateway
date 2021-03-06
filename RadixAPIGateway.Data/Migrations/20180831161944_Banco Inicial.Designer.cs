﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RadixAPIGateway.Data.Context;

namespace RadixAPIGateway.Data.Migrations
{
    [DbContext(typeof(EFContext))]
    [Migration("20180831161944_Banco Inicial")]
    partial class BancoInicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RadixAPIGateway.Domain.Models.SaleTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<int>("StoreId");

                    b.Property<float>("Total");

                    b.HasKey("Id");

                    b.HasIndex("StoreId");

                    b.ToTable("SaleTransaction");
                });

            modelBuilder.Entity("RadixAPIGateway.Domain.Models.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AcquirerId");

                    b.Property<bool>("HasAntiFraudAgreement");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Store");
                });

            modelBuilder.Entity("RadixAPIGateway.Domain.Models.SaleTransaction", b =>
                {
                    b.HasOne("RadixAPIGateway.Domain.Models.Store", "Store")
                        .WithMany("SalesTransaction")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
