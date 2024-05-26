using System.Windows.Forms;

namespace Sudoku
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            dataGridView1 = new DataGridView();
            newGameButton = new Button();
            solveButton = new Button();
            checkButton = new Button();
            cmbDifficulty = new ComboBox();
            lblSelectDifficulty = new Label();
            btnStartGame = new Button();
            lblRemarks = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.Location = new Point(51, 56);
            dataGridView1.Margin = new Padding(4, 3, 4, 3);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowTemplate.Height = 40;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView1.Size = new Size(361, 361);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
            dataGridView1.CellValidating += dataGridView1_CellValidating;
            dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 15);
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // 
            // newGameButton
            // 
            newGameButton.Location = new Point(33, 470);
            newGameButton.Margin = new Padding(4, 3, 4, 3);
            newGameButton.Name = "newGameButton";
            newGameButton.Size = new Size(88, 27);
            newGameButton.TabIndex = 1;
            newGameButton.Text = "New Game";
            newGameButton.UseVisualStyleBackColor = true;
            newGameButton.Click += newGameButton_Click;
            // 
            // solveButton
            // 
            solveButton.Location = new Point(194, 470);
            solveButton.Margin = new Padding(4, 3, 4, 3);
            solveButton.Name = "solveButton";
            solveButton.Size = new Size(88, 27);
            solveButton.TabIndex = 2;
            solveButton.Text = "Solve";
            solveButton.UseVisualStyleBackColor = true;
            solveButton.Click += solveButton_Click;
            // 
            // checkButton
            // 
            checkButton.Location = new Point(365, 470);
            checkButton.Margin = new Padding(4, 3, 4, 3);
            checkButton.Name = "checkButton";
            checkButton.Size = new Size(88, 27);
            checkButton.TabIndex = 3;
            checkButton.Text = "Check";
            checkButton.UseVisualStyleBackColor = true;
            checkButton.Click += checkButton_Click;
            // 
            // cmbDifficulty
            // 
            cmbDifficulty.FormattingEnabled = true;
            cmbDifficulty.Items.AddRange(new object[] { "Very Easy", "Easy", "Medium", "Hard", "Very Hard" });
            cmbDifficulty.Location = new Point(203, 11);
            cmbDifficulty.Name = "cmbDifficulty";
            cmbDifficulty.Size = new Size(121, 23);
            cmbDifficulty.TabIndex = 4;
            // 
            // lblSelectDifficulty
            // 
            lblSelectDifficulty.AutoSize = true;
            lblSelectDifficulty.Location = new Point(108, 14);
            lblSelectDifficulty.Name = "lblSelectDifficulty";
            lblSelectDifficulty.Size = new Size(89, 15);
            lblSelectDifficulty.TabIndex = 5;
            lblSelectDifficulty.Text = "Select Difficulty";
            // 
            // btnStartGame
            // 
            btnStartGame.BackColor = SystemColors.GradientActiveCaption;
            btnStartGame.Location = new Point(334, 11);
            btnStartGame.Name = "btnStartGame";
            btnStartGame.Size = new Size(119, 23);
            btnStartGame.TabIndex = 6;
            btnStartGame.Text = "Start Game";
            btnStartGame.UseVisualStyleBackColor = false;
            btnStartGame.Click += btnStartGame_Click;
            // 
            // lblRemarks
            // 
            lblRemarks.AutoSize = true;
            lblRemarks.Location = new Point(33, 447);
            lblRemarks.Name = "lblRemarks";
            lblRemarks.Size = new Size(0, 15);
            lblRemarks.TabIndex = 7;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(471, 510);
            Controls.Add(lblRemarks);
            Controls.Add(btnStartGame);
            Controls.Add(lblSelectDifficulty);
            Controls.Add(cmbDifficulty);
            Controls.Add(checkButton);
            Controls.Add(solveButton);
            Controls.Add(newGameButton);
            Controls.Add(dataGridView1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "Sudoku";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button newGameButton;
        private Button solveButton;
        private Button checkButton;
        private ComboBox cmbDifficulty;
        private Label lblSelectDifficulty;
        private Button btnStartGame;
        private Label lblRemarks;
        internal static DataGridView dataGridView1;
    }
}
