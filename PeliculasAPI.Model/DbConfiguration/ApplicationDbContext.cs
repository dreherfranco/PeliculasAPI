using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Model.Models;
using NetTopologySuite.Geometries;
using NetTopologySuite;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace PeliculasAPI.Model.DbConfiguration
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MoviesActors>()
                .HasKey(x => new { x.ActorId, x.MovieId });

            modelBuilder.Entity<MoviesGenders>()
                .HasKey(x => new { x.GenderId, x.MovieId });

            modelBuilder.Entity<MoviesCinemas>()
                .HasKey(x => new { x.CinemaId, x.MovieId });

            SeedData(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var roleAdminId = "9aae0b6d-d50c-4d0a-9b90-2a6873e3845d";
            var userAdminId = "5673b8cf-12de-44f6-92ad-fae4a77932ad";

            var rolAdmin = new IdentityRole()
            {
                Id = roleAdminId,
                Name = "Admin",
                NormalizedName = "Admin"
            };

            var passwordHasher = new PasswordHasher<IdentityUser>();

            var username = "franco@hotmail.com";

            var usuarioAdmin = new IdentityUser()
            {
                Id = userAdminId,
                UserName = username,
                NormalizedUserName = username,
                Email = username,
                NormalizedEmail = username,
                PasswordHash = passwordHasher.HashPassword(null, "Aa123456!")
            };

            /*modelBuilder.Entity<IdentityUser>()
                .HasData(usuarioAdmin);

            modelBuilder.Entity<IdentityRole>()
                .HasData(rolAdmin);

            modelBuilder.Entity<IdentityUserClaim<string>>()
                .HasData(new IdentityUserClaim<string>()
                {
                    Id = 1,
                    ClaimType = ClaimTypes.Role,
                    UserId = userAdminId,
                    ClaimValue = "Admin"
                });
            */
            var adventure = new Gender() { Id=4,Name="Adventure"};
            var animation = new Gender() { Id = 5, Name = "Animation" };
            var suspense = new Gender() { Id = 6, Name = "Suspense" };
            var romance = new Gender() { Id = 7, Name = "Romance" };
           
            modelBuilder.Entity<Gender>()
                .HasData(new List<Gender> 
                {
                    adventure, animation, suspense, romance
                });

            var jimCarrey = new Actor() { Id = 6, Name = "Jim Carrey", Birthday = new DateTime(1962, 01, 17) };
            var robertDowney = new Actor() { Id = 7, Name = "Robert Downey", Birthday = new DateTime(1965, 05, 20) };
            var chrisEvans = new Actor() { Id = 8, Name = "Chris Evans", Birthday = new DateTime(1980, 03, 19) };

            modelBuilder.Entity<Actor>()
                .HasData(new List<Actor> 
                { 
                    jimCarrey,robertDowney,chrisEvans
                });

            var endgame = new Movie()
            { 
                Id = 2, Title = "Avengers: Endgame",
                InTheaters = true,
                PremiereDate = new DateTime(2020, 12, 05) 
            };
            var iw = new Movie()
            {
                Id = 3,
                Title = "Avengers: Infinity war",
                InTheaters = true,
                PremiereDate = new DateTime(2020, 10, 05)
            };
            var sonic = new Movie()
            {
                Id = 4,
                Title = "Sonic",
                InTheaters = true,
                PremiereDate = new DateTime(2021, 04, 05)
            };
            var emma = new Movie()
            {
                Id = 5,
                Title = "Emma",
                InTheaters = false,
                PremiereDate = new DateTime(2021, 04, 05)
            };
            var wonderWoman = new Movie()
            {
                Id = 6,
                Title = "Wonder Woman",
                InTheaters = false,
                PremiereDate = new DateTime(2021, 04, 05)
            };

            modelBuilder.Entity<Movie>()
               .HasData(new List<Movie>
               {
                    endgame,iw,sonic,emma,wonderWoman
               });

           // var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            
            var granRex = new Cinema()
            {
                Id = 5,
               // Ubication = geometryFactory.CreatePoint(new Coordinate(-60.5687, 8.65548)),
                Name = "prueba geometry factory"
            };

            var lunaPark = new Cinema()
            {
                Id = 6,
                //Ubication =  geometryFactory.CreatePoint(new Coordinate(-50.5687, 85.65548)),
                Name = "prueba geometry factory 2"
            };

            modelBuilder.Entity<Cinema>()
               .HasData(new List<Cinema>
               {
                    granRex, lunaPark
               });

            modelBuilder.Entity<MoviesGenders>()
               .HasData(new List<MoviesGenders>() {
                    new MoviesGenders() { MovieId=endgame.Id,GenderId=adventure.Id},
                    new MoviesGenders() { MovieId=iw.Id,GenderId=adventure.Id},
                    new MoviesGenders() { MovieId=iw.Id,GenderId=suspense.Id},
                    new MoviesGenders() { MovieId=sonic.Id,GenderId=adventure.Id},
                    new MoviesGenders() { MovieId=emma.Id,GenderId=suspense.Id},
                    new MoviesGenders() { MovieId=emma.Id,GenderId=romance.Id},
                    new MoviesGenders() { MovieId=wonderWoman.Id,GenderId=animation.Id},
                    new MoviesGenders() { MovieId=wonderWoman.Id,GenderId=adventure.Id}
               });

            modelBuilder.Entity<MoviesActors>()
               .HasData(new List<MoviesActors>()
               {
                    new MoviesActors(){ MovieId=endgame.Id, ActorId=robertDowney.Id, Character="Tony Stark",Order=1},
                    new MoviesActors(){ MovieId=endgame.Id, ActorId=chrisEvans.Id, Character="Steve Rogers",Order=2},
                    new MoviesActors(){ MovieId=iw.Id, ActorId=robertDowney.Id, Character="Tony Stark",Order=1},
                    new MoviesActors(){ MovieId=iw.Id, ActorId=chrisEvans.Id, Character="Steve Rogers",Order=2},
                    new MoviesActors(){ MovieId=sonic.Id, ActorId=chrisEvans.Id, Character="DR Ivo",Order=2},
               });

            modelBuilder.Entity<MoviesCinemas>()
                .HasData(new List<MoviesCinemas>()
                {
                    new MoviesCinemas(){ MovieId = endgame.Id, CinemaId=granRex.Id},
                    new MoviesCinemas(){ MovieId = iw.Id, CinemaId=granRex.Id},
                    new MoviesCinemas(){ MovieId = sonic.Id, CinemaId=granRex.Id},
                    new MoviesCinemas(){ MovieId = endgame.Id, CinemaId=lunaPark.Id},
                    new MoviesCinemas(){ MovieId = iw.Id, CinemaId=lunaPark.Id},
                });

        }

        public DbSet<Gender> Genders { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinema { get; set; }
        public DbSet<MoviesGenders> MoviesGenders { get; set; }
        public DbSet<MoviesActors> MoviesActors { get; set; }
        public DbSet<MoviesCinemas> MoviesCinemas { get; set; }
    }
}
