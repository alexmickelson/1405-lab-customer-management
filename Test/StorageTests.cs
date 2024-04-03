global using MovieTuple = (string title, int runLengthMinutes);
global using ShowtimeTuple = (string title, System.DateTime showtime);

using Data;
using Shared;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace Test;
public class UnitTest1
{
    [Fact]
    public void CanAddANewMovieToTheatre()
    {
        //arrange
        Theatre.ResetData();

        // act
        Theatre.AddMovie("Dune 2", 210);

        // assert
        List<MovieTuple> expectedMovies = new List<MovieTuple>()
        {
            ("Dune 2", 210),
        };
        Assert.Equal(expectedMovies, Theatre.Movies);
    }

    [Fact]
    public void CanAddThreeMoviesToTheatre()
    {
        // arrange
        Theatre.ResetData();

        // act
        Theatre.AddMovie("Dune 2", 210);
        Theatre.AddMovie("Hitchhikers Guide to the Galaxy", 42);
        Theatre.AddMovie("A New Hope", 198);
        
        // assert
        List<MovieTuple> expectedMovies = new List<MovieTuple>()
        {
            ("Dune 2", 210),
            ("Hitchhikers Guide to the Galaxy", 42),
            ("A New Hope", 198)
        };
        Assert.Equal(expectedMovies, Theatre.Movies);
    }

    [Fact]
    public void CanStoreMovieToFile()
    {
        // arrange
        Theatre.ResetData();
        Theatre.AddMovie("Dune 2", 210);

        // act
        Theatre.SaveMoviesAndShowtimesToFiles();

        // assert
        string moviesFilePath = Persistence.GetBasePath() + "movieData.txt";
        string movieStringStoredInFile = File.ReadAllText(moviesFilePath);

        Assert.Contains("Dune 2;210", movieStringStoredInFile);
    }

    [Fact]
    public void CanStoreAndLoad_ASingleMovie()
    {
        // arrange
        Theatre.ResetData();
        Theatre.AddMovie("Dune 2", 210);
        Theatre.SaveMoviesAndShowtimesToFiles();
        Theatre.ResetData();

        // act
        Theatre.LoadMoviesAndShowtimesFromFiles();

        // assert
        List<MovieTuple> expectedMovies = new()
        {
            ("Dune 2", 210)
        };
        Assert.Equal(expectedMovies, Theatre.Movies);
    }

    [Fact]
    public void CanStoreAndLoadManyMovies()
    {
        // arrange
        Theatre.ResetData();
        Theatre.AddMovie("Dune 2", 210);
        Theatre.AddMovie("Hitchhikers Guide to the Galaxy", 42);
        Theatre.AddMovie("A New Hope", 198);
        Theatre.SaveMoviesAndShowtimesToFiles();
        Theatre.ResetData();
        
        // act
        Theatre.LoadMoviesAndShowtimesFromFiles();

        // assert
        List<MovieTuple> expectedMovies = new()
        {
            ("Dune 2", 210),
            ("Hitchhikers Guide to the Galaxy", 42),
            ("A New Hope", 198)
        };
        Assert.Equal(expectedMovies, Theatre.Movies);
    }

    [Fact]
    public void CanLoadShowtimeData()
    {
        Theatre.LoadMoviesAndShowtimesFromFiles();

        List<ShowtimeTuple> expectedShowTimes = new()
        {
            ("Dune 2", new DateTime(2024, 4, 10, 18, 35, 0)),
            ("Dune 2", new DateTime(2024, 4, 15, 20, 35, 0)),
            ("Hitchhikers Guide to the Galaxy", new DateTime(2024, 4, 14, 15, 32, 0)),
            ("A New Hope", new DateTime(2024, 4, 12, 17, 20, 0))
        };
        Assert.Equal(expectedShowTimes, Theatre.Showtimes);
    }
}