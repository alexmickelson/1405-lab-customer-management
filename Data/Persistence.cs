global using MovieTuple = (string title, int runLengthMinutes);
global using ShowtimeTuple = (string title, System.DateTime showtime);

namespace Data;


public static class Persistence
{
  public static List<MovieTuple> LoadMovies()
  {
    string filePath = GetBasePath() + "movieData.txt";

    List<MovieTuple> movies = new();

    foreach (var rawMovie in File.ReadAllLines(filePath))
    {
      var newMovieTuple = new MovieTuple();
      newMovieTuple.title = rawMovie.Split(";")[0];
      newMovieTuple.runLengthMinutes = int.Parse(rawMovie.Split(";")[1]);

      movies.Add(newMovieTuple);
    }
    return movies;
  }

  public static List<ShowtimeTuple> LoadShowtimes()
  {
    string filePath = GetBasePath() + "showtimeData.txt";
    List<ShowtimeTuple> movies = new();

    foreach (var rawMovie in File.ReadAllLines(filePath))
    {
      var newShowtimeTuple = new ShowtimeTuple();
      newShowtimeTuple.title = rawMovie.Split(";")[0];
      newShowtimeTuple.showtime = DateTime.Parse(rawMovie.Split(";")[1]);

      movies.Add(newShowtimeTuple);
    }
    return movies;
  }
  
  public static void PersistMovies(List<MovieTuple> movies)
  {
    // convert data into a string representation
    List<string> moviesAsStrings = new List<string>();
    foreach(MovieTuple movie in movies)
    {
      string movieAsString = $"{movie.title};{movie.runLengthMinutes}";
      moviesAsStrings.Add(movieAsString);
    }

    // store string representation in file
    string filePath = GetBasePath() + "movieData.txt";
    File.WriteAllLines(filePath, moviesAsStrings);

  }

  public static void PersistShowtimes(List<ShowtimeTuple> showtimes)
  {
    // List<string> showtimesAsStrings = new List<string>();
    // foreach(ShowtimeTuple showtime in showtimes)
    // {
    //   string showingAsString = $"{showtime.title};{showtime.showtime}";
    //   showtimesAsStrings.Add(showingAsString);
    // }

    // string filePath = GetBasePath() + "showtimeData.txt";
    // File.WriteAllLines(filePath, showtimesAsStrings);

  }


  public static string GetBasePath()
  {
    if (Directory.GetCurrentDirectory().Contains("Console"))
      return "../";
    if (Directory.GetCurrentDirectory().Contains("Test"))
      return "../../../../";

    return "./";
  }

}
