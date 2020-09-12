using System;
using System.Windows.Forms;
using HonStatsManager.Interface;

namespace HonStatsManager
{
    public static class Program
    {
        internal static MainForm MainForm;

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(MainForm = new MainForm());
        }
    }
}