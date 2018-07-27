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
using Excel = Microsoft.Office.Interop.Excel;//desbloquear 04/07/2018

namespace Casino
{
    public partial class Form2 : Form
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
        string datodg6;
        string datodg7;
        string datodg8;
        string datodg9;
        string datodg10;

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

        string usuariolog;
        int perfil;

        public Form2(string pasouser, int pasoperfil)
        {
            InitializeComponent();
            costosToolStripMenuItem.Enabled = false;
            reportesToolStripMenuItem.Enabled = false;
            usuariolog = pasouser;
            perfil = pasoperfil;

            if (perfil == 1)
            {
                emitirValesToolStripMenuItem.Enabled = true;
            }
            else
            {
                emitirValesToolStripMenuItem.Enabled = false;
            }
        }

        private void generaversion()
        {
            DateTime dt = DateTime.Now;
            log(dt + ": Versión 2.0.3");
        }

        private void costosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            limpiarobjetos();
            Form7 frm7 = new Form7();
            frm7.ShowDialog();
        }

        private void conectarABaseDeDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (conectarABaseDeDatosToolStripMenuItem.Text == "Conectado")
            {
                MessageBox.Show("Ya se encuentra conectado a la base de datos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Form3 frm3 = new Form3();
                frm3.ShowDialog();

                if (frm3.retcheck == 1)
                {
                    conectarABaseDeDatosToolStripMenuItem.Text = "Conectado";
                    conectarABaseDeDatosToolStripMenuItem.Enabled = false;
                    costosToolStripMenuItem.Enabled = true;
                    reportesToolStripMenuItem.Enabled = true;
                }
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void salirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
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

        private void reportesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cargarobjetos();
            cargadatosbd();
            radioButton1.Enabled = false;
            radioButton4.Enabled = false;
            button1.Enabled = false;
            pictureBox1.Hide();
            button4.Visible = true;
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

        private void Form2_Load(object sender, EventArgs e)
        {
            label6.Text = usuariolog;
            string txtopacityvalue = "40";
            float opacityvalue = float.Parse(txtopacityvalue) / 100;
            pictureBox1.Image = ImageUtils.ImageTransparency.ChangeOpacity(System.Drawing.Image.FromFile(path + @"\fondocasino.jpg"), opacityvalue);

            limpiarobjetos();

            if (File.Exists(path + @"\casino.out"))
            {
                cargadatosbd();
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

                conectarABaseDeDatosToolStripMenuItem.Text = "Conectado";
                conectarABaseDeDatosToolStripMenuItem.Enabled = false;
                costosToolStripMenuItem.Enabled = true;
                reportesToolStripMenuItem.Enabled = true;

            }
            else
            {
                MessageBox.Show("Debe seleccionar:\r'Conectar a Base de Datos'", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            dataGridView4.Rows.Clear();
            dataGridView1.Hide();
            dataGridView2.Hide();
            dataGridView3.Hide();
            dataGridView4.Hide();
            button1.Enabled = false;
            validachecked = 0;

            if (sale2 == 0)
            {
                radioButton5.Checked = false;

                try
                {
                    f2conectarbd();
                    String consulta = "select distinct DEPTID, DEPTNAME from DEPARTMENTS order by DEPTID";
                    SqlCommand cmd = new SqlCommand(consulta, f2conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string bddptoid = Convert.ToString(reader[0]);
                            string bddeptname = Convert.ToString(reader[1]);
                            checkedListBox2.Items.Add(bddptoid + "-" + bddeptname, CheckState.Unchecked);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No existen departamentos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        reader.Close();
                    }

                    sale2 = 1;
                }
                catch (Exception)
                {
                    errorconnbd();
                }

            }
            else
            {
                checkedListBox2.Items.Clear();
                dataGridView2.Hide();
                sale2 = 0;
            }

            radioButton1.Checked = false;
            radioButton4.Checked = false;
            
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            dataGridView4.Rows.Clear();
            dataGridView1.Hide();
            dataGridView2.Hide();
            dataGridView3.Hide();
            dataGridView4.Hide();
            button1.Enabled = false;
            validachecked = 0;

            if (sale3 == 0)
            {
                radioButton5.Checked = false;

                try
                {
                    f2conectarbd();

                    String consulta = "select timezoneid, name from ACTimeZones";
                    SqlCommand cmd = new SqlCommand(consulta, f2conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string bdtimezone = Convert.ToString(reader[0]);
                            string bdtimename = Convert.ToString(reader[1]);
                            checkedListBox3.Items.Add(bdtimezone + "-" + bdtimename, CheckState.Unchecked);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No existen departamentos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        reader.Close();
                    }
                    reader.Close();
                    f2conn.Close();
                    sale3 = 1;
                }
                catch (Exception)
                {
                    errorconnbd();
                }
            }
            else
            {
                checkedListBox3.Items.Clear();

                sale3 = 0;
            }

            radioButton1.Checked = false;
            radioButton4.Checked = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(fecini))
            {
                string fecha1 = dateTimePicker1.Value.Date.ToString("yyyyMMdd");
                fecini = fecha1;
            }
            if (string.IsNullOrEmpty(fecfin))
            {
                string fecha2 = dateTimePicker2.Value.Date.ToString("yyyyMMdd");
                fecfin = fecha2;
            }

            if (dateTimePicker2.Value < dateTimePicker1.Value)
            {
                MessageBox.Show("El campo 'Fecha Fin' no puede ser 'Menor' que el campo 'Fecha Inicio'", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                DateTime now = DateTime.Now;
                var fechainico = new DateTime(now.Year, now.Month, 1);

                dateTimePicker1.Format = DateTimePickerFormat.Short;
                dateTimePicker1.Value = new DateTime(fechainico.Year, fechainico.Month, fechainico.Day);
                dateTimePicker2.Format = DateTimePickerFormat.Short;
                dateTimePicker2.Value = new DateTime(now.Year, now.Month, now.Day);


                /*dateTimePicker1.Format = DateTimePickerFormat.Short;
                dateTimePicker1.Value = new DateTime(2015, 01, 01);
                dateTimePicker2.Format = DateTimePickerFormat.Short;
                dateTimePicker2.Value = new DateTime(2015, 01, 01);*/
            }
            else
            {
                if (radioButton4.Checked && radioButton5.Checked)
                {
                    validachecked4 = 1;
                    dataGridView1.Rows.Clear();
                    if (checkedListBox1.CheckedItems.Count != 0)
                    {
                        for (int x = 0; x <= checkedListBox1.CheckedItems.Count - 1; x++)
                        {
                            string valor = checkedListBox1.CheckedItems[x].ToString();
                            string[] campo1 = valor.Split('-');
                            int pasouser = Convert.ToInt32(campo1[0]);
                            reporte1(pasouser);
                        }
                    }

                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        checkedListBox1.SetItemChecked(i, false);
                    }

                    radioButton4.Checked = false;
                    validachecked4 = 0;

                    if (countfalla > 0)
                    {
                        countfalla = 0;
                    }

                    checkedListBox1.Items.Clear();

                    try
                    {
                        f2conectarbd();
                        String consulta = "select distinct DEPTID, DEPTNAME from DEPARTMENTS order by DEPTID";
                        SqlCommand cmd = new SqlCommand(consulta, f2conn);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string bddptoid = Convert.ToString(reader[0]);
                                string bddeptname = Convert.ToString(reader[1]);
                                checkedListBox1.Items.Add(bddptoid + "-" + bddeptname, CheckState.Unchecked);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No existen usuarios", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            reader.Close();
                        }

                        reader.Close();
                        f2conn.Close();
                        radioButton1.Enabled = false;
                        radioButton4.Enabled = false;
                        validachecked = 0;
                        button1.Enabled = true;
                        dataGridView1.Show();
                        tiporeporte = 1;
                    }
                    catch (Exception)
                    {
                        errorconnbd();
                    }
                }

                if (radioButton1.Checked && radioButton5.Checked)
                {
                    validachecked1 = 1;
                    dataGridView1.Rows.Clear();
                    if (checkedListBox1.CheckedItems.Count != 0)
                    {
                        for (int x = 0; x <= checkedListBox1.CheckedItems.Count - 1; x++)
                        {
                            string valor = checkedListBox1.CheckedItems[x].ToString();
                            string[] campo1 = valor.Split('-');
                            int pasouser = Convert.ToInt32(campo1[0]);
                            reporte1(pasouser);
                        }
                    }

                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        checkedListBox1.SetItemChecked(i, false);
                    }

                    radioButton4.Checked = false;
                    validachecked1 = 0;

                    if (countfalla > 0)
                    {
                        countfalla = 0;
                    }

                    button1.Enabled = true;
                    dataGridView1.Show();
                    tiporeporte = 2;

                }

                if (radioButton2.Checked)
                {
                    validachecked1 = 1;
                    dataGridView2.Rows.Clear();
                    if (checkedListBox2.CheckedItems.Count != 0)
                    {
                        for (int x = 0; x <= checkedListBox2.CheckedItems.Count - 1; x++)
                        {
                            string valor = checkedListBox2.CheckedItems[x].ToString();
                            string[] campo1 = valor.Split('-');
                            int pasodpto = Convert.ToInt32(campo1[0]);
                            reporte2(pasodpto);
                        }
                    }

                    for (int i = 0; i < checkedListBox2.Items.Count; i++)
                    {
                        checkedListBox2.SetItemChecked(i, false);
                    }

                    button1.Enabled = true;
                    dataGridView2.Show();
                    tiporeporte = 3;
                }

                if (radioButton3.Checked)
                {
                    validachecked1 = 1;
                    dataGridView4.Rows.Clear();
                    if (checkedListBox3.CheckedItems.Count != 0)
                    {
                        for (int x = 0; x <= checkedListBox3.CheckedItems.Count - 1; x++)
                        {
                            string valor = checkedListBox3.CheckedItems[x].ToString();
                            string[] campo1 = valor.Split('-');
                            int pasocserv = Convert.ToInt32(campo1[0]);
                            reporte4(pasocserv);
                        }
                        dataGridView4.Show();
                        button1.Enabled = true;
                    }
                    else
                    {
                        dataGridView4.Hide();
                    }

                    for (int i = 0; i < checkedListBox3.Items.Count; i++)
                    {
                        checkedListBox3.SetItemChecked(i, false);
                    }
                      
                    tiporeporte = 4;
                }

            }
        }

        private void Reporte_total_usuario(int cuserid)
        {
            //varcuserid = cuserid;

            try
            {
                f2conectarbd();

                String consulta = "select distinct dd.DEPTNAME, du.SSN, du.Name, da.Name, 1 cantidad, du.MINZU, dcc.costoservicio, convert(varchar, dc.fecha, 105) dcfecha, convert(varchar, dc.fecha, 108) dchora, dm.MachineAlias, dc.sn, dc.ultm_reg " +
                                    "from casino dc, " +
                                         "casino_costos dcc, " +
                                         "USERINFO du, " +
                                         "DEPARTMENTS dd, " +
                                         "ACTimeZones da, " +
                                         "Machines dm " +
                                    " where  du.USERID = dc.iduser " +
                                    " and convert(varchar,dc.fecha,112) >= " + fecini +
                                    " and convert(varchar,dc.fecha,112) <= " + fecfin +
                                    " and dc.servicio = da.TimeZoneID " +
                                    " and dc.servicio = dcc.idcosto " +
                                    " and dc.fecha >= dcc.fecinival " +
                                    " and dc.fecha <= dcc.fecfinval " +
                                    " and dc.iduser = du.USERID " +
                                    " and du.DEFAULTDEPTID = dd.DEPTID " +
                                    " and dc.sn = dm.sn " +
                                    " or dc.sn = 'MANUAL' " +
                                    " and du.USERID = dc.iduser " +
                                    " and convert(varchar,dc.fecha,112) >= " + fecini +
                                    " and convert(varchar,dc.fecha,112) <= " + fecfin +
                                    " and dc.servicio = da.TimeZoneID " +
                                    " and dc.servicio = dcc.idcosto " +
                                    " and dc.fecha >= dcc.fecinival " +
                                    " and dc.fecha <= dcc.fecfinval " +
                                    " and dc.iduser = du.USERID " +
                                    " and du.DEFAULTDEPTID = dd.DEPTID " +
                                    " order by dd.DEPTNAME, dcfecha, dchora, da.Name asc";
                SqlCommand cmd = new SqlCommand(consulta, f2conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //MessageBox.Show("hola");
                        string bddepto = Convert.ToString(reader[0]);
                        string bdssn = Convert.ToString(reader[1]);
                        string bdpersonal = Convert.ToString(reader[2]);
                        string bdservicio = Convert.ToString(reader[3]);
                        int bdcantidad = Convert.ToInt32(reader[4]);
                        string bdccosto = Convert.ToString(reader[5]);
                        int bdcostoserv = Convert.ToInt32(reader[6]);
                        string bdfecha = Convert.ToString(reader[7]);
                        string bdhora = Convert.ToString(reader[8]);
                        string bdmachine = Convert.ToString(reader[9]);
                        string snmachine = Convert.ToString(reader[10]);
                        // MessageBox.Show("maquina " + snmachine);
                        if (snmachine.CompareTo("MANUAL") == 0)
                        {
                            bdmachine = "MANUAL";
                        }
                        CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
                        string pasomiles = (bdcostoserv.ToString("0,0", elGR));
                        dataGridView1.Rows.Add(bddepto, bdssn, bdpersonal, bdservicio, bdcantidad, bdccosto, pasomiles, bdfecha, bdhora, bdmachine);
                    }
                }
                else
                {
                    countfalla = countfalla + 1;
                    reader.Close();
                }
                reader.Close();
                f2conn.Close();
            }
            catch (Exception)
            {
                errorconnbd();
            }

        }
        private void reporte1(int cuserid)
        {
            varcuserid = cuserid;

            try
            {
                f2conectarbd();

                String consulta = "select distinct dd.DEPTNAME, du.SSN, du.Name, da.Name, 1 cantidad, du.MINZU, dcc.costoservicio, convert(varchar, dc.fecha, 105) dcfecha, convert(varchar, dc.fecha, 108) dchora, dm.MachineAlias, dc.sn, dc.ultm_reg " +
                                    "from casino dc, " +
                                         "casino_costos dcc, " +
                                         "USERINFO du, " +
                                         "DEPARTMENTS dd, " +
                                         "ACTimeZones da, " +
                                         "Machines dm " +
                                    " where  du.Badgenumber = " + varcuserid +
                                    " and du.USERID = dc.iduser " +
                                    " and convert(varchar,dc.fecha,112) >= " + fecini +
                                    " and convert(varchar,dc.fecha,112) <= " + fecfin +
                                    " and dc.servicio = da.TimeZoneID " +
                                    " and dc.servicio = dcc.idcosto " +
                                    " and dc.fecha >= dcc.fecinival " +
                                    " and dc.fecha <= dcc.fecfinval " +
                                    " and dc.iduser = du.USERID " +
                                    " and du.DEFAULTDEPTID = dd.DEPTID " +
                                    " and dc.sn = dm.sn " +
                                    " or dc.sn = 'MANUAL' " +
                                    " and du.Badgenumber = " + varcuserid +
                                    " and du.USERID = dc.iduser " +
                                    " and convert(varchar,dc.fecha,112) >= " + fecini +
                                    " and convert(varchar,dc.fecha,112) <= " + fecfin +
                                    " and dc.servicio = da.TimeZoneID " +
                                    " and dc.servicio = dcc.idcosto " +
                                    " and dc.fecha >= dcc.fecinival " +
                                    " and dc.fecha <= dcc.fecfinval " +
                                    " and dc.iduser = du.USERID " +
                                    " and du.DEFAULTDEPTID = dd.DEPTID " +
                                    " order by dd.DEPTNAME, dcfecha, dchora, da.Name asc";
                SqlCommand cmd = new SqlCommand(consulta, f2conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string bddepto = Convert.ToString(reader[0]);
                        string bdssn = Convert.ToString(reader[1]);
                        string bdpersonal = Convert.ToString(reader[2]);
                        string bdservicio = Convert.ToString(reader[3]);
                        int bdcantidad = Convert.ToInt32(reader[4]);
                        string bdccosto = Convert.ToString(reader[5]);
                        int bdcostoserv = Convert.ToInt32(reader[6]);
                        string bdfecha = Convert.ToString(reader[7]);
                        string bdhora = Convert.ToString(reader[8]);
                        string bdmachine = Convert.ToString(reader[9]);
                        string snmachine = Convert.ToString(reader[10]);
                       // MessageBox.Show("maquina " + snmachine);
                        if (snmachine.CompareTo("MANUAL") == 0 )
                        {
                            bdmachine = "MANUAL";
                        }
                        CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
                        string pasomiles = (bdcostoserv.ToString("0,0", elGR));
                        dataGridView1.Rows.Add(bddepto, bdssn, bdpersonal, bdservicio, bdcantidad, bdccosto, pasomiles, bdfecha, bdhora, bdmachine);
                    }
                }
                else
                {
                    countfalla = countfalla + 1;
                    reader.Close();
                }
                reader.Close();
                f2conn.Close();
            }
            catch (Exception)
            {
                errorconnbd();
            }

        }

        private void reporte2(int cdpto)
        {
            varcdpto = cdpto;

            try
            {
                f2conectarbd();

                String consulta = "select repdept.DEPTNAME, repdept.Name, sum(repdept.costoservicio) costoservicio " +
                                   " from (select distinct du.Badgenumber, dd.DEPTNAME, da.Name, dcc.costoservicio, dc.fecha, dc.ultm_reg " +
                                   " from DEPARTMENTS dd, " +
                                   " USERINFO du, " +
                                   " casino dc, " +
                                   " casino_costos dcc, " +
                                   " ACTimeZones da " +
                                   " where dd.DEPTID = " + varcdpto +
                                   " and dd.DEPTID = du.DEFAULTDEPTID " +
                                   " and du.USERID = dc.iduser " +
                                   " and convert(varchar,dc.fecha,112) >= " + fecini +
                                   " and convert(varchar,dc.fecha,112) <= " + fecfin +
                                   " and dc.servicio = dcc.idcosto " +
                                   " and dc.fecha >= dcc.fecinival " +
                                   " and dc.fecha <= dcc.fecfinval " +
                                   " and dcc.idcosto = da.TimeZoneID) repdept " +
                                   " group by repdept.DEPTNAME, repdept.Name " +
                                   " order by repdept.DEPTNAME";

                SqlCommand cmd = new SqlCommand(consulta, f2conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string bddpto = Convert.ToString(reader[0]);
                        string bdservicio = Convert.ToString(reader[1]);
                        int bdcostoserv = Convert.ToInt32(reader[2]);
                        CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
                        string pasomiles = (bdcostoserv.ToString("0,0", elGR));
                        dataGridView2.Rows.Add(bddpto, bdservicio, pasomiles);
                    }
                }
                else
                {
                    countfalla = countfalla + 1;
                    reader.Close();
                }
                reader.Close();
                f2conn.Close();
            }
            catch (Exception)
            {
                errorconnbd();
            }

        }

        private void reporte3(int cserv)
        {
            varcserv = cserv;

            try
            {
                f2conectarbd();

                String consulta = "select da.Name, dd.DEPTNAME, sum(dcc.costoservicio) costoservicio, dc.ultm_reg " +
                                   " from DEPARTMENTS dd, " +
                                        " USERINFO du, " +
                                        " casino dc, " +
                                        " casino_costos dcc, " +
                                        " ACTimeZones da " +
                                   " where dd.DEPTID = du.DEFAULTDEPTID " +
                                   " and du.USERID = dc.iduser " +
                                   " and convert(varchar,dc.fecha,112) >= " + fecini +
                                   " and convert(varchar,dc.fecha,112) <= " + fecfin +
                                   " and dc.servicio = dcc.idcosto " +
                                   " and dcc.idcosto = da.TimeZoneID " +
                                   " and da.TimeZoneID = " + varcserv +
                                   " group by dd.DEPTNAME, da.Name " +
                                   " order by dd.DEPTNAME";
                SqlCommand cmd = new SqlCommand(consulta, f2conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string bddpto = Convert.ToString(reader[0]);
                        string bdservicio = Convert.ToString(reader[1]);
                        int bdcostoserv = Convert.ToInt32(reader[2]);
                        CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
                        string pasomiles = (bdcostoserv.ToString("0,0", elGR));
                        dataGridView3.Rows.Add(bddpto, bdservicio, pasomiles);
                    }
                }
                else
                {
                    countfalla = countfalla + 1;
                    reader.Close();
                }
                reader.Close();
                f2conn.Close();
            }
            catch (Exception)
            {
                errorconnbd();
            }

        }

        private void reporte4(int cserv4)
        {
            varcserv4 = cserv4;

            try
            {
                f2conectarbd();

                String consulta = "select repserv.Name, repserv.DEPTNAME, count(repserv.iduser) #Servicios, sum(repserv.costoservicio) costoservicio " +
                                   " from (select distinct du.Badgenumber, dd.DEPTNAME, da.Name, dcc.costoservicio, dc.fecha, dc.iduser, dc.ultm_reg " +
                                   " from DEPARTMENTS dd, " +
                                   " USERINFO du, " +
                                   " casino dc, " +
                                   " casino_costos dcc, " +
                                   " ACTimeZones da " +
                                   " where dd.DEPTID = du.DEFAULTDEPTID " +
                                   " and du.USERID = dc.iduser " +
                                   " and convert(varchar,dc.fecha,112) >= " + fecini +
                                   " and convert(varchar,dc.fecha,112) <= " + fecfin +
                                   " and dc.servicio = dcc.idcosto " +
                                   " and dc.fecha >= dcc.fecinival " +
                                   " and dc.fecha <= dcc.fecfinval " +
                                   " and dcc.idcosto = da.TimeZoneID " +
                                   " and da.TimeZoneID = " + varcserv4 + ") repserv " +
                                   " group by repserv.DEPTNAME, repserv.Name " +
                                   " order by repserv.DEPTNAME";

                SqlCommand cmd = new SqlCommand(consulta, f2conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string bddpto = Convert.ToString(reader[0]);
                        string bdservicio = Convert.ToString(reader[1]);
                        string bdnumserv = Convert.ToString(reader[2]);
                        int bdcostoserv = Convert.ToInt32(reader[3]);
                        CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
                        string pasomiles = (bdcostoserv.ToString("0,0", elGR));
                        dataGridView4.Rows.Add(bddpto, bdservicio, bdnumserv, pasomiles);
                    }
                }
                else
                {
                    countfalla = countfalla + 1;
                    reader.Close();
                }
                reader.Close();
                f2conn.Close();
            }
            catch (Exception)
            {
                errorconnbd();
            }

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DateTime validafec = dateTimePicker2.Value;
            DateTime validahoy = DateTime.Now;

            if (validafec > validahoy)
            {
                MessageBox.Show("La fecha fin no puede ser mayor a hoy", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                DateTime now = DateTime.Now;
                dateTimePicker2.Format = DateTimePickerFormat.Short;
                dateTimePicker2.Value = new DateTime(now.Year, now.Month, now.Day);

                /*dateTimePicker2.Format = DateTimePickerFormat.Short;
                dateTimePicker2.Value = new DateTime(2015, 01, 01);*/
            }

            date2 = dateTimePicker2.Value;
            fecfin = date2.ToString("yyyyMMdd");
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime validafec = dateTimePicker1.Value;
            DateTime validahoy = DateTime.Now;

            if (validafec > validahoy)
            {
                MessageBox.Show("La fecha inicio no puede ser mayor a hoy", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                DateTime now = DateTime.Now;
                var fechainico = new DateTime(now.Year, now.Month, 1);

                dateTimePicker1.Format = DateTimePickerFormat.Short;
                dateTimePicker1.Value = new DateTime(fechainico.Year, fechainico.Month, fechainico.Day);

                /*dateTimePicker1.Format = DateTimePickerFormat.Short;
                dateTimePicker1.Value = new DateTime(2015, 01, 01);*/
            }

            date1 = dateTimePicker1.Value;
            fecini = date1.ToString("yyyyMMdd");
        }

        private void cargarobjetos()
        {
            DateTime now = DateTime.Now;
            var fechainico = new DateTime(now.Year, now.Month, 1);

            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Value = new DateTime(fechainico.Year, fechainico.Month, fechainico.Day);
            dateTimePicker2.Format = DateTimePickerFormat.Short;
            dateTimePicker2.Value = new DateTime(now.Year, now.Month, now.Day);

            label1.Show();
            label2.Show();
            label3.Show();
            label4.Show();
            label5.Show();
            dateTimePicker1.Show();
            dateTimePicker2.Show();
            radioButton1.Show();
            radioButton2.Show();
            radioButton3.Show();
            checkedListBox1.Show();
            checkedListBox2.Show();
            checkedListBox3.Show();
            button1.Show();
            button2.Show();
            button3.Show();
            radioButton4.Show();
            radioButton5.Show();
            groupBox1.Show();
            groupBox2.Show();
            groupBox3.Show();
            groupBox4.Show();
        }

        private void limpiarobjetos()
        {
            label1.Hide();
            label2.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            dateTimePicker1.Hide();
            dateTimePicker2.Hide();
            radioButton1.Hide();
            radioButton2.Hide();
            radioButton3.Hide();
            radioButton4.Hide();
            radioButton5.Hide();
            checkedListBox1.Hide();
            checkedListBox2.Hide();
            checkedListBox3.Hide();
            dataGridView1.Hide();
            dataGridView2.Hide();
            dataGridView3.Hide();
            dataGridView4.Hide();
            button1.Hide();
            button2.Hide();
            button3.Hide();
            groupBox1.Hide();
            groupBox2.Hide();
            groupBox3.Hide();
            groupBox4.Hide();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            checkedListBox1.Items.Clear();
            checkedListBox2.Items.Clear();
            checkedListBox3.Items.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;
            limpiarobjetos();
            validachecked = 0;
            button1.Enabled = false;
            pictureBox1.Show();
            button4.Visible = false;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            dataGridView4.Rows.Clear();
            dataGridView1.Hide();
            dataGridView2.Hide();
            dataGridView3.Hide();
            dataGridView4.Hide();
            button1.Enabled = false;
            validachecked = 0;
            if (sale1 == 0)
            {
                radioButton2.Checked = false;
                radioButton3.Checked = false;

                try
                {
                    f2conectarbd();

                    String consulta = "select distinct DEPTID, DEPTNAME from DEPARTMENTS order by DEPTID";
                    SqlCommand cmd = new SqlCommand(consulta, f2conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string bddptoid = Convert.ToString(reader[0]);
                            string bddeptname = Convert.ToString(reader[1]);
                            checkedListBox1.Items.Add(bddptoid + "-" + bddeptname, CheckState.Unchecked);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No existen usuarios", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        reader.Close();
                    }
                    reader.Close();
                    f2conn.Close();
                    sale1 = 1;
                }
                catch (Exception)
                {
                    errorconnbd();
                }
            }
            else
            {
                checkedListBox1.Items.Clear();
                radioButton1.Enabled = false;
                radioButton4.Enabled = false;
                sale1 = 0;
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (validachecked == 0)
            {  
                if (checkedListBox1.CheckedItems.Count != 0)
                {
                    for (int x = 0; x <= checkedListBox1.CheckedItems.Count - 1; x++)
                    {
                        string valor = checkedListBox1.CheckedItems[x].ToString();
                        string[] campo1 = valor.Split('-');
                        pasouser = Convert.ToInt32(campo1[0]);
                    }
                }
                
                radioButton1.Enabled = true;
                radioButton4.Enabled = true;
                cargausuarios(pasouser);
            }
        }

        private void cargausuarios(int coddepto)
        {
            validachecked = 1;
            int reccoddpto = coddepto;

            try
            {
                f2conectarbd();

                String consulta = "select distinct bu.badgenumber, bu.Name " +
                                  "from USERINFO bu, " +
                                  "DEPARTMENTS bd " +
                                  "where bd.DEPTID = " + reccoddpto +
                                  "and bd.DEPTID = bu.DEFAULTDEPTID";
                SqlCommand cmd = new SqlCommand(consulta, f2conn);
                SqlDataReader reader = cmd.ExecuteReader();

                checkedListBox1.Items.Clear();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string bdiduser = Convert.ToString(reader[0]);
                        string bdname = Convert.ToString(reader[1]);

                        checkedListBox1.Items.Add(bdiduser + "-" + bdname, CheckState.Unchecked);
                    }
                }
                else
                {
                    MessageBox.Show("No existen usuarios para departamento seleccionado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reader.Close();
                }

                reader.Close();
                f2conn.Close();
            }
            catch (Exception)
            {
                errorconnbd();
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (validachecked4 == 0)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, true);
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (validachecked1 == 0)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, false);
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Form5 frm5 = new Form5();
            frm5.ShowDialog();
            valorexport = frm5.varf2;
            frm5folderName = frm5.varf3;

            if (valorexport == 1)
            {
                exportarpdf();
            }
            if (valorexport == 2)
            {
                exportarexcel();
            }

        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 info = new Form6();
            info.ShowDialog();
        }

        private void exportarpdf()
        {
            shora = DateTime.Now.ToString("HHmmss");

            if (tiporeporte == 1 || tiporeporte == 2)
            {
                string fechanomi = dateTimePicker1.Value.Date.ToString("yyyyMMdd");
                string fechanomf = dateTimePicker2.Value.Date.ToString("yyyyMMdd");

                Document doc = new Document(PageSize.LETTER);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@frm5folderName + "\\RepCostosxEmpleado_" + shora + "-" + fechanomi + "-" + fechanomf + ".pdf", FileMode.Create));
                doc.Open();

                Paragraph tituloreporte = new Paragraph("Reporte Casino - Costo por Empleado", FontFactory.GetFont("Calibri", 20, iTextSharp.text.Font.BOLD));
                tituloreporte.Alignment = Element.ALIGN_CENTER;
                doc.Add(tituloreporte);

                doc.Add(Chunk.NEWLINE);

                Paragraph Fechaimp = new Paragraph("FECHA INICIO: " + dateTimePicker1.Value.Date.ToString("dd-MM-yyyy") + "  a  FECHA FIN: " + dateTimePicker2.Value.Date.ToString("dd-MM-yyyy"), FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD));
                Fechaimp.Alignment = Element.ALIGN_CENTER;
                doc.Add(Fechaimp);

                DateTime rephoy = DateTime.Now;
                Paragraph Fecharep = new Paragraph("Fecha Creación Reporte: " + rephoy, FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD));
                Fecharep.Alignment = Element.ALIGN_CENTER;
                doc.Add(Fecharep);

                doc.Add(Chunk.NEWLINE);


                PdfPTable tblPrueba = new PdfPTable(5);
                tblPrueba.WidthPercentage = 100;

                string headerText1 = dataGridView1.Columns[0].HeaderText;
                string headerText2 = dataGridView1.Columns[1].HeaderText;
                string headerText3 = dataGridView1.Columns[2].HeaderText;
                string headerText4 = dataGridView1.Columns[3].HeaderText;
                string headerText5 = dataGridView1.Columns[4].HeaderText;
                string headerText6 = dataGridView1.Columns[5].HeaderText;
                string headerText7 = dataGridView1.Columns[6].HeaderText;
                string headerText8 = dataGridView1.Columns[7].HeaderText;
                string headerText9 = dataGridView1.Columns[8].HeaderText;
                string headerText10 = dataGridView1.Columns[9].HeaderText;

                PdfPCell clheader1 = new PdfPCell(new Phrase(headerText1, FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD)));
                PdfPCell clheader2 = new PdfPCell(new Phrase(headerText2, FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD)));
                PdfPCell clheader3 = new PdfPCell(new Phrase(headerText3, FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD)));
                PdfPCell clheader4 = new PdfPCell(new Phrase(headerText4, FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD)));
                PdfPCell clheader5 = new PdfPCell(new Phrase(headerText5, FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD)));
                PdfPCell clheader6 = new PdfPCell(new Phrase(headerText6, FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD)));
                PdfPCell clheader7 = new PdfPCell(new Phrase(headerText7, FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD)));
                PdfPCell clheader8 = new PdfPCell(new Phrase(headerText8, FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD)));
                PdfPCell clheader9 = new PdfPCell(new Phrase(headerText9, FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD)));
                PdfPCell clheader10 = new PdfPCell(new Phrase(headerText10, FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD)));

                tblPrueba.AddCell(clheader1);
                tblPrueba.AddCell(clheader2);
                tblPrueba.AddCell(clheader3);
                tblPrueba.AddCell(clheader4);
                tblPrueba.AddCell(clheader5);
                tblPrueba.AddCell(clheader6);
                tblPrueba.AddCell(clheader7);
                tblPrueba.AddCell(clheader8);
                tblPrueba.AddCell(clheader9);
                tblPrueba.AddCell(clheader10);

                foreach (DataGridViewRow row1 in dataGridView1.Rows)
                {
                    datodg1 = Convert.ToString(row1.Cells[0].Value);
                    datodg2 = Convert.ToString(row1.Cells[1].Value);
                    datodg3 = Convert.ToString(row1.Cells[2].Value);
                    datodg4 = Convert.ToString(row1.Cells[3].Value);
                    datodg5 = Convert.ToString(row1.Cells[4].Value);
                    datodg6 = Convert.ToString(row1.Cells[5].Value);
                    datodg7 = Convert.ToString(row1.Cells[6].Value);
                    datodg8 = Convert.ToString(row1.Cells[7].Value);
                    datodg9 = Convert.ToString(row1.Cells[8].Value);
                    datodg10 = Convert.ToString(row1.Cells[9].Value);

                    clheader1 = new PdfPCell(new Phrase(datodg1, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));
                    clheader2 = new PdfPCell(new Phrase(datodg2, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));
                    clheader3 = new PdfPCell(new Phrase(datodg3, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));
                    clheader4 = new PdfPCell(new Phrase(datodg4, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));
                    clheader5 = new PdfPCell(new Phrase(datodg5, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));
                    clheader6 = new PdfPCell(new Phrase(datodg6, FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL)));
                    clheader7 = new PdfPCell(new Phrase(datodg7, FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL)));
                    clheader8 = new PdfPCell(new Phrase(datodg8, FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL)));
                    clheader9 = new PdfPCell(new Phrase(datodg9, FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL)));
                    clheader10 = new PdfPCell(new Phrase(datodg10, FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.NORMAL)));

                    tblPrueba.AddCell(clheader1);
                    tblPrueba.AddCell(clheader2);
                    tblPrueba.AddCell(clheader3);
                    tblPrueba.AddCell(clheader4);
                    tblPrueba.AddCell(clheader5);
                    tblPrueba.AddCell(clheader6);
                    tblPrueba.AddCell(clheader7);
                    tblPrueba.AddCell(clheader8);
                    tblPrueba.AddCell(clheader9);
                    tblPrueba.AddCell(clheader10);

                }

                doc.Add(tblPrueba);

                doc.Close();
                writer.Close();

            }

            if (tiporeporte == 3)
            {
                string fechanomi = dateTimePicker1.Value.Date.ToString("yyyyMMdd");
                string fechanomf = dateTimePicker2.Value.Date.ToString("yyyyMMdd");

                Document doc = new Document(PageSize.LETTER);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@frm5folderName + "\\RepCostosxDpto_" + shora + "-" + fechanomi + "-" + fechanomf + ".pdf", FileMode.Create));
                doc.Open();

                Paragraph tituloreporte = new Paragraph("Reporte Casino - Costos por Departamento", FontFactory.GetFont("Calibri", 20, iTextSharp.text.Font.BOLD));
                tituloreporte.Alignment = Element.ALIGN_CENTER;
                doc.Add(tituloreporte);

                doc.Add(Chunk.NEWLINE);

                Paragraph Fechaimp = new Paragraph("FECHA INICIO: " + dateTimePicker1.Value.Date.ToString("dd-MM-yyyy") + "  a  FECHA FIN: " + dateTimePicker2.Value.Date.ToString("dd-MM-yyyy"), FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD));
                Fechaimp.Alignment = Element.ALIGN_CENTER;
                doc.Add(Fechaimp);

                DateTime rephoy = DateTime.Now;
                Paragraph Fecharep = new Paragraph("Fecha Creación Reporte: " + rephoy, FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD));
                Fecharep.Alignment = Element.ALIGN_CENTER;
                doc.Add(Fecharep);


                doc.Add(Chunk.NEWLINE);


                PdfPTable tblPrueba = new PdfPTable(3);
                tblPrueba.WidthPercentage = 100;

                string headerText1 = dataGridView2.Columns[0].HeaderText;
                string headerText2 = dataGridView2.Columns[1].HeaderText;
                string headerText3 = dataGridView2.Columns[2].HeaderText;

                PdfPCell clheader1 = new PdfPCell(new Phrase(headerText1, FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD)));
                PdfPCell clheader2 = new PdfPCell(new Phrase(headerText2, FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD)));
                PdfPCell clheader3 = new PdfPCell(new Phrase(headerText3, FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD)));

                tblPrueba.AddCell(clheader1);
                tblPrueba.AddCell(clheader2);
                tblPrueba.AddCell(clheader3);

                foreach (DataGridViewRow row2 in dataGridView2.Rows)
                {
                    datodg1 = Convert.ToString(row2.Cells[0].Value);
                    datodg2 = Convert.ToString(row2.Cells[1].Value);
                    datodg3 = Convert.ToString(row2.Cells[2].Value);

                    suma += Convert.ToDouble(datodg3);

                    if (datodg1 == cortereporte)
                    {
                        clheader1 = new PdfPCell(new Phrase(datodg1, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));
                        clheader2 = new PdfPCell(new Phrase(datodg2, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));
                        clheader3 = new PdfPCell(new Phrase(datodg3, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));

                        tblPrueba.AddCell(clheader1);
                        tblPrueba.AddCell(clheader2);
                        tblPrueba.AddCell(clheader3);
                    }
                    else
                    {
                        if (cortereporte2 == 0)
                        {
                            clheader1 = new PdfPCell(new Phrase(datodg1, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));
                            clheader2 = new PdfPCell(new Phrase(datodg2, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));
                            clheader3 = new PdfPCell(new Phrase(datodg3, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));

                            tblPrueba.AddCell(clheader1);
                            tblPrueba.AddCell(clheader2);
                            tblPrueba.AddCell(clheader3);
                            cortereporte = datodg1;
                            cortereporte2 = 1;
                        }
                        else
                        {
                            suma = suma - Convert.ToDouble(datodg3);
                            CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
                            string pasomiles = (suma.ToString("0,0", elGR));

                            Paragraph subtotalcosto = new Paragraph("Subtotal", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD));
                            Paragraph subtotalmiles = new Paragraph(pasomiles, FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD));
                            subtotalcosto.Alignment = Element.ALIGN_RIGHT;

                            subtotalcosto.Alignment = Element.ALIGN_RIGHT;

                            sumatotal += suma;

                            tblPrueba.AddCell("");
                            tblPrueba.AddCell(subtotalcosto);
                            tblPrueba.AddCell(subtotalmiles);

                            tblPrueba.AddCell("\n");
                            tblPrueba.AddCell("\n");
                            tblPrueba.AddCell("\n");

                            clheader1 = new PdfPCell(new Phrase(datodg1, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));
                            clheader2 = new PdfPCell(new Phrase(datodg2, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));
                            clheader3 = new PdfPCell(new Phrase(datodg3, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));

                            tblPrueba.AddCell(clheader1);
                            tblPrueba.AddCell(clheader2);
                            tblPrueba.AddCell(clheader3);

                            suma = Convert.ToDouble(datodg3);

                            cortereporte = datodg1;
                        }
                    }
                }

                CultureInfo elGRSF = CultureInfo.CreateSpecificCulture("el-GR");
                string pasomilesSf = (suma.ToString("0,0", elGRSF));

                Paragraph subtotalcostof = new Paragraph("Subtotal", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD));
                Paragraph subtotalmilesf = new Paragraph(pasomilesSf, FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD));
                subtotalcostof.Alignment = Element.ALIGN_RIGHT;

                tblPrueba.AddCell("");
                tblPrueba.AddCell(subtotalcostof);
                tblPrueba.AddCell(subtotalmilesf);

                tblPrueba.AddCell("\n");
                tblPrueba.AddCell("\n");
                tblPrueba.AddCell("\n");

                sumatotal += suma;
                CultureInfo elGRF = CultureInfo.CreateSpecificCulture("el-GR");
                string pasomilesf = (sumatotal.ToString("0,0", elGRF));

                Paragraph total = new Paragraph("Total", FontFactory.GetFont("Calibri", 15, iTextSharp.text.Font.BOLD));
                Paragraph totalmiles = new Paragraph(pasomilesf, FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD));
                total.Alignment = Element.ALIGN_RIGHT;

                tblPrueba.AddCell("");
                tblPrueba.AddCell(total);
                tblPrueba.AddCell(totalmiles);

                cortereporte = "";
                cortereporte2 = 0;
                suma = 0;
                sumatotal = 0;

                doc.Add(tblPrueba);

                doc.Close();
                writer.Close();
            }


            if (tiporeporte == 4)
            {
                string fechanomi = dateTimePicker1.Value.Date.ToString("yyyyMMdd");
                string fechanomf = dateTimePicker2.Value.Date.ToString("yyyyMMdd");

                Document doc = new Document(PageSize.LETTER);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@frm5folderName + "\\RepCostosxServ_" + shora + "-" + fechanomi + "-" + fechanomf + ".pdf", FileMode.Create));
                doc.Open();

                Paragraph tituloreporte = new Paragraph("Reporte Casino - Costo por Servicio (#Servicios)", FontFactory.GetFont("Calibri", 20, iTextSharp.text.Font.BOLD));
                tituloreporte.Alignment = Element.ALIGN_CENTER;
                doc.Add(tituloreporte);

                doc.Add(Chunk.NEWLINE);

                Paragraph Fechaimp = new Paragraph("FECHA INICIO: " + dateTimePicker1.Value.Date.ToString("dd-MM-yyyy") + "  a  FECHA FIN: " + dateTimePicker2.Value.Date.ToString("dd-MM-yyyy"), FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD));
                Fechaimp.Alignment = Element.ALIGN_CENTER;
                doc.Add(Fechaimp);

                DateTime rephoy = DateTime.Now;
                Paragraph Fecharep = new Paragraph("Fecha Creación Reporte: " + rephoy, FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD));
                Fecharep.Alignment = Element.ALIGN_CENTER;
                doc.Add(Fecharep);


                doc.Add(Chunk.NEWLINE);


                PdfPTable tblPrueba = new PdfPTable(4);
                tblPrueba.WidthPercentage = 100;

                string headerText1 = dataGridView4.Columns[0].HeaderText;
                string headerText2 = dataGridView4.Columns[1].HeaderText;
                string headerText3 = dataGridView4.Columns[2].HeaderText;
                string headerText4 = dataGridView4.Columns[3].HeaderText;

                PdfPCell clheader1 = new PdfPCell(new Phrase(headerText1, FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD)));
                PdfPCell clheader2 = new PdfPCell(new Phrase(headerText2, FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD)));
                PdfPCell clheader3 = new PdfPCell(new Phrase(headerText3, FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD)));
                PdfPCell clheader4 = new PdfPCell(new Phrase(headerText4, FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD)));

                tblPrueba.AddCell(clheader1);
                tblPrueba.AddCell(clheader2);
                tblPrueba.AddCell(clheader3);
                tblPrueba.AddCell(clheader4);

                foreach (DataGridViewRow row4 in dataGridView4.Rows)
                {
                    datodg1 = Convert.ToString(row4.Cells[0].Value);
                    datodg2 = Convert.ToString(row4.Cells[1].Value);
                    datodg3 = Convert.ToString(row4.Cells[2].Value);
                    datodg4 = Convert.ToString(row4.Cells[3].Value);

                    suma += Convert.ToDouble(datodg4);

                    if (datodg1 == cortereporte)
                    {
                        clheader1 = new PdfPCell(new Phrase(datodg1, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));
                        clheader2 = new PdfPCell(new Phrase(datodg2, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));
                        clheader3 = new PdfPCell(new Phrase(datodg3, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));
                        clheader4 = new PdfPCell(new Phrase(datodg4, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));

                        tblPrueba.AddCell(clheader1);
                        tblPrueba.AddCell(clheader2);
                        tblPrueba.AddCell(clheader3);
                        tblPrueba.AddCell(clheader4);
                    }
                    else
                    {
                        if (cortereporte2 == 0)
                        {
                            clheader1 = new PdfPCell(new Phrase(datodg1, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));
                            clheader2 = new PdfPCell(new Phrase(datodg2, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));
                            clheader3 = new PdfPCell(new Phrase(datodg3, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));
                            clheader4 = new PdfPCell(new Phrase(datodg4, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));

                            tblPrueba.AddCell(clheader1);
                            tblPrueba.AddCell(clheader2);
                            tblPrueba.AddCell(clheader3);
                            tblPrueba.AddCell(clheader4);
                            cortereporte = datodg1;
                            cortereporte2 = 1;
                        }
                        else
                        {
                            suma = suma - Convert.ToDouble(datodg4);
                            CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
                            string pasomiles = (suma.ToString("0,0", elGR));

                            Paragraph subtotalcosto = new Paragraph("Subtotal", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD));
                            Paragraph subtotalmiles = new Paragraph(pasomiles, FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD));
                            subtotalcosto.Alignment = Element.ALIGN_RIGHT;

                            subtotalcosto.Alignment = Element.ALIGN_RIGHT;

                            sumatotal += suma;

                            tblPrueba.AddCell("");
                            tblPrueba.AddCell("");
                            tblPrueba.AddCell(subtotalcosto);
                            tblPrueba.AddCell(subtotalmiles);

                            tblPrueba.AddCell("\n");
                            tblPrueba.AddCell("\n");
                            tblPrueba.AddCell("\n");
                            tblPrueba.AddCell("\n");

                            clheader1 = new PdfPCell(new Phrase(datodg1, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));
                            clheader2 = new PdfPCell(new Phrase(datodg2, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));
                            clheader3 = new PdfPCell(new Phrase(datodg3, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));
                            clheader4 = new PdfPCell(new Phrase(datodg4, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));

                            tblPrueba.AddCell(clheader1);
                            tblPrueba.AddCell(clheader2);
                            tblPrueba.AddCell(clheader3);
                            tblPrueba.AddCell(clheader4);

                            suma = Convert.ToDouble(datodg4);

                            cortereporte = datodg1;
                        }
                    }
                }

                CultureInfo elGRSF = CultureInfo.CreateSpecificCulture("el-GR");
                string pasomilesSf = (suma.ToString("0,0", elGRSF));

                Paragraph subtotalcostof = new Paragraph("Subtotal", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD));
                Paragraph subtotalmilesf = new Paragraph(pasomilesSf, FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD));
                subtotalcostof.Alignment = Element.ALIGN_RIGHT;

                tblPrueba.AddCell("");
                tblPrueba.AddCell("");
                tblPrueba.AddCell(subtotalcostof);
                tblPrueba.AddCell(subtotalmilesf);

                tblPrueba.AddCell("\n");
                tblPrueba.AddCell("\n");
                tblPrueba.AddCell("\n");
                tblPrueba.AddCell("\n");

                sumatotal += suma;
                CultureInfo elGRF = CultureInfo.CreateSpecificCulture("el-GR");
                string pasomilesf = (sumatotal.ToString("0,0", elGRF));

                Paragraph total = new Paragraph("Total", FontFactory.GetFont("Calibri", 15, iTextSharp.text.Font.BOLD));
                Paragraph totalmiles = new Paragraph(pasomilesf, FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD));
                total.Alignment = Element.ALIGN_RIGHT;

                tblPrueba.AddCell("");
                tblPrueba.AddCell("");
                tblPrueba.AddCell(total);
                tblPrueba.AddCell(totalmiles);

                cortereporte = "";
                cortereporte2 = 0;
                suma = 0;
                sumatotal = 0;

                doc.Add(tblPrueba);

                doc.Close();
                writer.Close();

            }

            if (tiporeporte == 5)
            {
                string fechanomi = dateTimePicker1.Value.Date.ToString("yyyyMMdd");
                string fechanomf = dateTimePicker2.Value.Date.ToString("yyyyMMdd");

                Document doc = new Document(PageSize.LETTER);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@frm5folderName + "\\RepCostosxServ_" + shora + "-" + fechanomi + "-" + fechanomf + ".pdf", FileMode.Create));
                doc.Open();

                Paragraph tituloreporte = new Paragraph("Reporte Casino - Costo por Servicio", FontFactory.GetFont("Calibri", 20, iTextSharp.text.Font.BOLD));
                tituloreporte.Alignment = Element.ALIGN_CENTER;
                doc.Add(tituloreporte);

                doc.Add(Chunk.NEWLINE);

                Paragraph Fechaimp = new Paragraph("FECHA INICIO: " + dateTimePicker1.Value.Date.ToString("dd-MM-yyyy") + "  a  FECHA FIN: " + dateTimePicker2.Value.Date.ToString("dd-MM-yyyy"), FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD));
                Fechaimp.Alignment = Element.ALIGN_CENTER;
                doc.Add(Fechaimp);

                DateTime rephoy = DateTime.Now;
                Paragraph Fecharep = new Paragraph("Fecha Creación Reporte: " + rephoy, FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD));
                Fecharep.Alignment = Element.ALIGN_CENTER;
                doc.Add(Fecharep);


                doc.Add(Chunk.NEWLINE);


                PdfPTable tblPrueba = new PdfPTable(3);
                tblPrueba.WidthPercentage = 100;

                string headerText1 = dataGridView2.Columns[0].HeaderText;
                string headerText2 = dataGridView2.Columns[1].HeaderText;
                string headerText3 = dataGridView2.Columns[2].HeaderText;

                PdfPCell clheader1 = new PdfPCell(new Phrase(headerText1, FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD)));
                PdfPCell clheader2 = new PdfPCell(new Phrase(headerText2, FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD)));
                PdfPCell clheader3 = new PdfPCell(new Phrase(headerText3, FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD)));

                tblPrueba.AddCell(clheader1);
                tblPrueba.AddCell(clheader2);
                tblPrueba.AddCell(clheader3);

                foreach (DataGridViewRow row3 in dataGridView3.Rows)
                {
                    datodg1 = Convert.ToString(row3.Cells[0].Value);
                    datodg2 = Convert.ToString(row3.Cells[1].Value);
                    datodg3 = Convert.ToString(row3.Cells[2].Value);

                    suma += Convert.ToDouble(datodg3);

                    if (datodg2 == cortereporte)
                    {
                        clheader1 = new PdfPCell(new Phrase(datodg1, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));
                        clheader2 = new PdfPCell(new Phrase(datodg2, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));
                        clheader3 = new PdfPCell(new Phrase(datodg3, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));

                        tblPrueba.AddCell(clheader1);
                        tblPrueba.AddCell(clheader2);
                        tblPrueba.AddCell(clheader3);
                    }
                    else
                    {
                        if (cortereporte2 == 0)
                        {
                            clheader1 = new PdfPCell(new Phrase(datodg1, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));
                            clheader2 = new PdfPCell(new Phrase(datodg2, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));
                            clheader3 = new PdfPCell(new Phrase(datodg3, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));

                            tblPrueba.AddCell(clheader1);
                            tblPrueba.AddCell(clheader2);
                            tblPrueba.AddCell(clheader3);
                            cortereporte = datodg2;
                            cortereporte2 = 1;
                        }
                        else
                        {
                            suma = suma - Convert.ToDouble(datodg3);
                            CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
                            string pasomiles = (suma.ToString("0,0", elGR));

                            Paragraph subtotalcosto = new Paragraph("Subtotal", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD));
                            Paragraph subtotalmiles = new Paragraph(pasomiles, FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD));
                            subtotalcosto.Alignment = Element.ALIGN_RIGHT;

                            subtotalcosto.Alignment = Element.ALIGN_RIGHT;

                            sumatotal += suma;

                            tblPrueba.AddCell("");
                            tblPrueba.AddCell(subtotalcosto);
                            tblPrueba.AddCell(subtotalmiles);

                            tblPrueba.AddCell("\n");
                            tblPrueba.AddCell("\n");
                            tblPrueba.AddCell("\n");

                            clheader1 = new PdfPCell(new Phrase(datodg1, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));
                            clheader2 = new PdfPCell(new Phrase(datodg2, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));
                            clheader3 = new PdfPCell(new Phrase(datodg3, FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)));

                            tblPrueba.AddCell(clheader1);
                            tblPrueba.AddCell(clheader2);
                            tblPrueba.AddCell(clheader3);

                            suma = Convert.ToDouble(datodg3);

                            cortereporte = datodg2;
                        }
                    }
                }

                CultureInfo elGRSF = CultureInfo.CreateSpecificCulture("el-GR");
                string pasomilesSf = (suma.ToString("0,0", elGRSF));

                Paragraph subtotalcostof = new Paragraph("Subtotal", FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD));
                Paragraph subtotalmilesf = new Paragraph(pasomilesSf, FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD));
                subtotalcostof.Alignment = Element.ALIGN_RIGHT;

                tblPrueba.AddCell("");
                tblPrueba.AddCell(subtotalcostof);
                tblPrueba.AddCell(subtotalmilesf);

                tblPrueba.AddCell("\n");
                tblPrueba.AddCell("\n");
                tblPrueba.AddCell("\n");

                sumatotal += suma;
                CultureInfo elGRF = CultureInfo.CreateSpecificCulture("el-GR");
                string pasomilesf = (sumatotal.ToString("0,0", elGRF));

                Paragraph total = new Paragraph("Total", FontFactory.GetFont("Calibri", 15, iTextSharp.text.Font.BOLD));
                Paragraph totalmiles = new Paragraph(pasomilesf, FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD));
                total.Alignment = Element.ALIGN_RIGHT;

                tblPrueba.AddCell("");
                tblPrueba.AddCell(total);
                tblPrueba.AddCell(totalmiles);

                cortereporte = "";
                cortereporte2 = 0;
                suma = 0;
                sumatotal = 0;

                doc.Add(tblPrueba);

                doc.Close();
                writer.Close();
            }

            MessageBox.Show("Archivo exportado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void exportarexcel()
        {
            shora = DateTime.Now.ToString("HHmmss");
            if (tiporeporte == 1 || tiporeporte == 2)
            {
                filaexcel = 7;
                colexcel = 2;
                string fechanomi = dateTimePicker1.Value.Date.ToString("yyyyMMdd");
                string fechanomf = dateTimePicker2.Value.Date.ToString("yyyyMMdd");

                DataGridView grd = new DataGridView();

                Microsoft.Office.Interop.Excel.Application aplicacion;
                Microsoft.Office.Interop.Excel.Workbook libros_trabajo;
                Microsoft.Office.Interop.Excel.Worksheet hoja_trabajo;
                aplicacion = new Microsoft.Office.Interop.Excel.Application();
                libros_trabajo = aplicacion.Workbooks.Add();
                hoja_trabajo = (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.get_Item(1);

                hoja_trabajo.Name = "Costo por Empleado";

                hoja_trabajo.Cells[1, 2] = "Reporte Casino - Costo por Empleado";
                hoja_trabajo.Cells[1, 2].Font.Bold = true;
                hoja_trabajo.Cells[1, 2].Font.Size = 24;
                hoja_trabajo.Cells[1, 2].Font.Underline = true;
                hoja_trabajo.Range["b1:f1"].Merge();
                Excel.Range rng5 = (Excel.Range)hoja_trabajo.get_Range("b1");
                rng5.HorizontalAlignment = 3;

                hoja_trabajo.Cells[3, 2] = "FECHA INICIO: " + dateTimePicker1.Value.Date.ToString("dd-MM-yyyy") + "  a  FECHA FIN: " + dateTimePicker2.Value.Date.ToString("dd-MM-yyyy");
                hoja_trabajo.Cells[3, 2].Font.Bold = true;
                hoja_trabajo.Range["b3:f3"].Merge();
                Excel.Range rng6 = (Excel.Range)hoja_trabajo.get_Range("b3");
                rng6.HorizontalAlignment = 3;

                DateTime rephoyex = DateTime.Now;
                hoja_trabajo.Cells[4, 2] = "Fecha Creación Reporte: " + rephoyex;
                hoja_trabajo.Cells[4, 2].Font.Bold = true;
                hoja_trabajo.Range["b4:f4"].Merge();
                Excel.Range rng7 = (Excel.Range)hoja_trabajo.get_Range("b4");
                rng7.HorizontalAlignment = 3;

                string headerText1 = dataGridView1.Columns[0].HeaderText;
                string headerText2 = dataGridView1.Columns[1].HeaderText;
                string headerText3 = dataGridView1.Columns[2].HeaderText;
                string headerText4 = dataGridView1.Columns[3].HeaderText;
                string headerText5 = dataGridView1.Columns[4].HeaderText;
                string headerText6 = dataGridView1.Columns[5].HeaderText;
                string headerText7 = dataGridView1.Columns[6].HeaderText;
                string headerText8 = dataGridView1.Columns[7].HeaderText;
                string headerText9 = dataGridView1.Columns[8].HeaderText;
                string headerText10 = dataGridView1.Columns[9].HeaderText;

                hoja_trabajo.Cells.EntireColumn.ColumnWidth = 17;

                hoja_trabajo.Cells[6, 2] = headerText1;
                hoja_trabajo.Cells[6, 2].Font.Bold = true;
                hoja_trabajo.Cells[6, 2].Font.Size = 12;
                hoja_trabajo.Cells[6, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                hoja_trabajo.Cells[6, 3] = headerText2;
                hoja_trabajo.Cells[6, 3].Font.Bold = true;
                hoja_trabajo.Cells[6, 3].Font.Size = 12;
                hoja_trabajo.Cells[6, 3].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                
                hoja_trabajo.Cells[6, 4] = headerText3;
                hoja_trabajo.Cells[6, 4].Font.Bold = true;
                hoja_trabajo.Cells[6, 4].Font.Size = 12;
                hoja_trabajo.Cells[6, 4].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                
                hoja_trabajo.Cells[6, 5] = headerText4;
                hoja_trabajo.Cells[6, 5].Font.Bold = true;
                hoja_trabajo.Cells[6, 5].Font.Size = 12;
                hoja_trabajo.Cells[6, 5].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                
                hoja_trabajo.Cells[6, 6] = headerText5;
                hoja_trabajo.Cells[6, 6].Font.Bold = true;
                hoja_trabajo.Cells[6, 6].Font.Size = 12;
                hoja_trabajo.Cells[6, 6].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


                hoja_trabajo.Cells[6, 7] = headerText6;
                hoja_trabajo.Cells[6, 7].Font.Bold = true;
                hoja_trabajo.Cells[6, 7].Font.Size = 12;
                hoja_trabajo.Cells[6, 7].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                hoja_trabajo.Cells[6, 8] = headerText7;
                hoja_trabajo.Cells[6, 8].Font.Bold = true;
                hoja_trabajo.Cells[6, 8].Font.Size = 12;
                hoja_trabajo.Cells[6, 8].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                hoja_trabajo.Cells[6, 9] = headerText8;
                hoja_trabajo.Cells[6, 9].Font.Bold = true;
                hoja_trabajo.Cells[6, 9].Font.Size = 12;
                hoja_trabajo.Cells[6, 9].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                hoja_trabajo.Cells[6, 10] = headerText9;
                hoja_trabajo.Cells[6, 10].Font.Bold = true;
                hoja_trabajo.Cells[6, 10].Font.Size = 12;
                hoja_trabajo.Cells[6, 10].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                hoja_trabajo.Cells[6, 11] = headerText10;
                hoja_trabajo.Cells[6, 11].Font.Bold = true;
                hoja_trabajo.Cells[6, 11].Font.Size = 12;
                hoja_trabajo.Cells[6, 11].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                foreach (DataGridViewRow row1 in dataGridView1.Rows)
                {
                    hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToString(row1.Cells[0].Value);
                    hoja_trabajo.Cells[filaexcel, colexcel].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    colexcel = colexcel + 1;
                    hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToString(row1.Cells[1].Value);
                    hoja_trabajo.Cells[filaexcel, colexcel].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    colexcel = colexcel + 1;
                    hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToString(row1.Cells[2].Value);
                    hoja_trabajo.Cells[filaexcel, colexcel].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    colexcel = colexcel + 1;
                    //MessageBox.Show("Explota con " + row1.Cells[3].Value);
                    hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToString(row1.Cells[3].Value);
                    hoja_trabajo.Cells[filaexcel, colexcel].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    colexcel = colexcel + 1;
                    hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToString(row1.Cells[4].Value);
                    hoja_trabajo.Cells[filaexcel, colexcel].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    colexcel = colexcel + 1;

                    hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToString(row1.Cells[5].Value);
                    hoja_trabajo.Cells[filaexcel, colexcel].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    colexcel = colexcel + 1;
                    hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToDecimal(row1.Cells[6].Value);
                    hoja_trabajo.Cells[filaexcel, colexcel].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    colexcel = colexcel + 1;
                    hoja_trabajo.Cells[filaexcel, colexcel].NumberFormat = "@";
                    hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToString(row1.Cells[7].Value);
                    hoja_trabajo.Cells[filaexcel, colexcel].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    colexcel = colexcel + 1;
                    hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToString(row1.Cells[8].Value);
                    hoja_trabajo.Cells[filaexcel, colexcel].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    colexcel = colexcel + 1;
                    hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToString(row1.Cells[9].Value);
                    hoja_trabajo.Cells[filaexcel, colexcel].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    filaexcel = filaexcel + 1;

                    colexcel = 2;

                }

                string fichero = @frm5folderName + "\\";
                string nomarch = "RepCostosxEmpleado_" + shora + "-" + fechanomi + "-" + fechanomf + ".xls";

                libros_trabajo.SaveAs(fichero + nomarch, Excel.XlFileFormat.xlWorkbookNormal);
                libros_trabajo.Close(true);
                aplicacion.Quit();

                MessageBox.Show("Archivo exportado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (tiporeporte == 3)
            {
                filaexcel = 7;
                colexcel = 3;
                string fechanomi = dateTimePicker1.Value.Date.ToString("yyyyMMdd");
                string fechanomf = dateTimePicker2.Value.Date.ToString("yyyyMMdd");

                DataGridView grd = new DataGridView();

                Microsoft.Office.Interop.Excel.Application aplicacion;
                Microsoft.Office.Interop.Excel.Workbook libros_trabajo;
                Microsoft.Office.Interop.Excel.Worksheet hoja_trabajo;
                aplicacion = new Microsoft.Office.Interop.Excel.Application();
                libros_trabajo = aplicacion.Workbooks.Add();
                hoja_trabajo = (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.get_Item(1);

                hoja_trabajo.Name = "Costos por Departamento";

                hoja_trabajo.Cells[1, 2] = "Reporte Casino - Costos por Departamento";
                hoja_trabajo.Cells[1, 2].Font.Bold = true;
                hoja_trabajo.Cells[1, 2].Font.Size = 24;
                hoja_trabajo.Cells[1, 2].Font.Underline = true;
                hoja_trabajo.Range["b1:f1"].Merge();
                Excel.Range rng5 = (Excel.Range)hoja_trabajo.get_Range("b1");
                rng5.HorizontalAlignment = 3;

                hoja_trabajo.Cells[3, 2] = "FECHA INICIO: " + dateTimePicker1.Value.Date.ToString("dd-MM-yyyy") + "  a  FECHA FIN: " + dateTimePicker2.Value.Date.ToString("dd-MM-yyyy");
                hoja_trabajo.Cells[3, 2].Font.Bold = true;
                hoja_trabajo.Range["b3:f3"].Merge();
                Excel.Range rng6 = (Excel.Range)hoja_trabajo.get_Range("b3");
                rng6.HorizontalAlignment = 3;

                DateTime rephoyex = DateTime.Now;
                hoja_trabajo.Cells[4, 2] = "Fecha Creación Reporte: " + rephoyex;
                hoja_trabajo.Cells[4, 2].Font.Bold = true;
                hoja_trabajo.Range["b4:f4"].Merge();
                Excel.Range rng7 = (Excel.Range)hoja_trabajo.get_Range("b4");
                rng7.HorizontalAlignment = 3;

                string headerText1 = dataGridView2.Columns[0].HeaderText;
                string headerText2 = dataGridView2.Columns[1].HeaderText;
                string headerText3 = dataGridView2.Columns[2].HeaderText;

                hoja_trabajo.Cells.EntireColumn.ColumnWidth = 17;

                hoja_trabajo.Cells[6, 3] = headerText1;
                hoja_trabajo.Cells[6, 3].Font.Bold = true;
                hoja_trabajo.Cells[6, 3].Font.Size = 12;
                hoja_trabajo.Cells[6, 3].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                hoja_trabajo.Cells[6, 4] = headerText2;
                hoja_trabajo.Cells[6, 4].Font.Bold = true;
                hoja_trabajo.Cells[6, 4].Font.Size = 12;
                hoja_trabajo.Cells[6, 4].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                
                hoja_trabajo.Cells[6, 5] = headerText3;
                hoja_trabajo.Cells[6, 5].Font.Bold = true;
                hoja_trabajo.Cells[6, 5].Font.Size = 12;
                hoja_trabajo.Cells[6, 5].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                foreach (DataGridViewRow row2 in dataGridView2.Rows)
                {
                    datodg1 = Convert.ToString(row2.Cells[0].Value);
                    datodg2 = Convert.ToString(row2.Cells[1].Value);
                    datodg3 = Convert.ToString(row2.Cells[2].Value);

                    suma += Convert.ToDouble(datodg3);

                    if (datodg1 == cortereporte)
                    {
                        hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToString(row2.Cells[0].Value);
                        colexcel = colexcel + 1;
                        hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToString(row2.Cells[1].Value);
                        colexcel = colexcel + 1;
                        hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToDecimal(row2.Cells[2].Value);
                        filaexcel = filaexcel + 1;
                        colexcel = 3;
                    }
                    else
                    {
                        if (cortereporte2 == 0)
                        {
                            hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToString(row2.Cells[0].Value);
                            colexcel = colexcel + 1;
                            hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToString(row2.Cells[1].Value);
                            colexcel = colexcel + 1;
                            hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToDecimal(row2.Cells[2].Value);
                            filaexcel = filaexcel + 1;
                            colexcel = 3; 
                            
                            cortereporte = datodg1;
                            cortereporte2 = 1;
                        }
                        else
                        {
                            suma = suma - Convert.ToDouble(datodg3);
                            CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
                            string pasomiles = (suma.ToString("0,0", elGR));

                            colexcel = colexcel + 1;

                            hoja_trabajo.Cells[filaexcel, colexcel] = "Subtotal";
                            hoja_trabajo.Cells[filaexcel, colexcel].Font.Bold = true;
                            hoja_trabajo.Cells[filaexcel, colexcel].Font.Size = 11;

                            colexcel = colexcel + 1;
                            hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToDecimal(pasomiles);
                            hoja_trabajo.Cells[filaexcel, colexcel].Font.Bold = true;
                            hoja_trabajo.Cells[filaexcel, colexcel].Font.Size = 11;

                            sumatotal += suma;

                            filaexcel = filaexcel + 2;
                            colexcel = colexcel - 2;

                            hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToString(row2.Cells[0].Value);
                            colexcel = colexcel + 1;
                            hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToString(row2.Cells[1].Value);
                            colexcel = colexcel + 1;
                            hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToDecimal(row2.Cells[2].Value);
                            filaexcel = filaexcel + 1;
                            colexcel = 3;

                            suma = Convert.ToDouble(datodg3);

                            cortereporte = datodg1;
                        }
                    }
                }

                CultureInfo elGRSF = CultureInfo.CreateSpecificCulture("el-GR");
                string pasomilesSf = (suma.ToString("0,0", elGRSF));

                colexcel = colexcel + 1;

                hoja_trabajo.Cells[filaexcel, colexcel] = "Subtotal";
                hoja_trabajo.Cells[filaexcel, colexcel].Font.Bold = true;
                hoja_trabajo.Cells[filaexcel, colexcel].Font.Size = 11;

                colexcel = colexcel + 1;
                hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToDecimal(pasomilesSf);
                hoja_trabajo.Cells[filaexcel, colexcel].Font.Bold = true;
                hoja_trabajo.Cells[filaexcel, colexcel].Font.Size = 11;

                sumatotal += suma;
                CultureInfo elGRF = CultureInfo.CreateSpecificCulture("el-GR");
                string pasomilesf = (sumatotal.ToString("0,0", elGRF));

                filaexcel = filaexcel + 2;
                colexcel = colexcel - 1;

                hoja_trabajo.Cells[filaexcel, colexcel] = "Total";
                hoja_trabajo.Cells[filaexcel, colexcel].Font.Bold = true;
                hoja_trabajo.Cells[filaexcel, colexcel].Font.Size = 12;
                colexcel = colexcel + 1;
                hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToDecimal(pasomilesf);
                hoja_trabajo.Cells[filaexcel, colexcel].Font.Bold = true;
                hoja_trabajo.Cells[filaexcel, colexcel].Font.Size = 12;

                cortereporte = "";
                cortereporte2 = 0;
                suma = 0;
                sumatotal = 0;
                filaexcel = 6;
                colexcel = 3;

                string fichero = @frm5folderName + "\\";
                string nomarch = "RepCostosxDpto_" + shora + "-" + fechanomi + "-" + fechanomf + ".xls";

                libros_trabajo.SaveAs(fichero + nomarch, Excel.XlFileFormat.xlWorkbookNormal);
                libros_trabajo.Close(true);
                aplicacion.Quit();

                MessageBox.Show("Archivo exportado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (tiporeporte == 4)
            {
                filaexcel = 7;
                colexcel = 2;

                string fechanomi = dateTimePicker1.Value.Date.ToString("yyyyMMdd");
                string fechanomf = dateTimePicker2.Value.Date.ToString("yyyyMMdd");

                DataGridView grd = new DataGridView();

                Microsoft.Office.Interop.Excel.Application aplicacion;
                Microsoft.Office.Interop.Excel.Workbook libros_trabajo;
                Microsoft.Office.Interop.Excel.Worksheet hoja_trabajo;
                aplicacion = new Microsoft.Office.Interop.Excel.Application();
                libros_trabajo = aplicacion.Workbooks.Add();
                hoja_trabajo = (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.get_Item(1);

                hoja_trabajo.Name = "Costo por Servicio (#Servicios)";

                hoja_trabajo.Cells[1, 2] = "Reporte Casino - Costo por Servicio (#Servicios)";
                hoja_trabajo.Cells[1, 2].Font.Bold = true;
                hoja_trabajo.Cells[1, 2].Font.Size = 24;
                hoja_trabajo.Cells[1, 2].Font.Underline = true;
                hoja_trabajo.Range["b1:f1"].Merge();
                Excel.Range rng5 = (Excel.Range)hoja_trabajo.get_Range("b1");
                rng5.HorizontalAlignment = 3;

                hoja_trabajo.Cells[3, 2] = "FECHA INICIO: " + dateTimePicker1.Value.Date.ToString("dd-MM-yyyy") + "  a  FECHA FIN: " + dateTimePicker2.Value.Date.ToString("dd-MM-yyyy");
                hoja_trabajo.Cells[3, 2].Font.Bold = true;
                hoja_trabajo.Range["b3:f3"].Merge();
                Excel.Range rng6 = (Excel.Range)hoja_trabajo.get_Range("b3");
                rng6.HorizontalAlignment = 3;

                DateTime rephoyex = DateTime.Now;
                hoja_trabajo.Cells[4, 2] = "Fecha Creación Reporte: " + rephoyex;
                hoja_trabajo.Cells[4, 2].Font.Bold = true;
                hoja_trabajo.Range["b4:f4"].Merge();
                Excel.Range rng7 = (Excel.Range)hoja_trabajo.get_Range("b4");
                rng7.HorizontalAlignment = 3;

                string headerText1 = dataGridView4.Columns[0].HeaderText;
                string headerText2 = dataGridView4.Columns[1].HeaderText;
                string headerText3 = dataGridView4.Columns[2].HeaderText;
                string headerText4 = dataGridView4.Columns[3].HeaderText;

                hoja_trabajo.Cells.EntireColumn.ColumnWidth = 17;

                hoja_trabajo.Cells[6, 2] = headerText1;
                hoja_trabajo.Cells[6, 2].Font.Bold = true;
                hoja_trabajo.Cells[6, 2].Font.Size = 12;
                hoja_trabajo.Cells[6, 2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                hoja_trabajo.Cells[6, 3] = headerText2;
                hoja_trabajo.Cells[6, 3].Font.Bold = true;
                hoja_trabajo.Cells[6, 3].Font.Size = 12;
                hoja_trabajo.Cells[6, 3].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                hoja_trabajo.Cells[6, 4] = headerText3;
                hoja_trabajo.Cells[6, 4].Font.Bold = true;
                hoja_trabajo.Cells[6, 4].Font.Size = 12;
                hoja_trabajo.Cells[6, 4].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                
                hoja_trabajo.Cells[6, 5] = headerText4;
                hoja_trabajo.Cells[6, 5].Font.Bold = true;
                hoja_trabajo.Cells[6, 5].Font.Size = 12;
                hoja_trabajo.Cells[6, 5].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                foreach (DataGridViewRow row4 in dataGridView4.Rows)
                {
                    datodg1 = Convert.ToString(row4.Cells[0].Value);
                    datodg2 = Convert.ToString(row4.Cells[1].Value);
                    datodg3 = Convert.ToString(row4.Cells[2].Value);
                    datodg4 = Convert.ToString(row4.Cells[3].Value);

                    suma += Convert.ToDouble(datodg4);

                    if (datodg1 == cortereporte)
                    {
                        hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToString(row4.Cells[0].Value);
                        colexcel = colexcel + 1;
                        hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToString(row4.Cells[1].Value);
                        colexcel = colexcel + 1;
                        hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToString(row4.Cells[2].Value);
                        colexcel = colexcel + 1;
                        hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToDecimal(row4.Cells[3].Value);
                        filaexcel = filaexcel + 1;
                        colexcel = 2;
                    }
                    else
                    {
                        if (cortereporte2 == 0)
                        {
                            hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToString(row4.Cells[0].Value);
                            colexcel = colexcel + 1;
                            hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToString(row4.Cells[1].Value);
                            colexcel = colexcel + 1;
                            hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToString(row4.Cells[2].Value);
                            colexcel = colexcel + 1;
                            hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToDecimal(row4.Cells[3].Value);
                            filaexcel = filaexcel + 1;
                            colexcel = 2;

                            cortereporte = datodg1;
                            cortereporte2 = 1;
                        }
                        else
                        {
                            suma = suma - Convert.ToDouble(datodg4);
                            CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
                            string pasomiles = (suma.ToString("0,0", elGR));

                            colexcel = colexcel + 2;

                            hoja_trabajo.Cells[filaexcel, colexcel] = "Subtotal";
                            hoja_trabajo.Cells[filaexcel, colexcel].Font.Bold = true;
                            hoja_trabajo.Cells[filaexcel, colexcel].Font.Size = 11;

                            colexcel = colexcel + 1;
                            hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToDecimal(pasomiles);
                            hoja_trabajo.Cells[filaexcel, colexcel].Font.Bold = true;
                            hoja_trabajo.Cells[filaexcel, colexcel].Font.Size = 11;

                            sumatotal += suma;

                            filaexcel = filaexcel + 2;
                            colexcel = colexcel - 3;

                            hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToString(row4.Cells[0].Value);
                            colexcel = colexcel + 1;
                            hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToString(row4.Cells[1].Value);
                            colexcel = colexcel + 1;
                            hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToString(row4.Cells[2].Value);
                            colexcel = colexcel + 1;
                            hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToDecimal(row4.Cells[3].Value);
                            filaexcel = filaexcel + 1;
                            colexcel = 2;

                            suma = Convert.ToDouble(datodg4);

                            cortereporte = datodg1;
                        }
                    }
                }

                CultureInfo elGRSF = CultureInfo.CreateSpecificCulture("el-GR");
                string pasomilesSf = (suma.ToString("0,0", elGRSF));

                colexcel = colexcel + 2;

                hoja_trabajo.Cells[filaexcel, colexcel] = "Subtotal";
                hoja_trabajo.Cells[filaexcel, colexcel].Font.Bold = true;
                hoja_trabajo.Cells[filaexcel, colexcel].Font.Size = 11;

                colexcel = colexcel + 1;
                hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToDecimal(pasomilesSf);
                hoja_trabajo.Cells[filaexcel, colexcel].Font.Bold = true;
                hoja_trabajo.Cells[filaexcel, colexcel].Font.Size = 11;

                sumatotal += suma;
                CultureInfo elGRF = CultureInfo.CreateSpecificCulture("el-GR");
                string pasomilesf = (sumatotal.ToString("0,0", elGRF));

                filaexcel = filaexcel + 2;
                colexcel = colexcel - 1;

                hoja_trabajo.Cells[filaexcel, colexcel] = "Total";
                hoja_trabajo.Cells[filaexcel, colexcel].Font.Bold = true;
                hoja_trabajo.Cells[filaexcel, colexcel].Font.Size = 12;
                colexcel = colexcel + 1;
                hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToDecimal(pasomilesf);
                hoja_trabajo.Cells[filaexcel, colexcel].Font.Bold = true;
                hoja_trabajo.Cells[filaexcel, colexcel].Font.Size = 12;

                cortereporte = "";
                cortereporte2 = 0;
                suma = 0;
                sumatotal = 0;
                filaexcel = 6;
                colexcel = 2;

                string fichero = @frm5folderName + "\\";
                string nomarch = "RepCostosxServ_" + shora + "-" + fechanomi + "-" + fechanomf + ".xls";

                libros_trabajo.SaveAs(fichero + nomarch, Excel.XlFileFormat.xlWorkbookNormal);
                libros_trabajo.Close(true);
                aplicacion.Quit();

                MessageBox.Show("Archivo exportado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (tiporeporte == 5)
            {

                filaexcel = 7;
                colexcel = 3;

                string fechanomi = dateTimePicker1.Value.Date.ToString("yyyyMMdd");
                string fechanomf = dateTimePicker2.Value.Date.ToString("yyyyMMdd");

                DataGridView grd = new DataGridView();

                Microsoft.Office.Interop.Excel.Application aplicacion;
                Microsoft.Office.Interop.Excel.Workbook libros_trabajo;
                Microsoft.Office.Interop.Excel.Worksheet hoja_trabajo;
                aplicacion = new Microsoft.Office.Interop.Excel.Application();
                libros_trabajo = aplicacion.Workbooks.Add();
                hoja_trabajo = (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.get_Item(1);

                hoja_trabajo.Name = "Costo por Servicio";

                hoja_trabajo.Cells[1, 2] = "Reporte Casino - Costo por Servicio";
                hoja_trabajo.Cells[1, 2].Font.Bold = true;
                hoja_trabajo.Cells[1, 2].Font.Size = 24;
                hoja_trabajo.Cells[1, 2].Font.Underline = true;
                hoja_trabajo.Range["b1:f1"].Merge();
                Excel.Range rng5 = (Excel.Range)hoja_trabajo.get_Range("b1");
                rng5.HorizontalAlignment = 3;

                hoja_trabajo.Cells[3, 2] = "FECHA INICIO: " + dateTimePicker1.Value.Date.ToString("dd-MM-yyyy") + "  a  FECHA FIN: " + dateTimePicker2.Value.Date.ToString("dd-MM-yyyy");
                hoja_trabajo.Cells[3, 2].Font.Bold = true;
                hoja_trabajo.Range["b3:f3"].Merge();
                Excel.Range rng6 = (Excel.Range)hoja_trabajo.get_Range("b3");
                rng6.HorizontalAlignment = 3;

                DateTime rephoyex = DateTime.Now;
                hoja_trabajo.Cells[4, 2] = "Fecha Creación Reporte: " + rephoyex;
                hoja_trabajo.Cells[4, 2].Font.Bold = true;
                hoja_trabajo.Range["b4:f4"].Merge();
                Excel.Range rng7 = (Excel.Range)hoja_trabajo.get_Range("b4");
                rng7.HorizontalAlignment = 3;

                string headerText1 = dataGridView3.Columns[0].HeaderText;
                string headerText2 = dataGridView3.Columns[1].HeaderText;
                string headerText3 = dataGridView3.Columns[2].HeaderText;

                hoja_trabajo.Cells.EntireColumn.ColumnWidth = 17;

                hoja_trabajo.Cells[6, 3] = headerText1;
                hoja_trabajo.Cells[6, 3].Font.Bold = true;
                hoja_trabajo.Cells[6, 3].Font.Size = 12;
                hoja_trabajo.Cells[6, 3].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                hoja_trabajo.Cells[6, 4] = headerText2;
                hoja_trabajo.Cells[6, 4].Font.Bold = true;
                hoja_trabajo.Cells[6, 4].Font.Size = 12;
                hoja_trabajo.Cells[6, 4].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                
                hoja_trabajo.Cells[6, 5] = headerText3;
                hoja_trabajo.Cells[6, 5].Font.Bold = true;
                hoja_trabajo.Cells[6, 5].Font.Size = 12;
                hoja_trabajo.Cells[6, 5].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                foreach (DataGridViewRow row3 in dataGridView3.Rows)
                {
                    datodg1 = Convert.ToString(row3.Cells[0].Value);
                    datodg2 = Convert.ToString(row3.Cells[1].Value);
                    datodg3 = Convert.ToString(row3.Cells[2].Value);

                    suma += Convert.ToDouble(datodg3);

                    if (datodg2 == cortereporte)
                    {
                        hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToString(row3.Cells[0].Value);
                        colexcel = colexcel + 1;
                        hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToString(row3.Cells[1].Value);
                        colexcel = colexcel + 1;
                        hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToDecimal(row3.Cells[2].Value);
                        filaexcel = filaexcel + 1;
                        colexcel = 3;
                    }
                    else
                    {
                        if (cortereporte2 == 0)
                        {
                            hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToString(row3.Cells[0].Value);
                            colexcel = colexcel + 1;
                            hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToString(row3.Cells[1].Value);
                            colexcel = colexcel + 1;
                            hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToDecimal(row3.Cells[2].Value);
                            filaexcel = filaexcel + 1;
                            colexcel = 3;

                            cortereporte = datodg2;
                            cortereporte2 = 1;
                        }
                        else
                        {
                            suma = suma - Convert.ToDouble(datodg3);
                            CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
                            string pasomiles = (suma.ToString("0,0", elGR));

                            colexcel = colexcel + 1;

                            hoja_trabajo.Cells[filaexcel, colexcel] = "Subtotal";
                            hoja_trabajo.Cells[filaexcel, colexcel].Font.Bold = true;
                            hoja_trabajo.Cells[filaexcel, colexcel].Font.Size = 11;

                            colexcel = colexcel + 1;
                            hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToDecimal(pasomiles);
                            hoja_trabajo.Cells[filaexcel, colexcel].Font.Bold = true;
                            hoja_trabajo.Cells[filaexcel, colexcel].Font.Size = 11;

                            sumatotal += suma;

                            filaexcel = filaexcel + 2;
                            colexcel = colexcel - 2;

                            hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToString(row3.Cells[0].Value);
                            colexcel = colexcel + 1;
                            hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToString(row3.Cells[1].Value);
                            colexcel = colexcel + 1;
                            hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToDecimal(row3.Cells[2].Value);
                            filaexcel = filaexcel + 1;
                            colexcel = 3;

                            suma = Convert.ToDouble(datodg3);

                            cortereporte = datodg2;
                        }
                    }
                }

                CultureInfo elGRSF = CultureInfo.CreateSpecificCulture("el-GR");
                string pasomilesSf = (suma.ToString("0,0", elGRSF));

                colexcel = colexcel + 1;

                hoja_trabajo.Cells[filaexcel, colexcel] = "Subtotal";
                hoja_trabajo.Cells[filaexcel, colexcel].Font.Bold = true;
                hoja_trabajo.Cells[filaexcel, colexcel].Font.Size = 11;

                colexcel = colexcel + 1;
                hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToDecimal(pasomilesSf);
                hoja_trabajo.Cells[filaexcel, colexcel].Font.Bold = true;
                hoja_trabajo.Cells[filaexcel, colexcel].Font.Size = 11;

                sumatotal += suma;
                CultureInfo elGRF = CultureInfo.CreateSpecificCulture("el-GR");
                string pasomilesf = (sumatotal.ToString("0,0", elGRF));

                filaexcel = filaexcel + 2;
                colexcel = colexcel - 1;

                hoja_trabajo.Cells[filaexcel, colexcel] = "Total";
                hoja_trabajo.Cells[filaexcel, colexcel].Font.Bold = true;
                hoja_trabajo.Cells[filaexcel, colexcel].Font.Size = 12;
                colexcel = colexcel + 1;
                hoja_trabajo.Cells[filaexcel, colexcel] = Convert.ToDecimal(pasomilesf);
                hoja_trabajo.Cells[filaexcel, colexcel].Font.Bold = true;
                hoja_trabajo.Cells[filaexcel, colexcel].Font.Size = 12;

                cortereporte = "";
                cortereporte2 = 0;
                suma = 0;
                sumatotal = 0;
                filaexcel = 6;
                colexcel = 2;

                string fichero = @frm5folderName + "\\";
                string nomarch = "RepCostosxServ_" + shora + "-" + fechanomi + "-" + fechanomf + ".xls";

                libros_trabajo.SaveAs(fichero + nomarch, Excel.XlFileFormat.xlWorkbookNormal);
                libros_trabajo.Close(true);
                aplicacion.Quit();

                MessageBox.Show("Archivo exportado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            limpiarobjetos();
            Form10 frm10 = new Form10();
            frm10.ShowDialog();
        }

        public void log(string text)
        {
            string archproclog = path + @"\LogCasino.log";

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(archproclog, true))
            {
                file.WriteLine(text);
            }
        }

        private void asignaciónServiciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            limpiarobjetos();
            Form12 frm12 = new Form12();
            frm12.ShowDialog();
        }

        private void quitarServiciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            limpiarobjetos();
            Form13 frm13 = new Form13();
            frm13.ShowDialog();
        }

        private void incorporarServicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            limpiarobjetos();
            Form9 frm9 = new Form9();
            frm9.ShowDialog();
        }

        private void modificarServicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            limpiarobjetos();
            Form14 frm14 = new Form14();
            frm14.ShowDialog();
        }

        private void emitirValesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            limpiarobjetos();
            Form20 frm20 = new Form20();
            frm20.ShowDialog();

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void serviciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            limpiarobjetos();
            Form9 frm9 = new Form9();
            frm9.ShowDialog();
        }

        private void configuracionIPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            limpiarobjetos();
            Form21 frm21 = new Form21();
            frm21.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            {
                string fecha1 = dateTimePicker1.Value.Date.ToString("yyyyMMdd");
                fecini = fecha1;
            }
            if (string.IsNullOrEmpty(fecfin))
            {
                string fecha2 = dateTimePicker2.Value.Date.ToString("yyyyMMdd");
                fecfin = fecha2;
            }

            if (dateTimePicker2.Value < dateTimePicker1.Value)
            {
                MessageBox.Show("El campo 'Fecha Fin' no puede ser 'Menor' que el campo 'Fecha Inicio'", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                DateTime now = DateTime.Now;
                var fechainico = new DateTime(now.Year, now.Month, 1);

                dateTimePicker1.Format = DateTimePickerFormat.Short;
                dateTimePicker1.Value = new DateTime(fechainico.Year, fechainico.Month, fechainico.Day);
                dateTimePicker2.Format = DateTimePickerFormat.Short;
                dateTimePicker2.Value = new DateTime(now.Year, now.Month, now.Day);


                /*dateTimePicker1.Format = DateTimePickerFormat.Short;
                dateTimePicker1.Value = new DateTime(2015, 01, 01);
                dateTimePicker2.Format = DateTimePickerFormat.Short;
                dateTimePicker2.Value = new DateTime(2015, 01, 01);*/
            }
            else
            {
                    Reporte_total_usuario(1);
                    validachecked4 = 1;
                    //dataGridView1.Rows.Clear();
                    checkedListBox1.Items.Clear();

                    try
                    {
                        radioButton1.Enabled = false;
                        radioButton4.Enabled = false;
                        validachecked = 0;
                        button1.Enabled = true;
                        dataGridView1.Show();
                        tiporeporte = 1;
                    }
                    catch (Exception)
                    {
                        errorconnbd();
                    }
                

            }
                
        }
    }
}
