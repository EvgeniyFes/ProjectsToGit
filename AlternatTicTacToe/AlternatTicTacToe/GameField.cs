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

        public int checkForWin()
        {
            if ((field[0] != 0) && (field[0] == field[4]) && (field[0] == field[8]))
                return field[0];
            else if ((field[2] != 0) && (field[2] == field[4]) && (field[2] == field[6]))
                return field[2];

            for(int i = 0; i < 9; i += 3)
            {
                if ((field[i] != 0) && (field[i + 1] == field[i]) && (field[i] == field[i + 2]))    //vertikal
                    return field[i];
                else if ((field[i / 3] != 0) && (field[i / 3 + 3] == field[i / 3]) && (field[i / 3 + 6] == field[i / 3]))   //horizontal
                    return field[i / 3];
            }
            return 0;
        }
        public bool checkIfFull()
        {
            for (int i = 0; i < 9; ++i)
            {
                if (field[i] == 0)
                    return false;
            }
            return true;
        }

        public void showWinRateGameField()
        {
            string line = "|";
            for(int i = 1; i <= 9; ++i)
            {
                switch (field[i - 1])
                {
                    case 0:
                        line = String.Format("{0} _ |", line);
                        break;
                    case 1:
                        line = String.Format("{0} X |", line);
                        break;
                    case 2:
                        line = String.Format("{0} O |", line);
                        break;
                    case 3:
                        line = String.Format("{0} * |", line);
                        break;
                    default:
                        break;
                }
                if (i % 3 == 0)
                {
                    Console.WriteLine(line);
                    line = "|";
                }
                
            }
        }

        public bool checkForEnable(int number)
        {
            if (field[number] != 0)
                return false;
            return true;
        }
    }
}