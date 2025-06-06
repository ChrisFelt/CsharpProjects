using MathGame;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Timers;

class Game
{
    // properties:
    // difficulty mode, no getter or setter
    // operation menu choice 1 - 5 (+, -, *, /, random), no getter or setter
    // timer object, no getter or setter
    // time elapsed, read-only
    // List of operations INCLUDING "random", no getter or setter
    // List of questions asked and answers given, read-only
    private string _difficultyMode;
    private string _operationChoice;
    private System.Timers.Timer _timer;
    private String[] _operations = { "+", "-", "*", "/" };
    private String[] _difficulties = { "too easy", "easy", "normal" };
    public int TimeElapsed { get; private set; }
    public List<string> QuestionHistory { get; private set; }

    // array to hold list of exact divisors for each number up to 100
    static private List<int>[] _divisors;

    // extra properties that may be used:
    // count of questions asked, read-only
    // count of correct answers given, read-only
    public int TotalQuestions { get; private set; }
    public int CorrectAnswers { get; private set; }

    // constructor
    public Game(string difficulty, string operation)
    {
        _difficultyMode = difficulty;
        _operationChoice = operation;
        StartTimer();
    }

    // static constructor allows ExactDivisors to be called only once 
    // and the results shared across all instances of Game
    static Game()
    {
        _divisors = ExactDivisors(100);
    }

    // methods:
    // 1. internal start and stop timer methods
    private void StartTimer()
    {
        // one second interval timer
        _timer = new System.Timers.Timer(1000);
        _timer.Elapsed += OnTimedEvent;
        _timer.AutoReset = true;  // enables repeated events
        _timer.Enabled = true;  // starts timer
    }
    public void StopTimer()
    {
        _timer.Stop();
        _timer.Dispose();
    }
    private void OnTimedEvent(object? source, ElapsedEventArgs e)
    {
        TimeElapsed++;
    }


    // 2. random number method uses difficulty mode to determine size of number
    private int GenerateNum(int start, int end)
    {
        Random rand = new Random();

        // to comply with division specifications:
        // in static constructor: generate all factors of every number between 0 and 100 and store in an array of lists
        // generate a random number between 0 and 100 for dividend
        // find a random factor of that number from the array of lists and use it as the divisor
        // 1 must always be an option
        // in case of a 0 for dividend, simply generate a random number between 1 and 100
        // 0 must never be an option for the divisor

        // todo: return expected value
        return 2;
    }

    static private List<int>[] ExactDivisors(int n)
    {
        // find all divisors for each number up to n using the Sieve of Eratosthenes
        // and store in result
        // time complexity of this algorithm: O(n log log n)
        List<int>[] result = new List<int>[n + 1];

        // initialize lists
        for (int i = 0; i < n + 1; i++)
        {
            result[i] = new List<int>();
        }

        for (int i = 1; i <= n; i++)
        {
            // add i to the list of each number in the array that it is a factor of
            for (int j = i; j <= n; j += i)
            {
                result[j].Add(i);
            }
        }

        return result;
    }

    // 3. check answer method takes operands, operator, and answer. 
    // Checks the answer given and updates questions asked and correct answers given properties
    // 4. play method uses (2) to generate random numbers, gets operator, and prints question, then gets user input and calls (3)
    // Finally, stores question and answer in history property
    // 5. print history method prints each question and answer on a new line, with total correct/total questions asked at the end
}