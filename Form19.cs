using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.Threading;

namespace Casino
{
    public partial class Form19 : Form
    {
        string Linea;
        string[] campo1;
        string[] campo2;
        string[] campo3;
        string[] campo4;
        string[] campo5;
        string[] campo6;
        string[] campo7;
        string[] campo8;
        string[] campo9;
        string[] campo10;
        string[] campo11;
        string[] campo12;
        string iptorn;
        string puertotorn;
        string msgbienvenida;
        string msgautoriza;
        string msgvencido;
        string msgnoexiste;
        string msgporvencer;
        string ipbd;
        string nombd;
        string userbd;
        string passbd;
        string displaymsg;
        string aperturapuerta;

        //string path = @"c:\TotalPack\";
        string path = Application.StartupPath;
        string f2vfipbdsoft;
        string f2vfbdsoft;
        string f2vfusersoft;
        string f2vfclavesoft;
        int f2check;

        System.Data.SqlClient.SqlConnection f2conn;

        public Form19()
        {
            InitializeComponent();
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
            }
        }

        private void f2conectarbd()
        {
            if (f2check == 0)
            {
                try
                {
                    f2conn = new System.Data.SqlClient.SqlConnection();
                    f2conn.ConnectionString = "Server=" + f2vfipbdsoft + ";initial catalog=" + f2vfbdsoft + ";user=" + f2vfusersoft + ";password=" + f2vfclavesoft + ";Trusted_Connection=FALSE";
                    f2conn.Open();
                }
                catch (Exception)
                {
                    errorconnbd();
                    this.Close();
                }
            }

            if (f2check == 1)
            {
                try
                {
                    f2conn = new System.Data.SqlClient.SqlConnection();
                    f2conn.ConnectionString = "Server=" + f2vfipbdsoft + ";initial catalog=" + f2vfbdsoft + ";user=" + f2vfusersoft + ";password=" + f2vfclavesoft + ";Trusted_Connection=FALSE";
                    f2conn.Open();

                }
                catch (Exception)
                {
                    errorconnbd();
                    this.Close();
                }
            }
        }

        private void errorconnbd()
        {
            MessageBox.Show("No se pudo establecer conexión con la base de datos.\rAplicación se cerrará.\rRevise su configuración de Base de Datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }

        private void validauser()
        {
            cargadatosbd();
            f2conectarbd();

            String consulta = "select distinct usuario, permisos " +
                              " from casino_accesoapp" +
                              " where usuario = '" + textBox1.Text + "'" +
                              " and contrasena = '" + textBox2.Text + "'";
            SqlCommand cmd = new SqlCommand(consulta, f2conn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                string bdusuario = reader.GetString(0);
                int bdperfil = reader.GetInt32(1);
                this.Hide();
                Form2 frm2 = new Form2(bdusuario, bdperfil);
                frm2.ShowDialog();
                f2conn.Close();
            }
            else
            {
                MessageBox.Show("Usuario/Contraseña Incorrectos");
                textBox2.Clear();
                textBox1.SelectAll();
                textBox1.Focus();
                f2conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            validauser();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                validauser();
            }
        }

        private void Form19_Load(object sender, EventArgs e)
        {

        }
    }
}
