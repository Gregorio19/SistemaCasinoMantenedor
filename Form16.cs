using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using System.IO;
using System.Globalization;
using System.Diagnostics;

namespace Casino
{
    public partial class Form16 : Form
    {

        //string path = @"c:\TotalPack\";
        string path = Application.StartupPath;
        string f2vfipbdsoft;
        string f2vfbdsoft;
        string f2vfusersoft;
        string f2vfclavesoft;
        string f2vfipreloj;
        string f2vfpuertoreloj;
        int f2check;
        int sale1;
        int sale2;
        int sale3;
        System.Data.SqlClient.SqlConnection f2conn;
        DateTime date1;
        DateTime date2;
        string fecini;
        string fecfin;
        int varcuserid;
        int varcdpto;
        int varcserv;
        int varcserv4;
        int pasouser;
        int validachecked = 0;
        int validachecked4 = 0;
        int validachecked1 = 0;
        int countfalla;
        int tiporeporte;

        string datodg1;
        string datodg2;
        string datodg3;
        string datodg4;
        string datodg5;

        int valorexport;
        string frm5folderName;
        string cortereporte;
        int cortereporte2 = 0;
        double suma;
        double sumatotal;

        int filaexcel;
        int colexcel;

        string[] servatt;
        string[] servconf;
        string shora;
        string dgviduser;
        int filaseleccionada;
        string dpto;

        string msgerror;

        public string ReturnDpto { get; set; }
        public int sindpto { get; set; }

        public Form16(string recibedpto)
        {
            dpto = recibedpto;
            InitializeComponent();
            listBox1.MouseDoubleClick += new MouseEventHandler(listBox1_DoubleClick);
        }

        private void listBox1_DoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBox1.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                ReturnDpto = listBox1.SelectedItem.ToString();
                this.Close();
            }
        }

        private void cargadatosbd()
        {
            using (StreamReader Lee = new StreamReader(path + @"\casino.out"))
            {
                string Linea;
                Linea = Lee.ReadLine();
                f2check = Convert.ToInt32(Linea);

                Linea = Lee.ReadLine();
                f2vfipbdsoft = Linea;

                Linea = Lee.ReadLine();
                f2vfbdsoft = Linea;

                Linea = Lee.ReadLine();
                f2vfusersoft = Linea;

                Linea = Lee.ReadLine();
                f2vfclavesoft = Linea;

                Linea = Lee.ReadLine();
                f2vfipreloj = Linea;

                Linea = Lee.ReadLine();
                f2vfpuertoreloj = Linea;
            }
        }

        private void errorconnbd()
        {
            MessageBox.Show("No se pudo establecer conexión con la base de datos.\rAplicación se cerrará.\rRevise su configuración de Base de Datos y Archivo Log.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }

        private void f2conectarbd()
        {
            try
            {
                f2conn = new System.Data.SqlClient.SqlConnection();
                f2conn.ConnectionString = "Server=" + f2vfipbdsoft + ";initial catalog=" + f2vfbdsoft + ";user=" + f2vfusersoft + ";password=" + f2vfclavesoft + ";Trusted_Connection=FALSE";
                f2conn.Open();
            }
            catch (Exception ex)
            {
                msgerror = ex.Message;
                admerrores();
                errorconnbd();
                this.Close();
            }
        }

        private void Form16_Load(object sender, EventArgs e)
        {
            sindpto = 0;
            for (int x = (listBox1.SelectedIndex - 1); x >= 0; x--)
            {
                listBox1.Items.RemoveAt(x);
            }
            listBox1.ClearSelected();

            cargadatosbd();
            f2conectarbd();

            try
            {
                String consdpto = "select DEPTID, DEPTNAME from DEPARTMENTS where DEPTNAME like '%" + dpto + "%' order by deptid asc";
                SqlCommand cmd = new SqlCommand(consdpto, f2conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string dptoid = Convert.ToString(reader[0]);
                        string dptoname = Convert.ToString(reader[1]);
                        listBox1.Items.Add(dptoid + " - " + dptoname);
                    }
                }
                else
                {
                    MessageBox.Show("No existen departamentos con texto ingresado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reader.Close();
                    sindpto = 1;
                }
                reader.Close();
                f2conn.Close();

                if (sindpto == 1)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                msgerror = ex.Message;
                MessageBox.Show("Error en proceso hacia la base de datos\rRevisar archivo Log");
                admerrores();
                f2conn.Close();
                this.Close();
            }
        }

        private void admerrores()
        {
            DateTime dterr = DateTime.Now;
            string msg = dterr + " -- " + msgerror;

            string archproclog = path + @"\RegistroErrores.log";
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(archproclog, true))
            {
                file.WriteLine(msg);
            }
        }
    }
}
