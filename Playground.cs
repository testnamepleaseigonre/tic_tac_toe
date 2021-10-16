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
            }
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
            string horizontal = checkHorizontal();
            string vertical = checkVertical();
            string diagonal = checkDiagonal();
            if(horizontal != null)
            {
                Console.WriteLine("Winner is " + horizontal);
                DisableActiveButtons();
            }
            else if (vertical != null)
            {
                Console.WriteLine("Winner is " + vertical);
                DisableActiveButtons();
            }
            else if (diagonal != null)
            {
                Console.WriteLine("Winner is " + diagonal);
                DisableActiveButtons();
            }
            else if(checkForDraw())
                Console.WriteLine("It's a draw!");
        }

        private void DisableActiveButtons()
        {
            foreach (Button button in board_buttons_list.FindAll(b => b.Enabled.Equals(true)))
            {
                button.Enabled = false;
            }
        }

        private bool checkForDraw()
        {
            if(board_buttons_list.FindAll(button => button.Enabled.Equals(true)).Count == 0)
            {
                return true;
            }
            return false;
        }

        private string checkDiagonal()
        {
            foreach (Button ongoing in board_buttons_list)
            {
                if (getButtonX(ongoing) + matches_to_win - 1 <= board_size && getButtonY(ongoing) + matches_to_win - 1 <= board_size && ongoing.Enabled.Equals(false))
                {
                    if (checkForWinDiagonalRight(ongoing))
                    {
                        return String.Format(ongoing.Text);
                    }
                }
                if(getButtonY(ongoing) >= matches_to_win && getButtonX(ongoing) <= board_size - matches_to_win + 1 && ongoing.Enabled.Equals(false))
                {
                    if (checkForWinDiagonalLeft(ongoing))
                    {
                        return String.Format(ongoing.Text);
                    }
                }
            }
            return null;
        }

        private bool checkForWinDiagonalLeft(Button checking)
        {
            int matches = 1;
            for (int i = 1; i < matches_to_win; i++)
            {
                foreach (Button ongoing in board_buttons_list)
                {
                    if (ongoing.Enabled.Equals(false) && getButtonX(ongoing).Equals(getButtonX(checking) + i) && getButtonY(ongoing).Equals(getButtonY(checking) - i) && ongoing.Text.Equals(checking.Text))
                    {
                        matches++;
                        continue;
                    }
                }
            }
            if (matches.Equals(matches_to_win))
                return true;
            return false;
        }

        private bool checkForWinDiagonalRight(Button checking)
        {
            int matches = 1;
            for (int i = 1; i < matches_to_win; i++)
            {
                foreach (Button ongoing in board_buttons_list)
                {
                    if (ongoing.Enabled.Equals(false) && getButtonX(ongoing).Equals(getButtonX(checking) + i) && getButtonY(ongoing).Equals(getButtonY(checking) + i) && ongoing.Text.Equals(checking.Text))
                    {
                        matches++;
                        continue;
                    }
                }
            }
            if (matches.Equals(matches_to_win))
                return true;
            return false;
        }

        private string checkVertical()
        {
            foreach (Button ongoing in board_buttons_list)
            {
                if (getButtonX(ongoing) + matches_to_win - 1 <= board_size && ongoing.Enabled.Equals(false))
                {
                    if (checkForWinVertical(ongoing))
                    {
                        return String.Format(ongoing.Text);
                    }
                }
            }
            return null;
        }

        private bool checkForWinVertical(Button checking)
        {
            int matches = 1;
            for (int i = 1; i < matches_to_win; i++)
            {
                foreach (Button ongoing in board_buttons_list)
                {
                    if (ongoing.Enabled.Equals(false) && getButtonY(checking).Equals(getButtonY(ongoing)) && getButtonX(ongoing).Equals(getButtonX(checking) + i) && ongoing.Text.Equals(checking.Text))
                    {
                        matches++;
                        continue;
                    }
                }
            }
            if (matches.Equals(matches_to_win))
                return true;
            return false;
        }

        private string checkHorizontal()
        {
            foreach (Button ongoing in board_buttons_list)
            {
                if (getButtonY(ongoing) + matches_to_win - 1 <= board_size && ongoing.Enabled.Equals(false))
                {
                    if(checkForWinHorizontal(ongoing))
                    {
                        return String.Format(ongoing.Text);
                    }
                }
            }
            return null;
        }

        private bool checkForWinHorizontal(Button checking)
        {
            int matches = 1;
            for (int i = 1; i < matches_to_win; i++)
            {
                foreach (Button ongoing in board_buttons_list)
                {
                    if (ongoing.Enabled.Equals(false) && getButtonX(checking).Equals(getButtonX(ongoing)) && getButtonY(ongoing).Equals(getButtonY(checking) + i) && ongoing.Text.Equals(checking.Text))
                    {
                        matches++;
                        continue;
                    }
                }
            }
            if(matches.Equals(matches_to_win))
                return true;
            return false;
        }

        private int getButtonX(Button button)
        {
            return ((Elements)button.Tag).getx();
        }

        private int getButtonY(Button button)
        {
            return ((Elements)button.Tag).gety();
        }
    }
}
