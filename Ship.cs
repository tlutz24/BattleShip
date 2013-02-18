using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShip
{
    class Ship
    {
        private int size;
        private int[,] locations;

        public Ship(int s)
        {
            size = s;
            locations = new int[size,2];
        }

        public void SetPositions(int y, int x, int dir)
        {
            if (dir == 0)
            {
                for (int i = 0; i < size; i++)
                {
                    locations[i, 0] = y;
                    locations[i, 1] = x;
                    y++;
                }
            }
            else
            {
                for (int i = 0; i < size; i++)
                {
                    locations[i, 0] = y;
                    locations[i, 1] = x;
                    x++;
                }
            }
        }

        public int[,] GetPositions()
        {
            return locations;
        }

        public int GetSize()
        {
            return size;
        }
    }
}
