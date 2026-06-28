
namespace MathGame_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            bool _isAppActive = true;
            string? _userInput = string.Empty;
            ActivePage activePage = ActivePage.MainPage;

            while (_isAppActive)
            {
                switch (activePage)
                {
                    case ActivePage.MainPage:
                        DisplayMainMenu();
                        _userInput = Console.ReadLine();
                        Console.WriteLine($"You entered:{_userInput}");
                        _userInput = _userInput?.Trim().ToUpper();
                        if (_userInput != null)
                            activePage = GetNextPageFromUserInput(_userInput);
                        break;
                    default:
                        _isAppActive = false;
                        ExitApplication();
                        break;
                }
            }
            Console.WriteLine("Goodbye!");
            Thread.Sleep(500);
        }
        
        private static void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("========== MATH GAME CONSOLE ==========");
            Console.WriteLine(string.Empty);
            Console.WriteLine(" Choose an option:");
            Console.WriteLine("  [ 1 ] = Addition");
            Console.WriteLine("  [ 2 ] = Subtraction");
            Console.WriteLine("  [ 3 ] = Multiplication");
            Console.WriteLine("  [ 4 ] = Division");
            Console.WriteLine("  [ x ] = Exit Application");
            Console.WriteLine();
            Console.Write(" Input: ");
        }

        private static void ExitApplication()
        {
            for (int i = 3; i >= 0; i--)
            {
                Console.Clear();
                Console.WriteLine($"Closing in {i}...");
                Thread.Sleep(500);
            }
        }

        private static ActivePage GetNextPageFromUserInput(string userInput)
        {
            switch (userInput)
            {
                case "X":
                    return ActivePage.ExitPage;
                default:
                    return ActivePage.MainPage;
            }
        }
        
        private enum ActivePage
        {
            MainPage = 0,
            GamePage = 1,
            ExitPage = 2
        }
    }
    
    
}