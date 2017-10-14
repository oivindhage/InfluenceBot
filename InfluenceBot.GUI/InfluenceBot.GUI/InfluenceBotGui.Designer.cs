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
            this.picBoard = new System.Windows.Forms.PictureBox();
            this.btnInitializeBoard = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStatistics = new System.Windows.Forms.TextBox();
            this.btnExtractAndPrint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picBoard)).BeginInit();
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
            this.btnInitializeBoard.Location = new System.Drawing.Point(318, 26);
            this.btnInitializeBoard.Name = "btnInitializeBoard";
            this.btnInitializeBoard.Size = new System.Drawing.Size(211, 23);
            this.btnInitializeBoard.TabIndex = 1;
            this.btnInitializeBoard.Text = "Initialize board";
            this.btnInitializeBoard.UseVisualStyleBackColor = true;
            this.btnInitializeBoard.Click += new System.EventHandler(this.btnInitializeBoard_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 329);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Statistics";
            // 
            // txtStatistics
            // 
            this.txtStatistics.Location = new System.Drawing.Point(12, 345);
            this.txtStatistics.Multiline = true;
            this.txtStatistics.Name = "txtStatistics";
            this.txtStatistics.Size = new System.Drawing.Size(517, 212);
            this.txtStatistics.TabIndex = 3;
            // 
            // btnExtractAndPrint
            // 
            this.btnExtractAndPrint.Location = new System.Drawing.Point(318, 55);
            this.btnExtractAndPrint.Name = "btnExtractAndPrint";
            this.btnExtractAndPrint.Size = new System.Drawing.Size(211, 23);
            this.btnExtractAndPrint.TabIndex = 4;
            this.btnExtractAndPrint.Text = "Extract state and print";
            this.btnExtractAndPrint.UseVisualStyleBackColor = true;
            this.btnExtractAndPrint.Click += new System.EventHandler(this.btnExtractAndPrint_Click);
            // 
            // InfluenceBotGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 588);
            this.Controls.Add(this.btnExtractAndPrint);
            this.Controls.Add(this.txtStatistics);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnInitializeBoard);
            this.Controls.Add(this.picBoard);
            this.Name = "InfluenceBotGui";
            this.Text = "Influence Bot";
            ((System.ComponentModel.ISupportInitialize)(this.picBoard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBoard;
        private System.Windows.Forms.Button btnInitializeBoard;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStatistics;
        private System.Windows.Forms.Button btnExtractAndPrint;
    }
}

