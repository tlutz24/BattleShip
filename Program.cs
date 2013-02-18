using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShip
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(100, 50);//sets window to a larger than average size in order to play the game fluently
            Start:GameBoard gb = new GameBoard();//create a new Gameboard gb
            SetGame(gb);//creates and places the 5 ships on the gameboard
            int[,] board = gb.GetBoard();//keeps a local copy of the filled gameboard
            //String[] coords;//coordinates given by user input
            String coords;
            int score = 100;//score starts at 100
            bool isDev = false;//debugging purposes
            String name = null;//string to record the player's name

            Console.WriteLine("__________         __    __  .__             _________.__    .__        ");
            Console.WriteLine("\\______   \\_____ _/  |__/  |_|  |   ____    /   _____/|  |__ |__|_____  ");
            Console.WriteLine(" |    |  _/\\__  \\\\   __\\   __\\  | _/ __ \\   \\_____  \\ |  |  \\|  \\____ \\ ");
            Console.WriteLine(" |    |   \\ / __ \\|  |  |  | |  |_\\  ___/   /        \\|   Y  \\  |  |_> >");
            Console.WriteLine(" |______  /(____  /__|  |__| |____/\\___  > /_______  /|___|  /__|   __/ ");
            Console.WriteLine("        \\/      \\/                     \\/          \\/      \\/   |__|    \n");
            Console.WriteLine("Press \"Enter\" to Continue");
            Console.ReadLine();
            Console.WriteLine("Enter your name please:");//prompts user for name
            name = Console.ReadLine();
            if (name.Equals("debug"))//if the name is debug then it is an admin
            {
                isDev = true;
                name = "Admin";
            }
            
            while (!gb.IsEmpty())//while the gameboard still has ships on it
            {
                
                Console.Clear();
                if(isDev)//if it is an admin then print the board showing where the ships are
                    Printer(board);
                Console.WriteLine("\nScore={0} | Num. Sunken ships={1}\n", score, gb.GetNumSunk());//show the user their current score
                score--;//subtract one point from their score for the current move
                gb.GridPrinter();//print the board for the user to make guesses
                Console.WriteLine();
                Idiots: try//try catch statement to prevent users from entering invalid coordinates
                {
                    System.Console.Write("Enter your desired guess (in the format a0):");//prompt user for coordinate guess
                    coords = Console.ReadLine();
                    int x = char.Parse(coords.Substring(0, 1).ToUpper()) - 65;
                    int y = int.Parse(coords.Substring(1,1));

                    while (gb.GetGrid()[y, x] == 'X' || gb.GetGrid()[y, x] == '-')//if the location hasbeen guessed before then prompt the user to enter new values
                    {
                        System.Console.Write("That location has been guessed, please choose another using the same format:");
                        coords = Console.ReadLine();
                        x = char.Parse(coords.Substring(0, 1).ToUpper()) - 65;
                        y = int.Parse(coords.Substring(1, 1));
                    }
                    gb.HitMiss(y,x);
                    /*
                    if (board[y, x] == 1)//if guessed location is a hit print such
                        gb.SetHit(y, x);
                    else gb.SetMiss(y, x);//if it is not that print sych as well
                     */
                }
				catch
                {
				    Console.WriteLine("You Sir cannot read directions! Please make another attempt.");
				    goto Idiots;//if the user does not enter the coordinates properly this will catch the error
				}
                gb.UpdateShips();

            }//after all ships are sunk the user will be shown their final scored and be asked if they wish to play again
            Console.WriteLine("You won {1}! Your final score was {0}\nWould you like to play again?", score++, name);
            if (Console.ReadLine().Substring(0, 1).ToLower().Equals("y"))
            {
                Console.Clear();
                goto Start;
            }
            else Console.WriteLine("Thank You for Playing!");
            Console.ReadLine();
        }

        //this function creates sets the ships on the gameboard
        private static void SetGame(GameBoard gb)
        {
            Ship[] ships = new Ship[7];
            ships[0] = new Ship(5);
            ships[1] = new Ship(4);
            ships[2] = new Ship(3);
            ships[3] = new Ship(2);
            ships[4] = new Ship(2);
            //new ships
            ships[5] = new Ship(1);
            ships[6] = new Ship(1);
            
            gb.AddListToGrid(ships);

        }

        //this function prints the board sent to it
        //either the board used for guesses or the board for keeping track of the ships
        private static void Printer(int[,] board)
        {

            Console.Write("  ");
            for (char i = 'A'; i < 75; i++)
                Console.Write("_{0}_", i);
            Console.WriteLine();
            for (int r = 0; r < 10; r++)
            {
                Console.Write("{0}|", r);
                for (int c = 0; c < 10; c++)
                {
                    Console.Write("[{0}]", board[r, c]);
                }
                Console.WriteLine("");
            }
        }
    }
}
