using System;
using System.Windows.Forms;

namespace tic_tac_toe
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void boar_size_choose_txtbox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(boar_size_choose_txtbox.Text))
                {
                    int temp = int.Parse(boar_size_choose_txtbox.Text);
                    if (temp < 3 || temp > 10)
                        throw new Exception();
                }
            }
            catch
            {
                boar_size_choose_txtbox.Text = "3";
                MessageBox.Show("Size must be a number. Minimum size: 3, maximum: 10.");
            }
        }

        private void play_start_button_Click(object sender, EventArgs e)
        {
            Playground playground = new Playground(int.Parse(boar_size_choose_txtbox.Text), GetWhoStarts());
            playground.ShowDialog();
        }

        private bool GetWhoStarts()
        {
            if (radioButton1.Checked)
                return true;
            return false;
        }
    }
}
