


while (true)
{
  Console.WriteLine(@"Welcome to the Movie Theatre. What would you like to do
1. Load Movies and Showtimes from files
2. View current and upcoming Movies
3. View Current Movie schedule
4. Register a new upcoming Movie
5. Add a Showtime to a current Movie
6. Store Movies and Showtimes to files
");
  // switch()

  Console.WriteLine("Press Any Key to Continue...");
  while (Console.KeyAvailable)
  {
    Console.ReadKey(true);
  }
  Console.Clear();
}