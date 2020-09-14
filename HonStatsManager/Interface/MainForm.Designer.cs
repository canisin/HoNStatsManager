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
            this.components = new System.ComponentModel.Container();
            this._console = new System.Windows.Forms.TextBox();
            this._menu = new System.Windows.Forms.MenuStrip();
            this._databasesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._matchesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._matchesUpdateMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._matchesResetMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._heroesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._heroesUpdateMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._filtersGroup = new System.Windows.Forms.GroupBox();
            this._clearFiltersButton = new System.Windows.Forms.Button();
            this._filterDataMissingHerosButton = new System.Windows.Forms.CheckBox();
            this._filterDataIncompleteButton = new System.Windows.Forms.CheckBox();
            this._resetFiltersButton = new System.Windows.Forms.Button();
            this._filterDataDiscosButton = new System.Windows.Forms.CheckBox();
            this._filterDataKicksButton = new System.Windows.Forms.CheckBox();
            this._filterMapMidwarsButton = new System.Windows.Forms.CheckBox();
            this._filterMapCaldavarButton = new System.Windows.Forms.CheckBox();
            this._filterMatchTypeOtherButton = new System.Windows.Forms.CheckBox();
            this._filterMatchTypeThreeVsTwoButton = new System.Windows.Forms.CheckBox();
            this._filterMatchTypeTwoVsTwoButton = new System.Windows.Forms.CheckBox();
            this._filterMatchTypeTwoVsOneButton = new System.Windows.Forms.CheckBox();
            this._filterMatchTypeOneVsOneButton = new System.Windows.Forms.CheckBox();
            this._mapStatsButton = new System.Windows.Forms.Button();
            this._typeStatsButton = new System.Windows.Forms.Button();
            this._playerStatsButton = new System.Windows.Forms.Button();
            this._heroStatsButton = new HonStatsManager.Interface.MenuButton();
            this._heroStatsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._heroStatsMenuAllItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._gameNightStatsButton = new System.Windows.Forms.Button();
            this._menu.SuspendLayout();
            this._filtersGroup.SuspendLayout();
            this._heroStatsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // _console
            // 
            this._console.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._console.Location = new System.Drawing.Point(12, 200);
            this._console.Multiline = true;
            this._console.Name = "_console";
            this._console.ReadOnly = true;
            this._console.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._console.Size = new System.Drawing.Size(776, 460);
            this._console.TabIndex = 7;
            // 
            // _menu
            // 
            this._menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._databasesMenu});
            this._menu.Location = new System.Drawing.Point(0, 0);
            this._menu.Name = "_menu";
            this._menu.Size = new System.Drawing.Size(800, 24);
            this._menu.TabIndex = 0;
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
            this._matchesUpdateMenu,
            this._matchesResetMenu});
            this._matchesMenu.Name = "_matchesMenu";
            this._matchesMenu.Size = new System.Drawing.Size(119, 22);
            this._matchesMenu.Text = "Matches";
            // 
            // _matchesUpdateMenu
            // 
            this._matchesUpdateMenu.Name = "_matchesUpdateMenu";
            this._matchesUpdateMenu.Size = new System.Drawing.Size(112, 22);
            this._matchesUpdateMenu.Text = "Update";
            this._matchesUpdateMenu.Click += new System.EventHandler(this.OnMatchesUpdateMenuClick);
            // 
            // _matchesResetMenu
            // 
            this._matchesResetMenu.Name = "_matchesResetMenu";
            this._matchesResetMenu.Size = new System.Drawing.Size(112, 22);
            this._matchesResetMenu.Text = "Reset";
            this._matchesResetMenu.Click += new System.EventHandler(this.OnMatchesResetMenuClick);
            // 
            // _heroesMenu
            // 
            this._heroesMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._heroesUpdateMenu});
            this._heroesMenu.Name = "_heroesMenu";
            this._heroesMenu.Size = new System.Drawing.Size(119, 22);
            this._heroesMenu.Text = "Heroes";
            // 
            // _heroesUpdateMenu
            // 
            this._heroesUpdateMenu.Name = "_heroesUpdateMenu";
            this._heroesUpdateMenu.Size = new System.Drawing.Size(112, 22);
            this._heroesUpdateMenu.Text = "Update";
            this._heroesUpdateMenu.Click += new System.EventHandler(this.OnHeroesUpdateMenuClick);
            // 
            // _filtersGroup
            // 
            this._filtersGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._filtersGroup.Controls.Add(this._clearFiltersButton);
            this._filtersGroup.Controls.Add(this._filterDataMissingHerosButton);
            this._filtersGroup.Controls.Add(this._filterDataIncompleteButton);
            this._filtersGroup.Controls.Add(this._resetFiltersButton);
            this._filtersGroup.Controls.Add(this._filterDataDiscosButton);
            this._filtersGroup.Controls.Add(this._filterDataKicksButton);
            this._filtersGroup.Controls.Add(this._filterMapMidwarsButton);
            this._filtersGroup.Controls.Add(this._filterMapCaldavarButton);
            this._filtersGroup.Controls.Add(this._filterMatchTypeOtherButton);
            this._filtersGroup.Controls.Add(this._filterMatchTypeThreeVsTwoButton);
            this._filtersGroup.Controls.Add(this._filterMatchTypeTwoVsTwoButton);
            this._filtersGroup.Controls.Add(this._filterMatchTypeTwoVsOneButton);
            this._filtersGroup.Controls.Add(this._filterMatchTypeOneVsOneButton);
            this._filtersGroup.Location = new System.Drawing.Point(12, 27);
            this._filtersGroup.Name = "_filtersGroup";
            this._filtersGroup.Size = new System.Drawing.Size(776, 138);
            this._filtersGroup.TabIndex = 1;
            this._filtersGroup.TabStop = false;
            this._filtersGroup.Text = "Filters";
            // 
            // _clearFiltersButton
            // 
            this._clearFiltersButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._clearFiltersButton.Location = new System.Drawing.Point(695, 78);
            this._clearFiltersButton.Name = "_clearFiltersButton";
            this._clearFiltersButton.Size = new System.Drawing.Size(75, 23);
            this._clearFiltersButton.TabIndex = 11;
            this._clearFiltersButton.Text = "Clear";
            this._clearFiltersButton.UseVisualStyleBackColor = true;
            this._clearFiltersButton.Click += new System.EventHandler(this.OnClearFiltersButtonClick);
            // 
            // _filterDataMissingHerosButton
            // 
            this._filterDataMissingHerosButton.AutoSize = true;
            this._filterDataMissingHerosButton.Location = new System.Drawing.Point(308, 88);
            this._filterDataMissingHerosButton.Name = "_filterDataMissingHerosButton";
            this._filterDataMissingHerosButton.Size = new System.Drawing.Size(117, 17);
            this._filterDataMissingHerosButton.TabIndex = 10;
            this._filterDataMissingHerosButton.Text = "Filter Missing Heros";
            this._filterDataMissingHerosButton.UseVisualStyleBackColor = true;
            // 
            // _filterDataIncompleteButton
            // 
            this._filterDataIncompleteButton.AutoSize = true;
            this._filterDataIncompleteButton.Location = new System.Drawing.Point(308, 65);
            this._filterDataIncompleteButton.Name = "_filterDataIncompleteButton";
            this._filterDataIncompleteButton.Size = new System.Drawing.Size(103, 17);
            this._filterDataIncompleteButton.TabIndex = 9;
            this._filterDataIncompleteButton.Text = "Filter Incomplete";
            this._filterDataIncompleteButton.UseVisualStyleBackColor = true;
            // 
            // _resetFiltersButton
            // 
            this._resetFiltersButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._resetFiltersButton.Location = new System.Drawing.Point(695, 107);
            this._resetFiltersButton.Name = "_resetFiltersButton";
            this._resetFiltersButton.Size = new System.Drawing.Size(75, 23);
            this._resetFiltersButton.TabIndex = 12;
            this._resetFiltersButton.Text = "Reset";
            this._resetFiltersButton.UseVisualStyleBackColor = true;
            this._resetFiltersButton.Click += new System.EventHandler(this.OnResetFiltersButtonClick);
            // 
            // _filterDataDiscosButton
            // 
            this._filterDataDiscosButton.AutoSize = true;
            this._filterDataDiscosButton.Location = new System.Drawing.Point(308, 42);
            this._filterDataDiscosButton.Name = "_filterDataDiscosButton";
            this._filterDataDiscosButton.Size = new System.Drawing.Size(83, 17);
            this._filterDataDiscosButton.TabIndex = 8;
            this._filterDataDiscosButton.Text = "Filter Discos";
            this._filterDataDiscosButton.UseVisualStyleBackColor = true;
            // 
            // _filterDataKicksButton
            // 
            this._filterDataKicksButton.AutoSize = true;
            this._filterDataKicksButton.Location = new System.Drawing.Point(308, 19);
            this._filterDataKicksButton.Name = "_filterDataKicksButton";
            this._filterDataKicksButton.Size = new System.Drawing.Size(77, 17);
            this._filterDataKicksButton.TabIndex = 7;
            this._filterDataKicksButton.Text = "Filter Kicks";
            this._filterDataKicksButton.UseVisualStyleBackColor = true;
            // 
            // _filterMapMidwarsButton
            // 
            this._filterMapMidwarsButton.AutoSize = true;
            this._filterMapMidwarsButton.Location = new System.Drawing.Point(145, 42);
            this._filterMapMidwarsButton.Name = "_filterMapMidwarsButton";
            this._filterMapMidwarsButton.Size = new System.Drawing.Size(90, 17);
            this._filterMapMidwarsButton.TabIndex = 6;
            this._filterMapMidwarsButton.Text = "Filter Midwars";
            this._filterMapMidwarsButton.UseVisualStyleBackColor = true;
            // 
            // _filterMapCaldavarButton
            // 
            this._filterMapCaldavarButton.AutoSize = true;
            this._filterMapCaldavarButton.Location = new System.Drawing.Point(145, 19);
            this._filterMapCaldavarButton.Name = "_filterMapCaldavarButton";
            this._filterMapCaldavarButton.Size = new System.Drawing.Size(93, 17);
            this._filterMapCaldavarButton.TabIndex = 5;
            this._filterMapCaldavarButton.Text = "Filter Caldavar";
            this._filterMapCaldavarButton.UseVisualStyleBackColor = true;
            // 
            // _filterMatchTypeOtherButton
            // 
            this._filterMatchTypeOtherButton.AutoSize = true;
            this._filterMatchTypeOtherButton.Location = new System.Drawing.Point(6, 111);
            this._filterMatchTypeOtherButton.Name = "_filterMatchTypeOtherButton";
            this._filterMatchTypeOtherButton.Size = new System.Drawing.Size(77, 17);
            this._filterMatchTypeOtherButton.TabIndex = 4;
            this._filterMatchTypeOtherButton.Text = "Filter Other";
            this._filterMatchTypeOtherButton.UseVisualStyleBackColor = true;
            // 
            // _filterMatchTypeThreeVsTwoButton
            // 
            this._filterMatchTypeThreeVsTwoButton.AutoSize = true;
            this._filterMatchTypeThreeVsTwoButton.Location = new System.Drawing.Point(6, 88);
            this._filterMatchTypeThreeVsTwoButton.Name = "_filterMatchTypeThreeVsTwoButton";
            this._filterMatchTypeThreeVsTwoButton.Size = new System.Drawing.Size(69, 17);
            this._filterMatchTypeThreeVsTwoButton.TabIndex = 3;
            this._filterMatchTypeThreeVsTwoButton.Text = "Filter 3v2";
            this._filterMatchTypeThreeVsTwoButton.UseVisualStyleBackColor = true;
            // 
            // _filterMatchTypeTwoVsTwoButton
            // 
            this._filterMatchTypeTwoVsTwoButton.AutoSize = true;
            this._filterMatchTypeTwoVsTwoButton.Location = new System.Drawing.Point(6, 65);
            this._filterMatchTypeTwoVsTwoButton.Name = "_filterMatchTypeTwoVsTwoButton";
            this._filterMatchTypeTwoVsTwoButton.Size = new System.Drawing.Size(69, 17);
            this._filterMatchTypeTwoVsTwoButton.TabIndex = 2;
            this._filterMatchTypeTwoVsTwoButton.Text = "Filter 2v2";
            this._filterMatchTypeTwoVsTwoButton.UseVisualStyleBackColor = true;
            // 
            // _filterMatchTypeTwoVsOneButton
            // 
            this._filterMatchTypeTwoVsOneButton.AutoSize = true;
            this._filterMatchTypeTwoVsOneButton.Location = new System.Drawing.Point(6, 42);
            this._filterMatchTypeTwoVsOneButton.Name = "_filterMatchTypeTwoVsOneButton";
            this._filterMatchTypeTwoVsOneButton.Size = new System.Drawing.Size(69, 17);
            this._filterMatchTypeTwoVsOneButton.TabIndex = 1;
            this._filterMatchTypeTwoVsOneButton.Text = "Filter 2v1";
            this._filterMatchTypeTwoVsOneButton.UseVisualStyleBackColor = true;
            // 
            // _filterMatchTypeOneVsOneButton
            // 
            this._filterMatchTypeOneVsOneButton.AutoSize = true;
            this._filterMatchTypeOneVsOneButton.Location = new System.Drawing.Point(6, 19);
            this._filterMatchTypeOneVsOneButton.Name = "_filterMatchTypeOneVsOneButton";
            this._filterMatchTypeOneVsOneButton.Size = new System.Drawing.Size(69, 17);
            this._filterMatchTypeOneVsOneButton.TabIndex = 0;
            this._filterMatchTypeOneVsOneButton.Text = "Filter 1v1";
            this._filterMatchTypeOneVsOneButton.UseVisualStyleBackColor = true;
            // 
            // _mapStatsButton
            // 
            this._mapStatsButton.Location = new System.Drawing.Point(12, 171);
            this._mapStatsButton.Name = "_mapStatsButton";
            this._mapStatsButton.Size = new System.Drawing.Size(75, 23);
            this._mapStatsButton.TabIndex = 2;
            this._mapStatsButton.Text = "Map Stats";
            this._mapStatsButton.UseVisualStyleBackColor = true;
            this._mapStatsButton.Click += new System.EventHandler(this.OnMapStatsButtonClick);
            // 
            // _typeStatsButton
            // 
            this._typeStatsButton.Location = new System.Drawing.Point(93, 171);
            this._typeStatsButton.Name = "_typeStatsButton";
            this._typeStatsButton.Size = new System.Drawing.Size(75, 23);
            this._typeStatsButton.TabIndex = 3;
            this._typeStatsButton.Text = "Type Stats";
            this._typeStatsButton.UseVisualStyleBackColor = true;
            this._typeStatsButton.Click += new System.EventHandler(this.OnTypeStatsButtonClick);
            // 
            // _playerStatsButton
            // 
            this._playerStatsButton.Location = new System.Drawing.Point(174, 171);
            this._playerStatsButton.Name = "_playerStatsButton";
            this._playerStatsButton.Size = new System.Drawing.Size(75, 23);
            this._playerStatsButton.TabIndex = 4;
            this._playerStatsButton.Text = "Player Stats";
            this._playerStatsButton.UseVisualStyleBackColor = true;
            this._playerStatsButton.Click += new System.EventHandler(this.OnPlayerStatsButtonClick);
            // 
            // _heroStatsButton
            // 
            this._heroStatsButton.Location = new System.Drawing.Point(255, 171);
            this._heroStatsButton.Menu = this._heroStatsMenu;
            this._heroStatsButton.Name = "_heroStatsButton";
            this._heroStatsButton.Size = new System.Drawing.Size(75, 23);
            this._heroStatsButton.TabIndex = 5;
            this._heroStatsButton.Text = "Hero Stats";
            this._heroStatsButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._heroStatsButton.UseVisualStyleBackColor = true;
            // 
            // _heroStatsMenu
            // 
            this._heroStatsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._heroStatsMenuAllItem,
            this.toolStripSeparator1});
            this._heroStatsMenu.Name = "_heroStatsMenu";
            this._heroStatsMenu.Size = new System.Drawing.Size(89, 32);
            this._heroStatsMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.OnHeroStatsButtonItemClicked);
            // 
            // _heroStatsMenuAllItem
            // 
            this._heroStatsMenuAllItem.Name = "_heroStatsMenuAllItem";
            this._heroStatsMenuAllItem.Size = new System.Drawing.Size(88, 22);
            this._heroStatsMenuAllItem.Text = "All";
            this._heroStatsMenuAllItem.Click += new System.EventHandler(this.OnHeroStatsMenuAllClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(85, 6);
            // 
            // _gameNightStatsButton
            // 
            this._gameNightStatsButton.Location = new System.Drawing.Point(336, 171);
            this._gameNightStatsButton.Name = "_gameNightStatsButton";
            this._gameNightStatsButton.Size = new System.Drawing.Size(75, 23);
            this._gameNightStatsButton.TabIndex = 6;
            this._gameNightStatsButton.Text = "Game Night Stats";
            this._gameNightStatsButton.UseVisualStyleBackColor = true;
            this._gameNightStatsButton.Click += new System.EventHandler(this.OnGameNightStatsButtonClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 672);
            this.Controls.Add(this._gameNightStatsButton);
            this.Controls.Add(this._heroStatsButton);
            this.Controls.Add(this._playerStatsButton);
            this.Controls.Add(this._typeStatsButton);
            this.Controls.Add(this._filtersGroup);
            this.Controls.Add(this._mapStatsButton);
            this.Controls.Add(this._console);
            this.Controls.Add(this._menu);
            this.MainMenuStrip = this._menu;
            this.Name = "MainForm";
            this.Text = "HoNzor Stats";
            this._menu.ResumeLayout(false);
            this._menu.PerformLayout();
            this._filtersGroup.ResumeLayout(false);
            this._filtersGroup.PerformLayout();
            this._heroStatsMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox _console;
        private System.Windows.Forms.MenuStrip _menu;
        private System.Windows.Forms.ToolStripMenuItem _databasesMenu;
        private System.Windows.Forms.ToolStripMenuItem _matchesMenu;
        private System.Windows.Forms.ToolStripMenuItem _matchesUpdateMenu;
        private System.Windows.Forms.ToolStripMenuItem _matchesResetMenu;
        private System.Windows.Forms.ToolStripMenuItem _heroesMenu;
        private System.Windows.Forms.ToolStripMenuItem _heroesUpdateMenu;
        private System.Windows.Forms.GroupBox _filtersGroup;
        private System.Windows.Forms.CheckBox _filterMatchTypeOneVsOneButton;
        private System.Windows.Forms.CheckBox _filterMatchTypeTwoVsOneButton;
        private System.Windows.Forms.CheckBox _filterMatchTypeTwoVsTwoButton;
        private System.Windows.Forms.CheckBox _filterMatchTypeThreeVsTwoButton;
        private System.Windows.Forms.CheckBox _filterMatchTypeOtherButton;
        private System.Windows.Forms.CheckBox _filterMapCaldavarButton;
        private System.Windows.Forms.CheckBox _filterMapMidwarsButton;
        private System.Windows.Forms.CheckBox _filterDataKicksButton;
        private System.Windows.Forms.CheckBox _filterDataDiscosButton;
        private System.Windows.Forms.CheckBox _filterDataIncompleteButton;
        private System.Windows.Forms.CheckBox _filterDataMissingHerosButton;
        private System.Windows.Forms.Button _resetFiltersButton;
        private System.Windows.Forms.Button _mapStatsButton;
        private System.Windows.Forms.Button _typeStatsButton;
        private System.Windows.Forms.Button _playerStatsButton;
        private MenuButton _heroStatsButton;
        private System.Windows.Forms.Button _clearFiltersButton;
        private System.Windows.Forms.ContextMenuStrip _heroStatsMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem _heroStatsMenuAllItem;
        private System.Windows.Forms.Button _gameNightStatsButton;
    }
}