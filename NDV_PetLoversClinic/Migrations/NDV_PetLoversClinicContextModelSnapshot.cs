﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NDV_PetLoversClinic.Data;

#nullable disable

namespace NDV_PetLoversClinic.Migrations
{
    [DbContext(typeof(NDV_PetLoversClinicContext))]
    partial class NDV_PetLoversClinicContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NDV_PetLoversClinic.Models.Person", b =>
                {
                    b.Property<int>("person_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("person_Id"));

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("bdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("gender")
                        .HasColumnType("int");

                    b.Property<string>("lname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("person_Id");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("NDV_PetLoversClinic.Models.Records.Breed", b =>
                {
                    b.Property<int>("breed_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("breed_Id"));

                    b.Property<string>("breed_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("specie_Id")
                        .HasColumnType("int");

                    b.HasKey("breed_Id");

                    b.HasIndex("specie_Id");

                    b.ToTable("Breeds");
                });

            modelBuilder.Entity("NDV_PetLoversClinic.Models.Records.Clients", b =>
                {
                    b.Property<int>("client_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("client_Id"));

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("person_Id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("client_Id");

                    b.HasIndex("person_Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("NDV_PetLoversClinic.Models.Records.Contact", b =>
                {
                    b.Property<int>("contact_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("contact_Id"));

                    b.Property<string>("contactNo")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<int>("person_Id")
                        .HasColumnType("int");

                    b.HasKey("contact_Id");

                    b.HasIndex("person_Id");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("NDV_PetLoversClinic.Models.Records.Pet", b =>
                {
                    b.Property<int>("pet_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("pet_Id"));

                    b.Property<DateTime>("bdate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("breed_Id")
                        .HasColumnType("int");

                    b.Property<int>("client_Id")
                        .HasColumnType("int");

                    b.Property<string>("color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("gender")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("pet_Id");

                    b.HasIndex("breed_Id");

                    b.HasIndex("client_Id");

                    b.ToTable("Pet");
                });

            modelBuilder.Entity("NDV_PetLoversClinic.Models.Records.Specie", b =>
                {
                    b.Property<int>("specie_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("specie_Id"));

                    b.Property<string>("specie_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("specie_Id");

                    b.ToTable("Species");
                });

            modelBuilder.Entity("NDV_PetLoversClinic.Models.Records.Breed", b =>
                {
                    b.HasOne("NDV_PetLoversClinic.Models.Records.Specie", "Specie")
                        .WithMany("Breeds")
                        .HasForeignKey("specie_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Specie");
                });

            modelBuilder.Entity("NDV_PetLoversClinic.Models.Records.Clients", b =>
                {
                    b.HasOne("NDV_PetLoversClinic.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("person_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("NDV_PetLoversClinic.Models.Records.Contact", b =>
                {
                    b.HasOne("NDV_PetLoversClinic.Models.Person", "Person")
                        .WithMany("IContact")
                        .HasForeignKey("person_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("NDV_PetLoversClinic.Models.Records.Pet", b =>
                {
                    b.HasOne("NDV_PetLoversClinic.Models.Records.Breed", "Breed")
                        .WithMany()
                        .HasForeignKey("breed_Id");

                    b.HasOne("NDV_PetLoversClinic.Models.Records.Clients", "Client")
                        .WithMany("IPet")
                        .HasForeignKey("client_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Breed");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("NDV_PetLoversClinic.Models.Person", b =>
                {
                    b.Navigation("IContact");
                });

            modelBuilder.Entity("NDV_PetLoversClinic.Models.Records.Clients", b =>
                {
                    b.Navigation("IPet");
                });

            modelBuilder.Entity("NDV_PetLoversClinic.Models.Records.Specie", b =>
                {
                    b.Navigation("Breeds");
                });
#pragma warning restore 612, 618
        }
    }
}
