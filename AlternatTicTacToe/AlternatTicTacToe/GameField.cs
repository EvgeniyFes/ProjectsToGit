using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlternatTicTacToe
{
    class GameField
    {

        public int[] field = new int[9];

        public GameField()
        {
            for(int i = 0; i < 9; i++)
            {
                field[i] = 0;
            }
        }
    }
}