namespace HonStatsManager.Interface
{
    partial class MainForm
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
            this._Console = new System.Windows.Forms.TextBox();
            this._runButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _Console
            // 
            this._Console.Location = new System.Drawing.Point(12, 41);
            this._Console.Multiline = true;
            this._Console.Name = "_Console";
            this._Console.ReadOnly = true;
            this._Console.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._Console.Size = new System.Drawing.Size(776, 397);
            this._Console.TabIndex = 1;
            // 
            // _runButton
            // 
            this._runButton.Location = new System.Drawing.Point(12, 12);
            this._runButton.Name = "_runButton";
            this._runButton.Size = new System.Drawing.Size(75, 23);
            this._runButton.TabIndex = 0;
            this._runButton.Text = "Run";
            this._runButton.UseVisualStyleBackColor = true;
            this._runButton.Click += new System.EventHandler(this.OnRunButtonClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._runButton);
            this.Controls.Add(this._Console);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "HoNzor Stats";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button _runButton;
        private System.Windows.Forms.TextBox _Console;
    }
}