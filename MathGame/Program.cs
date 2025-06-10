namespace MathGame;

class Program
{
    static void Main()
    {
        // initialize game history and defaults
        GameHistory history = new GameHistory();
        string[] difficulties = { "too easy", "easy", "normal" };
        string[] operations = { "+", "-", "*", "/", "random operator" };
        string[] menuOptions = { "1", "2", "3", "4", "5"};
        int currentDifficulty = 1;
        int currentOperation = 1;
        string? userInput;
        bool exitProgram = false;
        // outer loop:
        // create GameHistory object  <-- holds Game objects in a list and contains a method to print history
        // prompt user for:
        // difficulty level
        // operation via menu, or random operation mode
        // AFTER inner loop, prompt user for:
        // print history? y/n  <-- y prints history and continues, n continues
        // play again? y/n  <-- y continues outer loop, n closes program
        while (!exitProgram)
        {
            // display menu
            Console.WriteLine("Welcome to Math Game! Please select a menu option below.");
            Console.WriteLine($"1. Change difficulty level. Current difficulty: {difficulties[currentDifficulty - 1]}");
            Console.WriteLine($"2. Change operator. Current operator: {operations[currentOperation - 1]}");
            Console.WriteLine("3. Show game history. Currently under construction.");
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
            } while(true);

            switch (userInput)
            {
                case "1":
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "4":
                    break;
                case "5":
                    exitProgram = true;
                    break;
            }
        }


        // inner loop:
        // create Game object and add to GameHistory list
        // use play method in Game object to display question and prompt user for answer
        // prompt user to continue game y/n  <-- y continues inner loop, n stops Game timer and exits inner loop

    }
}