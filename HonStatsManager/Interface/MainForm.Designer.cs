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
            this._console = new System.Windows.Forms.TextBox();
            this._runButton = new System.Windows.Forms.Button();
            this._menu = new System.Windows.Forms.MenuStrip();
            this._databasesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._matchesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._matchesReloadMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._matchesUpdateMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._matchesResetMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._heroesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._heroesUpdateMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // _console
            // 
            this._console.Location = new System.Drawing.Point(12, 56);
            this._console.Multiline = true;
            this._console.Name = "_console";
            this._console.ReadOnly = true;
            this._console.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._console.Size = new System.Drawing.Size(776, 382);
            this._console.TabIndex = 1;
            // 
            // _runButton
            // 
            this._runButton.Location = new System.Drawing.Point(12, 27);
            this._runButton.Name = "_runButton";
            this._runButton.Size = new System.Drawing.Size(75, 23);
            this._runButton.TabIndex = 0;
            this._runButton.Text = "Run";
            this._runButton.UseVisualStyleBackColor = true;
            this._runButton.Click += new System.EventHandler(this.OnRunButtonClick);
            // 
            // _menu
            // 
            this._menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._databasesMenu});
            this._menu.Location = new System.Drawing.Point(0, 0);
            this._menu.Name = "_menu";
            this._menu.Size = new System.Drawing.Size(800, 24);
            this._menu.TabIndex = 2;
            this._menu.Text = "_menu";
            // 
            // _databasesMenu
            // 
            this._databasesMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._matchesMenu,
            this._heroesMenu});
            this._databasesMenu.Name = "_databasesMenu";
            this._databasesMenu.Size = new System.Drawing.Size(72, 20);
            this._databasesMenu.Text = "Databases";
            // 
            // _matchesMenu
            // 
            this._matchesMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._matchesReloadMenu,
            this._matchesUpdateMenu,
            this._matchesResetMenu});
            this._matchesMenu.Name = "_matchesMenu";
            this._matchesMenu.Size = new System.Drawing.Size(180, 22);
            this._matchesMenu.Text = "Matches";
            // 
            // _matchesReloadMenu
            // 
            this._matchesReloadMenu.Name = "_matchesReloadMenu";
            this._matchesReloadMenu.Size = new System.Drawing.Size(180, 22);
            this._matchesReloadMenu.Text = "Reload";
            this._matchesReloadMenu.Click += new System.EventHandler(this.OnMatchesReloadMenuClick);
            // 
            // _matchesUpdateMenu
            // 
            this._matchesUpdateMenu.Name = "_matchesUpdateMenu";
            this._matchesUpdateMenu.Size = new System.Drawing.Size(180, 22);
            this._matchesUpdateMenu.Text = "Update";
            this._matchesUpdateMenu.Click += new System.EventHandler(this.OnMatchesUpdateMenuClick);
            // 
            // _matchesResetMenu
            // 
            this._matchesResetMenu.Name = "_matchesResetMenu";
            this._matchesResetMenu.Size = new System.Drawing.Size(180, 22);
            this._matchesResetMenu.Text = "Reset";
            this._matchesResetMenu.Click += new System.EventHandler(this.OnMatchesResetMenuClick);
            // 
            // _heroesMenu
            // 
            this._heroesMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._heroesUpdateMenu});
            this._heroesMenu.Name = "_heroesMenu";
            this._heroesMenu.Size = new System.Drawing.Size(180, 22);
            this._heroesMenu.Text = "Heroes";
            // 
            // _heroesUpdateMenu
            // 
            this._heroesUpdateMenu.Name = "_heroesUpdateMenu";
            this._heroesUpdateMenu.Size = new System.Drawing.Size(180, 22);
            this._heroesUpdateMenu.Text = "Update";
            this._heroesUpdateMenu.Click += new System.EventHandler(this.OnHeroesUpdateMenuClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._runButton);
            this.Controls.Add(this._console);
            this.Controls.Add(this._menu);
            this.MainMenuStrip = this._menu;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "HoNzor Stats";
            this._menu.ResumeLayout(false);
            this._menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button _runButton;
        private System.Windows.Forms.TextBox _console;
        private System.Windows.Forms.MenuStrip _menu;
        private System.Windows.Forms.ToolStripMenuItem _databasesMenu;
        private System.Windows.Forms.ToolStripMenuItem _matchesMenu;
        private System.Windows.Forms.ToolStripMenuItem _matchesReloadMenu;
        private System.Windows.Forms.ToolStripMenuItem _matchesUpdateMenu;
        private System.Windows.Forms.ToolStripMenuItem _matchesResetMenu;
        private System.Windows.Forms.ToolStripMenuItem _heroesMenu;
        private System.Windows.Forms.ToolStripMenuItem _heroesUpdateMenu;
    }
}