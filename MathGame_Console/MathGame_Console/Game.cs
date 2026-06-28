namespace MathGame_Console;

public class Game
{
    public double Rating { get; set; }
    public int Duration { get; set; }
    public GameType GameType { get;}
    public Question[] Questions;
    
    public Game(GameType gameType, int numberOfQuestions)
    {
        GameType = gameType;
        Questions = new Question[numberOfQuestions];
        Questions = Question.CreateProblems(gameType, numberOfQuestions);
    }
}

public enum GameType
{
    Addition = 0,
    Subtraction = 1,
    Multiplication = 2,
    Division = 3,
    Random = 4
}