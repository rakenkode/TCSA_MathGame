
using System.Diagnostics;

namespace MathGame_Console
{
    class Program
    {
        private static bool _isAppActive = true;
        private static int _gameNumber = 0;
        private static Game[] _games = Array.Empty<Game>();
        private static Stopwatch _stopWatch = new Stopwatch();
        
        
        static void Main(string[] args)
        {
            ActivePage activePage = ActivePage.MainPage;

            while (_isAppActive)
            {
                switch (activePage)
                {
                    case ActivePage.MainPage:
                        DisplayMain();
                        var userInput = Console.ReadLine()?.Trim().ToUpper();
                        Console.WriteLine($"You entered:{userInput}");
                        if (userInput != null) activePage = NavigateFromMain(userInput);
                        break;
                    case ActivePage.GamePage:
                        if (_gameNumber < 5)
                        {
                            DisplayGame(_gameNumber);
                            var gameInput = Console.ReadLine()?.Trim().ToUpper();
                            if (gameInput != null) activePage = NavigateFromGame(gameInput);
                        }
                        else
                        {
                            DisplayResults();
                            var gameInput = Console.ReadLine()?.Trim().ToUpper();
                            if (gameInput != null) activePage = NavigateFromGameResults(gameInput);
                        }

                        break;
                    default:
                        ExitApplication();
                        break;
                }
            }
            Console.WriteLine("Goodbye!");
            Thread.Sleep(500);
        }
        
        private static void ExitApplication()
        {
            _isAppActive = false;
            
            if(_stopWatch.IsRunning)
                _stopWatch.Stop();
            
            for (int i = 3; i >= 0; i--)
            {
                Console.Clear();
                Console.WriteLine($"Closing in {i}...");
                Thread.Sleep(500);
            }
        }
        
        private static void DisplayMain()
        {
            Console.Clear();
            Console.WriteLine("========== MATH GAME CONSOLE ==========");
            Console.WriteLine(string.Empty);
            Console.WriteLine(" Choose an option:");
            Console.WriteLine("  [ 1 ] = Addition");
            Console.WriteLine("  [ 2 ] = Subtraction");
            Console.WriteLine("  [ 3 ] = Multiplication");
            Console.WriteLine("  [ 4 ] = Division");
            Console.WriteLine("  [ 5 ] = Random");
            Console.WriteLine("  [ x ] = Exit Application");
            Console.WriteLine();
            Console.Write(" Input: ");
        }
        
        private static ActivePage NavigateFromMain(string userInput)
        {
            switch (userInput)
            {
                case "X":
                    return ActivePage.ExitPage;
                case "1":
                    _gameNumber = 0;
                    _games = Game.CreateGames(0);
                    _stopWatch.Restart();
                    return ActivePage.GamePage;
                case "2":
                    _gameNumber = 0;
                    _games = Game.CreateGames(1);
                    _stopWatch.Restart();
                    return ActivePage.GamePage;
                case "3":
                    _gameNumber = 0;
                    _games = Game.CreateGames(2);
                    _stopWatch.Restart();
                    return ActivePage.GamePage;
                case "4":
                    _gameNumber = 0;
                    _games = Game.CreateGames(3);
                    _stopWatch.Restart();
                    return ActivePage.GamePage;
                case "5":
                    _gameNumber = 0;
                    _games = Game.CreateGames(4);
                    _stopWatch.Restart();
                    return ActivePage.GamePage;
                default:
                    return ActivePage.MainPage;
            }
        }

        private static void DisplayGame(int gameNumber)
        {
            Console.Clear();
            Console.WriteLine($"===========   GAME {gameNumber + 1} of 5   ===========");
            Console.WriteLine(string.Empty);
            Console.WriteLine($"      {_games[gameNumber].ToString()}");
            Console.WriteLine(string.Empty);
            Console.Write("      Answer: ");
        }

        private static ActivePage NavigateFromGame(string userInput)
        {
            if (Int32.TryParse(userInput, out int number))
            {
                _games[_gameNumber].PlayerAnswer = number;
                
                _gameNumber++;
                if (_gameNumber == 5)
                    _stopWatch.Stop();
            }
            else if (userInput.Equals("X", StringComparison.OrdinalIgnoreCase))
            {
                return ActivePage.ExitPage;
            }
            
            return ActivePage.GamePage;
        }

        private static void DisplayResults()
        {
            double rating = 100;
            int score = 0;
            Console.Clear();
            Console.WriteLine("===========   GAME   OVER   ===========");
            Console.WriteLine(string.Empty);
            Console.WriteLine($"   problem         answer      result");
            
            for (int i = 0; i < _games.Length; i++)
            {
                if (_games[i].IsPlayerCorrect())
                    score++;
                
                Console.WriteLine($"   {_games[i].ToString(),-15}  {_games[i].PlayerAnswer.ToString(),-10}  {(_games[i].IsPlayerCorrect() ? "pass" : "fail")}");
            }
            rating = (score / (double) _games.Length) * 100;
            
            Console.WriteLine(string.Empty);
            Console.WriteLine($"  Game time: {_stopWatch.ElapsedMilliseconds / 1000} sec     Rating: {rating:0}%");
            Console.WriteLine(string.Empty);
            Console.WriteLine(" Choose an option:");
            Console.WriteLine("  [ n ] = New Game");
            Console.WriteLine("  [ x ] = Exit Application");
            Console.WriteLine();
            Console.Write(" Input: ");
        }

        private static ActivePage NavigateFromGameResults(string userInput)
        {
            if (userInput.Equals("X", StringComparison.OrdinalIgnoreCase))
            {
                return ActivePage.ExitPage;
            }
            else if (userInput.Equals("N", StringComparison.OrdinalIgnoreCase))
            {
                return ActivePage.MainPage;
            }
            
            return ActivePage.GamePage;
        }
        
        private enum ActivePage
        {
            MainPage = 0,
            GamePage = 1,
            ExitPage = 2
        }
    }
    
    
}