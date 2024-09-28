// See https://aka.ms/new-console-template for more information

using System;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace MyApp
{
    internal class Program
    {

        public static char[,] charArray = new char[6, 6];
        // Grid size 6*6 creating 18 pairs of lower case and upper case letters

        static void Main(string[] args)
        {
            Console.Clear(); // Very important !!

            /*
            ---Game loop:---

            --> Start game
            --> Player 1's turn
                    Inputs a coordinate
                    Sees whats underneath that square
                    Inputs second coordinate
                    If wrong
                        Clear console after 2 seconds 
                        Spawn in the grid again completely covered
                    If correct
                        Add point for player one
                        Make squares blank

            --> Player 2's turn
                    (Same as Player 1)
            --> Repeat until board is empty
            --> Player with the most points wins (winning screen)
            --> Restart the game button

            ------------
            */

            // Might give the player the option to quit at every stage by writing quit

            // --> Enable later
            StartGameIntro();
            Console.Clear();
            CoolLogo();
            Thread.Sleep(1000);
            bool b = TutorialOrNot();
            if (b)
            {
                Thread.Sleep(1000);
                Tutorial();
            }
            // // Actual Game
            Console.Clear();

            bool player1Won = GameLoop();

            VictoryScreen(player1Won);

        }

        // Printing functions
        static void PrintChar1DArray(char[] input)
        {
            for (int x = 0; x < input.Length; x++)
            {
                Console.Write(input[x] + " ");
            }
            Console.Write("\n");
        }

        // (Legacy)
        static void PrintChar2DArray(char[,] input)
        {
            // Hard coded for 6,6 array
            for (int x = 0; x < 6; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    Console.Write(input[x, y] + " ");
                }
                Console.Write("\n");
            }
        }

        static void PrintGridFullArray(char[,] input)
        {

            Console.WriteLine("     A   B   C   D   E   F  ");
            Console.WriteLine("   +---+---+---+---+---+---+");
            for (int y = 0; y < 6; y++)
            {
                Console.Write(y + 1 + "  ");
                for (int x = 0; x < 6; x++)
                {
                    Console.Write("| ");
                    Console.Write(input[x, y]);
                    Console.Write(" ");
                }
                Console.Write("|");
                Console.WriteLine("\n   +---+---+---+---+---+---+");
            }

        }

        static void PrintGridHashArray(char[,] input)
        {

            Console.WriteLine("     A   B   C   D   E   F  ");
            Console.WriteLine("   +---+---+---+---+---+---+");
            for (int y = 0; y < 6; y++)
            {
                Console.Write(y + 1 + "  ");
                for (int x = 0; x < 6; x++)
                {
                    Console.Write("| ");
                    // Console.Write('#');
                    if (input[x, y] == 'Z')
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write("#");
                    }
                    Console.Write(" ");
                }
                Console.Write("|");
                Console.WriteLine("\n   +---+---+---+---+---+---+");
            }
        }

        static void PrintGridSingleItemArray(char[,] input, int xCord, int yCord)
        {
            Console.WriteLine("     A   B   C   D   E   F  ");
            Console.WriteLine("   +---+---+---+---+---+---+");
            for (int y = 0; y < 6; y++)
            {
                Console.Write(y + 1 + "  ");
                for (int x = 0; x < 6; x++)
                {
                    Console.Write("| ");
                    if (x == xCord && y == yCord)
                    {
                        Console.Write(input[xCord, yCord]);
                    }
                    else
                    {
                        Console.Write('#');
                    }

                    Console.Write(" ");
                }
                Console.Write("|");
                Console.WriteLine("\n   +---+---+---+---+---+---+");
            }
        }

        static void PrintGridDoubleItemArray(char[,] input, int xCord0, int yCord0, int xCord1, int yCord1)
        {
            Console.WriteLine("     A   B   C   D   E   F  ");
            Console.WriteLine("   +---+---+---+---+---+---+");
            for (int y = 0; y < 6; y++)
            {
                Console.Write(y + 1 + "  ");
                for (int x = 0; x < 6; x++)
                {
                    Console.Write("| ");
                    if (x == xCord0 && y == yCord0)
                    {
                        Console.Write(input[xCord0, yCord0]);
                    }
                    else if (x == xCord1 && y == yCord1)
                    {
                        Console.Write(input[xCord1, yCord1]);
                    }
                    else
                    {
                        Console.Write('#');
                    }

                    Console.Write(" ");
                }
                Console.Write("|");
                Console.WriteLine("\n   +---+---+---+---+---+---+");
            }
        }


        // Misc.
        static char[] ShortenCharArray(char[] input, int index)
        {
            char[] output = new char[input.Length - 1];

            int u = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (i != index)
                {
                    output[u] = input[i];
                    u++;
                }

            }

            return output;
        }


        // Randomizing Functions
        static char[,] RandomPairedCharArray()
        {
            char[,] output = new char[6, 6]; // Hard coded

            // Letters to be paired
            // A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R
            char[] uppercase = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R' };
            char[] lowercase = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r' };
            // Letters are randomized
            uppercase = RandomizeArray(uppercase);
            lowercase = RandomizeArray(lowercase);
            // Note: total letters = 36
            char[] caseArray = new char[36]; // either 'u' or 'l'
            for (int i = 0; i < caseArray.Length; i++)
            {
                if (i < caseArray.Length / 2)
                {
                    caseArray[i] = 'u';
                }
                else
                {
                    caseArray[i] = 'l';
                }
            }
            caseArray = RandomizeArray(caseArray); // This probably isn't the best way of doing it but it was easy (so easy)

            int caseArrayIndex = 0;
            int uppercaseIndex = 0;
            int lowercaseIndex = 0;
            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 6; x++)
                {
                    char nextLetter = new(); // Undefined at the moment, will be defined next step
                    if (caseArray[caseArrayIndex] == 'u') // if uppercase
                    {
                        nextLetter = uppercase[uppercaseIndex];
                        uppercaseIndex++;
                    }
                    else // lowercase
                    {
                        nextLetter = lowercase[lowercaseIndex];
                        lowercaseIndex++;
                    } // nextLetter is now defined and is to be put into the grid
                    caseArrayIndex++;
                    output[x, y] = nextLetter;
                }
            }

            return output;
        }

        static char[] RandomizeArray(char[] input)
        {
            char[] output = new char[input.Length];
            int originalLength = input.Length;
            Random r = new();

            for (int i = 0; i < originalLength; i++)
            {
                int randomNumber = r.Next(0, input.Length - 1);
                output[i] = input[randomNumber];
                input = ShortenCharArray(input, randomNumber);
            }

            return output;
        }


        // Game Loop Functions
        static bool StartGameIntro()
        {
            CoolLogo();
            Console.WriteLine("");
            Console.WriteLine("Would you like to start?\n(Y for yes, N to quit)");
            string? response = Console.ReadLine();


            switch (YesNoValidation(response))
            {
                case 0: // Yes --> Start Game

                    return true; // I don't actually have to make this function return anything, but for the sake of clarity I'm going to do so

                case 1: // No
                    Console.Clear();
                    Console.WriteLine("Ok? Then why would you open the program?\n");
                    Thread.Sleep(3000);
                    Console.Write("Quiting game");
                    Thread.Sleep(1000); // Pause just to let the player take in their mistake
                    Console.Write(".");
                    Thread.Sleep(1500); // Pause just to let the player take in their mistake
                    Console.Write(".");
                    Thread.Sleep(2000); // Pause just to let the player take in their mistake
                    Console.Write(".\n");


                    Environment.Exit(0); // Quit game function
                    break;
                case 2: // Invalid Input
                    ClearLines(1);
                    while (YesNoValidation(response) == 2) // Invalid Input
                    {
                        Console.WriteLine("Invalid Input, please try again (Y for yes, N to quit)");
                        response = Console.ReadLine();
                        ClearLines(1);
                    }
                    ClearLines(2);
                    break;
                default: // Will never get used but just in case
                    break;
            }

            return false; // Should never get to this point
        }

        static bool TutorialOrNot()
        {
            // True     = Wants a tutorial
            // False    = Doesn't want a tutorial

            Console.Clear();
            CoolLogo();
            Console.WriteLine("");
            Console.WriteLine("Wanna do the tutorial? (Highly Recommended)\n(Y for yes, N to skip)");
            string? response = Console.ReadLine();

            switch (YesNoValidation(response))
            {
                case 0: // Yes --> Start Game

                    return true;

                case 1: // No
                    LoadingScreen();
                    return false;

                case 2: // Invalid Input
                    ClearLines(1);
                    while (YesNoValidation(response) == 2) // Invalid Input
                    {
                        Console.WriteLine("Invalid Input, please try again (Y for yes, N to quit)");
                        response = Console.ReadLine();
                        ClearLines(1);
                    }
                    ClearLines(2);
                    break;
                default: // Will never get used but just in case
                    break;
            }

            return false; // Should never reach this point
        }

        // TODO Finish Tutorial Page
        static bool Tutorial()
        {
            Console.Clear();
            CoolLogo();
            Console.WriteLine("||------------------------------------------------||");
            Console.WriteLine("||                    Tutorial                    ||");
            Console.WriteLine("||------------------------------------------------||");
            Console.WriteLine("★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★");
            Console.WriteLine("||------------------------------------------------||");
            Console.WriteLine("||                                                ||");
            Console.WriteLine("||-> The goal of the game is to find 9 pairs of   ||");
            Console.WriteLine("||   of letters on the 6x6 grid                   ||");
            Console.WriteLine("||-> Initially all of the squares will be         ||");
            Console.WriteLine("||   covered and when it's your turn by entering  ||");
            Console.WriteLine("||   coordinates some pairs will be made visible  ||");
            Console.WriteLine("||-> A pair is one uppercase letter and one       ||");
            Console.WriteLine("||   lowercase letter                             ||");
            Console.WriteLine("||-> If you find a matching pair you will no      ||");
            Console.WriteLine("||   longer see it on the board                   ||");
            Console.WriteLine("||-> If you make a mistake you will see the board ||");
            Console.WriteLine("||   with the two letters for 3 seconds before it ||");
            Console.WriteLine("||   disappears!                                  ||");
            Console.WriteLine("||-> To beat your opponent you have to memorize   ||");
            Console.WriteLine("||   the squares you or he/she has seen so that   ||");
            Console.WriteLine("||   when it's your turn you can find the pair    ||");
            Console.WriteLine("||   and win the game!                            ||");
            Console.WriteLine("||                                                ||");
            Console.WriteLine("||------------------------------------------------||");
            Console.WriteLine("★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★");
            Console.WriteLine("||------------------------------------------------||");
            Console.WriteLine("||                  Example Grid                  ||");
            Console.WriteLine("||------------------------------------------------||");
            Console.WriteLine("★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★");
            Console.WriteLine("||                                                ||");
            Console.WriteLine("||   6x6 Grid, same as the one in-game            ||");
            Console.WriteLine("||                                                ||");
            Console.WriteLine("||      A   B   C   D   E   F                     ||");
            Console.WriteLine("||    +---+---+---+---+---+---+                   ||");
            Console.WriteLine("||  1 |   | # | # | # |   | # |  Empty squares    ||");
            Console.WriteLine("||    +---+---+---+---+---+---+                   ||");
            Console.WriteLine("||  2 | # | # | # | # | # | # |                   ||");
            Console.WriteLine("||    +---+---+---+---+---+---+                   ||");
            Console.WriteLine("||  3 | # | A | # | # | # | # |  Matching         ||");
            Console.WriteLine("||    +---+---+---+---+---+---+                   ||");
            Console.WriteLine("||  4 | # | # | # | # | a | # |  Pair (A,a)       ||");
            Console.WriteLine("||    +---+---+---+---+---+---+                   ||");
            Console.WriteLine("||  5 | # | # | # | # | # | # |                   ||");
            Console.WriteLine("||    +---+---+---+---+---+---+                   ||");
            Console.WriteLine("||  6 | # | # | # | # | # | # |                   ||");
            Console.WriteLine("||    +---+---+---+---+---+---+                   ||");
            Console.WriteLine("||                                                ||");
            Console.WriteLine("||    Explanation:                                ||");
            Console.WriteLine("||    -> The #'s are hidden squares               ||");
            Console.WriteLine("||    -> The empty squares are pairs that have    ||");
            Console.WriteLine("||       been found already                       ||");
            Console.WriteLine("||    -> The coordinates are represented by the   ||");
            Console.WriteLine("||       letters at the top and the numbers at    ||");
            Console.WriteLine("||       the side                                 ||");
            Console.WriteLine("||       An example coordinate is A1 (top left)   ||");
            Console.WriteLine("||       In-game you would write it as the letter ||");
            Console.WriteLine("||       followed by the number                   ||");
            Console.WriteLine("||                                                ||");
            Console.WriteLine("||    -> *Bonus*                                  ||");
            Console.WriteLine("||       All of the inputs are case-insensitive!  ||");
            Console.WriteLine("||                                                ||");
            Console.WriteLine("||------------------------------------------------||");
            Console.WriteLine("★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★");
            Console.WriteLine("||------------------------------------------------||");
            Console.WriteLine("||                     Enjoy!                     ||");
            Console.WriteLine("||------------------------------------------------||");
            Console.WriteLine("★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★\n\n");
            Console.WriteLine("Type Y to continue to the game, or N to quit :(     ");
            string? response = Console.ReadLine();
            switch (YesNoValidation(response))
            {
                case 0: // Yes --> Start Game

                    LoadingScreen();
                    return true;

                case 1: // No
                    Console.Clear();
                    Console.WriteLine("Ok? Then why would you open the program?\n");
                    Thread.Sleep(3000);
                    Console.Write("Quiting game");
                    Thread.Sleep(1000); // Pause just to let the player take in their mistake
                    Console.Write(".");
                    Thread.Sleep(1500); // Pause just to let the player take in their mistake
                    Console.Write(".");
                    Thread.Sleep(2000); // Pause just to let the player take in their mistake
                    Console.Write(".\n");

                    Environment.Exit(0); // Quit game function
                    break;
                case 2: // Invalid Input
                    ClearLines(1);
                    while (YesNoValidation(response) == 2) // Invalid Input
                    {
                        Console.WriteLine("Invalid Input, please try again (Y for yes, N to quit)");
                        response = Console.ReadLine();
                        ClearLines(1);
                    }
                    ClearLines(2);
                    break;
                default: // Will never get used but just in case
                    break;
            }

            return false;

        }

        static bool GameLoop()
        {
            // True if Player 1 won
            // False if Player 2 won

            bool gameOver = false;
            bool player1Won = false;
            bool player1Turn = true; // if false player 2's turn

            int player1Score = 0;
            int player2Score = 0;
            // Need to score 18 to win

            char[,] gameArray = RandomPairedCharArray();

            while (!gameOver) // Write the entire game loop inside of this loop
            {

                // Initial Coordinate
                int[] cord0 = BeginningSequence(player1Score, player2Score, player1Turn, gameArray);

                // Entering second coordinate
                Console.Clear();
                BeginningAesthetics(player1Score, player2Score, player1Turn);
                Console.WriteLine("");
                PrintGridSingleItemArray(gameArray, cord0[0], cord0[1]);

                Console.WriteLine("\nEnter a second coordinate");
                Console.WriteLine("(Letter + Number, e.g A3)");
                string? response = Console.ReadLine();
                bool valid = CoordinateInputVerification(response);
                while (!valid)
                {
                    ClearLines(1);
                    Console.WriteLine("Invalid Input, please try again (Letter + Number, e.g A3)");
                    response = Console.ReadLine();
                    valid = CoordinateInputVerification(response);
                }

                int[] cord1 = CoordinateInput(response); // Second Coordinate
                // TODO make sure its not the same coordinate

                // Show second coordinate grid 
                Console.Clear();
                BeginningAesthetics(player1Score, player2Score, player1Turn);
                Console.WriteLine("");
                PrintGridDoubleItemArray(gameArray, cord0[0], cord0[1], cord1[0], cord1[1]);

                // If correct or if incorrect
                if (MatchingPair(cord0, cord1, gameArray))
                {
                    // Pair matches
                    Console.WriteLine("\nCongrats!!");
                    Console.Write("Player ");
                    if (player1Turn)
                    {
                        Console.Write("1 gets a point!\n");
                        Thread.Sleep(8000);
                        player1Score++;
                    }
                    else
                    {
                        Console.Write("2 gets a point!\n");
                        Thread.Sleep(8000);
                        player2Score++;
                    };

                    gameArray[cord0[0], cord0[1]] = 'Z'; // Z means the square is now empty
                    gameArray[cord1[0], cord1[1]] = 'Z'; // Z means the square is now empty
                    Console.Clear();
                }
                else
                {
                    // Pair doesn't match

                    Console.WriteLine("\nIncorrect!");
                    Console.WriteLine("3 seconds to memorize...");
                    Thread.Sleep(700);
                    Console.Write("3  ");
                    Thread.Sleep(1000);
                    Console.Write("2  ");
                    Thread.Sleep(1000);
                    Console.Write("1!");
                    Thread.Sleep(1000);
                    Console.Clear();
                }

                if (player1Score == 9)
                {
                    player1Won = true;
                    gameOver = true;
                }

                if (player2Score == 9)
                {
                    player1Won = false;
                    gameOver = true;
                }

                player1Turn = !player1Turn;
            }

            return player1Won;
        }

        static int[] CoordinateInput(string? response)
        {

            // Takes in the response from the user and outputs the coordinate
            // x coord = A - F
            // y coord = 1 - 6
            int[] output = new int[2];
            // Function returns an array of only length 2, which is actually just the coordinate
            // E.g {3,0}

            if (response is null) // should never happen but just to keep VSC happy :)
            {
                return output;
            }

            response = response.Replace(" ", ""); // Remove spaces

            switch (response[0])
            {
                case 'A': // x = 0
                case 'a':
                    output[0] = 0;
                    break;
                case 'B': // x = 1 
                case 'b':
                    output[0] = 1;
                    break;
                case 'C': // x = 2
                case 'c':
                    output[0] = 2;
                    break;
                case 'D': // x = 3
                case 'd':
                    output[0] = 3;
                    break;
                case 'E': // x = 4
                case 'e':
                    output[0] = 4;
                    break;
                case 'F': // x = 5
                case 'f':
                    output[0] = 5;
                    break;
                default: // Should never happen but it's here anyways
                    break;
            }

            switch (response[1])
            { // Same number -1
                case '1':
                    output[1] = 0;
                    break;
                case '2':
                    output[1] = 1;
                    break;
                case '3':
                    output[1] = 2;
                    break;
                case '4':
                    output[1] = 3;
                    break;
                case '5':
                    output[1] = 4;
                    break;
                case '6':
                    output[1] = 5;
                    break;
                default:
                    break;
            }

            return output;
        }

        static int[] BeginningSequence(int player1Score, int player2Score, bool player1Turn, char[,] gameArray)
        {
            BeginningAesthetics(player1Score, player2Score, player1Turn);

            Console.WriteLine("");
            // PrintGridFullArray(gameArray); // Debug Purposes

            PrintGridHashArray(gameArray);

            // Take in coordinate input

            Console.WriteLine("\nEnter a coordinate");
            Console.WriteLine("(Letter + Number, e.g A3)");
            string? response = Console.ReadLine();
            bool valid = CoordinateInputVerification(response);
            while (!valid)
            {
                ClearLines(1);
                Console.WriteLine("Invalid Input, please try again (Letter + Number, e.g A3)");
                response = Console.ReadLine();
                valid = CoordinateInputVerification(response);
            }

            int[] cord = CoordinateInput(response);
            return cord;
        }

        // Verification Functions
        static int YesNoValidation(string? response)
        {
            // Got help from GPT
            // = 0 Yes
            // = 1 No
            // = 2 Invalid Input

            if (response is null)
            {
                return 2; // Invalid Input
            }

            // Remove spaces from the input string
            string noSpaceResponse = response.Replace(" ", "");

            // Compare strings with "N", "n", "Y", "y" using string.Equals()
            if (noSpaceResponse.Equals("N", StringComparison.OrdinalIgnoreCase)) // Case-insensitive comparison
            {
                return 1; // No
            }
            else if (noSpaceResponse.Equals("Y", StringComparison.OrdinalIgnoreCase)) // Case-insensitive comparison
            {
                return 0; // Yes
            }

            return 2; // Invalid Input
        }

        static bool CoordinateInputVerification(string? response)
        {
            // True if valid, false if invalid

            if (response is null)
            {
                return false;
            }

            response = response.Replace(" ", ""); // Remove spaces

            // Check first character is a letter from A to F
            // Check second character is a number from 1 to 6

            if (!(
                response[0] == 'A' || response[0] == 'a' ||
                response[0] == 'B' || response[0] == 'b' ||
                response[0] == 'C' || response[0] == 'c' ||
                response[0] == 'D' || response[0] == 'd' ||
                response[0] == 'E' || response[0] == 'e' ||
                response[0] == 'F' || response[0] == 'f'
            )) // If the first character is not a letter return false
            {
                return false;
            }

            if (!( // This is a bad way of doing it but it doesn't matter because there's only 6 values
                response[1] == '1' ||
                response[1] == '2' ||
                response[1] == '3' ||
                response[1] == '4' ||
                response[1] == '5' ||
                response[1] == '6'
            )) // If the second character is not a number return false
            {
                return false;
            }

            return true;
        }

        static bool MatchingPair(int[] cord0, int[] cord1, char[,] gameArray)
        {
            char firstLetter = gameArray[cord0[0], cord0[1]];
            char secondLetter = gameArray[cord1[0], cord1[1]];

            firstLetter = char.ToLower(firstLetter);
            secondLetter = char.ToLower(secondLetter);

            if (firstLetter == secondLetter)
            {
                return true;
            }

            return false;
        }

        // Aesthetic Functions
        static void BeginningAesthetics(int player1Score, int player2Score, bool player1Turn)
        {
            CoolLogo();
            Console.WriteLine("");
            Console.WriteLine("Player 1: " + player1Score);
            Console.WriteLine("Player 2: " + player2Score);
            Console.Write("\nPlayer ");
            if (player1Turn)
            {
                Console.Write("1's turn");
            }
            else
            {
                Console.Write("2's turn");
            }
            Console.WriteLine("");
        }

        static void ClearLines(int numberOfLines) // From GPT
        {
            // Move the cursor up the specified number of lines
            for (int i = 0; i < numberOfLines; i++)
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.WindowWidth)); // Overwrite with spaces
                Console.SetCursorPosition(0, Console.CursorTop - 1); // Reset cursor to beginning of cleared line
            }
        }

        static void CoolLogo()
        {
            Console.WriteLine("★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★");
            Console.WriteLine("||--------------||---------------||---------------||");
            Console.WriteLine("||                  Joker  Game                   ||");
            Console.WriteLine("||--------------||---------------||---------------||");
            Console.WriteLine("||                Made by soodles                 ||");
            Console.WriteLine("||--------------||---------------||---------------||");
            Console.WriteLine("★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★");
        }

        static void CoolLine()
        {
            Console.WriteLine("★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★");
        }

        static void CoolBorder()
        {
            Console.WriteLine("★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★");
            Console.WriteLine("||--------------||---------------||---------------||");
            Console.WriteLine("★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★");
        }

        static void VictoryScreen(bool player1Won)
        {
            if (player1Won)
            {
                Console.WriteLine("★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★");
                Console.WriteLine("||--------------||---------------||---------------||");
                Console.WriteLine("||                  Joker  Game                   ||");
                Console.WriteLine("||--------------||---------------||---------------||");
                Console.WriteLine("★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★");
                Console.WriteLine("||------------------------------------------------||");
                Console.WriteLine("||                                                ||");
                Console.WriteLine("||                   Player 1                     ||");
                Console.WriteLine("||                     won !                      ||");
                Console.WriteLine("||                                                ||");
                Console.WriteLine("||------------------------------------------------||");
                Console.WriteLine("★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★");
                Console.WriteLine("||------------------------------------------------||");
                Console.WriteLine("||                                                ||");
                Console.WriteLine("||              Thanks for playing!               ||");
                Console.WriteLine("||                Made by soodles                 ||");
                Console.WriteLine("||                                                ||");
                Console.WriteLine("||------------------------------------------------||");
                Console.WriteLine("||           *Inspired by Intro to CS*            ||");
                Console.WriteLine("||------------------------------------------------||");
                Console.WriteLine("★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★\n\n");
            }
            else
            {
                Console.WriteLine("★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★");
                Console.WriteLine("||--------------||---------------||---------------||");
                Console.WriteLine("||                  Joker  Game                   ||");
                Console.WriteLine("||--------------||---------------||---------------||");
                Console.WriteLine("★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★");
                Console.WriteLine("||------------------------------------------------||");
                Console.WriteLine("||                                                ||");
                Console.WriteLine("||                   Player 2                     ||");
                Console.WriteLine("||                     won !                      ||");
                Console.WriteLine("||                                                ||");
                Console.WriteLine("||------------------------------------------------||");
                Console.WriteLine("★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★");
                Console.WriteLine("||------------------------------------------------||");
                Console.WriteLine("||                                                ||");
                Console.WriteLine("||              Thanks for playing!               ||");
                Console.WriteLine("||                Made by soodles                 ||");
                Console.WriteLine("||                                                ||");
                Console.WriteLine("||------------------------------------------------||");
                Console.WriteLine("||           *Inspired by Intro to CS*            ||");
                Console.WriteLine("||------------------------------------------------||");
                Console.WriteLine("★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★\n\n");
            }

        }

        static void LoadingScreen()
        {
            Console.Clear();
            Console.WriteLine("★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★");
            Console.WriteLine("||--------------||---------------||---------------||");
            Console.WriteLine("||                                                ||");
            Console.WriteLine("||                    Loading.                    ||");
            Console.WriteLine("||                                                ||");
            Console.WriteLine("||--------------||---------------||---------------||");
            Console.WriteLine("★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★");
            Thread.Sleep(2100);
            Console.Clear();
            Console.WriteLine("★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★");
            Console.WriteLine("||--------------||---------------||---------------||");
            Console.WriteLine("||                                                ||");
            Console.WriteLine("||                   Loading..                    ||");
            Console.WriteLine("||                                                ||");
            Console.WriteLine("||--------------||---------------||---------------||");
            Console.WriteLine("★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★");
            Thread.Sleep(2100);
            Console.Clear();
            Console.WriteLine("★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★");
            Console.WriteLine("||--------------||---------------||---------------||");
            Console.WriteLine("||                                                ||");
            Console.WriteLine("||                  Loading...                    ||");
            Console.WriteLine("||                                                ||");
            Console.WriteLine("||--------------||---------------||---------------||");
            Console.WriteLine("★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★");
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★");
            Console.WriteLine("||--------------||---------------||---------------||");
            Console.WriteLine("||                                                ||");
            Console.WriteLine("||                  Game Loaded!                  ||");
            Console.WriteLine("||                   Playing...                   ||");
            Console.WriteLine("||                                                ||");
            Console.WriteLine("||--------------||---------------||---------------||");
            Console.WriteLine("★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★★彡[̲̅o̲̅][̲̅o̲̅][̲̅o̲̅][̲̅o̲̅]彡★");
            Thread.Sleep(1000);
            Console.Clear();

        }

    }
}