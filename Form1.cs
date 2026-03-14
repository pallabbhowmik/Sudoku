using System.Drawing;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class Form1 : Form
    {
        private const int GridSize = 9;
        private System.Windows.Forms.Timer gameTimer = null!;
        private int elapsedSeconds;

        public Form1()
        {
            InitializeComponent();
            InitializeGrid();
            InitializeTimer();
            cmbDifficulty.SelectedIndex = 0;
            cmbDifficulty.Visible = true;
            lblSelectDifficulty.Visible = true;
            lblSelectDifficulty.Enabled = true;
            btnStartGame.Visible = true;
            EnableContents(false);
        }

        private void InitializeTimer()
        {
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            elapsedSeconds++;
            lblTimer.Text = $"{elapsedSeconds / 60:D2}:{elapsedSeconds % 60:D2}";
        }

        private void EnableContents(bool val)
        {
            dataGridView1.Visible = val;
            newGameButton.Visible = val;
            solveButton.Visible = val;
            checkButton.Visible = val;
            btnHint.Visible = val;
            lblTimer.Visible = val;
            lblRemaining.Visible = val;
            lblRemarks.Text = "";
        }

        private void InitializeGrid()
        {
            dataGridView1.RowCount = GridSize;
            dataGridView1.ColumnCount = GridSize;

            for (int i = 0; i < GridSize; i++)
            {
                dataGridView1.Columns[i].Width = 40;
            }
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            gameTimer.Stop();
            elapsedSeconds = 0;
            lblTimer.Text = "00:00";
            lblSelectDifficulty.Text = "Select Difficulty";
            cmbDifficulty.Enabled = true;
            btnStartGame.Visible = true;
            EnableContents(false);
        }

        private void solveButton_Click(object sender, EventArgs e)
        {
            gameTimer.Stop();
            SudokuGenerator.PrintBackup();
            lblRemarks.Text = "Puzzle solved!";
            lblRemarks.ForeColor = Color.Blue;
            UpdateRemainingCount();
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

        internal bool IsValidSudoku()
        {
            int[,] board = new int[GridSize, GridSize];

            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    var cellValue = dataGridView1.Rows[i].Cells[j].Value;
                    if (cellValue == null || string.IsNullOrWhiteSpace(cellValue.ToString()))
                        return false;
                    if (!int.TryParse(cellValue.ToString(), out int val) || val < 1 || val > 9)
                        return false;
                    board[i, j] = val;
                }
            }

            for (int i = 0; i < GridSize; i++)
            {
                bool[] present = new bool[GridSize + 1];
                for (int j = 0; j < GridSize; j++)
                {
                    if (present[board[i, j]])
                        return false;
                    present[board[i, j]] = true;
                }
            }

            for (int j = 0; j < GridSize; j++)
            {
                bool[] present = new bool[GridSize + 1];
                for (int i = 0; i < GridSize; i++)
                {
                    if (present[board[i, j]])
                        return false;
                    present[board[i, j]] = true;
                }
            }

            for (int i = 0; i < GridSize; i += 3)
            {
                for (int j = 0; j < GridSize; j += 3)
                {
                    bool[] present = new bool[GridSize + 1];
                    for (int x = 0; x < 3; x++)
                    {
                        for (int y = 0; y < 3; y++)
                        {
                            int val = board[i + x, j + y];
                            if (present[val])
                                return false;
                            present[val] = true;
                        }
                    }
                }
            }

            return true;
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridView1.IsCurrentCellInEditMode)
            {
                string newValue = e.FormattedValue?.ToString() ?? "";

                if (string.IsNullOrWhiteSpace(newValue))
                    return;

                if (int.TryParse(newValue, out int number))
                {
                    if (number < 1 || number > 9)
                    {
                        e.Cancel = true;
                        lblRemarks.Text = "Please enter a number between 1 and 9.";
                        lblRemarks.ForeColor = Color.Red;
                    }
                }
                else
                {
                    e.Cancel = true;
                    lblRemarks.Text = "Please enter a valid number.";
                    lblRemarks.ForeColor = Color.Red;
                }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var cellValue = cell.Value;

            if (cellValue != null && int.TryParse(cellValue.ToString(), out int number) && number >= 1 && number <= 9)
            {
                cell.Style.BackColor = dataGridView1.DefaultCellStyle.BackColor;
                cell.Style.ForeColor = Color.DarkBlue;
                lblRemarks.Text = "";
            }
            else
            {
                cell.Style.BackColor = dataGridView1.DefaultCellStyle.BackColor;
                cell.Style.ForeColor = dataGridView1.DefaultCellStyle.ForeColor;
                lblRemarks.Text = "";
            }

            UpdateRemainingCount();
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            if (cmbDifficulty.SelectedItem == null || string.IsNullOrEmpty(cmbDifficulty.SelectedItem.ToString()))
            {
                MessageBox.Show("Please select a difficulty level.");
            }
            else
            {
                SudokuGenerator.GenerateSudokuPuzzle(cmbDifficulty.SelectedItem.ToString()!);
                lblSelectDifficulty.Text = "Difficulty";
                cmbDifficulty.Enabled = false;
                btnStartGame.Visible = false;
                elapsedSeconds = 0;
                lblTimer.Text = "00:00";
                gameTimer.Start();
                EnableContents(true);
                UpdateRemainingCount();
            }
        }

        private void btnHint_Click(object sender, EventArgs e)
        {
            if (SudokuGenerator.RevealHint())
            {
                UpdateRemainingCount();
                lblRemarks.Text = "Hint revealed!";
                lblRemarks.ForeColor = Color.Blue;
            }
            else
            {
                lblRemarks.Text = "No more hints available!";
                lblRemarks.ForeColor = Color.Orange;
            }
        }

        internal void UpdateRemainingCount()
        {
            int remaining = 0;
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    var val = dataGridView1.Rows[i].Cells[j].Value;
                    if (val == null || string.IsNullOrWhiteSpace(val.ToString()))
                        remaining++;
                }
            }
            lblRemaining.Text = $"Remaining: {remaining}";

            if (remaining == 0 && IsValidSudoku())
            {
                gameTimer.Stop();
                lblRemarks.Text = $"Congratulations! Solved in {lblTimer.Text}!";
                lblRemarks.ForeColor = Color.Green;
            }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);

            using (Pen pen = new Pen(Color.Gray, 1))
            using (Pen thickPen = new Pen(Color.Black, 3))
            {
                if (e.ColumnIndex == 2 || e.ColumnIndex == 5 || e.ColumnIndex == 8)
                    e.Graphics.DrawLine(thickPen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                else
                    e.Graphics.DrawLine(pen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);

                if (e.RowIndex == 2 || e.RowIndex == 5 || e.RowIndex == 8)
                    e.Graphics.DrawLine(thickPen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                else
                    e.Graphics.DrawLine(pen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);

                if (e.RowIndex == 0 || e.RowIndex == 3 || e.RowIndex == 6)
                    e.Graphics.DrawLine(thickPen, e.CellBounds.Left, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Top);

                if (e.ColumnIndex == 0 || e.ColumnIndex == 3 || e.ColumnIndex == 6)
                    e.Graphics.DrawLine(thickPen, e.CellBounds.Left, e.CellBounds.Top, e.CellBounds.Left, e.CellBounds.Bottom - 1);
            }

            e.Handled = true;
        }
    }
}
