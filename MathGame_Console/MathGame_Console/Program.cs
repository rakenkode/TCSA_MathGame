
using System.Diagnostics;

namespace MathGame_Console
{
    class Program
    {
        private static bool _isAppActive = true;
        private static int _questionNumber = 0;
        private static int _numberOfQuestions = 5;
        private static int _selectedGameNumber = 1;
        private static Game game = new Game(GameType.Addition, 5);
        private static List<Game> games = new List<Game>();
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
                        if (userInput != null) activePage = NavigateFromMain(userInput);
                        break;
                    case ActivePage.GamePage:
                        if (_questionNumber < 5)
                        {
                            DisplayGame(_questionNumber);
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
                    case ActivePage.HistoryPage:
                        if (games.Count == 0)
                        {
                            DisplayZeroGameHistory();
                            var historyInput = Console.ReadLine()?.Trim().ToUpper();
                            if (historyInput != null) activePage = NavigateFromZeroGameHistory(historyInput);
                        }
                        else
                        {
                            DisplayGameHistory();
                            var historyInput = Console.ReadLine()?.Trim().ToUpper();
                            if (historyInput != null) activePage = NavigateFromGameHistory(historyInput);
                        }
                        break;
                    case ActivePage.GameInfoPage:
                        DisplayGameInfo();
                        var gameDataInput = Console.ReadLine()?.Trim().ToUpper();
                        if (gameDataInput != null) activePage = NavigateFromGameInfo(gameDataInput);
                        break;
                    default:
                        ExitApplication();
                        break;
                }
            }
            
            Console.WriteLine("Goodbye!");
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
                Thread.Sleep(400);
            }
        }
        
        private static void DisplayMain()
        {
            Console.Clear();
            Console.WriteLine("========== MATH GAME CONSOLE ==========");
            Console.WriteLine(string.Empty);
            Console.WriteLine(" Choose an option:");
            Console.WriteLine("  [ 1 ] = Addition Game");
            Console.WriteLine("  [ 2 ] = Subtraction Game");
            Console.WriteLine("  [ 3 ] = Multiplication Game");
            Console.WriteLine("  [ 4 ] = Division Game");
            Console.WriteLine("  [ 5 ] = Random Game");
            Console.WriteLine("  [ 6 ] = Game History");
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
                    game = new Game(GameType.Addition, _numberOfQuestions);
                    InitGameParams();
                    return ActivePage.GamePage;
                case "2":
                    game = new Game(GameType.Subtraction, _numberOfQuestions);
                    InitGameParams();
                    return ActivePage.GamePage;
                case "3":
                    game = new Game(GameType.Multiplication, _numberOfQuestions);
                    InitGameParams();
                    return ActivePage.GamePage;
                case "4":
                    game = new Game(GameType.Division, _numberOfQuestions);
                    InitGameParams();
                    return ActivePage.GamePage;
                case "5":
                    game = new Game(GameType.Random, _numberOfQuestions);
                    InitGameParams();
                    return ActivePage.GamePage;
                case "6":
                    return ActivePage.HistoryPage;
                default:
                    return ActivePage.MainPage;
            }
        }

        private static void InitGameParams()
        {
            _questionNumber = 0;
            _stopWatch.Restart();
        }
        
        private static void DisplayGame(int gameNumber)
        {
            Console.Clear();
            Console.WriteLine($"===========   GAME {gameNumber + 1} of 5   ===========");
            Console.WriteLine(string.Empty);
            Console.WriteLine($"      Problem:  {game.Questions[gameNumber].ToString()}");
            Console.WriteLine(string.Empty);
            Console.Write("      Answer [ x  to quit ]: ");
        }

        private static ActivePage NavigateFromGame(string userInput)
        {
            if (Int32.TryParse(userInput, out int number))
            {
                game.Questions[_questionNumber].PlayerAnswer = number;
                _questionNumber++;
                if (_questionNumber == 5)
                {
                    _stopWatch.Stop();
                    game.Duration = (int) _stopWatch.ElapsedMilliseconds / 1000;
                    game.Evaluate();
                    games.Add(game);
                }
            }
            else if (userInput.Equals("X", StringComparison.OrdinalIgnoreCase))
            {
                return ActivePage.ExitPage;
            }
            
            return ActivePage.GamePage;
        }

        private static void DisplayGameData(Game game)
        {
            Console.WriteLine($"   problem         answer      result");
            for (int i = 0; i < game.Questions.Length; i++)
            {
                Console.WriteLine($"   {game.Questions[i].ToString(),-14}  {game.Questions[i].PlayerAnswer.ToString(),-10}  {(game.Questions[i].IsPlayerCorrect() ? "pass" : "fail")}");
            }
            Console.WriteLine(string.Empty);
            Console.WriteLine($"  Game time: {game.Duration} sec     Rating: {game.Rating}%");
        }
        
        private static void DisplayResults()
        {
            Console.Clear();
            Console.WriteLine("===========   GAME   OVER   ===========");
            Console.WriteLine(string.Empty);
            DisplayGameData(game);
            Console.WriteLine(string.Empty);
            Console.WriteLine(" Choose an option:");
            Console.WriteLine("  [ m ] = Main Menu");
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
            else if (userInput.Equals("M", StringComparison.OrdinalIgnoreCase))
            {
                return ActivePage.MainPage;
            }
            
            return ActivePage.GamePage;
        }

        private static void DisplayZeroGameHistory()
        {
            Console.Clear();
            Console.WriteLine("===========   GAME HISTORY   ==========");
            Console.WriteLine(string.Empty);
            Console.WriteLine(" There are no previous games played.");
            Console.WriteLine(string.Empty);
            Console.WriteLine(" Choose an option:");
            Console.WriteLine("  [ m ] = Main Menu");
            Console.WriteLine("  [ x ] = Exit Application");
            Console.WriteLine();
            Console.Write(" Input: ");
        }

        private static ActivePage NavigateFromZeroGameHistory(string userInput)
        {
            if (userInput.Equals("M", StringComparison.OrdinalIgnoreCase))
            {
                return ActivePage.MainPage;
            }
            else if (userInput.Equals("X", StringComparison.OrdinalIgnoreCase))
            {
                return ActivePage.ExitPage;
            }

            return ActivePage.HistoryPage;
        }

        private static void DisplayGameHistory()
        {
            Console.Clear();
            Console.WriteLine("===========   GAME HISTORY   ==========");
            Console.WriteLine(string.Empty);

            Console.WriteLine("Game#    Type            Rating      Duration");
            for (int i = 0; i < games.Count; i++)
            {
                Console.WriteLine($"  {(i + 1).ToString(),-6} {games[i].GameType,-15} {games[i].Rating + "%",-11} {games[i].Duration} sec");
            }
            
            Console.WriteLine(string.Empty);
            Console.WriteLine(" Choose an option:");
            Console.WriteLine("  [ n ] = Where n is Game# to view");
            Console.WriteLine("  [ m ] = Main Menu");
            Console.WriteLine("  [ x ] = Exit Application");
            Console.WriteLine();
            Console.Write(" Input: ");
        }

        private static ActivePage NavigateFromGameHistory(string userInput)
        {
            if (int.TryParse(userInput, out int number))
            {
                if (number > 0 && number <= games.Count)
                {
                    _selectedGameNumber = number;
                    return ActivePage.GameInfoPage;
                }
            }
            else
            {
                if (userInput.Equals("M", StringComparison.OrdinalIgnoreCase))
                {
                    return ActivePage.MainPage;
                }
                else if (userInput.Equals("X", StringComparison.OrdinalIgnoreCase))
                {
                    return ActivePage.ExitPage;
                }
            }
            return ActivePage.HistoryPage;
        }

        private static void DisplayGameInfo()
        {
            Console.Clear();
            Console.WriteLine("===========   GAME   INFO   ===========");
            Console.WriteLine(string.Empty);
            DisplayGameData(games[_selectedGameNumber - 1]);
            Console.WriteLine(string.Empty);
            Console.WriteLine(" Choose an option:");
            Console.WriteLine("  [ h ] = Game History");
            Console.WriteLine("  [ m ] = Main Menu");
            Console.WriteLine("  [ x ] = Exit Application");
            Console.WriteLine();
            Console.Write(" Input: ");
        }

        private static ActivePage NavigateFromGameInfo(string userInput)
        {
            switch (userInput)
            {
                case "H":
                    return ActivePage.HistoryPage;
                case "M":
                    return ActivePage.MainPage;
                case "X":
                    return ActivePage.ExitPage;
                default:
                    return ActivePage.GameInfoPage;
            }
        }
        
        private enum ActivePage
        {
            MainPage = 0,
            GamePage = 1,
            HistoryPage = 2,
            GameInfoPage = 3,
            ExitPage = 4
        }
    }
    
    
}