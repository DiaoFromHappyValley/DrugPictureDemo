using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Demo.DisplayImages
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

           // new ImageLoader().Load("4");

            Application.Run(new Form1());
        }
    }
}
