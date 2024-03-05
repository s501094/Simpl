using System;
using System.Windows.Forms;

namespace Simpl2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Set the application to use Windows XP visual styles, text rendering, etc.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Create and run an instance of Form1.
            Application.Run(new Simpl());
        }
    }
}
