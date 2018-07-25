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
    public partial class Form14 : Form
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

        int valcheckdias;
        string nomcombo;
        int idservicio;
        string valorvalidacombo;
        int menumodifica;
        int validanomnuevo;

        int validaconsconf;
        int validaconsconfe;

        DateTime reporteini;
        DateTime reportefin;

        TimeSpan tmars;

        public Form14()
        {
            InitializeComponent();
        }

        private void Form14_Load(object sender, EventArgs e)
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

            esconderobjetos();
            esconderobjetos2();
            dateTimePicker2.Value = dateTimePicker2.Value.AddHours(1);
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

        private void esconderobjetos()
        {
            label1.Hide();
            label2.Hide();
            comboBox1.Hide();
            comboBox1.SelectedIndex = -1;
            textBox1.Hide();
            textBox1.Text = "";
            button1.Hide();
            button2.Hide();
        }

        private void esconderobjetos2()
        {
            dateTimePicker1.Hide();
            dateTimePicker2.Hide();
            label3.Hide();
            label4.Hide();
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox1.Hide();
            checkBox2.Hide();
            checkBox3.Hide();
            checkBox4.Hide();
            checkBox5.Hide();
            checkBox6.Hide();
            checkBox7.Hide();
            button1.Hide();
            button2.Hide();
        }

        private void esconderobjetos3()
        {
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
        }

        private void cambiarNombreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menumodifica = 1;
            esconderobjetos2();
            cargacomboservicios();
            label1.Show();
            label2.Show();
            comboBox1.Show();
            textBox1.Show();
            button1.Show();
            button2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            menumodifica = 0;
            esconderobjetos();
            esconderobjetos2();
            this.Close();
        }

        private void cargacomboservicios()
        {
            comboBox1.Items.Clear();
            try
            {
                conectarbd();
                String cargacombo = "select distinct name from ACTimeZones order by 1 asc";
                SqlCommand cmdcc = new SqlCommand(cargacombo, f9conn);
                SqlDataReader leecombo = cmdcc.ExecuteReader();

                if (leecombo.HasRows)
                {
                    while (leecombo.Read())
                    {
                        string nameservicio = Convert.ToString(leecombo[0]);
                        comboBox1.Items.Add(nameservicio);
                    }
                    f9conn.Close();
                }
                else
                {
                    MessageBox.Show("No existen Servicios Configurados");
                    f9conn.Close();
                }
            }
            catch (Exception ex)
            {
                DateTime dt = DateTime.Now;
                log(dt + ": Error al cargar servicios - " + ex.Message);
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
            validaidservicio();

            if (menumodifica == 1)
            {
                validanombrenuevo();
                if (valorvalidacombo != textBox1.Text)
                {
                    if (validanomnuevo != 1)
                    {
                        try
                        {
                            conectarbd();
                            String identificaid = "update ACTimeZones set name = '" + textBox1.Text + "' where TimeZoneID = " + idservicio;
                            SqlCommand cmdiid = new SqlCommand(identificaid, f9conn);
                            cmdiid.ExecuteNonQuery();
                            f9conn.Close();

                            MessageBox.Show("Cambio de Nombre de Servicio Ejecutado Exitosamente");

                            esconderobjetos();
                        }
                        catch (Exception ex)
                        {
                            DateTime dt = DateTime.Now;
                            log(dt + ": Error al cambiar nombre de servicio - " + ex.Message);
                            f9conn.Close();
                        }
                        valorvalidacombo = "";
                    }
                    else
                    {
                        MessageBox.Show("Debe ingresar un nombre diferente de servicio");
                        valorvalidacombo = "";
                        validanomnuevo = 0;
                    }

                    validanomnuevo = 0;
                }
                else
                {
                    MessageBox.Show("Debe ingresar un nombre diferente de servicio");
                    valorvalidacombo = "";
                    validanomnuevo = 0;
                }
            }

            if (menumodifica == 2)
            {
                validadias();
                try
                {
                    conectarbd();
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
                                        "SatEnd = '" + SVSatEnd + "'" +
                                        "where TimeZoneID = '" + idservicio + "'";

                    SqlCommand cmdudps = new SqlCommand(updateserv, f9conn);
                    cmdudps.ExecuteNonQuery();
                    f9conn.Close();
                    MessageBox.Show("Modificación de servicio realizado exitosamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    esconderobjetos();
                    esconderobjetos2();
                }
                catch(Exception exudp)
                {
                    DateTime dtup = DateTime.Now;
                    log(dtup + ": Error al intentar modificar servicio - " + exudp.Message);
                }
            }
        }

        private void validaidservicio()
        {
            valorvalidacombo = comboBox1.SelectedItem.ToString();

            if (!string.IsNullOrEmpty(valorvalidacombo))
            {
                try
                {
                    conectarbd();
                    String identificaid = "select distinct TimeZoneID from ACTimeZones where name = '" + nomcombo + "'";
                    SqlCommand cmdii = new SqlCommand(identificaid, f9conn);
                    SqlDataReader leeid = cmdii.ExecuteReader();
                    leeid.Read();

                    if (leeid.HasRows)
                    {
                        idservicio = Convert.ToInt32(leeid[0]);
                        f9conn.Close();
                    }
                    else
                    {
                        MessageBox.Show("No existen Servicios Configurados");
                        f9conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    DateTime dt = DateTime.Now;
                    log(dt + ": Error al obtener id de servicio - " + ex.Message);
                    f9conn.Close();
                }
            }
            else
            {
                MessageBox.Show("Primero debe seleccionar un servicio");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = comboBox1.SelectedIndex;

            if (idx != -1)
            {
                nomcombo = comboBox1.SelectedItem.ToString();
                esconderobjetos3();
                consultaserviciosconf();
                idx = -1;
            }
        }

        private void consultaserviciosconf()
        {
            try
            {
                conectarbd();
                String consservconf = "select CONVERT(VARCHAR(8),monstart,108), " +
	                                  " CONVERT(VARCHAR(8),MonEnd,108), " +
	                                  " CONVERT(VARCHAR(8),tuesstart,108),  " +
	                                  " CONVERT(VARCHAR(8),tuesEnd,108), " +
	                                  " CONVERT(VARCHAR(8),wedstart,108),  " +
	                                  " CONVERT(VARCHAR(8),wedEnd,108), " +
	                                  " CONVERT(VARCHAR(8),thursstart,108),  " +
	                                  " CONVERT(VARCHAR(8),thursEnd,108), " +
	                                  " CONVERT(VARCHAR(8),fristart,108),  " +
	                                  " CONVERT(VARCHAR(8),friEnd,108), " +
	                                  " CONVERT(VARCHAR(8),satstart,108),  " +
	                                  " CONVERT(VARCHAR(8),satEnd,108), " +
	                                  " CONVERT(VARCHAR(8),sunstart,108),  " +
	                                  " CONVERT(VARCHAR(8),sunEnd,108) " +
                                      " from ACTimeZones where name = '" + nomcombo + "'";

                SqlCommand cmdcsc = new SqlCommand(consservconf, f9conn);
                SqlDataReader leecomboconf = cmdcsc.ExecuteReader();
                leecomboconf.Read();

                //=========================
                DateTime luns = Convert.ToDateTime(leecomboconf[0]);
                
                TimeSpan tluns = luns.TimeOfDay;
                if (Convert.ToString(tluns) != "00:00:00")
                {
                    checkBox1.Checked = true;
                    validaconsconf = 1;
                    reporteini = luns;
                }

                DateTime lune = Convert.ToDateTime(leecomboconf[1]);

                TimeSpan tlune = lune.TimeOfDay;
                if (Convert.ToString(tlune) != "00:00:00")
                {
                    validaconsconfe = 1;
                    reportefin = lune;
                }
                //=========================

                //=========================                
                DateTime mars = Convert.ToDateTime(leecomboconf[2]);

                TimeSpan tmars = mars.TimeOfDay;
                if (Convert.ToString(tmars) != "00:00:00")
                {
                    checkBox2.Checked = true;
                    validaconsconf = 1;
                    reporteini = mars;
                }

                DateTime mare = Convert.ToDateTime(leecomboconf[3]);

                TimeSpan tmare = mare.TimeOfDay;
                if (Convert.ToString(tmare) != "00:00:00")
                {
                    validaconsconfe = 1;
                    reportefin = mare;
                }

                //=========================                
                DateTime mies = Convert.ToDateTime(leecomboconf[4]);

                TimeSpan tmies = mies.TimeOfDay;
                if (Convert.ToString(tmies) != "00:00:00")
                {
                    checkBox3.Checked = true;
                    validaconsconf = 1;
                    reporteini = mies;
                }

                DateTime miee = Convert.ToDateTime(leecomboconf[5]);

                TimeSpan tmiee = miee.TimeOfDay;
                if (Convert.ToString(tmiee) != "00:00:00")
                {
                    validaconsconfe = 1;
                    reportefin = miee;
                }

                //=========================                
                DateTime jues = Convert.ToDateTime(leecomboconf[6]);

                TimeSpan tjues = jues.TimeOfDay;
                if (Convert.ToString(tjues) != "00:00:00")
                {
                    checkBox4.Checked = true;
                    validaconsconf = 1;
                    reporteini = jues;
                }

                DateTime juee = Convert.ToDateTime(leecomboconf[7]);

                TimeSpan tjuee = juee.TimeOfDay;
                if (Convert.ToString(tjuee) != "00:00:00")
                {
                    validaconsconfe = 1;
                    reportefin = juee;
                }

                //=========================                
                DateTime vies = Convert.ToDateTime(leecomboconf[8]);

                TimeSpan tvies = vies.TimeOfDay;
                if (Convert.ToString(tvies) != "00:00:00")
                {
                    checkBox5.Checked = true;
                    validaconsconf = 1;
                    reporteini = vies;
                }

                DateTime viee = Convert.ToDateTime(leecomboconf[9]);

                TimeSpan tviee = viee.TimeOfDay;
                if (Convert.ToString(tviee) != "00:00:00")
                {
                    validaconsconfe = 1;
                    reportefin = viee;
                }

                //=========================                
                DateTime sabs = Convert.ToDateTime(leecomboconf[10]);

                TimeSpan tsabs = sabs.TimeOfDay;
                if (Convert.ToString(tsabs) != "00:00:00")
                {
                    checkBox6.Checked = true;
                    validaconsconf = 1;
                    reporteini = sabs;
                }

                DateTime sabe = Convert.ToDateTime(leecomboconf[11]);

                TimeSpan tsabe = sabe.TimeOfDay;
                if (Convert.ToString(tsabe) != "00:00:00")
                {
                    validaconsconfe = 1;
                    reportefin = sabe;
                }

                //=========================                
                DateTime doms = Convert.ToDateTime(leecomboconf[12]);

                TimeSpan tdoms = doms.TimeOfDay;
                if (Convert.ToString(tdoms) != "00:00:00")
                {
                    checkBox7.Checked = true;
                    validaconsconf = 1;
                    reporteini = doms;
                }

                DateTime dome = Convert.ToDateTime(leecomboconf[13]);

                TimeSpan tdome = dome.TimeOfDay;
                if (Convert.ToString(tdome) != "00:00:00")
                {
                    validaconsconfe = 1;
                    reportefin = dome;
                }

                dateTimePicker1.Value = reporteini;
                dateTimePicker2.Value = reportefin;

            }
            catch (Exception exudp)
            {
                DateTime dtup = DateTime.Now;
                log(dtup + ": Error al intentar modificar servicio - " + exudp.Message);
            }
        }

        private void cambiarHorariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menumodifica = 2;
            DialogResult dialogResult = MessageBox.Show("Está seguro de Modificar Horarios\ny Días de un Servicio?", "Advertencia", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                esconderobjetos();
                cargacomboservicios();
                label1.Show();
                comboBox1.Show();
                dateTimePicker1.Show();
                dateTimePicker2.Show();
                label3.Show();
                label4.Show();
                checkBox1.Show();
                checkBox2.Show();
                checkBox3.Show();
                checkBox4.Show();
                checkBox5.Show();
                checkBox6.Show();
                checkBox7.Show();
                button1.Show();
                button2.Show();

                //validaselectdias();
                //validadias();
                //nomcombo = comboBox1.SelectedItem.ToString();

            }
            else if (dialogResult == DialogResult.No)
            {
                esconderobjetos();
                esconderobjetos2();
            }

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

        private void validadias()
        {
            if (checkBox1.Checked == true)
            {
                VMonStart = Convert.ToDateTime(dateTimePicker1.Value);
                VMonEnd = Convert.ToDateTime(dateTimePicker2.Value);
                SVMonStart = VMonStart.ToString("yyyy-MM-dd HH:mm:ss");
                SVMonEnd = VMonEnd.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                VMonStart = Convert.ToDateTime("2016-01-01 00:00:00");
                VMonEnd = Convert.ToDateTime("2016-01-01 00:00:00");
                SVMonStart = VMonStart.ToString("yyyy-MM-dd HH:mm:ss");
                SVMonEnd = VMonEnd.ToString("yyyy-MM-dd HH:mm:ss");
            }

            if (checkBox2.Checked == true)
            {
                VTuesStart = Convert.ToDateTime(dateTimePicker1.Value);
                VTuesEnd = Convert.ToDateTime(dateTimePicker2.Value);
                SVTuesStart = VTuesStart.ToString("yyyy-MM-dd HH:mm:ss");
                SVTuesEnd = VTuesEnd.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                VTuesStart = Convert.ToDateTime("2016-01-01 00:00:00");
                VTuesEnd = Convert.ToDateTime("2016-01-01 00:00:00");
                SVTuesStart = VTuesStart.ToString("yyyy-MM-dd HH:mm:ss");
                SVTuesEnd = VTuesEnd.ToString("yyyy-MM-dd HH:mm:ss");
            }

            if (checkBox3.Checked == true)
            {
                VWedStart = Convert.ToDateTime(dateTimePicker1.Value);
                VWedEnd = Convert.ToDateTime(dateTimePicker2.Value);
                SVWedStart = VWedStart.ToString("yyyy-MM-dd HH:mm:ss");
                SVWedEnd = VWedEnd.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                VWedStart = Convert.ToDateTime("2016-01-01 00:00:00");
                VWedEnd = Convert.ToDateTime("2016-01-01 00:00:00");
                SVWedStart = VWedStart.ToString("yyyy-MM-dd HH:mm:ss");
                SVWedEnd = VWedEnd.ToString("yyyy-MM-dd HH:mm:ss");
            }

            if (checkBox4.Checked == true)
            {
                VThursStart = Convert.ToDateTime(dateTimePicker1.Value);
                VThursEnd = Convert.ToDateTime(dateTimePicker2.Value);
                SVThursStart = VThursStart.ToString("yyyy-MM-dd HH:mm:ss");
                SVThursEnd = VThursEnd.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                VThursStart = Convert.ToDateTime("2016-01-01 00:00:00");
                VThursEnd = Convert.ToDateTime("2016-01-01 00:00:00");
                SVThursStart = VThursStart.ToString("yyyy-MM-dd HH:mm:ss");
                SVThursEnd = VThursEnd.ToString("yyyy-MM-dd HH:mm:ss");
            }

            if (checkBox5.Checked == true)
            {
                VFriStart = Convert.ToDateTime(dateTimePicker1.Value);
                VFriEnd = Convert.ToDateTime(dateTimePicker2.Value);
                SVFriStart = VFriStart.ToString("yyyy-MM-dd HH:mm:ss");
                SVFriEnd = VFriEnd.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                VFriStart = Convert.ToDateTime("2016-01-01 00:00:00");
                VFriEnd = Convert.ToDateTime("2016-01-01 00:00:00");
                SVFriStart = VFriStart.ToString("yyyy-MM-dd HH:mm:ss");
                SVFriEnd = VFriEnd.ToString("yyyy-MM-dd HH:mm:ss");
            }

            if (checkBox6.Checked == true)
            {
                VSatStart = Convert.ToDateTime(dateTimePicker1.Value);
                VSatEnd = Convert.ToDateTime(dateTimePicker2.Value);
                SVSatStart = VSatStart.ToString("yyyy-MM-dd HH:mm:ss");
                SVSatEnd = VSatEnd.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                VSatStart = Convert.ToDateTime("2016-01-01 00:00:00");
                VSatEnd = Convert.ToDateTime("2016-01-01 00:00:00");
                SVSatStart = VSatStart.ToString("yyyy-MM-dd HH:mm:ss");
                SVSatEnd = VSatEnd.ToString("yyyy-MM-dd HH:mm:ss");
            }

            if (checkBox7.Checked == true)
            {
                VSunStart = Convert.ToDateTime(dateTimePicker1.Value);
                VSunEnd = Convert.ToDateTime(dateTimePicker2.Value);
                SVSunStart = VSunStart.ToString("yyyy-MM-dd HH:mm:ss");
                SVSunEnd = VSunEnd.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                VSunStart = Convert.ToDateTime("2016-01-01 00:00:00");
                VSunEnd = Convert.ToDateTime("2016-01-01 00:00:00");
                SVSunStart = VSunStart.ToString("yyyy-MM-dd HH:mm:ss");
                SVSunEnd = VSunEnd.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        private void validanombrenuevo()
        {
            try
            {
                conectarbd();
                String valnomnuevo = "select distinct name from ACTimeZones where name = '" + textBox1.Text + "'";
                SqlCommand cmdvnn = new SqlCommand(valnomnuevo, f9conn);
                SqlDataReader leevnn = cmdvnn.ExecuteReader();
                leevnn.Read();

                if (leevnn.HasRows)
                {
                    validanomnuevo = 1;
                    f9conn.Close();
                }
                else
                {
                    validanomnuevo = 0;
                    f9conn.Close();
                }
            }
            catch (Exception ex)
            {
                DateTime dt = DateTime.Now;
                log(dt + ": Error al obtener id de servicio - " + ex.Message);
                f9conn.Close();
            }
        }
    }
}
