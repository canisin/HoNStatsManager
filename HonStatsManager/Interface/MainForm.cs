using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using HonStatsManager.Analysis;
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Logger.Initialize();
        }

        private void OnRunButtonClick(object sender, EventArgs e)
        {
            if (_isBusy)
                throw new InvalidOperationException();

            Run(Analyzer.Run);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (_isBusy)
                e.Cancel = true;
        }

        private void Run(Action action)
        {
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
            _runButton.Enabled = !_isBusy;
        }

        public void WriteLine(string line)
        {
            if (InvokeRequired)
            {
                Invoke((Action) (() => WriteLine(line)));
                return;
            }

            _Console.Text += line + Environment.NewLine;
            _Console.SelectionStart = _Console.Text.Length;
            _Console.ScrollToCaret();
            _Console.Refresh();
        }
    }
}