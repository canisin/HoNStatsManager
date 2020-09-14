using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HonStatsManager.Analysis;
using HonStatsManager.Data;
using HonStatsManager.Utility;

namespace HonStatsManager.Interface
{
    internal partial class MainForm : Form
    {
        private bool _isBusy;

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            MinimumSize = Size;

            Logger.Initialize();
            HeroDb.Initialize();
            MatchDb.Initialize();

            ResetFilters();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (_isBusy)
                e.Cancel = true;
        }

        private void Run(Action action)
        {
            Logger.Log();
            Logger.Log();

            SetBusy(true);
            Task.Factory.StartNew(() =>
            {
                try
                {
                    action();
                }
                catch (Exception exception)
                {
                    Logger.Log($"{exception.GetType()} caught! (See log file for details.)");
                    Logger.LogDebug(exception);
                }
                finally
                {
                    SetBusy(false);
                }
            });
        }

        private void SetBusy(bool isBusy)
        {
            if (InvokeRequired)
            {
                Invoke((Action) (() => SetBusy(isBusy)));
                return;
            }

            if (_isBusy == isBusy)
                throw new InvalidOperationException();

            _isBusy = isBusy;

            _matchesUpdateMenu.Enabled = !_isBusy;
            _matchesResetMenu.Enabled = !_isBusy;
            _heroesUpdateMenu.Enabled = !_isBusy;

            foreach (var checkBox in _filtersGroup.Controls.OfType<CheckBox>())
                checkBox.Enabled = !_isBusy;

            _clearFiltersButton.Enabled = !_isBusy;
            _resetFiltersButton.Enabled = !_isBusy;

            _mapStatsButton.Enabled = !_isBusy;
            _typeStatsButton.Enabled = !_isBusy;
            _playerStatsButton.Enabled = !_isBusy;
            _heroStatsButton.Enabled = !_isBusy;
        }

        public void WriteLine(string line)
        {
            if (InvokeRequired)
            {
                Invoke((Action) (() => WriteLine(line)));
                return;
            }

            _console.AppendText(line + Environment.NewLine);
        }

        private void OnMatchesUpdateMenuClick(object sender, EventArgs e)
        {
            Run(MatchDb.Update);
        }

        private void OnMatchesResetMenuClick(object sender, EventArgs e)
        {
            Run(MatchDb.Reset);
        }

        private void OnHeroesUpdateMenuClick(object sender, EventArgs e)
        {
            Run(HeroDb.Update);
        }

        private void OnMapStatsButtonClick(object sender, EventArgs e)
        {
            Run(() => new Analyzer().PrintMapStats());
        }

        private void OnTypeStatsButtonClick(object sender, EventArgs e)
        {
            Run(() => new Analyzer().PrintMatchTypeStats());
        }

        private void OnPlayerStatsButtonClick(object sender, EventArgs e)
        {
            Run(() => new Analyzer().PrintPlayerStats());
        }

        private void OnHeroStatsButtonClick(object sender, EventArgs e)
        {
            Run(() => new Analyzer().PrintHeroStats());
        }

        private void OnClearFiltersButtonClick(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void OnResetFiltersButtonClick(object sender, EventArgs e)
        {
            ResetFilters();
        }

        private void ClearFilters()
        {
            foreach (var checkBox in _filtersGroup.Controls.OfType<CheckBox>())
                checkBox.Checked = false;
        }

        private void ResetFilters()
        {
            ClearFilters();

            _filterMatchTypeOneVsOneButton.Checked = true;
            _filterMatchTypeTwoVsOneButton.Checked = true;
            _filterMatchTypeOtherButton.Checked = true;
            _filterMapCaldavarButton.Checked = true;
            _filterDataKicksButton.Checked = true;
            _filterDataDiscosButton.Checked = true;
            _filterDataIncompleteButton.Checked = true;
            _filterDataMissingHerosButton.Checked = true;
        }

        public FilterType GetFilters()
        {
            var ret = FilterType.None;

            if (_filterMatchTypeOneVsOneButton.Checked)
                ret |= FilterType.MatchTypeOneVsOne;

            if (_filterMatchTypeTwoVsOneButton.Checked)
                ret |= FilterType.MatchTypeTwoVsOne;

            if (_filterMatchTypeTwoVsTwoButton.Checked)
                ret |= FilterType.MatchTypeTwoVsTwo;

            if (_filterMatchTypeThreeVsTwoButton.Checked)
                ret |= FilterType.MatchTypeThreeVsTwo;

            if (_filterMatchTypeOtherButton.Checked)
                ret |= FilterType.MatchTypeOther;

            if (_filterMapCaldavarButton.Checked)
                ret |= FilterType.MapCaldavar;

            if (_filterMapMidwarsButton.Checked)
                ret |= FilterType.MapMidwars;

            if (_filterDataKicksButton.Checked)
                ret |= FilterType.DataKicks;

            if (_filterDataDiscosButton.Checked)
                ret |= FilterType.DataDiscos;

            if (_filterDataIncompleteButton.Checked)
                ret |= FilterType.DataIncomplete;

            if (_filterDataMissingHerosButton.Checked)
                ret |= FilterType.DataMissingHeroes;

            return ret;
        }
    }
}