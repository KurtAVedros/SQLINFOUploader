using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
// -------------------------------------------------------
// Created by Kurt A. Vedros
// 4/7/2018
// Program created to shorten the time it takes to write the SQL program
// -------------------------------------------------------

namespace ISHS_SQL_Shortcut
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
            Application.Run(new Form1());
        }
    }
}
