using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Casino
{
    public partial class Form3 : Form
    {

        string valip;
        string vfipbdsoft;
        string vfbdsoft;
        string vfusersoft;
        string vfclavesoft;
        int check;
        System.Data.SqlClient.SqlConnection conn;
        public int retcheck;
        //string path = @"c:\TotalPack\";
        string path = Application.StartupPath;


        public Form3()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conectarbd();
        }

        public static bool IsIPv4(string value)
        {
            var quads = value.Split('.');

            // if we do not have 4 quads, return false
            if (!(quads.Length == 4)) return false;

            // for each quad
            foreach (var quad in quads)
            {
                int q;
                // if parse fails 
                // or length of parsed int != length of quad string (i.e.; '1' vs '001')
                // or parsed int < 0
                // or parsed int > 255
                // return false
                if (!Int32.TryParse(quad, out q)
                    || !q.ToString().Length.Equals(quad.Length)
                    || q < 0
                    || q > 255) { return false; }
            }
            return true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = "Servidor";
            check = 0;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = "IP Servidor";
            check = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            retcheck = 0;
            this.Close();
        }

        public void creadirfile()
        {
            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(path))
                {
                    datos(Convert.ToString(check));
                    datos(vfipbdsoft);
                    datos(vfbdsoft);
                    datos(vfusersoft);
                    datos(vfclavesoft);
                }
                else
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(path);

                    datos(Convert.ToString(check));
                    datos(vfipbdsoft);
                    datos(vfbdsoft);
                    datos(vfusersoft);
                    datos(vfclavesoft);
                }
            }
            catch (Exception)
            {
                datos("Error");
            } 
        }


/*        public void eliminadirfile()
        {
            System.IO.Directory.Delete(path, true);
        }
*/
        public void datos(string text)
        {
            string archproclog = path + @"\casino.out";
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(archproclog, true))
            {
                file.WriteLine(text);
            }
        }

        public void conectarbd()
        {
            if (check == 0)
            {
                try
                {
                    vfipbdsoft = textBox1.Text;
                    vfbdsoft = textBox2.Text;
                    vfusersoft = textBox3.Text;
                    vfclavesoft = textBox4.Text;

                    creadirfile();

                    conn = new System.Data.SqlClient.SqlConnection();
                    conn.ConnectionString = "Server=" + vfipbdsoft + ";initial catalog=" + vfbdsoft + ";user=" + vfusersoft + ";password=" + vfclavesoft + ";Trusted_Connection=FALSE";
                    conn.Open();
                }
                catch (Exception)
                {
                    MessageBox.Show("No se pudo establecer conexión con la base de datos");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox1.Select();
                    conn.Close();
                }

                if (conn.State == ConnectionState.Open)
                {
                    this.Close();
                    retcheck = 1;
                }

            }

            if (check == 1)
            {
                try
                {
                    valip = textBox1.Text;
                    if (IsIPv4(valip) == true)
                    {
                        //string paso = "Paso OK";
                    }
                    else
                    {
                        MessageBox.Show("Debe ingresar una dirección IP Válida");
                    }

                    vfipbdsoft = textBox1.Text;
                    vfbdsoft = textBox2.Text;
                    vfusersoft = textBox3.Text;
                    vfclavesoft = textBox4.Text;

                    creadirfile();

                    conn = new System.Data.SqlClient.SqlConnection();
                    conn.ConnectionString = "Server=" + vfipbdsoft + ";initial catalog=" + vfbdsoft + ";user=" + vfusersoft + ";password=" + vfclavesoft + ";Trusted_Connection=FALSE";
                    conn.Open();

                }
                catch (Exception)
                {
                    MessageBox.Show("No se pudo establecer conexión con la base de datos");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox1.Select();
                }

                if (conn.State == ConnectionState.Open)
                {
                    this.Close();
                    retcheck = 1;
                }

            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                conectarbd();
            } 
        }

    }
}
