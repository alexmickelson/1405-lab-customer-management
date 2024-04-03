global using MovieTuple = (string title, int runLengthMinutes);
global using ShowtimeTuple = (string title, System.DateTime showtime);

using Shared;

public static class UserInterfaceFunctions
{
  public static void LoadMovies()
  {
    Theatre.LoadMoviesAndShowtimesFromFiles();
    Console.WriteLine("Movies Loaded!");
  }

  public static void ViewCurrentAndUpcomingMovies()
  {

    Console.WriteLine($"                                   Title | Runtime");
    Console.WriteLine($"-----------------------------------------+--------");
    foreach (MovieTuple movie in Theatre.Movies)
    {
      Console.WriteLine($"{movie.title,40} | {movie.runLengthMinutes}");
    }
  }

  public static void ViewCurrentMovieSchedule()
  {
    // TODO:
    // for each movie, show a list of showtimes

  }

  public static void RegisterNewMovie()
  {
    Console.WriteLine("Input the movie title");
    string movieTitle = Console.ReadLine();

    Console.WriteLine("Input the runtime in minutes");
    string runtimeString = Console.ReadLine();

    try
    {
      int runtime = int.Parse(runtimeString);
      Theatre.AddMovie(movieTitle, runtime);
      Console.WriteLine("Movie Added!");
    }
    catch (Exception e)
    {
      Console.WriteLine("Error creating movie " + e.Message);
      Console.WriteLine("Please try again");

      // recurse to have the user try to input a movie
      RegisterNewMovie();
    }
  }

  public static void AddShowtimeToCurrentMovie()
  {
    // TODO
    // ask the user for the movie title and datetime
    // use Theatre.AddShowtime() to add the showtime
  }

  public static void PersistMoviesAndShowtimes()
  {
    Theatre.SaveMoviesAndShowtimesToFiles();
    Console.WriteLine("Movies Saved!");
  }
}