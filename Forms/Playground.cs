using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace tic_tac_toe
{
    public partial class Playground : Form
    {
        private readonly int box_size = 75; //sizing ob nutton x and y
        private readonly int box_space = 6; //spacing between buttons on board
        private readonly int matches_to_win = 3; //tells matches needed to win
        private readonly int board_size; //shows how many rows and column will be on board, can't be less than matches_to_win
        private readonly Logic logic = new Logic();
        private bool turn; //true = X; false = Y
        private List<Button> board_buttons_list; //list of playable buttons on board
        
        

        public Playground(int boardSize, bool turn)
        {
            InitializeComponent();
            board_buttons_list = new List<Button>();
            board_size = boardSize;
            this.turn = turn;
        }

        private void Playground_Load(object sender, EventArgs e)
        {
            this.Size = new Size(count_window_size() + 16, count_window_size() + 40);
            spawn_buttons();
        }

        private void spawn_buttons()
        {
            for (int x = 0; x < board_size; x++)
            {
                for (int y = 0; y < board_size; y++)
                {
                    AddNewButtonToPlayground(x, y);
                }
            }
        }

        private void AddNewButtonToPlayground(int x, int y)
        {
            Button button = new Button();
            button.Name = "playground_button";
            button.Tag = new Elements(x + 1, y + 1);
            button.Size = new Size(box_size, box_size);
            button.Font = new Font(button.Font.FontFamily, 30);
            button.TabStop = false;
            button.Location = new Point(count_point(y), count_point(x));
            button.Click += new EventHandler(button_Click);
            button.MouseEnter += new EventHandler(button_Enter);
            button.MouseLeave += new EventHandler(button_Leave);
            board_buttons_list.Add(button);
            Controls.Add(button);
        }

        private int count_point(int x)
        {
            return box_space + ((box_size + box_space) * x);
        }

        private int count_window_size()
        {
            return box_space + ((box_size + box_space) * board_size);
        }
        private void button_Leave(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Enabled.Equals(true))
            {
                button.Text = String.Empty;
            }
        }

        private void button_Enter(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if(button.Enabled.Equals(true))
            {
                if (turn)
                    button.Text = "X";
                else
                    button.Text = "O";
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button clicked = (Button)sender;
            if (turn)
                clicked.Text = "X";
            else
                clicked.Text = "O";
            turn = !turn;
            clicked.Enabled = false;
            checkForWinner();
        }

        private void checkForWinner()
        {
            string horizontal = logic.checkHorizontal(board_buttons_list, board_size, matches_to_win);
            string vertical = logic.checkVertical(board_buttons_list, board_size, matches_to_win);
            string diagonal = logic.checkDiagonal(board_buttons_list, board_size, matches_to_win);
            if (horizontal != null)
            {
                IsActiveButtonChange(false);
                InformTheWinner($"The winner is {horizontal}.");
            }
            else if (vertical != null)
            {
                IsActiveButtonChange(false);
                InformTheWinner($"The winner is {vertical}.");
            }
            else if (diagonal != null)
            {
                IsActiveButtonChange(false);
                InformTheWinner($"The winner is {diagonal}.");
            }
            else if (logic.checkForDraw(board_buttons_list))
                InformTheWinner("It's a draw.");
        }

        private void InformTheWinner(string winner)
        {
            DialogResult dialogResult = MessageBox.Show(winner + " Do you wish to play again?", "Game is over!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                ResetAllPlaygroundButtonsText();
                IsActiveButtonChange(true);
            }
            else if (dialogResult == DialogResult.No)
            {
                this.Close();
            }
        }

        private void IsActiveButtonChange(bool status)
        {
            foreach (Button button in board_buttons_list)
            {
                button.Enabled = status;
            }
        }

        private void ResetAllPlaygroundButtonsText()
        {
            foreach (Button button in board_buttons_list)
            {
                button.Text = string.Empty;
            }
        }
    }
}
