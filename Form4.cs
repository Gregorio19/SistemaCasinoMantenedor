using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using System.IO;

namespace Casino
{
    public partial class Form4 : Form
    {
        string f4vfipbdsoft;
        string f4vfbdsoft;
        string f4vfusersoft;
        string f4vfclavesoft;
        int f4check;
        int codserv;

        System.Data.SqlClient.SqlConnection f4conn;

        public Form4()
        {
            InitializeComponent();
        }

        private void errorconnbd()
        {
            MessageBox.Show("No se pudo establecer conexión con la base de datos.\rAplicación se cerrará.\rRevise su configuración de Base de Datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            checkBox1.Enabled = false;
            button1.Enabled = false;

            using (StreamReader Lee = new StreamReader("C:\\TotalPack\\casino.out"))
            {
                string Linea;
                Linea = Lee.ReadLine();
                f4check = Convert.ToInt32(Linea);

                Linea = Lee.ReadLine();
                f4vfipbdsoft = Linea;

                Linea = Lee.ReadLine();
                f4vfbdsoft = Linea;

                Linea = Lee.ReadLine();
                f4vfusersoft = Linea;

                Linea = Lee.ReadLine();
                f4vfclavesoft = Linea;
            }

            try
            {
                conectarbd();

                String consulta = "select cc.idcosto, act.Name from casino_costos cc, ACTimeZones act where cc.idcosto = act.TimeZoneID";
                SqlCommand cmd = new SqlCommand(consulta, f4conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string valid = Convert.ToString(reader[0]);
                        string valnam = Convert.ToString(reader[1]);
                        comboBox1.Items.Add(valid + " - " + valnam);
                    }
                }
                else
                {
                    MessageBox.Show("No existen servicios.\rDebe configurar Attendance.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    reader.Close();
                    this.Close();
                }
                reader.Close();
                f4conn.Close();
            }
            catch (Exception)
            {
                errorconnbd();
            }
        }

        private void conectarbd()
        {
            if (f4check == 0)
            {
                try
                {
                    f4conn = new System.Data.SqlClient.SqlConnection();
                    f4conn.ConnectionString = "Server=" + f4vfipbdsoft + ";initial catalog=" + f4vfbdsoft + ";user=" + f4vfusersoft + ";password=" + f4vfclavesoft + ";Trusted_Connection=FALSE";
                    f4conn.Open();
                }
                catch (Exception)
                {
                    MessageBox.Show("No se pudo establecer conexión con la base de datos");
                    this.Close();
                }
            }

            if (f4check == 1)
            {
                try
                {
                    f4conn = new System.Data.SqlClient.SqlConnection();
                    f4conn.ConnectionString = "Server=" + f4vfipbdsoft + ";initial catalog=" + f4vfbdsoft + ";user=" + f4vfusersoft + ";password=" + f4vfclavesoft + ";Trusted_Connection=FALSE";
                    f4conn.Open();

                }
                catch (Exception)
                {
                    MessageBox.Show("No se pudo establecer conexión con la base de datos");
                    this.Close();
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox1.Enabled = true;

            try
            {
                conectarbd();

                string comboselect = comboBox1.SelectedItem.ToString();
                codserv = Convert.ToInt32(comboselect.Substring(0, 1));

                String consulta2 = "select costoservicio from casino_costos where idcosto = " + codserv;
                SqlCommand cmd2 = new SqlCommand(consulta2, f4conn);
                SqlDataReader reader2 = cmd2.ExecuteReader();
                reader2.Read();

                int costoserv = Convert.ToInt32(reader2[0]);
                textBox1.Text = Convert.ToString(costoserv);
                reader2.Close();
                f4conn.Close();
            }
            catch (Exception)
            {
                errorconnbd();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.ReadOnly = false;
                button1.Enabled = true;
            }
            if (checkBox1.Checked == false)
            {
                textBox1.ReadOnly = true;
                button1.Enabled = false;
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            int x;
            string pal2 = textBox1.Text;
            for (x = 0; x < pal2.Length; x++)
            {
                if (pal2[x] >= '0' && pal2[x] <= '9')
                {
                }
                else
                {
                    MessageBox.Show("Sólo debe ingresar números");
                    textBox1.Text = "";
                    textBox1.Focus();
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            conectarbd();

            try
            {
                String update = "update casino_costos set costoservicio = " + textBox1.Text + "where idcosto = " + codserv;
                SqlCommand cmd3 = new SqlCommand(update, f4conn);
                cmd3.ExecuteNonQuery();

                MessageBox.Show("Registro actualizado exitosamente");
                textBox1.ReadOnly = true;
                checkBox1.Checked = false;
                button1.Enabled = false;

            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo actualizar registro");
                textBox1.ReadOnly = true;
                checkBox1.Checked = false;
            }

        }


    }
}
