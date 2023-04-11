﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrananAPI.Data;

#nullable disable

namespace TrananAPI.Migrations
{
    [DbContext(typeof(TrananDbContext))]
    [Migration("20230410123310_changedpropertyinmovie")]
    partial class changedpropertyinmovie
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("ActorMovie", b =>
                {
                    b.Property<int>("ActorsActorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MoviesMovieId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ActorsActorId", "MoviesMovieId");

                    b.HasIndex("MoviesMovieId");

                    b.ToTable("actor_to_movie", (string)null);
                });

            modelBuilder.Entity("DirectorMovie", b =>
                {
                    b.Property<int>("DirectorsDirectorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MoviesMovieId")
                        .HasColumnType("INTEGER");

                    b.HasKey("DirectorsDirectorId", "MoviesMovieId");

                    b.HasIndex("MoviesMovieId");

                    b.ToTable("director_to_movie", (string)null);
                });

            modelBuilder.Entity("MovieScreeningSeat", b =>
                {
                    b.Property<int>("MovieScreeningsMovieScreeningId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SeatsSeatId")
                        .HasColumnType("INTEGER");

                    b.HasKey("MovieScreeningsMovieScreeningId", "SeatsSeatId");

                    b.HasIndex("SeatsSeatId");

                    b.ToTable("MovieScreeningSeat");
                });

            modelBuilder.Entity("ReservationSeat", b =>
                {
                    b.Property<int>("ReservationsReservationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SeatsSeatId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ReservationsReservationId", "SeatsSeatId");

                    b.HasIndex("SeatsSeatId");

                    b.ToTable("ReservationSeat");
                });

            modelBuilder.Entity("TrananAPI.Models.Actor", b =>
                {
                    b.Property<int>("ActorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ActorId");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("TrananAPI.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("TrananAPI.Models.Director", b =>
                {
                    b.Property<int>("DirectorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("DirectorId");

                    b.ToTable("Directors");
                });

            modelBuilder.Entity("TrananAPI.Models.Movie", b =>
                {
                    b.Property<int>("MovieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AmountOfScreenings")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("DurationMinutes")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("MaxScreenings")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ReleaseYear")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("MovieId");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("TrananAPI.Models.MovieScreening", b =>
                {
                    b.Property<int>("MovieScreeningId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateAndTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("MovieId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TheaterId")
                        .HasColumnType("INTEGER");

                    b.HasKey("MovieScreeningId");

                    b.HasIndex("MovieId");

                    b.HasIndex("TheaterId");

                    b.ToTable("MovieScreenings");
                });

            modelBuilder.Entity("TrananAPI.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MovieScreeningId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<int>("ReservationCode")
                        .HasColumnType("INTEGER");

                    b.HasKey("ReservationId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("MovieScreeningId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("TrananAPI.Models.Seat", b =>
                {
                    b.Property<int>("SeatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsBooked")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsNotBookable")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsWheelChairSpace")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Row")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SeatNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TheaterId")
                        .HasColumnType("INTEGER");

                    b.HasKey("SeatId");

                    b.HasIndex("TheaterId");

                    b.ToTable("Seats");
                });

            modelBuilder.Entity("TrananAPI.Models.Theater", b =>
                {
                    b.Property<int>("TheaterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaxAmountAvailebleSeats")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Rows")
                        .HasColumnType("INTEGER");

                    b.HasKey("TheaterId");

                    b.ToTable("Theaters");
                });

            modelBuilder.Entity("ActorMovie", b =>
                {
                    b.HasOne("TrananAPI.Models.Actor", null)
                        .WithMany()
                        .HasForeignKey("ActorsActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrananAPI.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("MoviesMovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DirectorMovie", b =>
                {
                    b.HasOne("TrananAPI.Models.Director", null)
                        .WithMany()
                        .HasForeignKey("DirectorsDirectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrananAPI.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("MoviesMovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieScreeningSeat", b =>
                {
                    b.HasOne("TrananAPI.Models.MovieScreening", null)
                        .WithMany()
                        .HasForeignKey("MovieScreeningsMovieScreeningId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrananAPI.Models.Seat", null)
                        .WithMany()
                        .HasForeignKey("SeatsSeatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReservationSeat", b =>
                {
                    b.HasOne("TrananAPI.Models.Reservation", null)
                        .WithMany()
                        .HasForeignKey("ReservationsReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrananAPI.Models.Seat", null)
                        .WithMany()
                        .HasForeignKey("SeatsSeatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrananAPI.Models.MovieScreening", b =>
                {
                    b.HasOne("TrananAPI.Models.Movie", "Movie")
                        .WithMany("MovieScreenings")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrananAPI.Models.Theater", "Theater")
                        .WithMany("MovieScreenings")
                        .HasForeignKey("TheaterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("Theater");
                });

            modelBuilder.Entity("TrananAPI.Models.Reservation", b =>
                {
                    b.HasOne("TrananAPI.Models.Customer", "Customer")
                        .WithMany("Reservations")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrananAPI.Models.MovieScreening", "MovieScreening")
                        .WithMany("Reservations")
                        .HasForeignKey("MovieScreeningId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("MovieScreening");
                });

            modelBuilder.Entity("TrananAPI.Models.Seat", b =>
                {
                    b.HasOne("TrananAPI.Models.Theater", "Theater")
                        .WithMany("Seats")
                        .HasForeignKey("TheaterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Theater");
                });

            modelBuilder.Entity("TrananAPI.Models.Customer", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("TrananAPI.Models.Movie", b =>
                {
                    b.Navigation("MovieScreenings");
                });

            modelBuilder.Entity("TrananAPI.Models.MovieScreening", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("TrananAPI.Models.Theater", b =>
                {
                    b.Navigation("MovieScreenings");

                    b.Navigation("Seats");
                });
#pragma warning restore 612, 618
        }
    }
}
