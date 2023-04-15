﻿// <auto-generated />
using System;
using Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CORE.Migrations
{
    [DbContext(typeof(TrananDbContext))]
    [Migration("20230415193923_trailerid")]
    partial class trailerid
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("ActorMovie", b =>
                {
                    b.Property<int>("ActorsActorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MoviesMovieId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ActorsActorId", "MoviesMovieId");

                    b.HasIndex("MoviesMovieId");

                    b.ToTable("ActorMovie");
                });

            modelBuilder.Entity("Core.Models.Actor", b =>
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

            modelBuilder.Entity("Core.Models.Customer", b =>
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

            modelBuilder.Entity("Core.Models.Director", b =>
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

            modelBuilder.Entity("Core.Models.Movie", b =>
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

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("MaxScreenings")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<int>("ReleaseYear")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TrailerId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("MovieId");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Core.Models.MovieScreening", b =>
                {
                    b.Property<int>("MovieScreeningId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateAndTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("MovieId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("PricePerPerson")
                        .HasColumnType("TEXT");

                    b.Property<int>("TheaterId")
                        .HasColumnType("INTEGER");

                    b.HasKey("MovieScreeningId");

                    b.HasIndex("MovieId");

                    b.HasIndex("TheaterId");

                    b.ToTable("MovieScreenings");
                });

            modelBuilder.Entity("Core.Models.Reservation", b =>
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

            modelBuilder.Entity("Core.Models.Seat", b =>
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

            modelBuilder.Entity("Core.Models.Theater", b =>
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

                    b.Property<decimal>("TheaterPrice")
                        .HasColumnType("TEXT");

                    b.HasKey("TheaterId");

                    b.ToTable("Theaters");
                });

            modelBuilder.Entity("DirectorMovie", b =>
                {
                    b.Property<int>("DirectorsDirectorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MoviesMovieId")
                        .HasColumnType("INTEGER");

                    b.HasKey("DirectorsDirectorId", "MoviesMovieId");

                    b.HasIndex("MoviesMovieId");

                    b.ToTable("DirectorMovie");
                });

            modelBuilder.Entity("MovieScreeningSeat", b =>
                {
                    b.Property<int>("MovieScreeningsMovieScreeningId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ReservedSeatsSeatId")
                        .HasColumnType("INTEGER");

                    b.HasKey("MovieScreeningsMovieScreeningId", "ReservedSeatsSeatId");

                    b.HasIndex("ReservedSeatsSeatId");

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

            modelBuilder.Entity("ActorMovie", b =>
                {
                    b.HasOne("Core.Models.Actor", null)
                        .WithMany()
                        .HasForeignKey("ActorsActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("MoviesMovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Models.MovieScreening", b =>
                {
                    b.HasOne("Core.Models.Movie", "Movie")
                        .WithMany("MovieScreenings")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Models.Theater", "Theater")
                        .WithMany("MovieScreenings")
                        .HasForeignKey("TheaterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("Theater");
                });

            modelBuilder.Entity("Core.Models.Reservation", b =>
                {
                    b.HasOne("Core.Models.Customer", "Customer")
                        .WithMany("Reservations")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Models.MovieScreening", "MovieScreening")
                        .WithMany("Reservations")
                        .HasForeignKey("MovieScreeningId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("MovieScreening");
                });

            modelBuilder.Entity("Core.Models.Seat", b =>
                {
                    b.HasOne("Core.Models.Theater", "Theater")
                        .WithMany("Seats")
                        .HasForeignKey("TheaterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Theater");
                });

            modelBuilder.Entity("DirectorMovie", b =>
                {
                    b.HasOne("Core.Models.Director", null)
                        .WithMany()
                        .HasForeignKey("DirectorsDirectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("MoviesMovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieScreeningSeat", b =>
                {
                    b.HasOne("Core.Models.MovieScreening", null)
                        .WithMany()
                        .HasForeignKey("MovieScreeningsMovieScreeningId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Models.Seat", null)
                        .WithMany()
                        .HasForeignKey("ReservedSeatsSeatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReservationSeat", b =>
                {
                    b.HasOne("Core.Models.Reservation", null)
                        .WithMany()
                        .HasForeignKey("ReservationsReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Models.Seat", null)
                        .WithMany()
                        .HasForeignKey("SeatsSeatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Models.Customer", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("Core.Models.Movie", b =>
                {
                    b.Navigation("MovieScreenings");
                });

            modelBuilder.Entity("Core.Models.MovieScreening", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("Core.Models.Theater", b =>
                {
                    b.Navigation("MovieScreenings");

                    b.Navigation("Seats");
                });
#pragma warning restore 612, 618
        }
    }
}
