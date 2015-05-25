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

            bool game = true;

            GameRules rules = new GameRules();
            rules.cell = cell;
            rules.global = global;

            while (game)
            {
                playerName(rules.player1);
                game = rules.takeDataForGame();
                rules.player1 = !rules.player1;

                if(!game)
                {
                    Console.WriteLine("See you later!");
                    return 0;
                }
                game = rules.checkForWin();
                showResultAtConsole(rules.cell, global);
            }
            return 0;
        }

        static void showResultAtConsole(GameField[] cell, GameField global)
        {

            showGobalField(global);

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

        static void showGobalField(GameField global)
        {
            string line = "|";
            for (int i = 1; i <= 9; ++i)
            {
                switch (global.field[i - 1])
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
    }
}