namespace MathGame_Console
{
    internal abstract class Game
    {
        private protected int _num1 = 0;
        private protected int _num2 = 0;
        private protected int _answer = 0;
        public int PlayerAnswer { get; set; }
        private protected Random _random = new Random();
        
        public int GetAnswer()
        {
            return _answer;
        }

        public int GetNum1()
        {
            return _num1;
        }

        public int GetNum2()
        {
            return _num2;
        }

        public bool IsPlayerCorrect()
        {
            return _answer == PlayerAnswer;
        }
    }
    
    internal class AdditionGame : Game
    {
        public AdditionGame()
        {
            _num1 = _random.Next(0, 101);
            do
            {
                _num2 = _random.Next(0, 101);
            } while(_num2 == _num1);
            
            _answer = _num1 + _num2;
            PlayerAnswer = -1;
        }
        
        public override string ToString()
        {
            return $"{_num1} + {_num2}";
        }
    }

    internal class SubtractionGame : Game
    {
        public SubtractionGame()
        {
            int temp1 = _random.Next(0, 101);
            int temp2 = 0;
            do
            {
                temp2 = _random.Next(0, 101);
            }while (temp1 == temp2);
            
            _num1 = Math.Max(temp1, temp2);
            _num2 = Math.Min(temp1, temp2);
            _answer = _num1 - _num2;
            PlayerAnswer = -1;
        }
        
        public override string ToString()
        {
            return $"{_num1} - {_num2}";
        }
    }

    internal class MultiplicationGame : Game
    {
        public MultiplicationGame()
        {
            _num1 = _random.Next(0, 101);
            _num2 = _random.Next(0, 101);
            _answer = _num1 * _num2;
            PlayerAnswer = -1;
        }
        
        public override string ToString()
        {
            return $"{_num1} x {_num2}";
        }
    }

    internal class DivisionGame : Game
    {
        public DivisionGame()
        {
            while (true)
            {
                _num2 = _random.Next(1, 101);
                _num1 = _num2 * _random.Next(2, 11);
                if (_num1 < 101)
                    break;
            }
            PlayerAnswer = -1;
        }

        public override string ToString()
        {
            return $"{_num1} / {_num2}";
        }
    }
}