using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlternatTicTacToe
{
    class GameRules : GameField
    {
        public GameField[] cell = new GameField[9];
        public GameField global = new GameField();
        public bool player1 = true;
        public int nextCell = -1;

        public int checkForWin(GameField cell)
        {
            if ((cell.field[0] != 0) && (cell.field[0] == cell.field[4]) && (cell.field[0] == cell.field[8]))
                return cell.field[0];
            else if ((cell.field[2] != 0) && (cell.field[2] == cell.field[4]) && (cell.field[2] == cell.field[6]))
                return cell.field[2];

            for (int i = 0; i < 9; i += 3)
            {
                if ((cell.field[i] != 0) && (cell.field[i + 1] == cell.field[i]) && (cell.field[i] == cell.field[i + 2]))    //vertikal
                    return cell.field[i];
                else if ((cell.field[i / 3] != 0) && (cell.field[i / 3 + 3] == cell.field[i / 3]) && (cell.field[i / 3 + 6] == cell.field[i / 3]))   //horizontal
                    return cell.field[i / 3];
            }
            return 0;
        }
        public bool checkIfFull(GameField cell)
        {
            for (int i = 0; i < 9; ++i)
            {
                if (cell.field[i] == 0)
                    return false;
            }
            return true;
        }

        public bool checkForEnable(GameField cell, int number)
        {
            if (cell.field[number] != 0)
                return false;
            return true;
        }

        public bool takeDataForGame()
        {
            if (nextCell != -1 && nextCell != -2)
            {
                if (checkIfFull(cell[nextCell]))
                {
                    nextCell = -1;
                    Console.WriteLine("Field is full. Choose another field");
                    return takeDataForGame();
                }
            }

            if (nextCell == -1)
            {
                nextCell = takeDataForGlobalField();
                return takeDataForGame();
            }
            if (nextCell == -2)
                return false;

            Console.WriteLine("You in field {0}", nextCell + 1);


            Console.Write("Cell number in local game: ");
            int cellNumber;

            string readValue = Console.ReadLine();
            if (readValue.Equals("exit"))
            {
                return false;
            }

            bool result = Int32.TryParse(readValue, out cellNumber);
            if (cellNumber >= 1 && cellNumber <= 9)
            {
                --cellNumber;
                if (checkForEnable(cell[nextCell], cellNumber))
                {
                    cell[nextCell].field[cellNumber] = (player1) ? 1 : 2;
                    nextCell = cellNumber;
                }
                else
                {
                    Console.WriteLine("Cell busy, choose another cell");
                    return takeDataForGame();
                }
            }
            else
            {
                Console.WriteLine("You input wrong value! Try again:");
                return takeDataForGame();
            }
            return true;
        }


        static int takeDataForGlobalField()
        {
            Console.Write("Field number in Global game: ");
            string readValue = Console.ReadLine();

            if (readValue.Equals("exit"))
            {
                return -2;
            }

            int number;

            bool result = Int32.TryParse(readValue, out number);
            if (number >= 1 && number <= 9)
            {
                return --number;
            }
            else
            {
                Console.WriteLine("You input wrong value! Try again:");
                return takeDataForGlobalField();
            }
        }


        public bool checkForWin()
        {
            for (int i = 0; i < 9; ++i)
            {
                if ((checkForWin(cell[i]) == 1 || checkForWin(cell[i]) == 2) && global.field[i] == 0)
                    global.field[i] = (checkForWin(cell[i]) == 1) ? 1 : 2;

                if (checkIfFull(cell[i]) && checkForWin(cell[i]) == 0)
                    global.field[i] = 3;
            }

            if (checkForWin(global) != 0)
            {
                Console.WriteLine("Player{0} - You Win!!!", checkForWin(global));
                return false;
            }

            if (checkForWin(global) == 0 && checkIfFull(global))
            {
                Console.WriteLine("Draw!");
                return false;
            }
            return true;
        }
    }
}
