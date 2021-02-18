﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PeliculasAPI.Model.DbConfiguration;

namespace PeliculasAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PeliculasAPI.Model.Models.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Actors");

                    b.HasData(
                        new
                        {
                            Id = 6,
                            Birthday = new DateTime(1962, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Jim Carrey"
                        },
                        new
                        {
                            Id = 7,
                            Birthday = new DateTime(1965, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Robert Downey"
                        },
                        new
                        {
                            Id = 8,
                            Birthday = new DateTime(1980, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Chris Evans"
                        });
                });

            modelBuilder.Entity("PeliculasAPI.Model.Models.Cinema", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.HasKey("Id");

                    b.ToTable("Cinema");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Gran Rex"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Luna Park"
                        });
                });

            modelBuilder.Entity("PeliculasAPI.Model.Models.Gender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Genders");

                    b.HasData(
                        new
                        {
                            Id = 4,
                            Name = "Adventure"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Animation"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Suspense"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Romance"
                        });
                });

            modelBuilder.Entity("PeliculasAPI.Model.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("InTheaters")
                        .HasColumnType("bit");

                    b.Property<string>("Poster")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PremiereDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.HasKey("Id");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            InTheaters = true,
                            PremiereDate = new DateTime(2020, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Avengers: Endgame"
                        },
                        new
                        {
                            Id = 3,
                            InTheaters = true,
                            PremiereDate = new DateTime(2020, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Avengers: Infinity war"
                        },
                        new
                        {
                            Id = 4,
                            InTheaters = true,
                            PremiereDate = new DateTime(2021, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Sonic"
                        },
                        new
                        {
                            Id = 5,
                            InTheaters = false,
                            PremiereDate = new DateTime(2021, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Emma"
                        },
                        new
                        {
                            Id = 6,
                            InTheaters = false,
                            PremiereDate = new DateTime(2021, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Wonder Woman"
                        });
                });

            modelBuilder.Entity("PeliculasAPI.Model.Models.MoviesActors", b =>
                {
                    b.Property<int>("ActorId")
                        .HasColumnType("int");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<string>("Character")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.HasKey("ActorId", "MovieId");

                    b.HasIndex("MovieId");

                    b.ToTable("MoviesActors");

                    b.HasData(
                        new
                        {
                            ActorId = 7,
                            MovieId = 2,
                            Character = "Tony Stark",
                            Order = 1
                        },
                        new
                        {
                            ActorId = 8,
                            MovieId = 2,
                            Character = "Steve Rogers",
                            Order = 2
                        },
                        new
                        {
                            ActorId = 7,
                            MovieId = 3,
                            Character = "Tony Stark",
                            Order = 1
                        },
                        new
                        {
                            ActorId = 8,
                            MovieId = 3,
                            Character = "Steve Rogers",
                            Order = 2
                        },
                        new
                        {
                            ActorId = 8,
                            MovieId = 4,
                            Character = "DR Ivo",
                            Order = 2
                        });
                });

            modelBuilder.Entity("PeliculasAPI.Model.Models.MoviesCinemas", b =>
                {
                    b.Property<int>("CinemaId")
                        .HasColumnType("int");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.HasKey("CinemaId", "MovieId");

                    b.HasIndex("MovieId");

                    b.ToTable("MoviesCinemas");

                    b.HasData(
                        new
                        {
                            CinemaId = 1,
                            MovieId = 2
                        },
                        new
                        {
                            CinemaId = 1,
                            MovieId = 3
                        },
                        new
                        {
                            CinemaId = 1,
                            MovieId = 4
                        },
                        new
                        {
                            CinemaId = 2,
                            MovieId = 2
                        },
                        new
                        {
                            CinemaId = 2,
                            MovieId = 3
                        });
                });

            modelBuilder.Entity("PeliculasAPI.Model.Models.MoviesGenders", b =>
                {
                    b.Property<int>("GenderId")
                        .HasColumnType("int");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.HasKey("GenderId", "MovieId");

                    b.HasIndex("MovieId");

                    b.ToTable("MoviesGenders");

                    b.HasData(
                        new
                        {
                            GenderId = 4,
                            MovieId = 2
                        },
                        new
                        {
                            GenderId = 4,
                            MovieId = 3
                        },
                        new
                        {
                            GenderId = 6,
                            MovieId = 3
                        },
                        new
                        {
                            GenderId = 4,
                            MovieId = 4
                        },
                        new
                        {
                            GenderId = 6,
                            MovieId = 5
                        },
                        new
                        {
                            GenderId = 7,
                            MovieId = 5
                        },
                        new
                        {
                            GenderId = 5,
                            MovieId = 6
                        },
                        new
                        {
                            GenderId = 4,
                            MovieId = 6
                        });
                });

            modelBuilder.Entity("PeliculasAPI.Model.Models.MoviesActors", b =>
                {
                    b.HasOne("PeliculasAPI.Model.Models.Actor", "Actor")
                        .WithMany("MoviesActors")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PeliculasAPI.Model.Models.Movie", "Movie")
                        .WithMany("MoviesActors")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("PeliculasAPI.Model.Models.MoviesCinemas", b =>
                {
                    b.HasOne("PeliculasAPI.Model.Models.Cinema", "Cinema")
                        .WithMany("MoviesCinemas")
                        .HasForeignKey("CinemaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PeliculasAPI.Model.Models.Movie", "Movie")
                        .WithMany("MoviesCinemas")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cinema");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("PeliculasAPI.Model.Models.MoviesGenders", b =>
                {
                    b.HasOne("PeliculasAPI.Model.Models.Gender", "Gender")
                        .WithMany("MoviesGenders")
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PeliculasAPI.Model.Models.Movie", "Movie")
                        .WithMany("MoviesGenders")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gender");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("PeliculasAPI.Model.Models.Actor", b =>
                {
                    b.Navigation("MoviesActors");
                });

            modelBuilder.Entity("PeliculasAPI.Model.Models.Cinema", b =>
                {
                    b.Navigation("MoviesCinemas");
                });

            modelBuilder.Entity("PeliculasAPI.Model.Models.Gender", b =>
                {
                    b.Navigation("MoviesGenders");
                });

            modelBuilder.Entity("PeliculasAPI.Model.Models.Movie", b =>
                {
                    b.Navigation("MoviesActors");

                    b.Navigation("MoviesCinemas");

                    b.Navigation("MoviesGenders");
                });
#pragma warning restore 612, 618
        }
    }
}
