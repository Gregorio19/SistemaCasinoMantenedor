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
using System.Diagnostics;

namespace Casino
{
    public partial class Form10 : Form
    {

        //string path = @"c:\TotalPack\";
        string path = Application.StartupPath;
        string f2vfipbdsoft;
        string f2vfbdsoft;
        string f2vfusersoft;
        string f2vfclavesoft;
        string f2vfipreloj;
        string f2vfpuertoreloj;
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
        int filaseleccionada;

        string msgerror;
        string consdpto;
        string dptoselect;
        string consssn;

        string ticketservicio;
        string asignadovale;

        int validainsupd;
        int nuevosvales;
        int obtieneuserid;
        int obtieneidservicio;
        
        string consultaporssn;

        int filaseleccionadadgv2;
        int filaseleccionadaxconsulta;
        string pasanombre;

        //test
        /*string diastar;
        string diaend;
        string diadesemana = DateTime.Now.DayOfWeek.ToString();
        string horahuella = DateTime.Now.ToString("hh:mm:ss");*/

        public Form10()
        {
            InitializeComponent();
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            //TEST FECHAS
            /*try
            {
                System.Diagnostics.Process appy = new System.Diagnostics.Process();
                appy.StartInfo.FileName = @"W:\Dropbox\04. Desarrollo_Vanntec\01. ReporteCasino\APPVB\CasinoV1.8_64bits_Sura_DesHabUsuario\CasinoDesHabUsuarios\CasinoDesHabUsuarios\obj\x86\Debug\CasinoDesHabUsuarios.exe";
                appy.StartInfo.Arguments = "Test";
                appy.Start();
                appy.WaitForExit();
            }
            catch(Exception a)
            {
                string aa = a.Message;
            }
            string insbirthday = DateTime.Now.ToString();
            string cvdia2 = insbirthday.Substring(0, 2);
            string cvmes2 = insbirthday.Substring(3, 2);
            string cvano2 = insbirthday.Substring(6, 4);
            string nvainsbirthday = cvmes2 + "-" + cvdia2 + "-" + cvano2;

            DateTime insinsbirthday = Convert.ToDateTime(nvainsbirthday);
            
            if (diadesemana == "Saturday" || diadesemana == "Sábado" || diadesemana == "Sabado")
            {
                diastar = "SatStart";
                diaend = "SatEnd";
            }

            String consulta = "select distinct TimeZoneID " +
                    " from ACTimeZones" +
                    " where '" + horahuella + "' >= CONVERT(VARCHAR," + diastar + ",108)" +
                    " and '" + horahuella + "' <= CONVERT(VARCHAR," + diaend + ",108)";


            string ss = DateTime.Now.DayOfWeek.ToString();

            if (ss == "Friday" || ss == "Viernes")
            {
                string diastart = "FriStart";
                string diaend = "FriEnd";
            }

            string aaaa = DateTime.Now.ToString("HH:mm:ss");

            if ((Convert.ToDateTime("21:43:05")) >= (Convert.ToDateTime(aaaa)))
            {
                string a = "ok";
            }

            cargadatosbd();
            f2conectarbd();

            String consulta = "select distinct TimeZoneID " +
                                " from ACTimeZones" +
                                " where '" + aaaa + "' >= CONVERT(VARCHAR,MonStart,108)" +
                                " and '" + aaaa + "' <= CONVERT(VARCHAR,MonEnd,108)";
            SqlCommand cmd = new SqlCommand(consulta, f2conn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string bdcargo = Convert.ToString(reader[0]);

                }
            }*/


            dataGridView1.Rows.Clear();
            label2.Hide();
            //groupBox2.Hide();
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

                Linea = Lee.ReadLine();
                f2vfipreloj = Linea;

                Linea = Lee.ReadLine();
                f2vfpuertoreloj = Linea;
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
            //groupBox2.Hide();
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            label2.Text = "";
            cargadatosbd();
            f2conectarbd();

            try
            {
                //String consulta = "select distinct cv.cargo, cv.iduser, ui.SSN, ui.NAME, cv.numvales " +
                String consulta = "select ssn, name, userid from USERINFO"; // where userid = " + textBox1.Text;
                                  //" from casino_valexusuarios cv," +
                                  //" userinfo ui" +
                                  //" where cv.iduser = ui.USERID";
                SqlCommand cmd = new SqlCommand(consulta, f2conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //string bdcargo = Convert.ToString(reader[0]);
                        //int bdiduser = Convert.ToInt32(reader[1]);
                        string bdrut = Convert.ToString(reader[0]);
                        string bdnombre = Convert.ToString(reader[1]);
                        obtieneuserid = Convert.ToInt32(reader[2]);
                        //int bdnumvales = Convert.ToInt32(reader[4]);
                        //dataGridView1.Rows.Add(bdcargo, bdiduser, bdrut, bdnombre, bdnumvales);
                        dataGridView1.Rows.Add(bdrut, bdnombre);
                    }
                }
                else
                {
                    MessageBox.Show("No existen usuarios configurados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Debe ingresar un valor a consultar");
                textBox1.Focus();
            }
            else
            {
                //groupBox2.Hide();
                dataGridView1.Rows.Clear();
                dataGridView2.Rows.Clear();
                label2.Text = "";

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
                        //String consulta2 = "select distinct cv.cargo, cv.iduser, ui.SSN, ui.NAME, cv.numvales " +
                        String consulta2 = "select ssn, name, userid from USERINFO where userid = " + textBox1.Text;
                                            //" from casino_valexusuarios cv," +
                                            //" userinfo ui" +
                                            //" where cv.iduser = ui.USERID" +
                                            //" and cv.iduser = " + textBox1.Text;
                        SqlCommand cmd2 = new SqlCommand(consulta2, f2conn);
                        SqlDataReader reader2 = cmd2.ExecuteReader();

                        if (reader2.HasRows)
                        {
                            while (reader2.Read())
                            {
                                //string bdcargo = Convert.ToString(reader2[0]);
                                //int bdiduser = Convert.ToInt32(reader2[1]);
                                string bdrut = Convert.ToString(reader2[0]);
                                string bdnombre = Convert.ToString(reader2[1]);
                                obtieneuserid = Convert.ToInt32(reader2[2]);
                                //int bdnumvales = Convert.ToInt32(reader2[4]);
                                //dataGridView1.Rows.Add(bdcargo, bdiduser, bdrut, bdnombre, bdnumvales);
                                dataGridView1.Rows.Add(bdrut, bdnombre);
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

        private void consultauserid()
        {
            cargadatosbd();
            f2conectarbd();

            try
            {
                String consulta233 = "select userid from USERINFO where ssn = '" + consssn + "'";
                SqlCommand cmd233 = new SqlCommand(consulta233, f2conn);
                SqlDataReader reader233 = cmd233.ExecuteReader();
                reader233.Read();
                obtieneuserid = Convert.ToInt32(reader233[0]);
                reader233.Close();
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Debe ingresar un valor a consultar");
                textBox1.Focus();
            }
            else
            {
                //groupBox2.Hide();
                dataGridView1.Rows.Clear();
                dataGridView2.Rows.Clear();
                label2.Text = "";
                cargadatosbd();
                f2conectarbd();

                try
                {
                    //String consulta2 = "select distinct cv.cargo, cv.iduser, ui.SSN, ui.NAME, cv.numvales " +
                    String consulta2 = "select ssn, name, userid from USERINFO where upper(title) like upper('%" + textBox1.Text + "%')";
                                        //" from casino_valexusuarios cv," +
                                        //" userinfo ui" +
                                        //" where cv.iduser = ui.USERID" +
                                        //" and upper(cv.cargo) like upper('%" + textBox1.Text + "%')";
                    SqlCommand cmd2 = new SqlCommand(consulta2, f2conn);
                    SqlDataReader reader2 = cmd2.ExecuteReader();

                    if (reader2.HasRows)
                    {
                        while (reader2.Read())
                        {
                            //string bdcargo = Convert.ToString(reader2[0]);
                            //int bdiduser = Convert.ToInt32(reader2[1]);
                            string bdrut = Convert.ToString(reader2[0]);
                            string bdnombre = Convert.ToString(reader2[1]);
                            obtieneuserid = Convert.ToInt32(reader2[2]);
                            //int bdnumvales = Convert.ToInt32(reader2[4]);
                            //dataGridView1.Rows.Add(bdcargo, bdiduser, bdrut, bdnombre, bdnumvales);
                            dataGridView1.Rows.Add(bdrut, bdnombre);
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

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
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
                //groupBox2.Hide();
                dataGridView1.Rows.Clear();
                dataGridView2.Rows.Clear();
                label2.Text = "";
                cargadatosbd();
                f2conectarbd();

                try
                {
                    //String consulta2 = "select distinct cv.cargo, cv.iduser, ui.SSN, ui.NAME, cv.numvales " +
                    String consulta2 = "select ssn, name, userid from USERINFO where name like '%" + textBox1.Text + "%'";
                                        //" from casino_valexusuarios cv," +
                                        //" userinfo ui" +
                                        //" where cv.iduser = ui.USERID" +
                                        //" and upper(ui.NAME) like upper('%" + textBox1.Text + "%')";
                    SqlCommand cmd2 = new SqlCommand(consulta2, f2conn);
                    SqlDataReader reader2 = cmd2.ExecuteReader();

                    if (reader2.HasRows)
                    {
                        while (reader2.Read())
                        {
                            //string bdcargo = Convert.ToString(reader2[0]);
                            //int bdiduser = Convert.ToInt32(reader2[1]);
                            string bdrut = Convert.ToString(reader2[0]);
                            string bdnombre = Convert.ToString(reader2[1]);
                            obtieneuserid = Convert.ToInt32(reader2[2]);
                            //int bdnumvales = Convert.ToInt32(reader2[4]);
                            //dataGridView1.Rows.Add(bdcargo, bdiduser, bdrut, bdnombre, bdnumvales);
                            dataGridView1.Rows.Add(bdrut, bdnombre);
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
           
        }

        private void idservicio()
        {
            try
            {
                cargadatosbd();
                f2conectarbd();

                String servid = "select distinct ac.TimeZoneID from ACTimeZones ac where name = '" + ticketservicio + "'";

                SqlCommand cmd2id = new SqlCommand(servid, f2conn);
                SqlDataReader readerid = cmd2id.ExecuteReader();
                readerid.Read();

                if (readerid.HasRows)
                {
                    obtieneidservicio = Convert.ToInt32(readerid[0]);
                }
                else
                {
                    MessageBox.Show("No existen servicios configurados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    readerid.Close();
                }
                readerid.Close();
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

        private void consultavalexusuario()
        {
            try
            {
                cargadatosbd();
                f2conectarbd();

                String serv = "select distinct act.Name, 0 " +
                               " from userinfo ui, " +
                               "      casino_servicioasig cs, " +
                               " 	  ACTimeZones act " +
                               " where ui.ssn = '" + consssn + "' " +
                               " and ui.USERID = cs.iduser " +
                               " and not exists (select 1 " +
                               "                   from casino_valexusuarios cv " +
                               "                   where cv.idserv = cs.idservicio " +
                               "				   and cv.iduser = cs.iduser) " +
                               " and cs.idservicio = act.TimeZoneID " +
                               " and cs.iduser = " + obtieneuserid +
                               " union " +
                               " select distinct act.Name, cv.numvales " +
                               " from casino_servicioasig cs, " +
                               "      casino_valexusuarios cv, " +
                               "      ACTimeZones act " +
                               " where exists (select 1 " +
                               "                   from casino_valexusuarios cv " +
                               "                   where cv.idserv = cs.idservicio " +
                               "                   and cv.iduser = cs.iduser) " +
                               " and cs.iduser = " + obtieneuserid +
                               " and cs.iduser = cv.iduser" +
                               " and cv.idserv = act.TimeZoneID";

                SqlCommand cmd2 = new SqlCommand(serv, f2conn);
                SqlDataReader reader2 = cmd2.ExecuteReader();

                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        string bdnameserv = Convert.ToString(reader2[0]);
                        int numserv = Convert.ToInt32(reader2[1]);
                        dataGridView2.Rows.Add(bdnameserv, numserv);
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

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            //groupBox2.Hide();
            textBox1.Focus();
        }

        private void insertupdatevales()
        {
            cargadatosbd();
            f2conectarbd();

            try
            {
                if (validainsupd == 0)
                {
                    String consulta2 = "insert into casino_valexusuarios(iduser, numvales, idserv) values (" + obtieneuserid + ", " +
                                        nuevosvales + ", " + obtieneidservicio + ")";
                    SqlCommand cmd2 = new SqlCommand(consulta2, f2conn);
                    cmd2.ExecuteNonQuery();
                    f2conn.Close();
                }
                else
                {
                    String consulta3 = "update casino_valexusuarios " +
                                        " set numvales = " + nuevosvales +
                                        " where iduser = " + obtieneuserid +
                                        " and idserv = " + obtieneidservicio;
                    SqlCommand cmd3 = new SqlCommand(consulta3, f2conn);
                    cmd3.ExecuteNonQuery();
                    f2conn.Close();
                }

                MessageBox.Show(nuevosvales + " Vales actualizados!"); //a: " + label2.Text + "\rActualmente tiene configurado: " + validainsupd + " vales");

                //groupBox2.Hide();
                //dataGridView1.Rows.Clear();
                //startApp();

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

        private void startApp()
        {
            string eje = path + @"\CasinoDesHabUsuarios.exe";
            ProcessStartInfo startInfo = new ProcessStartInfo(eje);
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;

            startInfo.Arguments = dgviduser + " " + f2vfipreloj + " " + f2vfpuertoreloj;
            Process.Start(startInfo);
        }

        private void historicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            //groupBox2.Hide();
            Form11 frm11 = new Form11();
            frm11.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string caption = "Eliminación de Usuario Seleccionado";
            MessageBoxButtons alternativa = MessageBoxButtons.YesNoCancel;
            DialogResult resultadoalternativa;

            resultadoalternativa = MessageBox.Show("Desea realmente eliminar el usuario seleccionado?", caption, alternativa);

            if (resultadoalternativa == System.Windows.Forms.DialogResult.Yes)
            {
                cargadatosbd();
                f2conectarbd();

                try
                {
                    String consulta3 = "delete casino_valexusuarios " +
                                        " where iduser = " + dgviduser;
                    SqlCommand cmd3 = new SqlCommand(consulta3, f2conn);
                    cmd3.ExecuteNonQuery();
                    f2conn.Close();
                    //groupBox2.Hide();
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
            if (resultadoalternativa == System.Windows.Forms.DialogResult.No)
            {
                dataGridView1.Refresh();
            }
            if (resultadoalternativa == System.Windows.Forms.DialogResult.Cancel)
            {
                dataGridView1.Refresh();
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

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Debe ingresar un valor a consultar");
                textBox1.Focus();
            }
            else
            {
                dataGridView1.Rows.Clear();
                dataGridView2.Rows.Clear();
                label2.Text = "";
                consdpto = textBox1.Text;
                Form16 frm16 = new Form16(consdpto);
                frm16.ShowDialog();

                if (frm16.sindpto == 0)
                {
                    dptoselect = frm16.ReturnDpto;

                    int result = 0;
                    bool success = int.TryParse(new string(dptoselect
                                         .SkipWhile(x => !char.IsDigit(x))
                                         .TakeWhile(x => char.IsDigit(x))
                                         .ToArray()), out result);

                    cargadatosbd();
                    f2conectarbd();

                    try
                    {
                        String cargauser = "select ssn, name, userid from USERINFO where DEFAULTDEPTID = '" + Convert.ToString(result) + "'";
                        SqlCommand cmduser = new SqlCommand(cargauser, f2conn);
                        SqlDataReader readeruser = cmduser.ExecuteReader();

                        if (readeruser.HasRows)
                        {
                            while (readeruser.Read())
                            {
                                string bdssn = Convert.ToString(readeruser[0]);
                                string bdname = Convert.ToString(readeruser[1]);
                                obtieneuserid = Convert.ToInt32(readeruser[2]);
                                dataGridView1.Rows.Add(bdssn, bdname);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No existen usuarios configurados para departamento seleccionado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            readeruser.Close();
                        }
                        readeruser.Close();
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
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            if (filaseleccionadaxconsulta != 0)
            {
                Form18 frm18 = new Form18(obtieneuserid, pasanombre);
                frm18.ShowDialog();

                if (frm18.insertopaso == 1)
                {
                    frm18.insertopaso = 0;
                    label2.Text = "";
                    dataGridView2.Rows.Clear();
                }
                else
                {
                    frm18.insertopaso = 0;
                    label2.Text = "";
                    dataGridView2.Rows.Clear();
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una persona antes de asignar Servicios");
            }

            filaseleccionadaxconsulta = 0;
            pasanombre = "";
        }

        private void dataGridView2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.Rows.Clear();
            label2.Show();
            consssn = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            pasanombre = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            label2.Text = pasanombre;
            //filaseleccionada = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
            filaseleccionadaxconsulta = dataGridView1.CurrentCell.RowIndex;

            if (!string.IsNullOrEmpty(consssn))
            {
                consultauserid();
                consultavalexusuario();
            }
            else
            {
                MessageBox.Show("No se puede consultar usuario sin Rut\rSeleccione un usuario válido");
                filaseleccionadaxconsulta = 0;
            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ticketservicio = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            asignadovale = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            //int filaseleccionadadgv2 = dataGridView2.Rows.GetRowCount(DataGridViewElementStates.Selected);
            filaseleccionadadgv2 = dataGridView2.CurrentCell.RowIndex;

            validainsupd = Convert.ToInt32(asignadovale);

            Form17 frm17 = new Form17(ticketservicio);
            frm17.ShowDialog();

            nuevosvales = Convert.ToInt32(frm17.ReturnNumVales);

            if (nuevosvales != 0)
            {
                idservicio();
                insertupdatevales();

                dataGridView2.Rows.Clear();
                consultauserid();
                consultavalexusuario();
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
