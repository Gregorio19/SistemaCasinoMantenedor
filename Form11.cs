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

namespace Casino
{
    public partial class Form11 : Form
    {
        //string path = @"c:\TotalPack\";
        string path = Application.StartupPath;
        string f2vfipbdsoft;
        string f2vfbdsoft;
        string f2vfusersoft;
        string f2vfclavesoft;
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
        string dgvcargo;
        string dgvnumvales;
        int filaseleccionada;

        string msgerror;

        public Form11()
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

        private void button3_Click(object sender, EventArgs e)
        {
            groupBox2.Hide();
            dataGridView1.Rows.Clear();
            cargadatosbd();
            f2conectarbd();

            try
            {
                String consulta = "select distinct ui.title, ui.userid, ui.ssn, ui.name " +
                                  " from USERINFO ui " +
                                  " where not exists (select 1 " +
                                  " from casino_valexusuarios cv " +
                                  " where cv.iduser = ui.userid)";
                SqlCommand cmd = new SqlCommand(consulta, f2conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string bdcargo = Convert.ToString(reader[0]);
                        int bdiduser = Convert.ToInt32(reader[1]);
                        string bdrut = Convert.ToString(reader[2]);
                        string bdnombre = Convert.ToString(reader[3]);
                        dataGridView1.Rows.Add(bdcargo, bdiduser, bdrut, bdnombre);
                    }
                }
                else
                {
                    MessageBox.Show("No existen usuarios configurados para Casino", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reader.Close();
                }
                reader.Close();
                f2conn.Close();
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

        private void Form11_Load(object sender, EventArgs e)
        {
            groupBox2.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Debe ingresar un valor a consultar");
                textBox1.Focus();
            }
            else
            {
                groupBox2.Hide();
                dataGridView1.Rows.Clear();
                int x;
                string pal2 = textBox1.Text;
                int y = 0;
                int z = 0;
                for (x = 0; x < pal2.Length; x++)
                {
                    if (pal2[x] >= '0' && pal2[x] <= '9')
                    {
                        y = 0;
                    }
                    else
                    {
                        z = 1;
                    }
                }

                if (y == 0 && z == 0)
                {
                    cargadatosbd();
                    f2conectarbd();

                    try
                    {
                        String consulta2 = "select distinct title, userid, ssn, name " +
                                            " from USERINFO" +
                                            " where userid = " + textBox1.Text;
                        SqlCommand cmd2 = new SqlCommand(consulta2, f2conn);
                        SqlDataReader reader2 = cmd2.ExecuteReader();

                        if (reader2.HasRows)
                        {
                            while (reader2.Read())
                            {
                                string bdcargo = Convert.ToString(reader2[0]);
                                int bdiduser = Convert.ToInt32(reader2[1]);
                                string bdrut = Convert.ToString(reader2[2]);
                                string bdnombre = Convert.ToString(reader2[3]);
                                dataGridView1.Rows.Add(bdcargo, bdiduser, bdrut, bdnombre);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No existen usuarios configurados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            reader2.Close();
                        }
                        reader2.Close();
                        f2conn.Close();
                        textBox1.Text = "";
                        textBox1.Focus();
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
                else
                {
                    MessageBox.Show("Sólo debe ingresar números");
                    textBox1.Text = "";
                    textBox1.Focus();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Debe ingresar un valor a consultar");
                textBox1.Focus();
            }
            else
            {
                groupBox2.Hide();
                dataGridView1.Rows.Clear();
                cargadatosbd();
                f2conectarbd();

                try
                {
                    String consulta2 = "select distinct title, userid, ssn, name " +
                                        " from USERINFO" +
                                        " where upper(title) like upper('%" + textBox1.Text + "%')";
                    SqlCommand cmd2 = new SqlCommand(consulta2, f2conn);
                    SqlDataReader reader2 = cmd2.ExecuteReader();

                    if (reader2.HasRows)
                    {
                        while (reader2.Read())
                        {
                            string bdcargo = Convert.ToString(reader2[0]);
                            int bdiduser = Convert.ToInt32(reader2[1]);
                            string bdrut = Convert.ToString(reader2[2]);
                            string bdnombre = Convert.ToString(reader2[3]);
                            dataGridView1.Rows.Add(bdcargo, bdiduser, bdrut, bdnombre);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No existen usuarios configurados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        reader2.Close();
                    }
                    reader2.Close();
                    f2conn.Close();

                    textBox1.Text = "";
                    textBox1.Focus();
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
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Debe ingresar un valor a consultar");
                textBox1.Focus();
            }
            else
            {
                groupBox2.Hide();
                dataGridView1.Rows.Clear();
                cargadatosbd();
                f2conectarbd();

                try
                {
                    String consulta2 = "select distinct title, userid, ssn, name " +
                                        " from USERINFO" +
                                        " where upper(name) like upper('%" + textBox1.Text + "%')";
                    SqlCommand cmd2 = new SqlCommand(consulta2, f2conn);
                    SqlDataReader reader2 = cmd2.ExecuteReader();

                    if (reader2.HasRows)
                    {
                        while (reader2.Read())
                        {
                            string bdcargo = Convert.ToString(reader2[0]);
                            int bdiduser = Convert.ToInt32(reader2[1]);
                            string bdrut = Convert.ToString(reader2[2]);
                            string bdnombre = Convert.ToString(reader2[3]);
                            dataGridView1.Rows.Add(bdcargo, bdiduser, bdrut, bdnombre);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No existen usuarios configurados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        reader2.Close();
                    }
                    reader2.Close();
                    f2conn.Close();

                    textBox1.Text = "";
                    textBox1.Focus();
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
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            groupBox2.Show();

            label2.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox2.Text = "0";
            dgvcargo = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            dgviduser = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            filaseleccionada = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            groupBox2.Hide();
            dataGridView1.Rows.Clear();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text) || Convert.ToInt32(textBox2.Text) <= 1)
            {
                MessageBox.Show("Debe ingresar un valor numérico superior a 1");
                textBox1.Focus();
            }
            else
            {
                cargadatosbd();
                f2conectarbd();

                try
                {
                    String consulta2 = "insert into casino_valexusuarios (cargo, iduser, numvales) " +
                                        " values (' " + dgvcargo + "', " + dgviduser + ", " + textBox2.Text + ")";
                    SqlCommand cmd2 = new SqlCommand(consulta2, f2conn);
                    cmd2.ExecuteNonQuery();
                    f2conn.Close();

                    //MessageBox.Show("Usuario y Asignación de Vales Configurado");
                    MessageBox.Show("Vales actualizados a: " + label2.Text + "\rActualmente tiene configurado: " + textBox2.Text + " vales");

                    groupBox2.Hide();

                    dataGridView1.Rows.Clear();
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

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
