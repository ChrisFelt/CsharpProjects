# Math Game

Welcome to Math Game, a simple console program written in C#.

Math Game tests the user's math skills with addition, subtraction, multiplication, and division.

To play, simply run the program and select the desired menu options from the main menu.

The main menu options are:
1. Change difficulty level.
    ```
    Adjusts the difficulty of the game.
    The current difficulty is listed next to this option in the main menu.

    There are three options to choose from:
    1. Too easy: problems contain two operands
    2. Easy: problems contain three operands
    3. Normal: problems contain four operands

    Only an integer 1 - 3 will be accepted as input.
    ```
2. Change operator.
    ```
    Changes the operation used in the next game. 
    Games consist entirely of the operation chosen.
    The current operator is listed next to this option in the main menu.

    There are five options to choose from:
    1. + (addition)
    2. - (subtraction)
    3. * (multiplication)
    4. / (division)
    5. Random Operator: a random operator is chosen for each question asked.

    Only an integer 1 - 5 will be accepted as input.
    ```
3. Show game history.
    ```
    Displays the problems from each game played along with the correct solution and the player's answer.
    Time taken and correct / total questions are also listed.
    
    The program pauses between listing each game and the user may press any key to continue.
    ```
4. Play game with current settings.
    ```
    Starts the game using the current difficulty and operator settings.
    
    The game will prompt the user to answer 10 total math questions one at a time, 
    pausing at the current question until the user types an integer answer into the console.

    Each time the user inputs an answer, the program provides immediate feedback notifying the user if they answered correctly.
    If the user answers incorrectly, the correct answer will be shown.

    The user may additionally quit at any time during the game by typing 'quit'.

    Once the game is complete, time taken and correct / total questions will be shown.
    ```
5. Exit program
    ```
    Immediately terminates program execution.
    ```

<br>

---

<br>

I wrote this program using the requirements listed by the C# Academy below. All requirements and challenge goals were met as written.

Project Outline Source: https://www.thecsharpacademy.com/project/53/math-game

Requirements:

1. You need to create a Math game containing the 4 basic operations

2. The divisions should result on INTEGERS ONLY and dividends should go from 0 to 100. Example: Your app shouldn't present the division 7/2 to the user, since it doesn't result in an integer.

3. Users should be presented with a menu to choose an operation

4. You should record previous games in a List and there should be an option in the menu for the user to visualize a history of previous games.

5. You don't need to record results on a database. Once the program is closed the results will be deleted.


Challenge Goals:

1. Try to implement levels of difficulty.

2. Add a timer to track how long the user takes to finish the game.

3. Create a 'Random Game' option where the players will be presented with questions from random operations

4. To follow the DRY Principle, try using just one method for all games. Additionally, double check your project and try to find opportunities to achieve the same functionality with less code, avoiding repetition when possible.