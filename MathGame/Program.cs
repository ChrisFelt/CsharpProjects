using MathGame;

class Program
{
    static void Main()
    {
        // initialize game history and defaults
        GameHistory history = new GameHistory();
        Game currentGame;
        string[] difficulties = { "Too Easy", "Easy", "Normal" };
        string[] operations = { "+", "-", "*", "/", "Random Operator" };
        string[] menuOptions = { "1", "2", "3", "4", "5" };
        int noQuestions = 10;  // set the number of questions to ask per game
        int currentDifficulty = 1;
        int currentOperation = 1;
        string? userInput;
        bool exitProgram = false;

        while (!exitProgram)
        {
            // display menu
            Console.WriteLine("Welcome to Math Game! Please select a menu option below.");
            Console.WriteLine($"1. Change difficulty level. Current difficulty: {difficulties[currentDifficulty - 1]}");
            Console.WriteLine($"2. Change operator. Current operator: {operations[currentOperation - 1]}");
            Console.WriteLine("3. Show game history.");
            Console.WriteLine("4. Play game with current settings.");
            Console.WriteLine("5. Exit program.");
            Console.WriteLine();
            Console.Write("Enter your selection: ");

            // validate input
            do
            {
                userInput = Console.ReadLine();
                if (menuOptions.Contains(userInput))
                {
                    break;
                }
                else
                {
                    Console.Write("Invalid input. Please enter a menu option from 1 to 5: ");
                }
            } while (true);

            switch (userInput)
            {
                case "1":
                    // prompt user to select game difficulty
                    Console.WriteLine("\nPlease select a difficulty.");
                    for (int i = 0; i < difficulties.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}. {difficulties[i]}");
                    }
                    Console.WriteLine();
                    Console.Write("Enter your selection: ");

                    // validate input
                    do
                    {
                        userInput = Console.ReadLine();
                        if (userInput == "1" || userInput == "2" || userInput == "3")
                        {
                            break;
                        }
                        else
                        {
                            Console.Write("Invalid input. Please enter a difficulty from 1 to 3: ");
                        }
                    } while (true);

                    // update difficulty
                    currentDifficulty = int.Parse(userInput);
                    Console.WriteLine();
                    break;

                case "2":
                    // prompt user to select operator or random
                    Console.WriteLine("\nPlease select an operator.");
                    for (int i = 0; i < operations.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}. {operations[i]}");
                    }
                    Console.WriteLine();
                    Console.Write("Enter your selection: ");

                    // validate input
                    do
                    {
                        userInput = Console.ReadLine();
                        if (menuOptions.Contains(userInput))
                        {
                            break;
                        }
                        else
                        {
                            Console.Write("Invalid input. Please enter a difficulty from 1 to 3: ");
                        }
                    } while (true);

                    // update operation
                    currentOperation = int.Parse(userInput);
                    Console.WriteLine();
                    break;

                case "3":
                    // display game history
                    Console.WriteLine();
                    history.ShowGames();
                    break;

                case "4":
                    // display instructions with note to enter "quit" to quit game at any time
                    Console.WriteLine("Game starting!");
                    Console.WriteLine($"You will be asked {noQuestions} questions.");
                    Console.WriteLine("Please enter your answer when a question is displayed, or type 'quit' to end the current game.");
                    
                    // create Game object and add to GameHistory list
                    currentGame = new Game(currentDifficulty, currentOperation, noQuestions);
                    history.AddGame(currentGame);

                    // call Play method from game - continues until user enters "quit"
                    currentGame.Play();
                    
                    // stop timer and display stats
                    currentGame.StopTimer();
                    Console.WriteLine();
                    Console.WriteLine($"Game over. Time taken to complete: {currentGame.TimeElapsed} seconds.");
                    Console.WriteLine($"You answered {currentGame.CorrectAnswers} / {currentGame.TotalQuestions} questions correctly.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadLine();
                    break;

                case "5":
                    exitProgram = true;
                    break;
            }
        }

    }
}