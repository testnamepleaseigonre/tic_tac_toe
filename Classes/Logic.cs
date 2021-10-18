using System;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace tic_tac_toe
{
    public class Logic
    {
        public Logic()
        {

        }
        public string checkHorizontal(List<Button> board_buttons_list, int board_size, int matches_to_win)
        {
            foreach (Button ongoing in board_buttons_list)
            {
                if (getButtonY(ongoing) + matches_to_win - 1 <= board_size && ongoing.Enabled.Equals(false))
                {
                    if (checkForWinHorizontal(ongoing, board_buttons_list, matches_to_win))
                    {
                        return String.Format(ongoing.Text);
                    }
                }
            }
            return null;
        }

        private bool checkForWinHorizontal(Button checking, List<Button> board_buttons_list, int matches_to_win)
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
            if (matches.Equals(matches_to_win))
                return true;
            return false;
        }

        public string checkVertical(List<Button> board_buttons_list, int board_size, int matches_to_win)
        {
            foreach (Button ongoing in board_buttons_list)
            {
                if (getButtonX(ongoing) + matches_to_win - 1 <= board_size && ongoing.Enabled.Equals(false))
                {
                    if (checkForWinVertical(ongoing, board_buttons_list, matches_to_win))
                    {
                        return String.Format(ongoing.Text);
                    }
                }
            }
            return null;
        }

        private bool checkForWinVertical(Button checking, List<Button> board_buttons_list, int matches_to_win)
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

        public string checkDiagonal(List<Button> board_buttons_list, int board_size, int matches_to_win)
        {
            foreach (Button ongoing in board_buttons_list)
            {
                if (getButtonX(ongoing) + matches_to_win - 1 <= board_size && getButtonY(ongoing) + matches_to_win - 1 <= board_size && ongoing.Enabled.Equals(false))
                {
                    if (checkForWinDiagonalRight(ongoing, board_buttons_list, matches_to_win))
                    {
                        return String.Format(ongoing.Text);
                    }
                }
                if (getButtonY(ongoing) >= matches_to_win && getButtonX(ongoing) <= board_size - matches_to_win + 1 && ongoing.Enabled.Equals(false))
                {
                    if (checkForWinDiagonalLeft(ongoing, board_buttons_list, matches_to_win))
                    {
                        return String.Format(ongoing.Text);
                    }
                }
            }
            return null;
        }

        private bool checkForWinDiagonalRight(Button checking, List<Button> board_buttons_list, int matches_to_win)
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

        private bool checkForWinDiagonalLeft(Button checking, List<Button> board_buttons_list, int matches_to_win)
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

        public bool checkForDraw(List<Button> board_buttons_list)
        {
            if (board_buttons_list.FindAll(button => button.Enabled.Equals(true)).Count == 0)
            {
                return true;
            }
            return false;
        }

        public int getButtonX(Button button)
        {
            return ((Elements)button.Tag).getx();
        }

        public int getButtonY(Button button)
        {
            return ((Elements)button.Tag).gety();
        }
    }
}
