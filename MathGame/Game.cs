using MathGame;

class Game
{
    // properties:
    // difficulty mode, no getter or setter
    // operation menu choice 1 - 5 (+, -, *, /, random), no getter or setter
    // timer object, no getter or setter
    // time elapsed, read-only
    // List of operations INCLUDING "random", no getter or setter
    // List of questions asked and answers given, read-only

    // extra properties that may be used:
    // count of questions asked, read-only
    // count of correct answers given, read-only

    // methods:
    // 1. internal start and stop timer methods
    // 2. random operand method randomly generates an operand uses difficulty mode to determine size of number
    // 3. check answer method takes operands, operator, and answer. 
    // Checks the answer given and updates questions asked and correct answers given properties
    // 4. play method uses (2) to generate random numbers, gets operator, and prints question, then gets user input and calls (3)
    // Finally, stores question and answer in history property
    // 5. print history method prints each question and answer on a new line, with total correct/total questions asked at the end
}