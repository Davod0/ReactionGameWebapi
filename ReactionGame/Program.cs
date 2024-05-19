using System.Diagnostics;
using System.Net.Http.Json;
namespace ReactionGame
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            await RunReactionGame();
        }

        static async Task RunReactionGame()
        {
            Random random = new Random();
            while (true)
            {
                Console.Clear();
                Stopwatch stopwatch = new();
                Console.WriteLine("Tryck valfri tangent för att starta spelet!");
                Console.ReadKey(true);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\nVänta lite");

                float waitTime = random.Next(500, 3000);
                while (Console.KeyAvailable != true && waitTime > 0)
                {
                    Thread.Sleep(100);
                    waitTime -= 100;
                    Console.Write(".");
                }
                if (waitTime > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nTjuvstart! Prova igen.");
                    Console.ReadKey(true);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nTryck NU!!\n");
                    stopwatch.Start();
                    Console.ReadKey(true);
                    stopwatch.Stop();

                    Console.ResetColor();
                    Console.Write("Det tog: " + stopwatch.ElapsedMilliseconds + " millisekunder! ");

                    if (await IsNewHighscore(stopwatch.ElapsedMilliseconds))
                    {
                        RegisterNewHighscore(stopwatch.ElapsedMilliseconds);
                    }

                    Console.WriteLine("\n\nHIGHSCORE:");
                    var topTen = await GetTopTenHighscores();
                    for (int i = 0; i < topTen.Count; i++)
                    {
                        Console.WriteLine(topTen[i]);
                    }
                }
                Console.ResetColor();
                Console.WriteLine("\nTryck på valfri tangent för att börja om, eller Q för att avsluta.");
                if (Console.ReadKey(true).Key == ConsoleKey.Q) Environment.Exit(0);
            }
        }
        private static async Task<bool> IsNewHighscore(long elapsedMilliseconds)
        {
            var list = await GetAllHighscores();
            if (list.Count == 0) return true;
            bool lowerFound = false;
            foreach (Highscore score in list)
            {
                if (score.Score <= elapsedMilliseconds)
                {
                    lowerFound = true;
                }
            }
            return !lowerFound;
        }
        static async void RegisterNewHighscore(long time)
        {
            Console.Write("Nytt rekord!");
            Console.Write("\nAnge ditt namn: ");
            string? PlayerName = Console.ReadLine();
            Console.Write("\nAnge spel namnet: ");
            string? GameName = Console.ReadLine();
            Highscore highscore = new Highscore
            {
                PlayerName = PlayerName,
                Score = time,
                GameName = GameName
            };
            await PostHighscore(highscore);
        }
        static async Task<List<Highscore>> GetAllHighscores()
        {
            HttpClient httpClient = new();
            try
            {
                List<Highscore>? highscores = await httpClient.GetFromJsonAsync<List<Highscore>>("http://localhost:5203/highscores");
                return highscores;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving high score: {ex.Message}");
                return null;
            }
            finally
            {
                httpClient.Dispose();
            }
        }
        static async Task<List<Highscore>> GetTopTenHighscores()
        {
            HttpClient httpClient = new();
            try
            {
                List<Highscore>? highscores = await httpClient.GetFromJsonAsync<List<Highscore>>("http://localhost:5203/highscores/top10");
                return highscores;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving high score: {ex.Message}");
                return null;
            }
            finally
            {
                httpClient.Dispose();
            }
        }
        static async Task<string> PostHighscore(Highscore hs)
        {
            HttpClient httpClient = new();
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("http://localhost:5203/highscores", hs);
            if (response.IsSuccessStatusCode)
            {
                return $"Highscore successfully posted";
            }
            else
            {
                return $"Failed to post highscore. Status code: {response.StatusCode}";
            }
        }
    }
}




