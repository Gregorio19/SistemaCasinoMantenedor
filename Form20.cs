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
using System.Globalization;
using System.Diagnostics;

namespace Casino
{
    public partial class Form20 : Form
    {
        string path = Application.StartupPath;
        string f7vfipbdsoft;
        string f7vfbdsoft;
        string f7vfusersoft;
        string f7vfclavesoft;
        int f7check;
        int codserv;
        int costoserv;
        int ultreg;
        int validanulo;
        string intserid;
        string intsernam;

        string valormodif;
        DateTime fecinival2;
        DateTime fecfinval2;

        String consulta12;

        int seleccionopcion;
        int idx;

        string[] compservicio;
        string idserv;
        string nomserv;
        int userid;
        string msgerror;
        int validainsupd;
        int validaemite;

        int continua;
        string[] armarut;
        string rssn;
        string Susuario = "Usuario";
        string rutFormateado;

        string rfecha;
        string rfechasp;
        string rhora;

        string SiYear;
        string SiMonth;
        string SiDay;
        string SiHour;
        string SiMinute;
        string SiSecond;

        string Snombre;
        string Sdescripcion;
        string Sdpto;

        string archservicio;

        int validaimp;

        int numregistroscasino;

        System.Data.SqlClient.SqlConnection f7conn;

        public Form20()
        {
            InitializeComponent();
        }

        private void errorconnbd()
        {
            MessageBox.Show("No se pudo establecer conexión con la base de datos.\rAplicación se cerrará.\rRevise su configuración de Base de Datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }

        private void cargadatosbd()
        {
            using (StreamReader Lee = new StreamReader(path + @"\casino.out"))
            {
                string Linea;
                Linea = Lee.ReadLine();
                f7check = Convert.ToInt32(Linea);

                Linea = Lee.ReadLine();
                f7vfipbdsoft = Linea;

                Linea = Lee.ReadLine();
                f7vfbdsoft = Linea;

                Linea = Lee.ReadLine();
                f7vfusersoft = Linea;

                Linea = Lee.ReadLine();
                f7vfclavesoft = Linea;
            }
        }

        private void Form20_Load(object sender, EventArgs e)
        {
            cargadatosbd();
            try
            {
                conectarbd();

                String consulta7 = "select TimeZoneID, Name servicio from ACTimeZones order by 1";
                SqlCommand cmd7 = new SqlCommand(consulta7, f7conn);
                SqlDataReader reader7 = cmd7.ExecuteReader();

                if (reader7.HasRows)
                {
                    while (reader7.Read())
                    {
                        intserid = Convert.ToString(reader7[0]);
                        intsernam = Convert.ToString(reader7[1]);
                        comboBox1.Items.Add(intserid + " - " + intsernam);
                    }
                }
                else
                {
                    MessageBox.Show("No existen servicios.\rDebe configurar los Servicios.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    reader7.Close();
                    //this.Close();
                }

            }
            catch (Exception)
            {
                errorconnbd();
            }
        }

        private void conectarbd()
        {
            if (f7check == 0)
            {
                try
                {
                    f7conn = new System.Data.SqlClient.SqlConnection();
                    f7conn.ConnectionString = "Server=" + f7vfipbdsoft + ";initial catalog=" + f7vfbdsoft + ";user=" + f7vfusersoft + ";password=" + f7vfclavesoft + ";Trusted_Connection=FALSE";
                    f7conn.Open();
                }
                catch (Exception)
                {
                    MessageBox.Show("No se pudo establecer conexión con la base de datos");
                    this.Close();
                }
            }

            if (f7check == 1)
            {
                try
                {
                    f7conn = new System.Data.SqlClient.SqlConnection();
                    f7conn.ConnectionString = "Server=" + f7vfipbdsoft + ";initial catalog=" + f7vfbdsoft + ";user=" + f7vfusersoft + ";password=" + f7vfclavesoft + ";Trusted_Connection=FALSE";
                    f7conn.Open();
                }
                catch (Exception)
                {
                    MessageBox.Show("No se pudo establecer conexión con la base de datos");
                    this.Close();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = comboBox1.SelectedIndex;

            if (idx != -1)
            {
                string nomcombo = comboBox1.SelectedItem.ToString();
                compservicio = nomcombo.Split();
                idserv = compservicio[0].ToString();
                nomserv = compservicio[2].ToString();
                textBox1.Select();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Para obligar a que sólo se introduzcan números 
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan 
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                textBox2.Select();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            validauserid();

            if (continua == 1)
            {
                //validainsertupdate();
                insertupdatevales();
                if (validaemite == 0)
                {
                    armamensaje();
                    imprimir();
                }
                continua = 0;
            }

            comboBox1.SelectedIndex = -1;
            textBox1.Text = "";
            textBox2.Text = "";

            if (validaimp == 1)
            {
                MessageBox.Show("Proceso Finalizado Correctamente");
                validaimp = 0;
            }
            else
            {
                MessageBox.Show("Existe un problema al finalizar proceso\rRevisar archivo Logs");
                validaimp = 0;
            }
        }

        private void insertupdatevales()
        {
            try
            {
                cargadatosbd();

                while (numregistroscasino < Convert.ToInt32(textBox2.Text))
                {
                    conectarbd();

                    DateTime insertfecha = DateTime.Now;
                    String consulta2 = "insert into casino(iduser, fecha, servicio, sn) values (" + userid + ",'" +
                        insertfecha + "'," + idserv + ", 'MANUAL')";
                    SqlCommand cmd2 = new SqlCommand(consulta2, f7conn);
                    cmd2.ExecuteNonQuery();
                    f7conn.Close();
                    numregistroscasino = numregistroscasino + 1;
                }
                MessageBox.Show("Vales para servicio: " + nomserv + " - configurados correctamente\rSe Imprimirá Vale");
            }
            catch (Exception ex)
            {
                msgerror = ex.Message;
                MessageBox.Show("Error en proceso hacia la base de datos\rRevisar archivo Log");
                admerrores();
                f7conn.Close();
                this.Close();
            }
        }

        private void validauserid()
        {
            cargadatosbd();
            conectarbd();

            try
            {
                String consulta = "select distinct usr.userid, usr.ssn, usr.name, dpto.DEPTNAME " +
                                  " from USERINFO usr, " +
                                  " DEPARTMENTS dpto " +
                                  " where ssn = '" + textBox1.Text + "'" +
                                  " and usr.DEFAULTDEPTID = dpto.DEPTID";
                SqlCommand cmd = new SqlCommand(consulta, f7conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        userid = Convert.ToInt32(reader[0]);
                        rssn = Convert.ToString(reader[1]);
                        Snombre = Convert.ToString(reader[2]);
                        Sdpto = Convert.ToString(reader[3]);
                    }

                    continua = 1;
                }
                else
                {
                    MessageBox.Show("No existe usuario con Rut: " + textBox1.Text, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reader.Close();
                    continua = 0;
                }
                reader.Close();
                f7conn.Close();
            }
            catch (Exception ex)
            {
                msgerror = ex.Message;
                MessageBox.Show("Error en proceso hacia la base de datos\rRevisar archivo Log");
                admerrores();
                f7conn.Close();
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

        private void imprimir()
        {
            try
            {
                ProcessStartInfo info = new ProcessStartInfo(archservicio);
                info.Verb = "Print";
                info.CreateNoWindow = false;
                info.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(info);
                validaimp = 1;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                validaimp = 0;
            }
        }

        private void armamensaje()
        {
            if (File.Exists(path + @"\" + Snombre + "_" + nomserv + ".txt"))
            {
                File.Delete(path + @"\" + Snombre + "_" + nomserv + ".txt");
            }

            formatearut();
            generafecha();
            string msgimprimepersonal1;

            msgimprimepersonal1 = "------------------------------------------------\r";
            msgimprimepersonal1 += "        " + Sdpto + "\r";
            msgimprimepersonal1 += "------------------------------------------------\r";
            msgimprimepersonal1 += "                                                \r";
            msgimprimepersonal1 += " " + Susuario + ": " + Snombre + "\r";
            msgimprimepersonal1 += " Rut: " + rutFormateado + "\r";
            msgimprimepersonal1 += " Fecha: " + rfecha + "\r";
            msgimprimepersonal1 += " Hora: " + rhora + "\r";
            msgimprimepersonal1 += " Servicio: " + nomserv + "\r";
            msgimprimepersonal1 += " # Vales Autorizados: " + textBox2.Text + "\r";
            msgimprimepersonal1 += "                                                \r";
            msgimprimepersonal1 += "                                                \r";
            msgimprimepersonal1 += "                                                \r";
            msgimprimepersonal1 += "  ______________________                        \r";
            msgimprimepersonal1 += "          FIRMA                                 \r";
            msgimprimepersonal1 += "------------------------------------------------\r";
            msgimprimepersonal1 += "                                                \r";
            msgimprimepersonal1 += "                                                \r";
            msgimprimepersonal1 += "                                                \r";


            archservicio = path + @"\" + Snombre + "_" + nomserv + ".txt";
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(archservicio, true))
            {
                file.WriteLine(msgimprimepersonal1);
            }
        }

        private void formatearut()
        {
            if (Susuario == "Usuario")
            {
                armarut = rssn.Split('-');
            }
            if (Susuario == "Visita")
            {
                //armarut = Srut_invitado.Split('-');
            }
            string NewString;
            NewString = armarut[0];
            NewString += armarut[1];
            rutFormateado = String.Empty;
            string rutTemporal = NewString.Substring(0, NewString.Length - 1);
            string dv = NewString.Substring(NewString.Length - 1, 1);
            Int64 rut;
            if (!Int64.TryParse(rutTemporal, out rut))
            {
                rut = 0;
            }
            rutFormateado = rut.ToString("N0");
            if (rutFormateado.Equals("0"))
            {
                rutFormateado = string.Empty;
            }
            else
            {
                rutFormateado += "-" + dv;
            }
        }

        private void generafecha()
        {
            DateTime fechaahora = DateTime.Now;
            string StartTime1 = fechaahora.ToString("dd-MM-yyyy");
            string StartTime2 = fechaahora.ToString("HH:mm:ss");
            string[] fec1 = StartTime1.Split('-');
            string[] fec2 = StartTime2.Split(':');

            SiDay = fec1[0];
            SiMonth = fec1[1];
            SiYear = fec1[2];
            SiHour = fec2[0];
            SiMinute = fec2[1];

            rfecha = SiDay + "-" + SiMonth + "-" + SiYear;
            rhora = SiHour + ":" + SiMinute;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                listBox1.Visible = false;
            }
            else
            {
                listBox1.Visible = true;

                cargadatosbd();
                conectarbd();
                listBox1.Items.Clear();
                try
                {
                    
                    String consulta = "select distinct bu.userid, bu.badgenumber, bu.Name, bu.SSN from USERINFO bu where bu.Name LIKE '%" + textBox1.Text + "%' OR bu.ssn LIKE '%" + textBox1.Text + "%'";
                    SqlCommand cmd = new SqlCommand(consulta, f7conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //MessageBox.Show("trae resultado ");
                            int bduserid = Convert.ToInt32(reader[0]);
                            string bdiduser = Convert.ToString(reader[1]);
                            string bdname = Convert.ToString(reader[2]);
                            string idusertextbox = Convert.ToString(reader[3]);

                            listBox1.Items.Add(bdname + "/" + idusertextbox);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No existe usuario con Rut: " + textBox1.Text, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        reader.Close();
                    }
                    reader.Close();
                    f7conn.Close();
                }
                catch (Exception ex)
                {
                    msgerror = ex.Message;
                    MessageBox.Show("Error en proceso hacia la base de datos\rRevisar archivo Log");
                    admerrores();
                    f7conn.Close();
                    this.Close();
                }

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(listBox1.SelectedItem.ToString());
            string[] palabras = listBox1.SelectedItem.ToString().Split('/');
            textBox1.Text = palabras[1];
            
            listBox1.Visible = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
