using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace K0ntr0l
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Properties.Settings.Default.CheckForUpdates)
            {
                if (!Debugger.IsAttached)
                {
                    try
                    {
                        var versionChecker = new K0ntr0lVersionChecker();
                        if (versionChecker.UpdateRequired)
                        {
                            Application.Run(new FormNewerVersionAvailable());
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Failed to check for updates. Error : '{0}'", ex.Message);
                    }
                }
            }

            Application.Run(new Form1());
        }
    }
}
