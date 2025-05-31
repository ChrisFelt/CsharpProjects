namespace MathGame;

class Program
{
    static void Main()
    {
        // outer loop:
        // create GameHistory object  <-- holds Game objects in a list and contains a method to print history
        // prompt user for:
        // difficulty level
        // operation via menu, or random operation mode
        // AFTER inner loop, prompt user for:
        // print history? y/n  <-- y prints history and continues, n continues
        // play again? y/n  <-- y continues outer loop, n closes program

        // inner loop:
        // create Game object and add to GameHistory list
        // use play method in Game object to display question and prompt user for answer
        // prompt user to continue game y/n  <-- y continues inner loop, n stops Game timer and exits inner loop

    }
}