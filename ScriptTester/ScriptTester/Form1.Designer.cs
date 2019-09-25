namespace ScriptTester
{
    partial class Form1
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
            this.btnBrowse = new System.Windows.Forms.Button();
            this.textBoxInputUrl = new System.Windows.Forms.TextBox();
            this.labelInput = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.labelDate = new System.Windows.Forms.Label();
            this.timerElapsed = new System.Windows.Forms.Timer(this.components);
            this.labelTimerElapsed = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(127, 115);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(273, 54);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // textBoxInputUrl
            // 
            this.textBoxInputUrl.Location = new System.Drawing.Point(127, 71);
            this.textBoxInputUrl.Name = "textBoxInputUrl";
            this.textBoxInputUrl.Size = new System.Drawing.Size(661, 20);
            this.textBoxInputUrl.TabIndex = 1;
            this.textBoxInputUrl.Text = "https://www.24sata.hr";
            // 
            // labelInput
            // 
            this.labelInput.AutoSize = true;
            this.labelInput.Location = new System.Drawing.Point(124, 55);
            this.labelInput.Name = "labelInput";
            this.labelInput.Size = new System.Drawing.Size(29, 13);
            this.labelInput.TabIndex = 2;
            this.labelInput.Text = "URL";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Location = new System.Drawing.Point(12, 25);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(30, 13);
            this.labelDate.TabIndex = 3;
            this.labelDate.Text = "Date";
            // 
            // timerElapsed
            // 
            this.timerElapsed.Interval = 1000;
            this.timerElapsed.Tick += new System.EventHandler(this.timerElapsed_Tick);
            // 
            // labelTimerElapsed
            // 
            this.labelTimerElapsed.AutoSize = true;
            this.labelTimerElapsed.Location = new System.Drawing.Point(12, 136);
            this.labelTimerElapsed.Name = "labelTimerElapsed";
            this.labelTimerElapsed.Size = new System.Drawing.Size(33, 13);
            this.labelTimerElapsed.TabIndex = 4;
            this.labelTimerElapsed.Text = "Timer";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelTimerElapsed);
            this.Controls.Add(this.labelDate);
            this.Controls.Add(this.labelInput);
            this.Controls.Add(this.textBoxInputUrl);
            this.Controls.Add(this.btnBrowse);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox textBoxInputUrl;
        private System.Windows.Forms.Label labelInput;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Timer timerElapsed;
        private System.Windows.Forms.Label labelTimerElapsed;
    }
}

