using MathGame;

class GameHistory
{
    // properties:
    // empty List that will contain Game objects, no getters or setters
    private List<Game> _gameList;

    public GameHistory()
    {
        _gameList = new List<Game>();
    }

    // methods:
    // 1. add game, takes a Game object and returns nothing. Adds game object to game list
    public void AddGame(Game game)
    {
        _gameList.Add(game);
    }
    // 2. print all, loops through every Game object in the game list and calls its print history method
    // Returns no games played message if empty
    public void ShowGames()
    {
        Console.WriteLine("Displaying Game History...");

        // notify user if no history
        if (_gameList.Count == 0)
        {
            Console.WriteLine("No games played yet!");
            Console.WriteLine("Check back after you've played the game.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }
        
        // display history for each game in the list
        else
        {
            Console.WriteLine($"Total games played: {_gameList.Count}");
            for (int i = 0; i < _gameList.Count; i++)
            {
                Console.WriteLine($"Game {i + 1} history:");
                // display questions, answers, and solutions from QuestionHistory of current game in _gameList
                for (int j = 0; j < _gameList[i].QuestionHistory.Count; j++)
                {
                    (string question, int answer, int solution) = _gameList[i].QuestionHistory[j];
                    Console.Write($"{j + 1}. {question} = {solution}. ");

                    // display user's answer and notify the user if it was correct or incorrect
                    if (answer == solution)
                    {
                        Console.WriteLine($"You answered: {answer}. Correct!");
                    }
                    else
                    {
                        Console.WriteLine($"You answered: {answer}. Incorrect.");
                    }
                }

                // display stats for the current game
                Console.WriteLine();
                Console.WriteLine($"Time taken for game {i + 1}: {_gameList[i].TimeElapsed} seconds.");
                Console.WriteLine($"{_gameList[i].CorrectAnswers} / {_gameList[i].TotalQuestions} questions answered correctly.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
            }
        }
    }
}