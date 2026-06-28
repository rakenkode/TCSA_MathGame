namespace MathGame_Console
{
    internal abstract class Question
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
        
        public static Question[] CreateProblems(int mode)
        {
            int numQuestion = 5;
            Question[] questions = new Question[numQuestion];
            Random random = new Random();
            int type = 0;
            
            for (int i = 0; i < numQuestion; i++)
            {
                if (mode < 4)
                {
                    type = mode;
                }
                else
                {
                    Thread.Sleep(random.Next(0, 101));
                    type = random.Next(0, 4);
                }
                
                switch (type)
                {
                    case 0:
                        questions[i] = new AdditionProblem();
                        break;
                    case 1:
                        questions[i] = new SubtractionProblem();
                        break;
                    case 2:
                        questions[i] = new MultiplicationProblem();
                        break;
                    case 3:
                        questions[i] = new DivisionProblem();
                        break;
                }
            }
            
            return questions;
        }
    }
    
    internal class AdditionProblem : Question
    {
        public AdditionProblem()
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

    internal class SubtractionProblem : Question
    {
        public SubtractionProblem()
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

    internal class MultiplicationProblem : Question
    {
        public MultiplicationProblem()
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

    internal class DivisionProblem : Question
    {
        public DivisionProblem()
        {
            while (true)
            {
                _num2 = _random.Next(1, 101);
                _num1 = _num2 * _random.Next(2, 11);
                if (_num1 < 101)
                    break;
            }
            _answer = _num1 / _num2;
            PlayerAnswer = -1;
        }

        public override string ToString()
        {
            return $"{_num1} / {_num2}";
        }
    }
}