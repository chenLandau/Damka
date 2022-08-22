using System;
using System.Windows.Forms;
using System.Drawing;

namespace UserInterface
{
    internal class FormDamka : Form
    {
        private const int k_ButtonHeightAndWidth = 50;
        private const int k_ButtonSpaceFromLeft = 30;
        private const int k_ButtonSpaceFromTop = 60;
        private const int k_LabelSpaceFromTop = 20;

        private readonly FormGameSetting r_GameSettingForm = new FormGameSetting();
        // $G$ CSS-002 (-5) Bad member variable name (should be m_CamelCased)
        private readonly Button[,] buttons;
        private readonly Label labelPlayer1Name = new Label();
        private readonly Label labelPlayer2Name = new Label();
        private readonly Label labelPlayer1Score = new Label();
        private readonly Label labelPlayer2Score = new Label();

        private readonly GameLogic.MovePointsEventArgs r_ButtonsClickedPoints = new GameLogic.MovePointsEventArgs();
        private bool m_IsFirstClickInMove = true;
        private bool m_IsPlayer1Turn = true;
        private bool m_IsNextMoveIsEating = false;

        public event GameLogic.MoveEnteredEventHandler MoveEntered;

        public FormDamka()
        {
            if (r_GameSettingForm.ShowDialog() != DialogResult.OK)
            {
                Close();
            }

            resetForm(r_GameSettingForm.BoardGameSize);
            buttons = new Button[r_GameSettingForm.BoardGameSize, r_GameSettingForm.BoardGameSize];
            resetPlayersNameLabels(r_GameSettingForm.BoardGameSize, r_GameSettingForm.TextBoxPlayer1, r_GameSettingForm.TextBoxPlayer2);
            resetPlayersScoreLabels();
        }

        public FormGameSetting GameSettingForm
        {
            get { return r_GameSettingForm; }
        }

        public bool IsPlayer1Turn
        {
            get { return m_IsPlayer1Turn; }
            set { m_IsPlayer1Turn = value; }
        }

        public bool IsNextMoveIsEating
        {
            get { return m_IsNextMoveIsEating; }
            set { m_IsNextMoveIsEating = value; }
        }

        private void resetForm(int i_BoardSize)
        {
            Text = "Damka";
            Size = new Size(
                (k_ButtonHeightAndWidth * i_BoardSize) + (2 * k_ButtonSpaceFromLeft),
                (k_ButtonHeightAndWidth * i_BoardSize) + (2 * k_ButtonSpaceFromTop));
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            MinimizeBox = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        public void ResetButtons(GameLogic.BoardGame i_Board)
        {
            for (int i = 0; i < i_Board.BoardSize; i++)
            {
                for (int j = 0; j < i_Board.BoardSize; j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Size = new Size(k_ButtonHeightAndWidth, k_ButtonHeightAndWidth);

                    if (i_Board.Board[i, j] != GameLogic.eObjectType.BlockedCell)
                    {
                        buttons[i, j].Enabled = true;
                        buttons[i, j].BackColor = Color.White;
                        buttons[i, j].BackgroundImage = Properties.Resources.WhiteBackground;
                        // $G$ NTT-999 (-5) You should use foreach loop here
                        if (i_Board.Board[i, j] != GameLogic.eObjectType.Empty)
                        {
                            if(i_Board.Board[i, j] == GameLogic.eObjectType.White)
                            {
                                buttons[i, j].AccessibleName = "O";
                                buttons[i, j].Image = Properties.Resources.White;
                            }
                            else
                            {
                                buttons[i, j].AccessibleName = "X";
                                buttons[i, j].Image = Properties.Resources.Black;
                            }
                        }
                        else
                        {
                            buttons[i, j].Image = null;
                            buttons[i, j].AccessibleName = string.Empty;
                        }
                    }
                    else
                    {
                        buttons[i, j].Enabled = false;
                        buttons[i, j].BackColor = Color.Gray;
                        buttons[i, j].BackgroundImage = Properties.Resources.BlackBackground;
                    }

                    resetButtonStartingLocation(i, j);
                    Controls.Add(buttons[i, j]);
                    buttons[i, j].Click += button_Click;
                }
            }
        }

        private void resetButtonStartingLocation(int i_Row, int i_Col)
        {
            if (i_Row == 0)
            {
                if (i_Col == 0)
                {
                    buttons[i_Row, i_Col].Location = new Point(k_ButtonSpaceFromLeft, k_ButtonSpaceFromTop);
                }
                else
                {
                    buttons[i_Row, i_Col].Location = new Point(buttons[i_Row, i_Col - 1].Right, k_ButtonSpaceFromTop);
                }
            }
            else
            {
                if (i_Col == 0)
                {
                    buttons[i_Row, i_Col].Location = new Point(k_ButtonSpaceFromLeft, buttons[i_Row - 1, i_Col].Bottom);
                }
                else
                {
                    buttons[i_Row, i_Col].Location = new Point(buttons[i_Row - 1, i_Col - 1].Right, buttons[i_Row - 1, i_Col - 1].Bottom);
                }
            }
        }

        private void resetPlayersNameLabels(int i_BoardSize, string i_Player1Name, string i_Player2Name)
        {
            // $G$ DSN-004 (-3) Code duplication!
            labelPlayer1Name.Text = i_Player1Name + ":";
            labelPlayer1Name.Location = new Point(k_ButtonSpaceFromLeft + k_ButtonHeightAndWidth, k_LabelSpaceFromTop);
            labelPlayer1Name.Font = new Font("Microsoft YaHei", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelPlayer1Name.AutoSize = true;
            Controls.Add(labelPlayer1Name);

            labelPlayer2Name.Text = i_Player2Name + ":";
            labelPlayer2Name.Location = new Point(k_ButtonSpaceFromLeft + (((i_BoardSize / 2) + 1) * k_ButtonHeightAndWidth), k_LabelSpaceFromTop);
            labelPlayer2Name.Font = new Font("Microsoft YaHei", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelPlayer2Name.AutoSize = true;
            Controls.Add(labelPlayer2Name);
        }

        private void resetPlayersScoreLabels()
        {
            // $G$ DSN-004 (-3) Code duplication!
            labelPlayer1Score.Text = "0";
            labelPlayer1Score.Location = new Point(labelPlayer1Name.Right, k_LabelSpaceFromTop);
            labelPlayer1Score.Font = new Font("Microsoft YaHei", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelPlayer1Score.AutoSize = true;
            Controls.Add(labelPlayer1Score);

            labelPlayer2Score.Text = "0";
            labelPlayer2Score.Location = new Point(labelPlayer2Name.Right, k_LabelSpaceFromTop);
            labelPlayer2Score.Font = new Font("Microsoft YaHei", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelPlayer2Score.AutoSize = true;
            Controls.Add(labelPlayer2Score);
        }

        public void button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            GameLogic.Point clickedPoint = new GameLogic.Point(
                (button.Location.Y - k_ButtonSpaceFromTop) / k_ButtonHeightAndWidth,
                (button.Location.X - k_ButtonSpaceFromLeft) / k_ButtonHeightAndWidth);

            if (m_IsFirstClickInMove)
            {
                // $G$ NTT-999 (-5) You should have used constants \ enum here
                if ((m_IsPlayer1Turn && (button.AccessibleName == "O" || button.AccessibleName == "U")) ||
                    (!m_IsPlayer1Turn && (button.AccessibleName == "X" || button.AccessibleName == "K")))
                {
                    if ((m_IsNextMoveIsEating && clickedPoint == r_ButtonsClickedPoints.DestinationPoint) || !m_IsNextMoveIsEating)
                    {
                        button.BackColor = Color.LightBlue;
                        button.BackgroundImage = Properties.Resources.LightBlueBackground;
                        r_ButtonsClickedPoints.SourcePoint = clickedPoint;
                        m_IsFirstClickInMove = false;
                    }
                    else
                    {
                        ShowInvalidMoveMessage();
                    }
                }
            }
            else
            {
                if (button.BackColor != Color.LightBlue)
                {
                    r_ButtonsClickedPoints.DestinationPoint = clickedPoint;
                    OnMoveEntered();
                }
                else
                {
                    buttons[r_ButtonsClickedPoints.SourcePoint.X, r_ButtonsClickedPoints.SourcePoint.Y].BackColor = Color.White;
                    buttons[r_ButtonsClickedPoints.SourcePoint.X, r_ButtonsClickedPoints.SourcePoint.Y].BackgroundImage = Properties.Resources.WhiteBackground;
                    m_IsFirstClickInMove = true;
                }   
            }
        }

        protected virtual void OnMoveEntered()
        {
            if(MoveEntered != null)
            {
                MoveEntered.Invoke(r_ButtonsClickedPoints);
            }
        }

        public void ShowInvalidMoveMessage()
        {
            MessageBox.Show("Invalid move!", "Damka");
        }

        public void SetButtonsAfterMove(GameLogic.Point i_SourcePoint, GameLogic.Point i_DestinationPoint)
        {
            buttons[i_DestinationPoint.X, i_DestinationPoint.Y].AccessibleName = buttons[i_SourcePoint.X, i_SourcePoint.Y].AccessibleName;
            buttons[i_DestinationPoint.X, i_DestinationPoint.Y].Image = buttons[i_SourcePoint.X, i_SourcePoint.Y].Image;
            buttons[i_SourcePoint.X, i_SourcePoint.Y].AccessibleName = string.Empty;
            buttons[i_SourcePoint.X, i_SourcePoint.Y].Image = null;
            buttons[r_ButtonsClickedPoints.SourcePoint.X, r_ButtonsClickedPoints.SourcePoint.Y].BackColor = Color.White;
            buttons[r_ButtonsClickedPoints.SourcePoint.X, r_ButtonsClickedPoints.SourcePoint.Y].BackgroundImage = Properties.Resources.WhiteBackground;
            m_IsFirstClickInMove = true;
        }

        public void SetButtonAfterEating(GameLogic.Point i_Point)
        {
            buttons[i_Point.X, i_Point.Y].AccessibleName = string.Empty;
            buttons[i_Point.X, i_Point.Y].Image = null;
        }

        public void SetKing(GameLogic.Point i_Point, GameLogic.ePlayerObjectsColor i_PlayerColor)
        {
            if(i_PlayerColor == GameLogic.ePlayerObjectsColor.White)
            {
                buttons[i_Point.X, i_Point.Y].AccessibleName = "U";
                buttons[i_Point.X, i_Point.Y].Image = Properties.Resources.WhiteKing;
            }
            else
            {
                buttons[i_Point.X, i_Point.Y].AccessibleName = "K";
                buttons[i_Point.X, i_Point.Y].Image = Properties.Resources.BlackKing;
            }
        }

        public bool HandleTieResult()
        {
            return MessageBox.Show(
                $"Tie!{ Environment.NewLine}Another Round?",
                "Damka",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes ? true : false;
        }

        public bool HandleWinResult(GameLogic.Player i_Winner)
        {
            string winnerScore = i_Winner.Score.ToString();

            if (i_Winner.Color == GameLogic.ePlayerObjectsColor.White)
            {
                labelPlayer1Score.Text = winnerScore;
            }
            else
            {
                labelPlayer2Score.Text = winnerScore;
            }

            return MessageBox.Show(
                $"{i_Winner.Name} Won!{ Environment.NewLine}Another Round?",
                "Damka",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes ? true : false;
        }

        public void ResetFormForAnotherRound(GameLogic.BoardGame i_Board)
        {
            m_IsPlayer1Turn = true;
            m_IsNextMoveIsEating = false;

            for (int i = 0; i < i_Board.BoardSize; i++)
            {
                for (int j = 0; j < i_Board.BoardSize; j++)
                {
                    if (i_Board.Board[i, j] != GameLogic.eObjectType.Empty && buttons[i, j].Enabled)
                    {
                        if (i_Board.Board[i, j] == GameLogic.eObjectType.White)
                        {
                            buttons[i, j].AccessibleName = "O";
                            buttons[i, j].Image = Properties.Resources.White;
                        }
                        else
                        {
                            buttons[i, j].AccessibleName = "X";
                            buttons[i, j].Image = Properties.Resources.Black;
                        }
                    }
                    else if (i_Board.Board[i, j] == GameLogic.eObjectType.Empty)
                    {
                        buttons[i, j].Image = null;
                        buttons[i, j].AccessibleName = string.Empty;
                    }
                }
            }
        }
    }
}
