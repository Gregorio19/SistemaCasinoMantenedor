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

namespace Casino
{
    public partial class Form9 : Form
    {
        string path = Application.StartupPath;
        string f9vfipbdsoft;
        string f9vfbdsoft;
        string f9vfusersoft;
        string f9vfclavesoft;
        int f9check;
        int codserv;
        int costoserv;
        int ultreg;
        int validanulo;
        string intserid;
        string intsernam;

        string valormodif;

        string Servicio_selc;
        DateTime fecinival2;
        DateTime fecfinval2;

        String consulta12;

        System.Data.SqlClient.SqlConnection f9conn;

        //Variables para insert servicio
        int VTimeZoneID;
        string VName;
        DateTime VSunStart;
        DateTime VSunEnd;
        DateTime VMonStart;
        DateTime VMonEnd;
        DateTime VTuesStart;
        DateTime VTuesEnd;
        DateTime VWedStart;
        DateTime VWedEnd;
        DateTime VThursStart;
        DateTime VThursEnd;
        DateTime VFriStart;
        DateTime VFriEnd;
        DateTime VSatStart;
        DateTime VSatEnd;

        string SVSunStart;
        string SVSunEnd;
        string SVMonStart;
        string SVMonEnd;
        string SVTuesStart;
        string SVTuesEnd;
        string SVWedStart;
        string SVWedEnd;
        string SVThursStart;
        string SVThursEnd;
        string SVFriStart;
        string SVFriEnd;
        string SVSatStart;
        string SVSatEnd;

        string servicio_select;
        DateTime lunes;
        DateTime lunesfin;
        DateTime martes;
        DateTime martesfin;
        DateTime miercoles;
        DateTime miercolesfin;
        DateTime jueves;
        DateTime juevesfin;
        DateTime viernes;
        DateTime viernesfin;
        DateTime sabado;
        DateTime sabadofin;
        DateTime domingo;
        DateTime domingofin;

        int valcheckdias;

        public string cadenaAlmuerzo;
        //public string [] cadenaAlmuerzo1;

        public Form9()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Debe ingresar Servicio", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Select();
            }
            else
            {
                validaselectdias();
                try
                {
                    VName = textBox1.Text;

                    conectarbd();
                    String validaservicio = "select 1 from ACTimeZones where name = '" + VName + "'";
                    SqlCommand cmdvs = new SqlCommand(validaservicio, f9conn);
                    SqlDataReader leevalidaservicio = cmdvs.ExecuteReader();
                    leevalidaservicio.Read();

                    if (leevalidaservicio.HasRows)
                    {
                        MessageBox.Show("Nombre de Servicio Ingresado ya existe");
                        f9conn.Close();
                    }
                    else
                    {
                        if (valcheckdias != 0)
                        {
                            conectarbd();
                            String consulta12 = "select max(timezoneid) from ACTimeZones";
                            SqlCommand cmd12 = new SqlCommand(consulta12, f9conn);
                            SqlDataReader leeultservicio = cmd12.ExecuteReader();
                            leeultservicio.Read();

                            if (leeultservicio.IsDBNull(0))
                            {
                                VTimeZoneID = 1;
                                f9conn.Close();
                            }
                            else
                            {
                                VTimeZoneID = Convert.ToInt32(leeultservicio[0]) + 1;
                                f9conn.Close();
                            }
                            //MessageBox.Show("Sin error antes de validar");
                            validadias();
                            // MessageBox.Show("Sin error despues de validar");
                            //MessageBox.Show("Sin error despues de validar" + SVSatStart.ToString());
                            conectarbd();
                            String consulta13 = "INSERT INTO ACTimeZones (TimeZoneID,Name,SunStart,SunEnd,MonStart,MonEnd,TuesStart,TuesEnd,WedStart,WedEnd,ThursStart,ThursEnd,FriStart,FriEnd,SatStart,SatEnd) " +
                                                "VALUES (" + VTimeZoneID + ",'" +
                                                VName + "','" + SVSunStart + "','" + SVSunEnd + "','" +
                                                SVMonStart + "','" + SVMonEnd + "','" +
                                                SVTuesStart + "','" + SVTuesEnd + "','" +
                                                SVWedStart + "','" + SVWedEnd + "','" +
                                                SVThursStart + "','" + SVThursEnd + "','" +
                                                SVFriStart + "','" + SVFriEnd + "','" +
                                                SVSatStart + "','" + SVSatEnd + "')";

                            SqlCommand cmd13 = new SqlCommand(consulta13, f9conn);
                            cmd13.ExecuteNonQuery();
                            f9conn.Close();
                            MessageBox.Show("Configuración de servicio registrado exitosamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            limpiaventana();
                            valcheckdias = 0;
                        }
                        else
                        {
                            MessageBox.Show("Debe al menos seleccionar un día para configurar turno");
                            valcheckdias = 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    string err = ex.Message;
                    MessageBox.Show("Error al insertar registro: " + err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void conectarbd()
        {
            if (f9check == 0)
            {
                try
                {
                    f9conn = new System.Data.SqlClient.SqlConnection();
                    f9conn.ConnectionString = "Server=" + f9vfipbdsoft + ";initial catalog=" + f9vfbdsoft + ";user=" + f9vfusersoft + ";password=" + f9vfclavesoft + ";Trusted_Connection=FALSE";
                    f9conn.Open();
                }
                catch (Exception)
                {
                    MessageBox.Show("No se pudo establecer conexión con la base de datos");
                    this.Close();
                }
            }

            if (f9check == 1)
            {
                try
                {
                    f9conn = new System.Data.SqlClient.SqlConnection();
                    f9conn.ConnectionString = "Server=" + f9vfipbdsoft + ";initial catalog=" + f9vfbdsoft + ";user=" + f9vfusersoft + ";password=" + f9vfclavesoft + ";Trusted_Connection=FALSE";
                    f9conn.Open();
                }
                catch (Exception)
                {
                    MessageBox.Show("No se pudo establecer conexión con la base de datos");
                    this.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            using (StreamReader Lee = new StreamReader(path + @"\casino.out"))
            {
                string Linea;
                Linea = Lee.ReadLine();
                f9check = Convert.ToInt32(Linea);

                Linea = Lee.ReadLine();
                f9vfipbdsoft = Linea;

                Linea = Lee.ReadLine();
                f9vfbdsoft = Linea;

                Linea = Lee.ReadLine();
                f9vfusersoft = Linea;

                Linea = Lee.ReadLine();
                f9vfclavesoft = Linea;
            }
            dateTimePicker2.Value = dateTimePicker2.Value.AddHours(1);
        }

        private void validadias()
        {
            if (checkBox1.Checked == true)
            {
                VMonStart = Convert.ToDateTime(dateTimePicker1.Value);
                VMonEnd = Convert.ToDateTime(dateTimePicker2.Value);
                SVMonStart = VMonStart.ToString("yyyy-dd-MM HH:mm:ss");
                SVMonEnd = VMonEnd.ToString("yyyy-dd-MM HH:mm:ss");
            }
            else
            {
                VMonStart = Convert.ToDateTime("2016-01-01 00:00:00");
                VMonEnd = Convert.ToDateTime("2016-01-01 00:00:00");
                SVMonStart = VMonStart.ToString("yyyy-dd-MM HH:mm:ss");
                SVMonEnd = VMonEnd.ToString("yyyy-dd-MM HH:mm:ss");
            }

            if (checkBox2.Checked == true)
            {
                VTuesStart = Convert.ToDateTime(dateTimePicker1.Value);
                VTuesEnd = Convert.ToDateTime(dateTimePicker2.Value);
                SVTuesStart = VTuesStart.ToString("yyyy-dd-MM HH:mm:ss");
                SVTuesEnd = VTuesEnd.ToString("yyyy-dd-MM HH:mm:ss");
            }
            else
            {
                VTuesStart = Convert.ToDateTime("2016-01-01 00:00:00");
                VTuesEnd = Convert.ToDateTime("2016-01-01 00:00:00");
                SVTuesStart = VTuesStart.ToString("yyyy-dd-MM HH:mm:ss");
                SVTuesEnd = VTuesEnd.ToString("yyyy-dd-MM HH:mm:ss");
            }

            if (checkBox3.Checked == true)
            {
                VWedStart = Convert.ToDateTime(dateTimePicker1.Value);
                VWedEnd = Convert.ToDateTime(dateTimePicker2.Value);
                SVWedStart = VWedStart.ToString("yyyy-dd-MM HH:mm:ss");
                SVWedEnd = VWedEnd.ToString("yyyy-dd-MM HH:mm:ss");
            }
            else
            {
                VWedStart = Convert.ToDateTime("2016-01-01 00:00:00");
                VWedEnd = Convert.ToDateTime("2016-01-01 00:00:00");
                SVWedStart = VWedStart.ToString("yyyy-dd-MM HH:mm:ss");
                SVWedEnd = VWedEnd.ToString("yyyy-dd-MM HH:mm:ss");
            }

            if (checkBox4.Checked == true)
            {
                VThursStart = Convert.ToDateTime(dateTimePicker1.Value);
                VThursEnd = Convert.ToDateTime(dateTimePicker2.Value);
                SVThursStart = VThursStart.ToString("yyyy-dd-MM HH:mm:ss");
                SVThursEnd = VThursEnd.ToString("yyyy-dd-MM HH:mm:ss");
            }
            else
            {
                VThursStart = Convert.ToDateTime("2016-01-01 00:00:00");
                VThursEnd = Convert.ToDateTime("2016-01-01 00:00:00");
                SVThursStart = VThursStart.ToString("yyyy-dd-MM HH:mm:ss");
                SVThursEnd = VThursEnd.ToString("yyyy-dd-MM HH:mm:ss");
            }

            if (checkBox5.Checked == true)
            {
                VFriStart = Convert.ToDateTime(dateTimePicker1.Value);
                VFriEnd = Convert.ToDateTime(dateTimePicker2.Value);
                SVFriStart = VFriStart.ToString("yyyy-dd-MM HH:mm:ss");
                SVFriEnd = VFriEnd.ToString("yyyy-dd-MM HH:mm:ss");
            }
            else
            {
                VFriStart = Convert.ToDateTime("2016-01-01 00:00:00");
                VFriEnd = Convert.ToDateTime("2016-01-01 00:00:00");
                SVFriStart = VFriStart.ToString("yyyy-dd-MM HH:mm:ss");
                SVFriEnd = VFriEnd.ToString("yyyy-dd-MM HH:mm:ss");
            }

            if (checkBox6.Checked == true)
            {
                VSatStart = Convert.ToDateTime(dateTimePicker1.Value);
                VSatEnd = Convert.ToDateTime(dateTimePicker2.Value);
                SVSatStart = VSatStart.ToString("yyyy-dd-MM HH:mm:ss");
                SVSatEnd = VSatEnd.ToString("yyyy-dd-MM HH:mm:ss");
            }
            else
            {
                VSatStart = Convert.ToDateTime("2016-01-01 00:00:00");
                VSatEnd = Convert.ToDateTime("2016-01-01 00:00:00");
                SVSatStart = VSatStart.ToString("yyyy-dd-MM HH:mm:ss");
                SVSatEnd = VSatEnd.ToString("yyyy-dd-MM HH:mm:ss");
            }

            if (checkBox7.Checked == true)
            {
                VSunStart = Convert.ToDateTime(dateTimePicker1.Value);
                VSunEnd = Convert.ToDateTime(dateTimePicker2.Value);
                SVSunStart = VSunStart.ToString("yyyy-dd-MM HH:mm:ss");
                SVSunEnd = VSunEnd.ToString("yyyy-dd-MM HH:mm:ss");
            }
            else
            {
                VSunStart = Convert.ToDateTime("2016-01-01 00:00:00");
                VSunEnd = Convert.ToDateTime("2016-01-01 00:00:00");
                SVSunStart = VSunStart.ToString("yyyy-dd-MM HH:mm:ss");
                SVSunEnd = VSunEnd.ToString("yyyy-dd-MM HH:mm:ss");
            }

        }

        private void limpiaventana()
        {
            textBox1.Text = "";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            textBox1.Select();
        }

        private void validaselectdias()
        {
            if (checkBox1.Checked == true)
            {
                valcheckdias = valcheckdias + 1;
            }
            if (checkBox2.Checked == true)
            {
                valcheckdias = valcheckdias + 1;
            }
            if (checkBox3.Checked == true)
            {
                valcheckdias = valcheckdias + 1;
            }
            if (checkBox4.Checked == true)
            {
                valcheckdias = valcheckdias + 1;
            }
            if (checkBox5.Checked == true)
            {
                valcheckdias = valcheckdias + 1;
            }
            if (checkBox6.Checked == true)
            {
                valcheckdias = valcheckdias + 1;
            }
            if (checkBox7.Checked == true)
            {
                valcheckdias = valcheckdias + 1;
            }
        }

        public void Actualizar_servicio() {

            try
            {

                conectarbd();
                String validaservicio = "select Name from ACTimeZones";
                SqlCommand cmdvs = new SqlCommand(validaservicio, f9conn);
                SqlDataReader leevalidaservicio = cmdvs.ExecuteReader();
                cadenaAlmuerzo = "";
                while (leevalidaservicio.Read())
                {
                    cadenaAlmuerzo += leevalidaservicio.GetString(0) + "|";
                }

                string[] cadena = cadenaAlmuerzo.Substring(0, cadenaAlmuerzo.Length - 1).Split('|');

                dgInformacion.DataBindings.Clear();
                dgInformacion.Refresh();
                dgInformacion.Items.Clear();


                foreach (var item in cadena)
                {
                    dgInformacion.Items.Add(item);
                }



                if (leevalidaservicio.HasRows)
                {
                    f9conn.Close();
                }

            }
            catch (Exception ex)
            {
                string err = ex.Message;
                MessageBox.Show("Error al insertar registro: " + err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void edit_serv_Click(object sender, EventArgs e)
        {
            try
            {

                conectarbd();
                String validaservicio = "select Name from ACTimeZones";
                SqlCommand cmdvs = new SqlCommand(validaservicio, f9conn);
                SqlDataReader leevalidaservicio = cmdvs.ExecuteReader();
                cadenaAlmuerzo = "";
                if (leevalidaservicio.HasRows)
                {
                    while (leevalidaservicio.Read())
                    {
                        cadenaAlmuerzo += leevalidaservicio.GetString(0) + "|";
                    }

                    string[] cadena = cadenaAlmuerzo.Substring(0, cadenaAlmuerzo.Length - 1).Split('|');

                    dgInformacion.DataBindings.Clear();
                    dgInformacion.Refresh();
                    dgInformacion.Items.Clear();


                    foreach (var item in cadena)
                    {
                        dgInformacion.Items.Add(item);
                    }
                    MessageBox.Show("Lista de Servicio a Editar Cargada");
                    f9conn.Close();
                }
                else
                {
                    MessageBox.Show("No Existe de Servicio a Editar Cargada");
                    f9conn.Close();
                }
                



               

            }
            catch (Exception ex)
            {
                string err = ex.Message;
                MessageBox.Show("Error al insertar registro: " + err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void dgInformacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = dgInformacion.SelectedIndex;

            if (idx != -1)
            {
                Servicio_selc = dgInformacion.SelectedItem.ToString();
                idx = -1;

                try
                {

                    conectarbd();
                    String validaservicio = "select * from ACTimeZones where name = '" + Servicio_selc + "'";
                    SqlCommand cmdvs = new SqlCommand(validaservicio, f9conn);
                    SqlDataReader leevalidaservicio = cmdvs.ExecuteReader();
                    leevalidaservicio.Read();
                    //leevalidaservicio.GetInt32(0);
                    MessageBox.Show("Servicio a modificar: " + leevalidaservicio.GetString(1));
                    codserv = leevalidaservicio.GetInt16(0);
                    textBox2.Text = leevalidaservicio.GetString(1);
                    servicio_select = leevalidaservicio.GetString(1);
                    lunes = leevalidaservicio.GetDateTime(4);
                    lunesfin = leevalidaservicio.GetDateTime(5);
                    martes = leevalidaservicio.GetDateTime(6);
                    martesfin = leevalidaservicio.GetDateTime(7);
                    miercoles = leevalidaservicio.GetDateTime(8);
                    miercolesfin = leevalidaservicio.GetDateTime(9);
                    jueves = leevalidaservicio.GetDateTime(10);
                    juevesfin = leevalidaservicio.GetDateTime(11);
                    viernes = leevalidaservicio.GetDateTime(12);
                    viernesfin = leevalidaservicio.GetDateTime(13);
                    sabado = leevalidaservicio.GetDateTime(14);
                    sabadofin = leevalidaservicio.GetDateTime(15);
                    domingo = leevalidaservicio.GetDateTime(2);
                    domingofin = leevalidaservicio.GetDateTime(3);
                    DateTime comparar = Convert.ToDateTime("2016-01-01 00:00:00");

                    if (lunes == comparar)
                    {
                        checkBox14.Checked = false;
                    }
                    else
                    {
                        checkBox14.Checked = true;
                        dateTimePicker4.Value = lunes;
                        dateTimePicker3.Value = lunesfin;
                    }

                    if (martes == comparar)
                    {
                        checkBox13.Checked = false;
                    }
                    else
                    {
                        checkBox13.Checked = true;
                        dateTimePicker4.Value = martes;
                        dateTimePicker3.Value = martesfin;
                    }

                    if (miercoles == comparar)
                    {
                        checkBox12.Checked = false;
                    }
                    else
                    {
                        checkBox12.Checked = true;

                        dateTimePicker4.Value = miercoles;
                        dateTimePicker3.Value = miercolesfin;
                    }

                    if (jueves == comparar)
                    {
                        checkBox11.Checked = false;
                    }
                    else
                    {
                        checkBox11.Checked = true;
                        dateTimePicker4.Value = jueves;
                        dateTimePicker3.Value = juevesfin;
                    }

                    if (viernes == comparar)
                    {
                        checkBox10.Checked = false;
                    }
                    else
                    {
                        checkBox10.Checked = true;
                        dateTimePicker4.Value = viernes;
                        dateTimePicker3.Value = viernesfin;
                    }

                    if (sabado == comparar)
                    {
                        checkBox9.Checked = false;
                    }
                    else
                    {
                        checkBox9.Checked = true;
                        dateTimePicker4.Value = sabado;
                        dateTimePicker3.Value = sabadofin;
                    }

                    if (domingo == comparar)
                    {
                        checkBox8.Checked = false;
                    }
                    else
                    {
                        checkBox8.Checked = true;
                        dateTimePicker4.Value = domingo;
                        dateTimePicker3.Value = domingofin;
                    }


                }
                catch (Exception ex)
                {
                    string err = ex.Message;
                    MessageBox.Show("Error al insertar registro: " + err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (checkBox14.Checked == true)
            {
                VMonStart = Convert.ToDateTime(dateTimePicker4.Value);
                VMonEnd = Convert.ToDateTime(dateTimePicker3.Value);
                SVMonStart = VMonStart.ToString("yyyy-dd-MM HH:mm:ss");
                SVMonEnd = VMonEnd.ToString("yyyy-dd-MM HH:mm:ss");
            }

            else
            {
                VMonStart = Convert.ToDateTime("2016-01-01 00:00:00");
                VMonEnd = Convert.ToDateTime("2016-01-01 00:00:00");
                SVMonStart = VMonStart.ToString("yyyy-dd-MM HH:mm:ss");
                SVMonEnd = VMonEnd.ToString("yyyy-dd-MM HH:mm:ss");
            }

            if (checkBox13.Checked == true)
            {
                VTuesStart = Convert.ToDateTime(dateTimePicker4.Value);
                VTuesEnd = Convert.ToDateTime(dateTimePicker3.Value);
                SVTuesStart = VTuesStart.ToString("yyyy-dd-MM HH:mm:ss");
                SVTuesEnd = VTuesEnd.ToString("yyyy-dd-MM HH:mm:ss");
            }

            else
            {
                VTuesStart = Convert.ToDateTime("2016-01-01 00:00:00");
                VTuesEnd = Convert.ToDateTime("2016-01-01 00:00:00");
                SVTuesStart = VTuesStart.ToString("yyyy-dd-MM HH:mm:ss");
                SVTuesEnd = VTuesEnd.ToString("yyyy-dd-MM HH:mm:ss");
            }

            if (checkBox12.Checked == true)
            {
                VWedStart = Convert.ToDateTime(dateTimePicker4.Value);
                VWedEnd = Convert.ToDateTime(dateTimePicker3.Value);
                SVWedStart = VWedStart.ToString("yyyy-dd-MM HH:mm:ss");
                SVWedEnd = VWedEnd.ToString("yyyy-dd-MM HH:mm:ss");
            }

            else
            {
                VWedStart = Convert.ToDateTime("2016-01-01 00:00:00");
                VWedEnd = Convert.ToDateTime("2016-01-01 00:00:00");
                SVWedStart = VWedStart.ToString("yyyy-dd-MM HH:mm:ss");
                SVWedEnd = VWedEnd.ToString("yyyy-dd-MM HH:mm:ss");
            }
            if (checkBox11.Checked == true)
            {
                VThursStart = Convert.ToDateTime(dateTimePicker4.Value);
                VThursEnd = Convert.ToDateTime(dateTimePicker3.Value);
                SVThursStart = VThursStart.ToString("yyyy-dd-MM HH:mm:ss");
                SVThursEnd = VThursEnd.ToString("yyyy-dd-MM HH:mm:ss");
            }

            else
            {
                VThursStart = Convert.ToDateTime("2016-01-01 00:00:00");
                VThursEnd = Convert.ToDateTime("2016-01-01 00:00:00");
                SVThursStart = VThursStart.ToString("yyyy-dd-MM HH:mm:ss");
                SVThursEnd = VThursEnd.ToString("yyyy-dd-MM HH:mm:ss");
            }
            if (checkBox10.Checked == true)
            {
                VFriStart = Convert.ToDateTime(dateTimePicker4.Value);
                VFriEnd = Convert.ToDateTime(dateTimePicker3.Value);
                SVFriStart = VFriStart.ToString("yyyy-dd-MM HH:mm:ss");
                SVFriEnd = VFriEnd.ToString("yyyy-dd-MM HH:mm:ss");
            }

            else
            {
                VFriStart = Convert.ToDateTime("2016-01-01 00:00:00");
                VFriEnd = Convert.ToDateTime("2016-01-01 00:00:00");
                SVFriStart = VFriStart.ToString("yyyy-dd-MM HH:mm:ss");
                SVFriEnd = VFriEnd.ToString("yyyy-dd-MM HH:mm:ss");
            }

            if (checkBox9.Checked == true)
            {
                VSatStart = Convert.ToDateTime(dateTimePicker4.Value);
                VSatEnd = Convert.ToDateTime(dateTimePicker3.Value);
                SVSatStart = VSatStart.ToString("yyyy-dd-MM HH:mm:ss");
                SVSatEnd = VSatEnd.ToString("yyyy-dd-MM HH:mm:ss");
            }

            else
            {
                VSatStart = Convert.ToDateTime("2016-01-01 00:00:00");
                VSatEnd = Convert.ToDateTime("2016-01-01 00:00:00");
                SVSatStart = VSatStart.ToString("yyyy-dd-MM HH:mm:ss");
                SVSatEnd = VSatEnd.ToString("yyyy-dd-MM HH:mm:ss");
            }

            if (checkBox8.Checked == true)
            {
                VSunStart = Convert.ToDateTime(dateTimePicker4.Value);
                VSunEnd = Convert.ToDateTime(dateTimePicker3.Value);
                SVSunStart = VSunStart.ToString("yyyy-dd-MM HH:mm:ss");
                SVSunEnd = VSunEnd.ToString("yyyy-dd-MM HH:mm:ss");
            }

            else
            {
                VSunStart = Convert.ToDateTime("2016-01-01 00:00:00");
                VSunEnd = Convert.ToDateTime("2016-01-01 00:00:00");
                SVSunStart = VSunStart.ToString("yyyy-dd-MM HH:mm:ss");
                SVSunEnd = VSunEnd.ToString("yyyy-dd-MM HH:mm:ss");
            }

            try
            {
                servicio_select = textBox2.Text;
                conectarbd();
                //MessageBox.Show("Sin error despues de validar" + SVSatStart.ToString());
                String updateserv = "update ACTimeZones " +
                                    "set SunStart = '" + SVSunStart + "', " +
                                    "SunEnd = '" + SVSunEnd + "', " +
                                    "MonStart = '" + SVMonStart + "', " +
                                    "MonEnd = '" + SVMonEnd + "', " +
                                    "TuesStart = '" + SVTuesStart + "', " +
                                    "TuesEnd = '" + SVTuesEnd + "', " +
                                    "WedStart = '" + SVWedStart + "', " +
                                    "WedEnd = '" + SVWedEnd + "', " +
                                    "ThursStart = '" + SVThursStart + "', " +
                                    "ThursEnd = '" + SVThursEnd + "', " +
                                    "FriStart = '" + SVFriStart + "', " +
                                    "FriEnd = '" + SVFriEnd + "', " +
                                    "SatStart = '" + SVSatStart + "', " +
                                    "SatEnd = '" + SVSatEnd + "', " +
                                    "Name = '" + servicio_select + "'" +
                                    "where TimeZoneID = '" + codserv + "'";

                SqlCommand cmdudps = new SqlCommand(updateserv, f9conn);
                cmdudps.ExecuteNonQuery();
                f9conn.Close();
                MessageBox.Show("Modificación de servicio realizado exitosamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exudp)
            {
                DateTime dtup = DateTime.Now;
                MessageBox.Show(dtup + ": Error al intentar modificar servicio - " + exudp.Message);
                f9conn.Close();
            }
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
