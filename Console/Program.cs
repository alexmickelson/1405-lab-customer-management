

// the main function will run the infinite loop for the menu
// each option will be handled by the functions in UserInterfaceFunctions.cs
while (true)
{
  Console.Clear();
  Console.WriteLine(@"Welcome to the Movie Theatre. What would you like to do
1. Load Movies and Showtimes from files
2. View current and upcoming Movies
3. View Current Movie schedule
4. Register a new upcoming Movie
5. Add a Showtime to a current Movie
6. Store Movies and Showtimes to files
");
  var userMenuChoice = Console.ReadLine();
  switch (userMenuChoice)
  {
    case "1":
      UserInterfaceFunctions.LoadMovies();
      break;
    case "2":
      UserInterfaceFunctions.ViewCurrentAndUpcomingMovies();
      break;
    case "3":
      UserInterfaceFunctions.ViewCurrentMovieSchedule();
      break;
    case "4":
      UserInterfaceFunctions.RegisterNewMovie();
      break;
    case "5":
      UserInterfaceFunctions.AddShowtimeToCurrentMovie();
      break;
    case "6":
      UserInterfaceFunctions.PersistMoviesAndShowtimes();
      break;
    default:
      Console.WriteLine("Invalid option");
      break;
  }

  Console.WriteLine("Press Any Key to Continue...");
  Console.ReadKey(true);
  while (Console.KeyAvailable)
  {
    Console.ReadKey(true);
  }
}
