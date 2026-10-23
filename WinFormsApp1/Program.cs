using System;
using System.Windows.Forms;
using WinFormsApp1;

namespace WinFormsApp1
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}