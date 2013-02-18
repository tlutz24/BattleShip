using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShip
{
    class GameBoard
    {
        private char[,] grid;
        private int[,] board;
        private Ship[] ships;
        private int numSunk;

        //TODO: look into turning class operators private etc.

        //constructor for the GameBoard
        public GameBoard()
        {
            grid = new char[10, 10];
            board = new int[10, 10];
            ships = new Ship[5];
            numSunk = 0;
        }

        //Getter for the number of sunken ships
        public int GetNumSunk()
        {
            return numSunk;
        }

        //Getter for the gameboard
        public char[,] GetGrid()
        {
            return grid;
        }

        public int[,] GetBoard()
        {
            return board;
        }

        public void AddListToGrid(Ship[] BS)
        {
            int[] Loc;
            for (int i = 0; i < 5; i++)
            {

                do
                {
                    Loc = GetOpenLoc(BS[i]);
                } while (Loc[0] == -1);
                AddToGrid(BS[i], Loc[0], Loc[1], Loc[2], BS[i].GetSize());
                ships[i] = BS[i];
            }
        }

        public int[] GetOpenLoc(Ship BS)
        {
            bool cantPos=false;
            int[] coords=new int[3];
            Random R=new Random();
            int size = BS.GetSize();
            int times = 0;
            do
            {
                //Console.WriteLine("{0} try to find loc", times);
                times++;
                coords[0] = R.Next(0,10);
                coords[1] = R.Next(0,10);
                coords[2] = R.Next(0,2);
                
                if (coords[2] == 0)
                {
                    int countSize = 0;
                    int c=coords[1];
                    for (int r = coords[0]; r < coords[0]+size&&r<10; r++)
                    {
                        countSize++;
                        if (board[r, c] == 1)
                        {
                            cantPos = true;
                            break;
                        }
                    }
                    if (countSize < size)
                        cantPos = true;
                }
                else
                {
                    int countSize = 0;
                    for (int r = coords[0]; r == coords[0]; r++)
                    {
                        for (int c = coords[1]; c < coords[1] + size&&c<10; c++)
                        {
                            countSize++;
                            if (board[r, c] == 1)
                            {
                                cantPos = true;
                                break;
                            }
                        }
                        if (cantPos)
                            break;
                    }
                    if (countSize < size)
                        cantPos = true;
                }
                if (times > 1000)
                {
                    coords[0] = -1;
                    break;
                }
                
            } while (cantPos);
            return coords;
        }

        public void AddToGrid(Ship BS, int y, int x, int dir, int size)
        {
            
                if (dir == 0)
                {
                    BS.SetPositions(y, x, dir);
                    int c = x;
                    for (int r = y; r < (y + size); r++)
                    {
                        board[r, c] = 1;
                    }
                }
                else
                {
                    BS.SetPositions(y, x, dir);
                    for (int r = y; r == y; r++)
                    {
                        for (int c = x; c < x + size; c++)
                        {
                            
                            board[r, c] = 1;
                        }
                        
                    }
                }

            
        }

        public void UpdateShips()
        {
            for (int i = 4; i >= 0; i--)
            {
                if (UpdateShip(ships[i]))
                {
                    ships[i] = null;
                    Console.WriteLine("You have sunken a ship!");
                    numSunk++;
                    Console.ReadLine();
                }
            }
        }

        public bool UpdateShip(Ship BS)
        {
            if (BS == null)
                return false;
            
            int[,] locs = BS.GetPositions();
            for (int r = 0; r < BS.GetSize(); r++)
            {
                if (board[locs[r, 0], locs[r, 1]] == 1)
                {
                    return false;
                }
            }
            return true;   
        }

        public void HitMiss(int r, int c)
        {
            if (board[r, c] == 1)
            {
                grid[r, c] = 'X';
                board[r, c] = 0;
                Console.Clear();
                Console.WriteLine("  ___ ___ .__  __  ._.");
                Console.WriteLine(" /   |   \\|__|/  |_| |");
                Console.WriteLine("/    ~    \\  \\   __\\ |");
                Console.WriteLine("\\    Y    /  ||  |  \\|");
                Console.WriteLine(" \\___|_  /|__||__|  __");
                Console.WriteLine("       \\/           \\/");
                Console.ReadLine();
            }
            else
            {
                grid[r, c] = '-';
                Console.Clear();
                Console.WriteLine("   _____  .__               ");
                Console.WriteLine("  /     \\ |__| ______ ______");
                Console.WriteLine(" /  \\ /  \\|  |/  ___//  ___/");
                Console.WriteLine("/    Y    \\  |\\___ \\ \\___ \\ ");
                Console.WriteLine("\\____|__  /__/____  >____  >");
                Console.WriteLine("        \\/        \\/     \\/ ");
                Console.ReadLine();
            }
        }

        public void SetHit(int r, int c)
        {
            
            grid[r, c] = 'X';
            board[r, c] = 0;
            Console.Clear();
            Console.WriteLine("  ___ ___ .__  __  ._.");
            Console.WriteLine(" /   |   \\|__|/  |_| |");
            Console.WriteLine("/    ~    \\  \\   __\\ |");
            Console.WriteLine("\\    Y    /  ||  |  \\|");
            Console.WriteLine(" \\___|_  /|__||__|  __");
            Console.WriteLine("       \\/           \\/");
            Console.ReadLine();

            grid[r, c] = '-';
            Console.Clear();
            Console.WriteLine("   _____  .__               ");
            Console.WriteLine("  /     \\ |__| ______ ______");
            Console.WriteLine(" /  \\ /  \\|  |/  ___//  ___/");
            Console.WriteLine("/    Y    \\  |\\___ \\ \\___ \\ ");
            Console.WriteLine("\\____|__  /__/____  >____  >");
            Console.WriteLine("        \\/        \\/     \\/ ");
            Console.ReadLine();
        }

        public void SetMiss(int r, int c)
        {
            grid[r, c] = '-';
            Console.Clear();
            Console.WriteLine("   _____  .__               ");
            Console.WriteLine("  /     \\ |__| ______ ______");
            Console.WriteLine(" /  \\ /  \\|  |/  ___//  ___/");
            Console.WriteLine("/    Y    \\  |\\___ \\ \\___ \\ ");
            Console.WriteLine("\\____|__  /__/____  >____  >");
            Console.WriteLine("        \\/        \\/     \\/ ");
            Console.ReadLine();
        }


        public bool IsEmpty()
        {
            for (int r = 0; r < 10; r++)
            {
                for (int c = 0; c < 10; c++)
                {
                    if (board[r, c] == 1)
                        return false;
                }
            }
            return true;
        }

        public void GridPrinter()
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
                    Console.Write("[{0}]",grid[r, c]);
                }
                Console.WriteLine("");
            }
        }
    }
}
