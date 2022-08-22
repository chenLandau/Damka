using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UserInterface
{
    internal partial class FormGameSetting : Form
    {
        public FormGameSetting()
        {
            InitializeComponent();
        }

        public int BoardGameSize
        {
            // $G$ NTT-999 (-3) You should use enum for the bord sizes
            get
            {
                int boardSize;

                if (radioButton6x6.Checked)
                {
                    boardSize = 6;
                }
                else if(radioButton8x8.Checked)
                {
                    boardSize = 8;
                }
                else
                {
                    boardSize = 10;
                }

                return boardSize;
            }
        }

        public string TextBoxPlayer1
        {
            get { return textBoxPlayer1.Text; }
        }

        public string TextBoxPlayer2
        {
            get { return checkBoxPlayer2.Checked ? textBoxPlayer2.Text : "Computer"; }
        }

        public bool CheckPlayer2Type
        {
            get { return checkBoxPlayer2.Checked; }
        }

        private void radioButtonClicked(RadioButton i_Clicked, RadioButton i_UnCheckedButton1, RadioButton i_UnCheckedButton2)
        {
            i_Clicked.Checked = true;
            i_UnCheckedButton1.Checked = false;
            i_UnCheckedButton2.Checked = false;
        }

        private void radioButton6x6_Click(object sender, EventArgs e)
        {
            radioButtonClicked(radioButton6x6, radioButton8x8, radioButton10x10);
        }

        private void radioButton8x8_Click(object sender, EventArgs e)
        {
            radioButtonClicked(radioButton8x8, radioButton6x6, radioButton10x10);
        }

        private void radioButton10x10_Click(object sender, EventArgs e)
        {
            radioButtonClicked(radioButton10x10, radioButton6x6, radioButton8x8);
        }

        private void handledKeyPressInput(object sender, KeyPressEventArgs e)
        {
            bool isControl = char.IsControl(e.KeyChar);
            bool isLetter = char.IsLetter(e.KeyChar);
            string messageToShow;

            if (textBoxPlayer1.Text.Length == 20)
            {
                if (!isControl)
                {
                    messageToShow = isLetter ? "Maximum 20 letters!" : "Please enter only letters!";
                    MessageBox.Show(messageToShow);
                    e.Handled = true;
                }
            }
            else
            {
                if (!isControl && !isLetter)
                {
                    MessageBox.Show("Please enter only letters!");
                    e.Handled = true;
                }
            }
        }

        private void textBoxPlayer1_KeyPress(object sender, KeyPressEventArgs e)
        {
            handledKeyPressInput(sender, e);
        }

        private void checkBoxPlayer2_Click(object sender, EventArgs e)
        {
            textBoxPlayer2.Enabled = checkBoxPlayer2.Checked;
            // $G$ NTT-999 (-5) You should have used constants here
            textBoxPlayer2.Text = textBoxPlayer2.Enabled ? string.Empty : "[Computer]"; 
        }

        private void textBoxPlayer2_KeyPress(object sender, KeyPressEventArgs e)
        {
            handledKeyPressInput(sender, e);
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            if(textBoxPlayer1.Text.Length == 0 || (checkBoxPlayer2.Checked && textBoxPlayer2.Text.Length == 0))
            {
                MessageBox.Show("Please fill missed information!", "Game Setting");
            }
            else if(TextBoxPlayer1 == TextBoxPlayer2)
            {
                MessageBox.Show("Players names should be different!", "Game Setting");
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
        }
    }
}
