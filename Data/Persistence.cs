global using MovieTuple = (string title, int runLengthMinutes);
global using ShowtimeTuple = (string title, System.DateTime showtime);

namespace Data;


public static class Persistence
{
  public static List<MovieTuple> LoadMovies()
  {
    string filePath = GetBasePath() + "movieData.txt";

    List<MovieTuple> movies = new();

    // for each line in the file, parse out the MovieTuple
    foreach (string rawMovie in File.ReadAllLines(filePath))
    {
      MovieTuple newMovieTuple = new MovieTuple();
      newMovieTuple.title = rawMovie.Split(";")[0];
      newMovieTuple.runLengthMinutes = int.Parse(rawMovie.Split(";")[1]);

      movies.Add(newMovieTuple);
    }
    return movies;
  }

  public static List<ShowtimeTuple> LoadShowtimes()
  {
    // TODO: actually read showtime file and convert lines into ShowtimeTuple's
    return new List<ShowtimeTuple>();
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
    // TODO: Actually store showtimes to a file
  }


  // do not change, makes datafiles discoverable between tests and console
  public static string GetBasePath()
  {
    if (Directory.GetCurrentDirectory().Contains("Console"))
      return "../";
    if (Directory.GetCurrentDirectory().Contains("Test"))
      return "../../../../";

    return "./";
  }

}
