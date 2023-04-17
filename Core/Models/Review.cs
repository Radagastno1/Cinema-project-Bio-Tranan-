namespace Core.Models;

public class Review
{
    public int ReviewId{get;set;}
    public string Alias{get;set;}
    public int Rating{get;set;}
    public string Comment{get;set;}
    public int ReservationCode{get;set;}
    public Movie Movie{get;set;}
    public int MovieId{get;set;}
    public Review(){}

    public Review(int reviewId, int rating, string comment, Movie movie)
    {
        ReviewId = reviewId;
        Rating = rating;
        Comment = comment;
        Movie = movie;
        MovieId = movie.MovieId;
    }
}