namespace UserInterface
{
    internal partial class FormGameSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelBoardSize = new System.Windows.Forms.Label();
            this.radioButton6x6 = new System.Windows.Forms.RadioButton();
            this.radioButton8x8 = new System.Windows.Forms.RadioButton();
            this.radioButton10x10 = new System.Windows.Forms.RadioButton();
            this.labelPlayers = new System.Windows.Forms.Label();
            this.labelPlayer1 = new System.Windows.Forms.Label();
            this.textBoxPlayer1 = new System.Windows.Forms.TextBox();
            this.textBoxPlayer2 = new System.Windows.Forms.TextBox();
            this.checkBoxPlayer2 = new System.Windows.Forms.CheckBox();
            this.buttonDone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelBoardSize
            // 
            this.labelBoardSize.AutoSize = true;
            this.labelBoardSize.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBoardSize.Location = new System.Drawing.Point(20, 20);
            this.labelBoardSize.Name = "labelBoardSize";
            this.labelBoardSize.Size = new System.Drawing.Size(97, 28);
            this.labelBoardSize.TabIndex = 0;
            this.labelBoardSize.Text = "Board Size:";
            this.labelBoardSize.UseCompatibleTextRendering = true;
            // 
            // radioButton6x6
            // 
            this.radioButton6x6.AutoSize = true;
            this.radioButton6x6.Checked = true;
            this.radioButton6x6.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton6x6.Location = new System.Drawing.Point(30, 50);
            this.radioButton6x6.Name = "radioButton6x6";
            this.radioButton6x6.Size = new System.Drawing.Size(70, 27);
            this.radioButton6x6.TabIndex = 1;
            this.radioButton6x6.TabStop = true;
            this.radioButton6x6.Text = "6 x 6";
            this.radioButton6x6.UseVisualStyleBackColor = true;
            this.radioButton6x6.Click += new System.EventHandler(this.radioButton6x6_Click);
            // 
            // radioButton8x8
            // 
            this.radioButton8x8.AutoSize = true;
            this.radioButton8x8.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton8x8.Location = new System.Drawing.Point(120, 50);
            this.radioButton8x8.Name = "radioButton8x8";
            this.radioButton8x8.Size = new System.Drawing.Size(70, 27);
            this.radioButton8x8.TabIndex = 2;
            this.radioButton8x8.Text = "8 x 8";
            this.radioButton8x8.UseVisualStyleBackColor = true;
            this.radioButton8x8.Click += new System.EventHandler(this.radioButton8x8_Click);
            // 
            // radioButton10x10
            // 
            this.radioButton10x10.AutoSize = true;
            this.radioButton10x10.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton10x10.Location = new System.Drawing.Point(210, 50);
            this.radioButton10x10.Name = "radioButton10x10";
            this.radioButton10x10.Size = new System.Drawing.Size(90, 27);
            this.radioButton10x10.TabIndex = 3;
            this.radioButton10x10.Text = "10 x 10";
            this.radioButton10x10.UseVisualStyleBackColor = true;
            this.radioButton10x10.Click += new System.EventHandler(this.radioButton10x10_Click);
            // 
            // labelPlayers
            // 
            this.labelPlayers.AutoSize = true;
            this.labelPlayers.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlayers.Location = new System.Drawing.Point(20, 90);
            this.labelPlayers.Name = "labelPlayers";
            this.labelPlayers.Size = new System.Drawing.Size(71, 23);
            this.labelPlayers.TabIndex = 4;
            this.labelPlayers.Text = "Players:";
            // 
            // labelPlayer1
            // 
            this.labelPlayer1.AutoSize = true;
            this.labelPlayer1.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlayer1.Location = new System.Drawing.Point(30, 130);
            this.labelPlayer1.Name = "labelPlayer1";
            this.labelPlayer1.Size = new System.Drawing.Size(78, 23);
            this.labelPlayer1.TabIndex = 5;
            this.labelPlayer1.Text = "Player 1:";
            // 
            // textBoxPlayer1
            // 
            this.textBoxPlayer1.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPlayer1.Location = new System.Drawing.Point(150, 127);
            this.textBoxPlayer1.Name = "textBoxPlayer1";
            this.textBoxPlayer1.Size = new System.Drawing.Size(160, 29);
            this.textBoxPlayer1.TabIndex = 6;
            this.textBoxPlayer1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPlayer1_KeyPress);
            // 
            // textBoxPlayer2
            // 
            this.textBoxPlayer2.Enabled = false;
            this.textBoxPlayer2.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPlayer2.Location = new System.Drawing.Point(150, 167);
            this.textBoxPlayer2.Name = "textBoxPlayer2";
            this.textBoxPlayer2.Size = new System.Drawing.Size(160, 29);
            this.textBoxPlayer2.TabIndex = 7;
            this.textBoxPlayer2.Text = "[Computer]";
            this.textBoxPlayer2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPlayer2_KeyPress);
            // 
            // checkBoxPlayer2
            // 
            this.checkBoxPlayer2.AutoSize = true;
            this.checkBoxPlayer2.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxPlayer2.Location = new System.Drawing.Point(30, 170);
            this.checkBoxPlayer2.Name = "checkBoxPlayer2";
            this.checkBoxPlayer2.Size = new System.Drawing.Size(100, 27);
            this.checkBoxPlayer2.TabIndex = 8;
            this.checkBoxPlayer2.Text = "Player 2:";
            this.checkBoxPlayer2.UseVisualStyleBackColor = true;
            this.checkBoxPlayer2.Click += new System.EventHandler(this.checkBoxPlayer2_Click);
            // 
            // buttonDone
            // 
            this.buttonDone.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDone.Location = new System.Drawing.Point(220, 221);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(90, 35);
            this.buttonDone.TabIndex = 9;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // formGameSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 268);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.checkBoxPlayer2);
            this.Controls.Add(this.textBoxPlayer2);
            this.Controls.Add(this.textBoxPlayer1);
            this.Controls.Add(this.labelPlayer1);
            this.Controls.Add(this.labelPlayers);
            this.Controls.Add(this.radioButton10x10);
            this.Controls.Add(this.radioButton8x8);
            this.Controls.Add(this.radioButton6x6);
            this.Controls.Add(this.labelBoardSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formGameSetting";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Game Setting";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelBoardSize;
        private System.Windows.Forms.RadioButton radioButton6x6;
        private System.Windows.Forms.RadioButton radioButton8x8;
        private System.Windows.Forms.RadioButton radioButton10x10;
        private System.Windows.Forms.Label labelPlayers;
        private System.Windows.Forms.Label labelPlayer1;
        private System.Windows.Forms.TextBox textBoxPlayer1;
        private System.Windows.Forms.TextBox textBoxPlayer2;
        private System.Windows.Forms.CheckBox checkBoxPlayer2;
        private System.Windows.Forms.Button buttonDone;
    }
}