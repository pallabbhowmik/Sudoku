using System.Drawing;
using System.Drawing.Printing;
using System.Security.Policy;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeGrid();
            cmbDifficulty.SelectedIndex = 0;
            cmbDifficulty.Visible = true;
            lblSelectDifficulty.Visible = true;
            lblSelectDifficulty.Enabled = true;
            btnStartGame.Visible = true;
            EnableCotents(false);



        }
        private void EnableCotents(Boolean val)
        {
            dataGridView1.Visible = val;
            newGameButton.Visible = val;
            solveButton.Visible = val;
            checkButton.Visible = val;
            lblRemarks.Text = "";
        }



        private const int GridSize = 9;

        private void InitializeGrid()
        {
            dataGridView1.RowCount = GridSize;
            dataGridView1.ColumnCount = GridSize;

            for (int i = 0; i < GridSize; i++)
            {
                dataGridView1.Columns[i].Width = 40;
            }
        }

        private int[][] board = new int[GridSize][];

        private void newGameButton_Click(object sender, EventArgs e)
        {

            lblSelectDifficulty.Text = "Select Difficulty";
            cmbDifficulty.Enabled = true;
            btnStartGame.Visible = true;
            EnableCotents(false);
        }

        private void solveButton_Click(object sender, EventArgs e)
        {
            SudokuGenerator.PrintBackup();
        }

        private void checkButton_Click(object sender, EventArgs e)
        {
            if (IsValidSudoku())
            {
                lblRemarks.Text = "Solution is valid!";
                lblRemarks.ForeColor = Color.Green;
            }
            else
            {
                lblRemarks.Text = "Solution is invalid!";
                lblRemarks.ForeColor = Color.Red;
            }
        }


        private bool SolveSudoku(int[][] board)
        {
            // Your existing Sudoku solving logic
            return true;
        }

        internal bool IsValidSudoku()
        {

            int[,] board = new int[GridSize, GridSize];
            // Read the values from the DataGridView into the board array
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    if (!int.TryParse(Form1.dataGridView1.Rows[i].Cells[j].Value.ToString(), out board[i, j]))
                    {
                        // If the cell is not a valid integer, return false
                        return false;
                    }
                }
            }

            // Check each row
            for (int i = 0; i < GridSize; i++)
            {
                bool[] present = new bool[GridSize + 1];
                for (int j = 0; j < GridSize; j++)
                {
                    if (!board[i, j].ToString().Equals(""))
                    {
                        if (present[board[i, j]])
                        {
                            return false;
                        }
                        present[board[i, j]] = true;
                    }
                }
            }

            // Check each column
            for (int j = 0; j < GridSize; j++)
            {
                bool[] present = new bool[GridSize + 1];
                for (int i = 0; i < GridSize; i++)
                {
                    if (!board[i, j].ToString().Equals(""))
                    {
                        if (present[board[i, j]])
                        {
                            return false;
                        }
                        present[board[i, j]] = true;
                    }
                }
            }

            // Check each 3x3 box
            for (int i = 0; i < GridSize; i += 3)
            {
                for (int j = 0; j < GridSize; j += 3)
                {
                    bool[] present = new bool[GridSize + 1];
                    for (int x = 0; x < 3; x++)
                    {
                        for (int y = 0; y < 3; y++)
                        {
                            if (!board[i, j].ToString().Equals(""))
                            {
                                if (present[board[i + x, j + y]])
                                {
                                    return false;
                                }
                                present[board[i + x, j + y]] = true;
                            }
                        }
                    }
                }
            }

            // If no invalid numbers are found, the Sudoku is valid
            return true;
        }


        private void DisplayBoard(int[][] board)
        {
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = board[i][j] == 0 ? "" : board[i][j].ToString();
                }
            }
        }

        /*  private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
          {
              // Check if the cell is in edit mode
              if (dataGridView1.IsCurrentCellInEditMode)
              {
                  // Get the new value entered by the user
                  string newValue = e.FormattedValue.ToString();

                  // Try to parse the new value as an integer
                  if (int.TryParse(newValue, out int number))
                  {
                      // Check if the number is within the allowed range (1-9)
                      if (number < 1 || number > 9)
                      {
                          // If not, cancel the event and show an error message
                          e.Cancel = true;
                          dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                          dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.White;
                          lblRemarks.Text = "Please enter a number between 1 and 9.";
                          lblRemarks.ForeColor = Color.Red;
                          // MessageBox.Show("Please enter a number between 1 and 9.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                      }
                  }
                  else
                  {
                      // If the value is not a valid integer, cancel the event and show an error message
                      e.Cancel = true;
                      dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                      dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.White;
                      lblRemarks.Text = "Please enter a valid number.";
                      lblRemarks.ForeColor = Color.Red;
                      //MessageBox.Show("Please enter a valid number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  }
              }
          }*/

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // Check if the cell is in edit mode
            if (dataGridView1.IsCurrentCellInEditMode)
            {
                // Get the new value entered by the user
                string newValue = e.FormattedValue.ToString();

                // Try to parse the new value as an integer
                if (int.TryParse(newValue, out int number))
                {
                    // Check if the number is within the allowed range (1-9)
                    if (number < 1 || number > 9)
                    {
                        // If not, cancel the event and show an error message
                        e.Cancel = true;
                        lblRemarks.Text = "Please enter a number between 1 and 9.";
                        lblRemarks.ForeColor = Color.Red;
                    }
                }
                else
                {
                    // If the value is not a valid integer, cancel the event and show an error message
                    e.Cancel = true;
                    lblRemarks.Text = "Please enter a valid number.";
                    lblRemarks.ForeColor = Color.Red;
                }
            }
        }

        private void dataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            // If the cell is invalid, change its color
            if (!string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].ErrorText))
            {
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.White;
            }
            else
            {
                // If the cell is valid, reset its color
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = default;
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = default;
            }
        }


        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];

            // Try to parse the cell value as an integer
            if (int.TryParse(cell.Value.ToString(), out int number))
            {
                // Check if the number is within the allowed range (1-9)
                if (number >= 1 && number <= 9)
                {
                    // If the value is valid, reset the cell style to default
                    cell.Style.BackColor = dataGridView1.DefaultCellStyle.BackColor;
                    cell.Style.ForeColor = dataGridView1.DefaultCellStyle.ForeColor;
                    lblRemarks.Text = "";
                }
            }
            else
            {
                // If the value is not a valid integer, reset the cell style to default
                cell.Style.BackColor = dataGridView1.DefaultCellStyle.BackColor;
                cell.Style.ForeColor = dataGridView1.DefaultCellStyle.ForeColor;
                lblRemarks.Text = "";
            }
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            if (cmbDifficulty.SelectedItem.ToString().Equals(""))
            {
                MessageBox.Show("Please select a difficulty level.");
            }
            else
            {
                SudokuGenerator.GenerateSudokuPuzzle(cmbDifficulty.SelectedItem.ToString());
                lblSelectDifficulty.Text = "Difficulty";
                cmbDifficulty.Enabled = false;
                btnStartGame.Visible = false;
                EnableCotents(true);
            }
        }
    }
}
