using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace Casino
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Generador_clave clave = new Generador_clave();
            string bb;
            bb = clave.generar_key();
            string serialout, path = Application.StartupPath;
            using (StreamReader Lee = new StreamReader(path + @"\casino.out"))
            {

                serialout = Lee.ReadLine();
                serialout = Lee.ReadLine();
                serialout = Lee.ReadLine();
                serialout = Lee.ReadLine();
                serialout = Lee.ReadLine();
                serialout = Lee.ReadLine();
            }
            if (serialout == bb)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else
            {
                MessageBox.Show("Este Computador no esta Licenciada por Totalpack --- Por favor contacte con Servicio Tecnico");
            }
            
        }

    }
}
