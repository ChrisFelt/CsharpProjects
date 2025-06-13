using MathGame;
using System;
using System.Buffers;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Timers;

class Game
{
    // fields:
    private int _difficultyMode;
    private int _operationChoice;
    private System.Timers.Timer _timer;
    private string[] _operations = { "+", "-", "*", "/" };
    // array to hold list of exact divisors for each number up to 100
    static private List<int>[] _divisors;
    private const int _operandMax = 100;
    private const int _operandMin = 0;

    // properties:
    public int TimeElapsed { get; private set; }
    public int TotalQuestions { get; private set; }
    public int CorrectAnswers { get; private set; }
    public List<(string question, int answer, int solution)> QuestionHistory { get; private set; }

    // constructor
    public Game(int difficulty, int operation)
    {
        TimeElapsed = 0;
        _difficultyMode = difficulty;
        _operationChoice = operation - 1;
        QuestionHistory = new List<(string question, int answer, int solution)>();
        StartTimer();
    }

    // static constructor allows ExactDivisors to be called only once 
    // and the results shared across all instances of Game
    static Game()
    {
        _divisors = ExactDivisors(_operandMax);
    }

    // methods:
    // 1. exact divisors method finds all divisors for each number up to n using the Sieve of Eratosthenes and stores in output
    static private List<int>[] ExactDivisors(int n)
    {
        // time complexity of this algorithm: O(n log log n)
        // initialize array of lists 
        // each index of the array represents the integer for which factors will be listed
        List<int>[] result = new List<int>[n + 1];
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

    // 2. start and stop timer methods
    private void StartTimer()
    {
        // one second interval timer
        _timer = new System.Timers.Timer(1000);
        _timer.Elapsed += OnTimedEvent;
        _timer.AutoReset = true;  // enables repeated events
        _timer.Enabled = true;  // starts timer
    }
    // TODO: make private and call when user enters "quit" in Play method?
    public void StopTimer()
    {
        _timer.Stop();
        _timer.Dispose();
    }
    private void OnTimedEvent(object? source, ElapsedEventArgs e)
    {
        TimeElapsed++;
    }

    // 3. generate question method uses difficulty mode and operator to generate a question
    private (string question, int solution) GenerateQuestion()
    {
        Random rand = new Random();
        int solution = rand.Next(_operandMax + 1);
        string question = $"{solution}";
        int next;

        // determine operation mode
        string mode;
        // randomize operator when user chooses random
        if (_operationChoice == 4)
        {
            mode = _operations[rand.Next(_operations.Length)];
        }
        // use user operator choice
        else
        {
            mode = _operations[_operationChoice];
        }

        // generate an extra number for each difficulty mode
        for (int i = 0; i < _difficultyMode; i++)
        {
            // switch case to check for mode
            switch (mode)
            {
                // all modes besides division: gen rand number, then determine question and solution
                case "+":
                    next = rand.Next(_operandMax + 1);
                    question += " + " + next;
                    solution += next;
                    break;

                case "-":
                    next = rand.Next(_operandMax + 1);
                    question += " - " + next;
                    solution -= next;
                    break;

                case "*":
                    next = rand.Next(_operandMax + 1);
                    question += " * " + next;
                    solution *= next;
                    break;

                // division: find a random factor of solution from _divisors, then determine question and solution
                case "/":
                    // account for 0 dividend edge case
                    if (solution == 0)
                    {
                        next = rand.Next(1, _operandMax + 1);  // divisor cannot be 0
                    }
                    else
                    {
                        next = _divisors[solution][rand.Next(_divisors[solution].Count)];
                    }
                    question += " / " + next;
                    solution /= next;
                    break;
            }

        }

        return (question, solution);
    }

    // 4. check answer method takes operands, operator, and answer. 
    // Checks the answer given and updates questions asked and correct answers given properties
    private void CheckAnswer(string question, int answer, int solution)
    {
        // notify user of results
        if (answer == solution)
        {
            Console.WriteLine($"Correct! {question} = {solution}");
            CorrectAnswers += 1;
        }
        else
        {
            Console.WriteLine($"Incorrect. {question} = {solution}");
        }

        // update stats and history
        TotalQuestions += 1;
        QuestionHistory.Add((question, answer, solution));
    }

    // 5. play method uses (2) to generate a question, prompts user for answer, and uses (4) to check the results
    // Repeats until user quits game
    public void Play()
    {
        while (true)
        {
            string? userInput;
            int userAnswer;
            bool validInput;
            Console.WriteLine();
            // assign question and solution tuple to separate variables
            (string question, int solution) = GenerateQuestion();

            // print question and get user input
            Console.Write($"{question} = ");

            // validate answer
            do
            {
                userInput = Console.ReadLine();

                // exit when user inputs "quit"
                if (userInput == "quit")
                {
                    // stop timer and display stats
                    StopTimer();
                    Console.WriteLine();
                    Console.WriteLine($"Game over. Time taken to complete: {TimeElapsed} seconds.");
                    Console.WriteLine($"You answered {CorrectAnswers} / {TotalQuestions} questions correctly.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadLine();
                    Console.WriteLine();
                    return;
                }

                validInput = int.TryParse(userInput, out userAnswer);
                if (!validInput)
                {
                    Console.Write("\nInvalid input. Please type an integer number: ");
                }

            } while (!validInput);

            CheckAnswer(question, userAnswer, solution);
        }
    }

    // 6. print history method prints each question and answer on a new line, with total correct/total questions asked at the end
}