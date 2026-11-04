using System;
using System.Windows.Forms;

namespace WinFormsApp1.CentroMedico.WinForms
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