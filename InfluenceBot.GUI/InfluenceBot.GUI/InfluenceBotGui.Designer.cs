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
            this.btnEndTurn = new System.Windows.Forms.Button();
            this.tmrGame = new System.Windows.Forms.Timer(this.components);
            this.btnStartStopGame = new System.Windows.Forms.Button();
            this.numTimerInterval = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAdvanceOneTick = new System.Windows.Forms.Button();
            this.btnFullSpeedLearning = new System.Windows.Forms.Button();
            this.btnSaveNetworks = new System.Windows.Forms.Button();
            this.btnLoadNetworks = new System.Windows.Forms.Button();
            this.txtNetworkPath = new System.Windows.Forms.TextBox();
            this.btnRedrawEverything = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblEpisode = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbrEpsilon = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.picBoard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimerInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrEpsilon)).BeginInit();
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
            this.btnInitializeBoard.Location = new System.Drawing.Point(12, 389);
            this.btnInitializeBoard.Name = "btnInitializeBoard";
            this.btnInitializeBoard.Size = new System.Drawing.Size(143, 23);
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
            this.btnExtractAndPrint.Location = new System.Drawing.Point(12, 418);
            this.btnExtractAndPrint.Name = "btnExtractAndPrint";
            this.btnExtractAndPrint.Size = new System.Drawing.Size(143, 23);
            this.btnExtractAndPrint.TabIndex = 4;
            this.btnExtractAndPrint.Text = "Extract state and print";
            this.btnExtractAndPrint.UseVisualStyleBackColor = true;
            this.btnExtractAndPrint.Click += new System.EventHandler(this.btnExtractAndPrint_Click);
            // 
            // btnEndTurn
            // 
            this.btnEndTurn.Location = new System.Drawing.Point(12, 476);
            this.btnEndTurn.Name = "btnEndTurn";
            this.btnEndTurn.Size = new System.Drawing.Size(143, 23);
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
            this.btnStartStopGame.Location = new System.Drawing.Point(160, 447);
            this.btnStartStopGame.Name = "btnStartStopGame";
            this.btnStartStopGame.Size = new System.Drawing.Size(152, 23);
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
            this.numTimerInterval.Location = new System.Drawing.Point(255, 476);
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
            this.label2.Location = new System.Drawing.Point(179, 478);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Timer interval";
            // 
            // btnAdvanceOneTick
            // 
            this.btnAdvanceOneTick.Location = new System.Drawing.Point(12, 505);
            this.btnAdvanceOneTick.Name = "btnAdvanceOneTick";
            this.btnAdvanceOneTick.Size = new System.Drawing.Size(143, 23);
            this.btnAdvanceOneTick.TabIndex = 10;
            this.btnAdvanceOneTick.Text = "Advance one tick";
            this.btnAdvanceOneTick.UseVisualStyleBackColor = true;
            this.btnAdvanceOneTick.Click += new System.EventHandler(this.btnAdvanceOneTick_Click);
            // 
            // btnFullSpeedLearning
            // 
            this.btnFullSpeedLearning.Location = new System.Drawing.Point(12, 447);
            this.btnFullSpeedLearning.Name = "btnFullSpeedLearning";
            this.btnFullSpeedLearning.Size = new System.Drawing.Size(143, 23);
            this.btnFullSpeedLearning.TabIndex = 11;
            this.btnFullSpeedLearning.Text = "Full speed learning";
            this.btnFullSpeedLearning.UseVisualStyleBackColor = true;
            this.btnFullSpeedLearning.Click += new System.EventHandler(this.btnFullSpeedLearning_Click);
            // 
            // btnSaveNetworks
            // 
            this.btnSaveNetworks.Location = new System.Drawing.Point(12, 534);
            this.btnSaveNetworks.Name = "btnSaveNetworks";
            this.btnSaveNetworks.Size = new System.Drawing.Size(143, 23);
            this.btnSaveNetworks.TabIndex = 12;
            this.btnSaveNetworks.Text = "Save networks";
            this.btnSaveNetworks.UseVisualStyleBackColor = true;
            this.btnSaveNetworks.Click += new System.EventHandler(this.btnSaveNetworks_Click);
            // 
            // btnLoadNetworks
            // 
            this.btnLoadNetworks.Location = new System.Drawing.Point(161, 534);
            this.btnLoadNetworks.Name = "btnLoadNetworks";
            this.btnLoadNetworks.Size = new System.Drawing.Size(151, 23);
            this.btnLoadNetworks.TabIndex = 13;
            this.btnLoadNetworks.Text = "Load networks";
            this.btnLoadNetworks.UseVisualStyleBackColor = true;
            this.btnLoadNetworks.Click += new System.EventHandler(this.btnLoadNetworks_Click);
            // 
            // txtNetworkPath
            // 
            this.txtNetworkPath.Location = new System.Drawing.Point(12, 563);
            this.txtNetworkPath.Name = "txtNetworkPath";
            this.txtNetworkPath.Size = new System.Drawing.Size(300, 20);
            this.txtNetworkPath.TabIndex = 14;
            this.txtNetworkPath.Text = "G:\\Temp";
            // 
            // btnRedrawEverything
            // 
            this.btnRedrawEverything.Location = new System.Drawing.Point(160, 505);
            this.btnRedrawEverything.Name = "btnRedrawEverything";
            this.btnRedrawEverything.Size = new System.Drawing.Size(151, 23);
            this.btnRedrawEverything.TabIndex = 15;
            this.btnRedrawEverything.Text = "Redraw everything";
            this.btnRedrawEverything.UseVisualStyleBackColor = true;
            this.btnRedrawEverything.Click += new System.EventHandler(this.btnRedrawEverything_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 338);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Episode";
            // 
            // lblEpisode
            // 
            this.lblEpisode.AutoSize = true;
            this.lblEpisode.Location = new System.Drawing.Point(76, 338);
            this.lblEpisode.Name = "lblEpisode";
            this.lblEpisode.Size = new System.Drawing.Size(13, 13);
            this.lblEpisode.TabIndex = 17;
            this.lblEpisode.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(252, 364);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Exploration";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(159, 364);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Exploitation";
            // 
            // tbrEpsilon
            // 
            this.tbrEpsilon.Location = new System.Drawing.Point(162, 332);
            this.tbrEpsilon.Maximum = 100;
            this.tbrEpsilon.Name = "tbrEpsilon";
            this.tbrEpsilon.Size = new System.Drawing.Size(150, 45);
            this.tbrEpsilon.TabIndex = 20;
            this.tbrEpsilon.ValueChanged += new System.EventHandler(this.tbrEpsilon_ValueChanged);
            // 
            // InfluenceBotGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1154, 639);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblEpisode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnRedrawEverything);
            this.Controls.Add(this.txtNetworkPath);
            this.Controls.Add(this.btnLoadNetworks);
            this.Controls.Add(this.btnSaveNetworks);
            this.Controls.Add(this.btnFullSpeedLearning);
            this.Controls.Add(this.btnAdvanceOneTick);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numTimerInterval);
            this.Controls.Add(this.btnStartStopGame);
            this.Controls.Add(this.btnEndTurn);
            this.Controls.Add(this.btnExtractAndPrint);
            this.Controls.Add(this.txtStatistics);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnInitializeBoard);
            this.Controls.Add(this.picBoard);
            this.Controls.Add(this.tbrEpsilon);
            this.Name = "InfluenceBotGui";
            this.Text = "Influence Bot";
            this.Validated += new System.EventHandler(this.InfluenceBotGui_Validated);
            ((System.ComponentModel.ISupportInitialize)(this.picBoard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimerInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrEpsilon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBoard;
        private System.Windows.Forms.Button btnInitializeBoard;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStatistics;
        private System.Windows.Forms.Button btnExtractAndPrint;
        private System.Windows.Forms.Button btnEndTurn;
        private System.Windows.Forms.Timer tmrGame;
        private System.Windows.Forms.Button btnStartStopGame;
        private System.Windows.Forms.NumericUpDown numTimerInterval;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAdvanceOneTick;
        private System.Windows.Forms.Button btnFullSpeedLearning;
        private System.Windows.Forms.Button btnSaveNetworks;
        private System.Windows.Forms.Button btnLoadNetworks;
        private System.Windows.Forms.TextBox txtNetworkPath;
        private System.Windows.Forms.Button btnRedrawEverything;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblEpisode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar tbrEpsilon;
    }
}

