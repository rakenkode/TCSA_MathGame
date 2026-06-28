namespace MathGame_Console;

public class Game
{
    private double _rating = 0;
    public double Rating
    {
        get { return _rating; }
    }
    public int Duration { get; set; }
    public GameType GameType { get;}
    public Question[] Questions;
    
    public Game(GameType gameType, int numberOfQuestions)
    {
        GameType = gameType;
        Questions = new Question[numberOfQuestions];
        Questions = Question.CreateProblems(gameType, numberOfQuestions);
    }
    
    public void Evaluate()
    {
        int score = 0;
        
        for (int i = 0; i < Questions.Length; i++)
        {
            if (Questions[i].IsPlayerCorrect())
                score++;
        }
        _rating = Math.Round((score / (double) Questions.Length) * 100, 1);
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