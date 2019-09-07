namespace Tetris
{
    partial class TetrisApplication
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gamefieldPanel = new Tetris.TetrisApplication.MyPanel();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.descriptionLabel1 = new System.Windows.Forms.Label();
            this.currentScoreLabel = new System.Windows.Forms.Label();
            this.startGameButton = new System.Windows.Forms.Button();
            this.settingsButton = new System.Windows.Forms.Button();
            this.timerForTetris = new System.Windows.Forms.Timer(this.components);
            this.gamefieldPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // gamefieldPanel
            // 
            this.gamefieldPanel.BackColor = System.Drawing.SystemColors.Info;
            this.gamefieldPanel.Controls.Add(this.messageTextBox);
            this.gamefieldPanel.Location = new System.Drawing.Point(12, 32);
            this.gamefieldPanel.Name = "gamefieldPanel";
            this.gamefieldPanel.Size = new System.Drawing.Size(251, 501);
            this.gamefieldPanel.TabIndex = 0;
            // 
            // messageTextBox
            // 
            this.messageTextBox.Location = new System.Drawing.Point(76, 240);
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.ReadOnly = true;
            this.messageTextBox.Size = new System.Drawing.Size(100, 20);
            this.messageTextBox.TabIndex = 7;
            this.messageTextBox.Text = "Игра окончена";
            this.messageTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.messageTextBox.Visible = false;
            // 
            // descriptionLabel1
            // 
            this.descriptionLabel1.AutoSize = true;
            this.descriptionLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.descriptionLabel1.Location = new System.Drawing.Point(12, 10);
            this.descriptionLabel1.Name = "descriptionLabel1";
            this.descriptionLabel1.Size = new System.Drawing.Size(48, 16);
            this.descriptionLabel1.TabIndex = 1;
            this.descriptionLabel1.Text = "Счет:";
            this.descriptionLabel1.Visible = false;
            // 
            // currentScoreLabel
            // 
            this.currentScoreLabel.AutoSize = true;
            this.currentScoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.currentScoreLabel.Location = new System.Drawing.Point(57, 10);
            this.currentScoreLabel.Name = "currentScoreLabel";
            this.currentScoreLabel.Size = new System.Drawing.Size(16, 16);
            this.currentScoreLabel.TabIndex = 2;
            this.currentScoreLabel.Text = "0";
            this.currentScoreLabel.Visible = false;
            // 
            // startGameButton
            // 
            this.startGameButton.Location = new System.Drawing.Point(268, 32);
            this.startGameButton.Name = "startGameButton";
            this.startGameButton.Size = new System.Drawing.Size(107, 47);
            this.startGameButton.TabIndex = 3;
            this.startGameButton.Text = "Новая игра";
            this.startGameButton.UseVisualStyleBackColor = true;
            this.startGameButton.Click += new System.EventHandler(this.StartGameButton_Click);
            // 
            // settingsButton
            // 
            this.settingsButton.Location = new System.Drawing.Point(268, 85);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(107, 47);
            this.settingsButton.TabIndex = 4;
            this.settingsButton.Text = "Управление";
            this.settingsButton.UseVisualStyleBackColor = true;
            this.settingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // timerForTetris
            // 
            this.timerForTetris.Interval = 150;
            this.timerForTetris.Tick += new System.EventHandler(this.TimerForTetris_Tick);
            // 
            // TetrisApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 543);
            this.Controls.Add(this.settingsButton);
            this.Controls.Add(this.startGameButton);
            this.Controls.Add(this.currentScoreLabel);
            this.Controls.Add(this.descriptionLabel1);
            this.Controls.Add(this.gamefieldPanel);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "TetrisApplication";
            this.Text = "Тетрис";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TetrisApplication_KeyPress);
            this.gamefieldPanel.ResumeLayout(false);
            this.gamefieldPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label descriptionLabel1;
        private System.Windows.Forms.Label currentScoreLabel;
        private System.Windows.Forms.Button startGameButton;
        private System.Windows.Forms.Button settingsButton;
        private MyPanel gamefieldPanel;
        private System.Windows.Forms.Timer timerForTetris;
        private System.Windows.Forms.TextBox messageTextBox;
    }
}

