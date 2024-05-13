// Base class representing a player
abstract class Player
{
    public string Name { get; set; } = "";
    public int GamesWon { get; set; } = 0;
    public int Disks { get; set; } = 0;
    public char WhichPieces { get; set; } = ' ';
}

// PlayerOne class, child of parent class
class PlayerOne : Player
{
    // Constructor for PlayerOne
    public PlayerOne(string name, char whichPieces, int gamesWon, int disks)
    {
        Name = name;
        GamesWon = gamesWon;
        Disks = disks;
        WhichPieces = whichPieces;
    }
}
// PlayerTwo class, child of parent class
class PlayerTwo : Player
{
    // Constructor for PlayerTwo
    public PlayerTwo(string name, char whichPieces, int gamesWon, int disks)
    {
        Name = name;
        GamesWon = gamesWon;
        Disks = disks;
        WhichPieces = whichPieces;
    }
}
// Board class to represent the game board and its functions
class Board
{
    private Player playerOne;
    private Player playerTwo;

    public char[,] GameBoard = new char[6, 7];

    // Constructor to initialize players
    public Board(Player player1, Player player2)
    {
        playerOne = player1;
        playerTwo = player2;
    }

    // Method to initialize the game board to be empty at the start of a new game
    public void InitialiseBoard()
    {
        for (int i = 0; i <= 5; i++)
        {
            for (int j = 0; j <= 6; j++)
            {
                GameBoard[i, j] = ' ';
            }
        }
    }

    // Method to print the game board
    public void PrintBoard()
    {
        // Print the game board with colors based on player pieces
        // 'X' is red, 'O' is blue, ' ' is the empty slot where no counter has been dropped
        for (int i = 0; i <= 5; i++)
        {
            for (int j = 0; j <= 6; j++)
            {
                Console.Write($"| ");
                if (GameBoard[i, j] == 'X')
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{GameBoard[i, j]} ");
                }
                else if (GameBoard[i, j] == 'O')
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"{GameBoard[i, j]} ");
                }
                else
                {
                    Console.ResetColor();
                    Console.Write($"{GameBoard[i, j]} ");
                }

                // Reset the colour so that the boards columns does not get printed out as red or blue
                Console.ResetColor();
            }
            Console.WriteLine("|");
        }
    }
    // Method to handle dropping a counter into a specific column
    public int DropCounter(int choice, char player)
    {
        // Method to drop a counter into the specified column
        int counter = 0;
        for (int i = 5; i >= 0; i--)
        {
            if (GameBoard[i, choice] == ' ')
            {
                counter = i;
                break;
            }
            else if (GameBoard[i, choice] == 'X' || GameBoard[i, choice] == 'O')
            {
                counter++;
            }
            else
            {
                Console.WriteLine("Please enter a valid column");
                return -1;
            }
        }

        // Update the player's disk count
        if (player == 'X')
        {
            playerOne.Disks--;
            GameBoard[counter, choice] = playerOne.WhichPieces;
        }

        else if (player == 'O')
        {
            playerTwo.Disks--;
            GameBoard[counter, choice] = playerTwo.WhichPieces;
        }
        return counter;
    }
    // Method to check if the game is a draw by checking each players disk count
    public bool CheckDraw()
    {
        // If either player runs out of disks, the game is a draw
        if (playerOne.Disks == 0 || playerTwo.Disks == 0)
        {
            Console.WriteLine("It is a draw");
            return true;
        }
        else
        {
            return false;
        }
    }
    // Method to check if a player has won by checking win conditions: vertical, horizontal or diagonal
    public bool HasWon(char player, Board board, int choice, int counter)
    {
        return CheckUp(player, board, choice, counter) || CheckAcross(player, board, choice, counter) || CheckDiagonal(player, board, choice, counter);
    }

    // Check for four counters connected vertically
    private bool CheckUp(char player, Board board, int choice, int counter)
    {
        int count = 1;
        for (int i = counter - 1; i >= 0 && i >= counter - 3; i--)
        {
            if (board.GameBoard[i, choice] == player)
            {
                count++;
            }
            else
            {
                break;
            }
        }
        for (int i = counter + 1; i < 6 && i <= counter + 3; i++)
        {
            if (board.GameBoard[i, choice] == player)
            {
                count++;
            }
            else
            {
                break;
            }
        }
        return count >= 4;

    }

    // Check for four counters connected diagonally
    private bool CheckDiagonal(char player, Board board, int choice, int counter)
    {
        int count = 1;
        for (int i = counter - 1, j = choice + 1; i >= 0 && j < 7 && i >= counter - 3 && j <= choice + 3; i--, j++)
        {
            if (board.GameBoard[i, j] == player)
            {
                count++;
            }
            else
            {
                break;
            }
        }

        for (int i = counter + 1, j = choice - 1; i < 6 && j >= 0 && i <= counter + 3 && j >= choice - 3; i++, j--)
        {
            if (board.GameBoard[i, j] == player)
            {
                count++;
            }
            else
            {
                break;
            }
        }

        if (count >= 4)
        {
            return true;
        }

        count = 1;
        for (int i = counter - 1, j = choice - 1; i >= 0 && j >= 0 && i >= counter - 3 && j >= choice - 3; i--, j--)
        {
            if (board.GameBoard[i, j] == player)
            {
                count++;
            }
            else
            {
                break;
            }
        }

        for (int i = counter + 1, j = choice + 1; i < 6 && j < 7 && i <= counter + 3 && j <= choice + 3; i++, j++)
        {
            if (board.GameBoard[i, j] == player)
            {
                count++;
            }
            else
            {
                break;
            }
        }

        return count >= 4;
    }

    // Check for four counters connected horizontally
    private bool CheckAcross(char player, Board board, int choice, int counter)
    {
        int count = 1;
        for (int i = choice - 1; i >= 0 && i >= choice - 3; i--)
        {
            if (board.GameBoard[counter, i] == player)
            {
                count++;
            }
            else
            {
                break;
            }
        }
        for (int i = choice + 1; i < 7 && i <= choice + 3; i++)
        {
            if (board.GameBoard[counter, i] == player)
            {
                count++;
            }
            else
            {
                break;
            }
        }
        return count >= 4;
    }
}

// class representing a players turn and what needs to happen each turn 
class Turn
{
    private int turns = 1;

    // Method to handle a player's turn
    public bool Main(Board board, PlayerOne playerOne, PlayerTwo playerTwo)
    {
        string playerName;
        char player = WhoseGo();
        int choice;

        // Assign playerName depending on which players go it currently is
        if (player == 'X')
        {
            playerName = playerOne.Name;
        }
        else
        {
            playerName = playerTwo.Name;
        }

        Console.WriteLine($"{playerName} please select a column to drop your counter into");

        // Prompt the user until they enter a column number between 1-7
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 7)
        {
            Console.WriteLine("Invalid input. Please enter a number between 1 and 7.");
        }

        int counter = board.DropCounter(choice - 1, player);

        if (counter == -1)
        {
            // Handle the error condition
            Console.WriteLine("Error in dropping counter. Try again.");
        }

        // Print board so the player can see where their piece has dropped
        board.PrintBoard();

        // update the piece the player is using according to the set of pieces theyre using 
        if (player == 'X')
        {
            player = playerOne.WhichPieces;
        }
        else
        {
            player = playerTwo.WhichPieces;
        }
        
        if (board.HasWon(player, board, choice - 1, counter))
        {
            CountWins(playerOne, playerTwo);
            Console.WriteLine($"{playerName} won!");
            return true;
        }

        turns++;
        return false;
    }

    // Method that returns which players go it is from how many total turns there have been
    private char WhoseGo()
    {
        return (turns % 2 == 1) ? 'X' : 'O';
    }

    // Method that tracks each players wins and updates them on each win 
    private void CountWins(PlayerOne playerOne, PlayerTwo playerTwo)
    {
        char player = WhoseGo();
        if (player == 'X')
        {
            playerOne.GamesWon += 1;
        }
        else if (player == 'O')
        {
            playerTwo.GamesWon += 1;
        }
    }
}
class Program
{
    static void Main()
    {
        MainMenu();
    }

    // Method that starts a new game and initializes new players
    public static void Game()
    {
        (PlayerOne playerOne, PlayerTwo playerTwo) = CreatePlayers();
        do
        {
            Board board = new Board(playerOne, playerTwo);
            board.InitialiseBoard();

            Turn turn = new Turn();

            AssignPieces(playerOne, playerTwo);
            board.PrintBoard();

            while (turn.Main(board, playerOne, playerTwo) == false)
            {
                Console.WriteLine("Next Players turn");

                if (board.CheckDraw())
                {
                    break;
                }
            }

            // Reset each players counters for the next game
            playerOne.Disks = 21;
            playerTwo.Disks = 21;

            Console.WriteLine($"Games won by {playerOne.Name}: {playerOne.GamesWon}");
            Console.WriteLine($"Games won by {playerTwo.Name}: {playerTwo.GamesWon}");
            Console.WriteLine("Would you like to play again: y/n");
        } while (Console.ReadLine().Trim().ToLower() == "y");
    }

    // Method to create instances of players
    public static (PlayerOne, PlayerTwo) CreatePlayers()
    {

        string? name;
        int gamesWon = 0;
        char pieces = ' ';
        int reddisks = 21;
        int bluedisks = 21;

        Console.WriteLine("Please enter the name of player one");
        name = Console.ReadLine();
        PlayerOne playerOne = new PlayerOne(name, pieces, gamesWon, reddisks);

        Console.WriteLine("Please enter the name of player two");
        name = Console.ReadLine();
        PlayerTwo playerTwo = new PlayerTwo(name, pieces, gamesWon, bluedisks);

        return (playerOne, playerTwo);
    }

    // Method to assign game pieces to players
    public static void AssignPieces(PlayerOne playerOne, PlayerTwo playerTwo)
    {
        Console.WriteLine($"{playerOne.Name}, what pieces would you like to use, red or blue?");
        string? choice = Console.ReadLine();

        // Assign game pieces to both players depending on player ones choice - either red or blue
        if (choice == "red" || choice == "r")
        {
            playerOne.WhichPieces = 'X';
            playerTwo.WhichPieces = 'O';
        }

        else if (choice == "blue" || choice == "b")
        {
            playerOne.WhichPieces = 'O';
            playerTwo.WhichPieces = 'X';
        }

        else 
        {
            Console.WriteLine("Please select a valid colour, either 'red' or 'blue'");
            AssignPieces(playerOne, playerTwo);
            return;
        }

        if (playerOne.WhichPieces == 'X')
        {
            Console.WriteLine($"Okay that leaves {playerTwo.Name} with blue pieces");
        }

        else if (playerOne.WhichPieces == 'O')
        {
            Console.WriteLine($"Okay that leaves {playerTwo.Name} with red pieces");
        }
    }

    // Method to dispay the main menu
    static void MainMenu()
    {
        Console.WriteLine("--------Main Menu--------");
        Console.WriteLine("1)   Start a new game");
        Console.WriteLine("2)     Exit game");
        Console.WriteLine("-------------------------");

        // Read user's choice
        string ?choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Game();
                break;
            case "2":
                break;
            default:
                Console.WriteLine("Please select a valid option by entering '1' or '2'");
                MainMenu();
                break;
        }
    }
}
    
