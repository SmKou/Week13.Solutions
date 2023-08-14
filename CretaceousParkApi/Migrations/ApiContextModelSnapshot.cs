﻿// <auto-generated />
using CretaceousParkApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CretaceousParkApi.Migrations
{
    [DbContext(typeof(ApiContext))]
    partial class ApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CretaceousParkApi.Models.Animal", b =>
                {
                    b.Property<int>("AnimalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Species")
                        .HasColumnType("longtext");

                    b.HasKey("AnimalId");

                    b.ToTable("Animals");

                    b.HasData(
                        new
                        {
                            AnimalId = 1,
                            Age = 7,
                            Name = "Matilda",
                            Species = "Woolly Mammoth"
                        },
                        new
                        {
                            AnimalId = 2,
                            Age = 10,
                            Name = "Rexie",
                            Species = "Dinosaur"
                        },
                        new
                        {
                            AnimalId = 3,
                            Age = 2,
                            Name = "Matilda",
                            Species = "Dinosaur"
                        },
                        new
                        {
                            AnimalId = 4,
                            Age = 4,
                            Name = "Pip",
                            Species = "Shark"
                        },
                        new
                        {
                            AnimalId = 5,
                            Age = 22,
                            Name = "Bartholomew",
                            Species = "Dinosaur"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
