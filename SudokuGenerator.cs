using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    internal class SudokuGenerator
    {
        private const int SIZE = 9;
        private const int EMPTY = 0;
        private static readonly Random RANDOM = new Random();
        static int[,] boardBackup;

        internal static void GenerateSudokuPuzzle(string difficulty)
        {
            int[,] board = new int[SIZE, SIZE];
            boardBackup = new int[SIZE, SIZE];
            FillDiagonalRegions(board);
            FillRemaining(board, 0, 3);
            Array.Copy(board, boardBackup, board.Length);

            // Define the number of cells to empty based on difficulty
            int cellsToEmpty;
            switch (difficulty)
            {
                case "Very Easy":
                    cellsToEmpty = 20;
                    break;
                case "Easy":
                    cellsToEmpty = 30;
                    break;
                case "Medium":
                    cellsToEmpty = 35;
                    break;
                case "Hard":
                    cellsToEmpty = 40;
                    break;
                case "Very Hard":
                    cellsToEmpty = 45;
                    break;
                default:
                    throw new ArgumentException("Invalid difficulty level");
            }

            // Empty cells
            for (int i = 0; i < cellsToEmpty; i++)
            {
                int row, col;
                do
                {
                    row = RANDOM.Next(SIZE);
                    col = RANDOM.Next(SIZE);
                } while (board[row, col] == EMPTY);  // Ensure we don't empty an already empty cell

                board[row, col] = EMPTY;
            }

            Print(board);
        }

        private static void FillDiagonalRegions(int[,] board)
        {
            for (int i = 0; i < SIZE; i += 3)
            {
                FillRegion(board, i, i);
            }
        }

        private static bool FillRemaining(int[,] board, int i, int j)
        {
            if (j >= SIZE && i < SIZE - 1)
            {
                i++;
                j = 0;
            }
            if (i >= SIZE && j >= SIZE)
            {
                return true;
            }
            if (i < 3)
            {
                if (j < 3)
                {
                    j = 3;
                }
            }
            else if (i < SIZE - 3)
            {
                if (j == (i / 3) * 3)
                {
                    j += 3;
                }
            }
            else
            {
                if (j == SIZE - 3)
                {
                    i++;
                    j = 0;
                    if (i >= SIZE)
                    {
                        return true;
                    }
                }
            }

            for (int num = 1; num <= SIZE; num++)
            {
                if (IsSafe(board, i, j, num))
                {
                    board[i, j] = num;
                    if (FillRemaining(board, i, j + 1))
                    {
                        return true;
                    }
                    board[i, j] = EMPTY;
                }
            }
            return false;
        }

        private static void FillRegion(int[,] board, int row, int col)
        {
            int num;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    do
                    {
                        num = RANDOM.Next(SIZE) + 1;
                    } while (!IsSafe(board, row, col, num));
                    board[row + i, col + j] = num;
                }
            }
        }

        private static bool IsSafe(int[,] board, int row, int col, int num)
        {
            for (int x = 0; x <= 8; x++)
            {
                if (board[row, x] == num)
                {
                    return false;
                }
            }
            for (int y = 0; y <= 8; y++)
            {
                if (board[y, col] == num)
                {
                    return false;
                }
            }
            int startRow = row - row % 3;
            int startCol = col - col % 3;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i + startRow, j + startCol] == num)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static void Print(int[,] board)
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    if (board[i, j].ToString().Equals("0"))
                    { 
                        Form1.dataGridView1.Rows[i].Cells[j].Value = "";
                        EnableCell(i, j);
                    }
                    else
                    {
                        Form1.dataGridView1.Rows[i].Cells[j].Value = board[i, j].ToString();
                        DisableCell(i, j);
                    }
                }
            }
        }
        internal static void PrintBackup()
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                        Form1.dataGridView1.Rows[i].Cells[j].Value = boardBackup[i, j].ToString();
                }
            }
        }
        private static void DisableCell(int rowIndex, int columnIndex)
        {
            // Set the cell to read-only
            Form1.dataGridView1.Rows[rowIndex].Cells[columnIndex].ReadOnly = true;

            // Change the cell style to indicate it is disabled
            Form1.dataGridView1.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Color.LightGray;
            Form1.dataGridView1.Rows[rowIndex].Cells[columnIndex].Style.ForeColor = Color.Black;
            Form1.dataGridView1.Rows[rowIndex].Cells[columnIndex].Style.SelectionBackColor = Color.LightGray;
            Form1.dataGridView1.Rows[rowIndex].Cells[columnIndex].Style.SelectionForeColor = Color.Black;
        }
        private static void EnableCell(int rowIndex, int columnIndex)
        {
            // Set the cell to editable

            Form1.dataGridView1.Rows[rowIndex].Cells[columnIndex].ReadOnly = false;

            // Reset the cell style to default
            Form1.dataGridView1.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Form1.dataGridView1.DefaultCellStyle.BackColor;
            Form1.dataGridView1.Rows[rowIndex].Cells[columnIndex].Style.ForeColor = Form1.dataGridView1.DefaultCellStyle.ForeColor;
            Form1.dataGridView1.Rows[rowIndex].Cells[columnIndex].Style.SelectionBackColor = Form1.dataGridView1.DefaultCellStyle.SelectionBackColor;
            Form1.dataGridView1.Rows[rowIndex].Cells[columnIndex].Style.SelectionForeColor = Form1.dataGridView1.DefaultCellStyle.SelectionForeColor;
                
        }
    }
}
