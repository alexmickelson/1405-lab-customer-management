global using MovieTuple = (string title, int runLengthMinutes);
global using ShowtimeTuple = (string title, System.DateTime showtime);

using Data;
using Shared;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace Test;
public class StorageTests
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
    public void CannotAddADuplicateMovie()
    {
        // arrange
        Theatre.ResetData();
        Theatre.AddMovie("Dune 2", 210);

        // act
        try
        {
            // should throw an exception
            Theatre.AddMovie("Dune 2", 5);
            // if an exception was not thrown, the test should fail
            Assert.Fail();
        }
        catch (InvalidOperationException e)
        {
            // if an exception was thrown with the correct message, pass the test
            Assert.Equal("Cannot add duplicate movie", e.Message);
        }
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
    public void CanAddShowtime()
    {
        // arrange
        Theatre.ResetData();
        Theatre.AddMovie("Dune 2", 210);

        // act
        Theatre.AddShowtime("Dune 2", new DateTime(2024, 4, 10, 18, 35, 0));

        // assert        
        List<ShowtimeTuple> expectedShowTimes = new()
        {
            ("Dune 2", new DateTime(2024, 4, 10, 18, 35, 0)),
        };
        Assert.Equal(expectedShowTimes, Theatre.Showtimes);
    }

    [Fact]
    public void CannotAddShowtime_IfMovieNotAdded()
    {
        // arrange
        Theatre.ResetData();
        try
        {
            // act
            Theatre.AddShowtime("Dune 2", new DateTime(2024, 4, 10, 18, 35, 0));
            // assert
            Assert.Fail();
        }
        catch (InvalidOperationException e)
        {
            // assert
            Assert.Equal("Cannot add showtime when movie not added", e.Message);
        }
    }

    [Fact]
    public void CanAddTwoShowtimes_ForTheSameMovie()
    {
        // arrange
        Theatre.ResetData();
        Theatre.AddMovie("Dune 2", 210);

        // act
        Theatre.AddShowtime("Dune 2", new DateTime(2024, 4, 10, 18, 35, 0));
        Theatre.AddShowtime("Dune 2", new DateTime(2024, 4, 9, 15, 35, 0));

        // assert        
        List<ShowtimeTuple> expectedShowTimes = new()
        {
            ("Dune 2", new DateTime(2024, 4, 10, 18, 35, 0)),
            ("Dune 2", new DateTime(2024, 4, 9, 15, 35, 0)),
        };
        Assert.Equal(expectedShowTimes, Theatre.Showtimes);
    }

    [Fact]
    public void CanAddTwoShowtimes_ForDifferentMovie()
    {
        // arrange
        Theatre.ResetData();
        Theatre.AddMovie("Dune 2", 210);
        Theatre.AddMovie("Hitchhikers Guide to the Galaxy", 42);

        // act
        Theatre.AddShowtime("Dune 2", new DateTime(2024, 4, 10, 18, 35, 0));
        Theatre.AddShowtime("Hitchhikers Guide to the Galaxy", new DateTime(2024, 4, 9, 15, 35, 0));

        // assert        
        List<ShowtimeTuple> expectedShowTimes = new()
        {
            ("Dune 2", new DateTime(2024, 4, 10, 18, 35, 0)),
            ("Hitchhikers Guide to the Galaxy", new DateTime(2024, 4, 9, 15, 35, 0)),
        };
        Assert.Equal(expectedShowTimes, Theatre.Showtimes);
    }

    [Fact]
    public void CanSaveShowtimes()
    {
        // arrange
        Theatre.ResetData();
        Theatre.AddMovie("Dune 2", 210);
        Theatre.AddMovie("Hitchhikers Guide to the Galaxy", 42);

        Theatre.AddShowtime("Dune 2", new DateTime(2024, 4, 10, 18, 35, 0));
        Theatre.AddShowtime("Dune 2", new DateTime(2024, 4, 11, 18, 35, 0));
        Theatre.AddShowtime("Hitchhikers Guide to the Galaxy", new DateTime(2024, 4, 9, 15, 35, 0));

        // act
        Theatre.SaveMoviesAndShowtimesToFiles();

        // assert
        string showtimeFilePath = Persistence.GetBasePath() + "showtimeData.txt";
        string showtimeFileString = File.ReadAllText(showtimeFilePath);
        Assert.Contains("Dune 2;4/10/2024 6:35:00", showtimeFileString);
        Assert.Contains("Dune 2;4/11/2024 6:35:00", showtimeFileString);
        Assert.Contains("Hitchhikers Guide to the Galaxy;4/9/2024 3:35:00", showtimeFileString);
    }

    [Fact]
    public void CanLoadShowtimeData_AfterAddingAndStoring()
    {
        // arrange
        Theatre.ResetData();
        Theatre.AddMovie("Dune 2", 210);
        Theatre.AddMovie("Hitchhikers Guide to the Galaxy", 42);
        Theatre.AddMovie("A New Hope", 198);

        Theatre.AddShowtime("Dune 2", new DateTime(2024, 4, 10, 18, 35, 0));
        Theatre.AddShowtime("Dune 2", new DateTime(2024, 4, 15, 20, 35, 0));
        Theatre.AddShowtime("Hitchhikers Guide to the Galaxy", new DateTime(2024, 4, 14, 15, 32, 0));
        Theatre.AddShowtime("A New Hope", new DateTime(2024, 4, 12, 17, 20, 0));
        Theatre.SaveMoviesAndShowtimesToFiles();
        Theatre.ResetData();

        // act
        Theatre.LoadMoviesAndShowtimesFromFiles();

        // assert
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