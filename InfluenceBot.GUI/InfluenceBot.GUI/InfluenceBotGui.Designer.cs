namespace InfluenceBot.GUI
{
    partial class InfluenceBotGui
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
            this.components = new System.ComponentModel.Container();
            this.picBoard = new System.Windows.Forms.PictureBox();
            this.btnInitializeBoard = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStatistics = new System.Windows.Forms.TextBox();
            this.btnExtractAndPrint = new System.Windows.Forms.Button();
            this.btnCurrentPlayerAttack = new System.Windows.Forms.Button();
            this.btnEndTurn = new System.Windows.Forms.Button();
            this.tmrGame = new System.Windows.Forms.Timer(this.components);
            this.btnStartStopGame = new System.Windows.Forms.Button();
            this.numTimerInterval = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAdvanceOneTick = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picBoard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimerInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // picBoard
            // 
            this.picBoard.Location = new System.Drawing.Point(12, 26);
            this.picBoard.Name = "picBoard";
            this.picBoard.Size = new System.Drawing.Size(300, 300);
            this.picBoard.TabIndex = 0;
            this.picBoard.TabStop = false;
            // 
            // btnInitializeBoard
            // 
            this.btnInitializeBoard.Location = new System.Drawing.Point(12, 332);
            this.btnInitializeBoard.Name = "btnInitializeBoard";
            this.btnInitializeBoard.Size = new System.Drawing.Size(300, 23);
            this.btnInitializeBoard.TabIndex = 1;
            this.btnInitializeBoard.Text = "Initialize board";
            this.btnInitializeBoard.UseVisualStyleBackColor = true;
            this.btnInitializeBoard.Click += new System.EventHandler(this.btnInitializeBoard_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(318, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Statistics";
            // 
            // txtStatistics
            // 
            this.txtStatistics.Location = new System.Drawing.Point(321, 26);
            this.txtStatistics.Multiline = true;
            this.txtStatistics.Name = "txtStatistics";
            this.txtStatistics.Size = new System.Drawing.Size(800, 557);
            this.txtStatistics.TabIndex = 3;
            // 
            // btnExtractAndPrint
            // 
            this.btnExtractAndPrint.Location = new System.Drawing.Point(12, 361);
            this.btnExtractAndPrint.Name = "btnExtractAndPrint";
            this.btnExtractAndPrint.Size = new System.Drawing.Size(300, 23);
            this.btnExtractAndPrint.TabIndex = 4;
            this.btnExtractAndPrint.Text = "Extract state and print";
            this.btnExtractAndPrint.UseVisualStyleBackColor = true;
            this.btnExtractAndPrint.Click += new System.EventHandler(this.btnExtractAndPrint_Click);
            // 
            // btnCurrentPlayerAttack
            // 
            this.btnCurrentPlayerAttack.Location = new System.Drawing.Point(12, 390);
            this.btnCurrentPlayerAttack.Name = "btnCurrentPlayerAttack";
            this.btnCurrentPlayerAttack.Size = new System.Drawing.Size(300, 23);
            this.btnCurrentPlayerAttack.TabIndex = 5;
            this.btnCurrentPlayerAttack.Text = "Current player attack";
            this.btnCurrentPlayerAttack.UseVisualStyleBackColor = true;
            this.btnCurrentPlayerAttack.Click += new System.EventHandler(this.btnCurrentPlayerAttack_Click);
            // 
            // btnEndTurn
            // 
            this.btnEndTurn.Location = new System.Drawing.Point(12, 419);
            this.btnEndTurn.Name = "btnEndTurn";
            this.btnEndTurn.Size = new System.Drawing.Size(300, 23);
            this.btnEndTurn.TabIndex = 6;
            this.btnEndTurn.Text = "End turn";
            this.btnEndTurn.UseVisualStyleBackColor = true;
            this.btnEndTurn.Click += new System.EventHandler(this.btnEndTurn_Click);
            // 
            // tmrGame
            // 
            this.tmrGame.Tick += new System.EventHandler(this.tmrGame_Tick);
            // 
            // btnStartStopGame
            // 
            this.btnStartStopGame.Location = new System.Drawing.Point(12, 448);
            this.btnStartStopGame.Name = "btnStartStopGame";
            this.btnStartStopGame.Size = new System.Drawing.Size(300, 23);
            this.btnStartStopGame.TabIndex = 7;
            this.btnStartStopGame.Text = "Start/Stop game";
            this.btnStartStopGame.UseVisualStyleBackColor = true;
            this.btnStartStopGame.Click += new System.EventHandler(this.btnStartStopGame_Click);
            // 
            // numTimerInterval
            // 
            this.numTimerInterval.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numTimerInterval.Location = new System.Drawing.Point(88, 517);
            this.numTimerInterval.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numTimerInterval.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numTimerInterval.Name = "numTimerInterval";
            this.numTimerInterval.Size = new System.Drawing.Size(57, 20);
            this.numTimerInterval.TabIndex = 8;
            this.numTimerInterval.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numTimerInterval.ValueChanged += new System.EventHandler(this.numTimerInterval_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 519);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Timer interval";
            // 
            // btnAdvanceOneTick
            // 
            this.btnAdvanceOneTick.Location = new System.Drawing.Point(12, 477);
            this.btnAdvanceOneTick.Name = "btnAdvanceOneTick";
            this.btnAdvanceOneTick.Size = new System.Drawing.Size(300, 23);
            this.btnAdvanceOneTick.TabIndex = 10;
            this.btnAdvanceOneTick.Text = "Advance one tick";
            this.btnAdvanceOneTick.UseVisualStyleBackColor = true;
            this.btnAdvanceOneTick.Click += new System.EventHandler(this.btnAdvanceOneTick_Click);
            // 
            // InfluenceBotGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1154, 608);
            this.Controls.Add(this.btnAdvanceOneTick);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numTimerInterval);
            this.Controls.Add(this.btnStartStopGame);
            this.Controls.Add(this.btnEndTurn);
            this.Controls.Add(this.btnCurrentPlayerAttack);
            this.Controls.Add(this.btnExtractAndPrint);
            this.Controls.Add(this.txtStatistics);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnInitializeBoard);
            this.Controls.Add(this.picBoard);
            this.Name = "InfluenceBotGui";
            this.Text = "Influence Bot";
            ((System.ComponentModel.ISupportInitialize)(this.picBoard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimerInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBoard;
        private System.Windows.Forms.Button btnInitializeBoard;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStatistics;
        private System.Windows.Forms.Button btnExtractAndPrint;
        private System.Windows.Forms.Button btnCurrentPlayerAttack;
        private System.Windows.Forms.Button btnEndTurn;
        private System.Windows.Forms.Timer tmrGame;
        private System.Windows.Forms.Button btnStartStopGame;
        private System.Windows.Forms.NumericUpDown numTimerInterval;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAdvanceOneTick;
    }
}

