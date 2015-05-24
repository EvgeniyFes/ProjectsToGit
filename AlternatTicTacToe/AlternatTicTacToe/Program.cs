using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlternatTicTacToe
{
    class Program
    {
        static void playerName(bool t)
        {
            if (t)
                Console.WriteLine("Player1 - X:");
            else
                Console.WriteLine("Player2 - O:");
        }
        static int Main(string[] args)
        {
            GameField global = new GameField();
            GameField[] cell = new GameField[9];
            for (int i = 0; i < 9; ++i)
                cell[i] = new GameField();


            Console.WriteLine("TicTacToe game:");
            Console.WriteLine("1 | 2 | 3");
            Console.WriteLine("-----------");
            Console.WriteLine("4 | 5 | 6");
            Console.WriteLine("-----------");
            Console.WriteLine("7 | 8 | 9");
            Console.WriteLine("fields and cells count from 1 to 9");
            Console.WriteLine("to leave the game write \"exit\"\n");

            int nextCell = -1;
            bool player1 = true;
            bool game = true;

            while (game)
            {
                playerName(player1);
                game = takeDataForGame(player1, ref nextCell, global, cell);
                if (game)
                    player1 = !player1;
                else
                {
                    Console.WriteLine("See you later!");
                    return 0;
                }

                game = checkForWin(cell, global);

                showResultAtConsole(cell, global);
            }
            return 0;
        }

        static bool takeDataForGame(bool player, ref int nextCell, GameField global, GameField[] cell)
        {
            if (nextCell != -1 && nextCell != -2)
            {
                if (cell[nextCell].checkIfFull())
                {
                    nextCell = -1;
                    Console.WriteLine("Field is full. Choose another field");
                    return takeDataForGame(player, ref nextCell, global, cell);
                }
            }

            if (nextCell == -1)
            {
                nextCell = takeDataForGlobalField();
                return takeDataForGame(player, ref nextCell, global, cell);
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
                if (cell[nextCell].checkForEnable(cellNumber))
                {
                    cell[nextCell].field[cellNumber] = (player) ? 1 : 2;
                    nextCell = cellNumber;
                }
                else
                {
                    Console.WriteLine("Cell busy, choose another cell");
                    return takeDataForGame(player, ref nextCell, global, cell);
                }
            }
            else
            {
                Console.WriteLine("You input wrong value! Try again:");
                return takeDataForGame(player, ref nextCell, global, cell);
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


        static bool checkForWin(GameField[] cell, GameField global)
        {
            for (int i = 0; i < 9; ++i)
            {
                if ((cell[i].checkForWin() == 1 || cell[i].checkForWin() == 2) && global.field[i] == 0)
                    global.field[i] = (cell[i].checkForWin() == 1) ? 1 : 2;

                if (cell[i].checkIfFull() && cell[i].checkForWin() == 0)
                    global.field[i] = 3;
            }

            if (global.checkForWin() != 0)
            {
                Console.WriteLine("Player{0} - You Win!!!", global.checkForWin());
                return false;
            }

            if(global.checkForWin() == 0 && global.checkIfFull())
            {
                Console.WriteLine("Draw!");
                return false;
            }
            return true;
        }


        static void showResultAtConsole(GameField[] cell, GameField global)
        {
            global.showWinRateGameField();

            string line = "";
            for (int cells = 0; cells < 3; ++cells)
            {
                for (int stringNumber = 0; stringNumber < 3; ++stringNumber)
                {
                    for (int j = 0; j < 3; ++j)  //for 3 cells
                    {
                        for (int i = 0; i < 3; ++i) //for elements in string
                        {
                            switch (cell[3 * cells + j].field[3 * stringNumber + i])
                            {
                                case 0:
                                    line = String.Format("{0} _", line);
                                    break;
                                case 1:
                                    line = String.Format("{0} X", line);
                                    break;
                                case 2:
                                    line = String.Format("{0} O", line);
                                    break;
                                default:
                                    break;
                            }
                        }
                        line = String.Format("{0} |", line);
                    }
                    Console.WriteLine(line);
                    line = "";
                }
                Console.WriteLine(" -----------------------");
            }
        }
    }
}