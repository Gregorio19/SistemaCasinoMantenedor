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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Globalization;

namespace Casino
{
    public partial class Form21 : Form
    {

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

        int valorturno;
        string valorselect;
        bool maquinaconip = false;
        public Form21()
        {
            InitializeComponent();
            cargadatosbd();
            f2conectarbd();
            cargaservicios();

            try
            {

                f2conectarbd();
                String validaservicio = "select ip from Machines";
                SqlCommand cmdvs = new SqlCommand(validaservicio, f2conn);
                SqlDataReader leevalidaservicio = cmdvs.ExecuteReader();
                string cadenamachine = "";
                if (leevalidaservicio.HasRows)
                {
                    while (leevalidaservicio.Read())
                    {
                        cadenamachine += leevalidaservicio.GetString(0) + "|";
                    }

                    string[] cadena = cadenamachine.Substring(0, cadenamachine.Length - 1).Split('|');

                    comboBox1.DataBindings.Clear();
                    comboBox1.Refresh();
                    comboBox1.Items.Clear();


                    foreach (var item in cadena)
                    {
                        comboBox1.Items.Add(item);
                    }
                    //MessageBox.Show("Lista de Servicio a Editar Cargada");
                    f2conn.Close();
                }
                else
                {
                    //MessageBox.Show("No Existe de Servicio a Editar Cargada");
                    f2conn.Close();
                }






            }
            catch (Exception ex)
            {
                string err = ex.Message;
                MessageBox.Show("Error al insertar registro: " + err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form21_Load(object sender, EventArgs e)
        {

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

        private void cargausuarios()
        {
            validachecked = 1;
            try
            {
                f2conectarbd();
                String consulta = "select distinct bu.userid, bu.badgenumber, bu.Name " +
                                   " from USERINFO bu";
                SqlCommand cmd = new SqlCommand(consulta, f2conn);
                SqlDataReader reader = cmd.ExecuteReader();

                //checkedListBox1.Items.Clear();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int bduserid = Convert.ToInt32(reader[0]);
                        string bdiduser = Convert.ToString(reader[1]);
                        string bdname = Convert.ToString(reader[2]);
                        MessageBox.Show("se cargo bien todo");

                        //checkedListBox1.Items.Add(bduserid + " - " + bdiduser + " - " + bdname, CheckState.Unchecked);
                    }
                    reader.Close();
                }
                else
                {
                    MessageBox.Show("No existen usuarios configurados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reader.Close();
                }
                f2conn.Close();
            }
            catch (Exception ex1)
            {
                DateTime dtex1 = DateTime.Now;
                log(dtex1 + ": " + ex1.Message);
                errorconnbd();
            }
        }

        private void cargaservicios()
        {
            validachecked = 1;
            try
            {
                f2conectarbd();

                String consultaser = "select distinct TimeZoneID, name from ACTimeZones";
                SqlCommand cmdser = new SqlCommand(consultaser, f2conn);
                SqlDataReader readerser = cmdser.ExecuteReader();

                if (readerser.HasRows)
                {
                    while (readerser.Read())
                    {
                        string idser = Convert.ToString(readerser[0]);
                        string nameser = Convert.ToString(readerser[1]);
                        //MessageBox.Show("se cargo bien todo");
                        //comboBox1.Items.Add(nameser);
                    }
                    readerser.Close();
                }
                else
                {
                    //MessageBox.Show("No existen Servicios Configurados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    readerser.Close();
                }
                f2conn.Close();
            }
            catch (Exception ex2)
            {
                DateTime dtex2 = DateTime.Now;
                log(dtex2 + ": " + ex2.Message);
                errorconnbd();
            }
        }

        private void errorconnbd()
        {
            MessageBox.Show("No se pudo establecer conexión con la base de datos.\rAplicación se cerrará.\rRevise su configuración de Base de Datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
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

       

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            f2conn.Close();
        }

      

        private void validaidservicio()
        {
            try
            {
                f2conectarbd();
                //valorselect = comboBox1.SelectedItem.ToString();
                String consultaselect = "select distinct timezoneid from ACTimeZones where name = '" + valorselect + "'";
                SqlCommand cmdselect = new SqlCommand(consultaselect, f2conn);
                SqlDataReader readerselect = cmdselect.ExecuteReader();

                if (readerselect.HasRows)
                {
                    readerselect.Read();
                    valorturno = Convert.ToInt32(readerselect[0]);
                    readerselect.Close();
                }
                f2conn.Close();
            }
            catch (Exception ex3)
            {
                DateTime dtex3 = DateTime.Now;
                log(dtex3 + ": " + ex3.Message);
                errorconnbd();
            }
        }

        
        public void log(string text)
        {
            string archproclog = path + @"\LogCasino.log";

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(archproclog, true))
            {
                file.WriteLine(text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cargadatosbd();
           // f2conectarbd();

            //string consultaasig = "insert into ASISTENCIA_RELOJ_IMP([ipreloj],[impresora]) values ('"+ textBox1.Text + "','" + textBox2.Text + "');";
            //SqlCommand cmdasig = new SqlCommand(consultaasig, f2conn);
            //cmdasig.ExecuteNonQuery();

            try
            {
                f2conectarbd();
                //valorselect = comboBox1.SelectedItem.ToString();
                string copyipreloj = textBox1.Text;
                string copyiprinter = textBox2.Text;
                string[] Ipreloj = copyipreloj.Split(' ');
                string[] Ipprinter = copyiprinter.Split(' ');
                if (Ipreloj.Length > 1 && Ipprinter.Length > 1)
                {
                    MessageBox.Show("Los campos de llenado no pueden contener espacios");
                }
                else 
                {
                    copyipreloj = textBox1.Text;
                    copyiprinter = textBox2.Text;
                    Ipreloj = copyipreloj.Split('.');
                    Ipprinter = copyiprinter.Split('.');
                    if (Ipreloj.Length != 4 && Ipprinter.Length != 4)
                    {
                        //MessageBox.Show("ipreloj: "+ Ipreloj.Length + " ip impresora: "+ Ipprinter.Length);
                        MessageBox.Show("Error en el seteo de la ip  \rPor Favor verifique nuevamente");
                    }
                    else
                    {
                        Ipprinter = Ipprinter[3].Split(':');
                        foreach (var word in Ipprinter)
                        {
                            //MessageBox.Show("palabra: "+ word+ "Ipprinter tamaño: "+ Ipprinter.Length);
                        }
                        if (Ipprinter.Length == 1 || Ipprinter.Length > 2 || Ipprinter[1] == "" || Ipprinter[1] == " ") 
                        {
                            copyiprinter = textBox2.Text;
                            Ipprinter = copyiprinter.Split(':');
                            copyiprinter = Ipprinter[0] + ":9100";
                            //MessageBox.Show("Datos " + copyiprinter);
                            string consultaasig;
                            if (maquinaconip == false)
                            {
                                 consultaasig = "insert into ASISTENCIA_RELOJ_IMP([ipreloj],[impresora]) values ('" + textBox1.Text + "','" + copyiprinter + "');";
                            }
                            else
                            {
                                consultaasig = "UPDATE   ASISTENCIA_RELOJ_IMP SET ipreloj = '" + textBox1.Text + "', impresora = '" + copyiprinter + "' where ipreloj = '" + textBox1.Text + "'";
                            }
                           
                            SqlCommand cmdasig = new SqlCommand(consultaasig, f2conn);
                            cmdasig.ExecuteNonQuery();
                            MessageBox.Show("Datos configurados correctamente");
                            f2conn.Close();
                        }

                        else
                        {
                            
                            string consultaasig;
                            if (maquinaconip == false)
                            {
                                consultaasig = "insert into ASISTENCIA_RELOJ_IMP([ipreloj],[impresora]) values ('" + textBox1.Text + "','" + textBox2.Text + "');";
                            }
                            else
                            {
                                consultaasig = "UPDATE   ASISTENCIA_RELOJ_IMP SET ipreloj = '" + textBox1.Text + "', impresora = '" + textBox2.Text + "' where ipreloj = '" + textBox1.Text + "'";
                            }
                            SqlCommand cmdasig = new SqlCommand(consultaasig, f2conn);
                            cmdasig.ExecuteNonQuery();
                            MessageBox.Show("Datos configurados correctamente");
                            f2conn.Close();
                        }
                    }
                }
                
            }
            catch (Exception ex3)
            {
                DateTime dtex3 = DateTime.Now;
                log(dtex3 + ": " + ex3.Message);
                errorconnbd();
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = comboBox1.SelectedIndex;
            string ip_select = "";
            if (idx != -1)
            {
                ip_select = comboBox1.SelectedItem.ToString();
                idx = -1;

                try
                {

                    f2conectarbd();
                    String validaservicio = "select mc.ip, asip.impresora from Machines mc, ASISTENCIA_RELOJ_IMP asip where '"+ ip_select + "' = asip.ipreloj";
                    SqlCommand cmdvs = new SqlCommand(validaservicio, f2conn);
                    SqlDataReader leevalidaservicio = cmdvs.ExecuteReader();
                    if (leevalidaservicio.HasRows)
                    {
                        leevalidaservicio.Read();
                        textBox1.Text = leevalidaservicio.GetString(0);
                        textBox2.Text = leevalidaservicio.GetString(1);
                        maquinaconip = true;
                    }
                    else
                    {
                        textBox1.Text = ip_select;
                        maquinaconip = false;
                    }
                   
                }
                catch (Exception ex)
                {
                    string err = ex.Message;
                    MessageBox.Show("Error al insertar registro: " + err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
