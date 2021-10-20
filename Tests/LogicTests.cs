using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using Xunit;
using System;

namespace tic_tac_toe.Tests
{
    public class LogicTests
    {
        private readonly Logic _sut;

        public LogicTests()
        {
            _sut = new Logic();
        }

        [Theory]
        [InlineData("X", 3)]
        [InlineData("X", 4)]
        [InlineData("X", 5)]
        [InlineData("X", 6)]
        [InlineData("X", 7)]
        [InlineData("X", 8)]
        [InlineData("X", 9)]
        [InlineData("X", 10)]
        public void CheckHorizontalTest(string expected, int boardSize)
        {
            string actual = _sut.checkHorizontal(GenerateButtonsListHorizontal(expected, boardSize), boardSize, 3);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("X", 3)]
        [InlineData("X", 4)]
        [InlineData("X", 5)]
        [InlineData("X", 6)]
        [InlineData("X", 7)]
        [InlineData("X", 8)]
        [InlineData("X", 9)]
        [InlineData("X", 10)]
        public void CheckVerticalTest(string expected, int boardSize)
        {
            string actual = _sut.checkVertical(GenerateButtonsListVertical(expected, boardSize), boardSize, 3);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("X", 3, true)]
        [InlineData("X", 4, true)]
        [InlineData("X", 5, true)]
        [InlineData("X", 6, true)]
        [InlineData("X", 7, true)]
        [InlineData("X", 8, true)]
        [InlineData("X", 9, true)]
        [InlineData("X", 10, true)]

        [InlineData("X", 3, false)]
        [InlineData("X", 4, false)]
        [InlineData("X", 5, false)]
        [InlineData("X", 6, false)]
        [InlineData("X", 7, false)]
        [InlineData("X", 8, false)]
        [InlineData("X", 9, false)]
        [InlineData("X", 10, false)]
        public void CheckDiagonalTest(string expected, int boardSize, bool toSide)
        {
            string actual = _sut.checkDiagonal(GenerateButtonsListDiagonal(expected, boardSize, toSide), boardSize, 3);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(true, 3)]
        [InlineData(true, 4)]
        [InlineData(true, 5)]
        [InlineData(true, 6)]
        [InlineData(true, 7)]
        [InlineData(true, 8)]
        [InlineData(true, 9)]
        [InlineData(true, 10)]
        public void CheckForDrawTest(bool expected, int boardSize)
        {
            bool actual = _sut.checkForDraw(GenerateButtonsDraw(boardSize));
            Assert.Equal(expected, actual);
        }

        private List<Button> GenerateButtonsListHorizontal(string expected, int board_size)
        {
            List<Button> buttonsList = new List<Button>();
            Random random = new Random();
            int row = random.Next(1, board_size + 1);
            int col = random.Next(1, board_size - 1);
            for (int x = 0; x < board_size; x++)
            {
                for (int y = 0; y < board_size; y++)
                {
                    if(x + 1 == row && y + 1 >= col && y + 1 <= col + 2)
                        buttonsList.Add(GenerateButton(x, y, expected, false));
                    buttonsList.Add(GenerateButton(x, y, "any", true));
                }
            }
            return buttonsList;
        }

        private List<Button> GenerateButtonsListVertical(string expected, int board_size)
        {
            List<Button> buttonsList = new List<Button>();
            Random random = new Random();
            int col = random.Next(1, board_size + 1);
            int row = random.Next(1, board_size - 1);
            for (int x = 0; x < board_size; x++)
            {
                for (int y = 0; y < board_size; y++)
                {
                    if (y + 1 == col && x + 1 >= row && x + 1 <= row + 2)
                        buttonsList.Add(GenerateButton(x, y, expected, false));
                    buttonsList.Add(GenerateButton(x, y, "any", true));
                }
            }
            return buttonsList;
        }

        private List<Button> GenerateButtonsListDiagonal(string expected, int board_size, bool toSide)
        {
            List<Button> buttonsList = new List<Button>();
            Random random = new Random();
            for (int x = 0; x < board_size; x++)
            {
                for (int y = 0; y < board_size; y++)
                {
                    buttonsList.Add(GenerateButton(x, y, "any", true));
                }
            }
            if (toSide)//right
            {
                int col = random.Next(1, board_size - 1);
                int row = random.Next(1, board_size - 1);
                for (int i = 0; i < 3; i++)
                {
                    foreach (Button ongoing in buttonsList)
                    {
                        if ( ((Elements)ongoing.Tag).getx() == row + i && ((Elements)ongoing.Tag).gety() == col + i )
                        {
                            ongoing.Text = expected;
                            ongoing.Enabled = false;
                        }
                    }
                }
            }
            else//left
            {
                int col = random.Next(1, board_size - 1);
                int row = random.Next(3, board_size + 1);
                for (int i = 0; i < 3; i++)
                {
                    foreach (Button ongoing in buttonsList)
                    {
                        if (((Elements)ongoing.Tag).getx() == row - i && ((Elements)ongoing.Tag).gety() == col + i)
                        {
                            ongoing.Text = expected;
                            ongoing.Enabled = false;
                        }
                    }
                }

            }
            return buttonsList;
        }

        private List<Button> GenerateButtonsDraw(int board_size)
        {
            List<Button> buttonsList = new List<Button>();
            for (int x = 0; x < board_size; x++)
            {
                for (int y = 0; y < board_size; y++)
                {
                    buttonsList.Add(GenerateButton(x, y, "any", false));
                }
            }
            return buttonsList;
        }

        private Button GenerateButton(int x, int y, string text, bool isEnabled)
        {
            Button button = new Button();
            button.Name = "playground_button";
            button.Tag = new Elements(x + 1, y + 1);
            button.Size = new Size(3, 3);
            button.Text = text;
            button.Font = new Font(button.Font.FontFamily, 30);
            button.TabStop = false;
            button.Location = new Point(1, 1);
            button.Enabled = isEnabled;
            return button;
        }
    }
}
