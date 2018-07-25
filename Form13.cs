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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Globalization;

namespace Casino
{
    public partial class Form13 : Form
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

        int valorturno;
        string valorselect;

        public Form13()
        {
            InitializeComponent();
            cargadatosbd();
            f2conectarbd();
            cargaservicios();
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

                checkedListBox1.Items.Clear();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int bduserid = Convert.ToInt32(reader[0]);
                        string bdiduser = Convert.ToString(reader[1]);
                        string bdname = Convert.ToString(reader[2]);

                        checkedListBox1.Items.Add(bduserid + " - " +bdiduser + " - " + bdname, CheckState.Unchecked);
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

                        comboBox1.Items.Add(nameser);
                    }
                    readerser.Close();
                }
                else
                {
                    MessageBox.Show("No existen Servicios Configurados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            f2conectarbd();
            cargausuarios();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            f2conectarbd();
            cargausuarios();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            f2conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)//Nothing selected
            {
                MessageBox.Show("Debes seleccionar un Servicio", "Error");
            }
            else
            {
                validaidservicio();
                f2conectarbd();

                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        try
                        {
                            string lineauser = checkedListBox1.Items[i].ToString();
                            string[] valiritem = lineauser.Split('-');
                            string insetasig = valiritem[0];
                            string asigvalor = insetasig.Trim();

                            //f2conectarbd();
                            string consultaasig = "delete casino_servicioasig where iduser = " + asigvalor + " and idservicio = " + valorturno;
                            SqlCommand cmdasig = new SqlCommand(consultaasig, f2conn);
                            cmdasig.ExecuteNonQuery();
                            //f2conn.Close();

                        }
                        catch(Exception err)
                        {
                            DateTime dterr = DateTime.Now;
                            log(dterr + ": Error al eliminar usuario de un servicio - " + err.Message);
                            f2conn.Close();
                        }

                    }
                }
                f2conn.Close();
                checkedListBox1.Items.Clear();
            }
            MessageBox.Show("Se eliminó servicio a usuarios seleccionados");

        }

        private void validaidservicio()
        {
            try
            {
                f2conectarbd();
                valorselect = comboBox1.SelectedItem.ToString();
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            validaidservicio();
            checkedListBox1.Items.Clear();
            try
            {
                f2conectarbd();
                String consultacombo = "select distinct bu.userid, bu.badgenumber, bu.Name " +
                                   " from USERINFO bu " +
                                   " where exists (select 1 " +
                                                 " from casino_servicioasig cs " +
                                                 " where bu.USERID = cs.iduser " +
                                                 " and cs.idservicio = " + valorturno + ")";
                SqlCommand cmdcombo = new SqlCommand(consultacombo, f2conn);
                SqlDataReader readercombo = cmdcombo.ExecuteReader();

                if (readercombo.HasRows)
                {
                    while (readercombo.Read())
                    {
                        int bduseridcombo = Convert.ToInt32(readercombo[0]);
                        string bdidusercombo = Convert.ToString(readercombo[1]);
                        string bdnamecombo = Convert.ToString(readercombo[2]);

                        checkedListBox1.Items.Add(bduseridcombo + " - " + bdidusercombo + " - " + bdnamecombo, CheckState.Unchecked);
                        //listBox1.Items.Add(bduseridcombo + " - " + bdidusercombo + " - " + bdnamecombo);
                    }
                    readercombo.Close();
                }
                else
                {
                    MessageBox.Show("No existen usuarios para servicio seleccionado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    readercombo.Close();
                }
                f2conn.Close();
            }
            catch (Exception ex5)
            {
                DateTime dtex5 = DateTime.Now;
                log(dtex5 + ": " + ex5.Message);
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

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
            radioButton1.Checked = false;
        }

        private void Form13_Load(object sender, EventArgs e)
        {

        }
    }
}
