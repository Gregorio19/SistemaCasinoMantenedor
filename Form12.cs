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
    public partial class Form12 : Form
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

        string user_a_buscar;

        public Form12()
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

                        checkedListBox1.Items.Add(bduserid + " - " + bdiduser + " - " + bdname, CheckState.Unchecked);
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

        private void cargar_usuarios_text()
        {

            validachecked = 1;
            try
            {
                f2conectarbd();
                String consulta = "select distinct bu.userid, bu.badgenumber, bu.Name from USERINFO bu where bu.Name LIKE '%" + user_a_buscar + "%'";
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

                        checkedListBox1.Items.Add(bduserid + " - " + bdiduser + " - " + bdname, CheckState.Unchecked);
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


        private void cargar_usuarios_text2()
        {
            validachecked = 1;
            try
            {
                f2conectarbd();
                String consulta = "select distinct bu.userid, bu.badgenumber, bu.Name from USERINFO bu where exists  (select 1  from casino_servicioasig cs where bu.USERID = cs.iduser and cs.idservicio = " + valorturno + ") AND bu.Name LIKE '%" + user_a_buscar + "%'";
                //MessageBox.Show("valor servicio " + valorturno + " usuario a buscar " + user_a_buscar);
                SqlCommand cmd = new SqlCommand(consulta, f2conn);
                SqlDataReader reader = cmd.ExecuteReader();

                checkedListBox2.Items.Clear();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int bduserid = Convert.ToInt32(reader[0]);
                        string bdiduser = Convert.ToString(reader[1]);
                        string bdname = Convert.ToString(reader[2]);

                        checkedListBox2.Items.Add(bduserid + " - " + bdiduser + " - " + bdname, CheckState.Unchecked);
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
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            f2conectarbd();
            cargausuarios();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            f2conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int encontrados = 0;
            int encontradof = 0;
            if (comboBox1.SelectedIndex == -1)//Nothing selected
            {
                MessageBox.Show("Debes seleccionar un Servicio", "Error");
            }
            else
            {
                //checkedListBox2.Items.Clear();

                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    encontrados = 0;
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        for (int j = 0; j < checkedListBox2.Items.Count; j++)
                        {
                            if ((string)checkedListBox1.Items[i] == (string)checkedListBox2.Items[j])
                            {
                                encontrados = 1;
                                encontradof = 1;
                            }
                        }

                        if (encontrados == 0)
                        {
                            checkedListBox2.Items.Add((string)checkedListBox1.Items[i]);
                        }
                    }
                }
                if (encontradof == 1)
                {
                    MessageBox.Show("Uno o mas elementos se encuentran en la lista el resto han sido agregados ");
                }




                validaidservicio();

                try
                {
                    f2conectarbd();
                    foreach (string item in checkedListBox2.Items)
                    {
                        string[] valiritem = item.Split('-');
                        string insetasig = valiritem[0];
                        string asigvalor = insetasig.Trim();

                        //f2conectarbd();
                        String consultaparainsert = "select 1 from casino_servicioasig where iduser = '" + asigvalor + "' and idservicio = '" + valorturno + "'";
                        SqlCommand cmdparainsert = new SqlCommand(consultaparainsert, f2conn);
                        SqlDataReader readerparainsert = cmdparainsert.ExecuteReader();

                        if (readerparainsert.HasRows)
                        {
                            readerparainsert.Read();
                            valorselect = comboBox1.SelectedItem.ToString();
                            DateTime dt6 = DateTime.Now;
                            log(dt6 + ": Ya existe usuario " + asigvalor + " para servicio '" + valorselect + "' seleccionado");
                            readerparainsert.Close();
                        }
                        else
                        {
                            //f2conectarbd();
                            readerparainsert.Close();
                            string consultaasig = "insert into casino_servicioasig(iduser,idservicio) values (" + asigvalor + "," + valorturno + ");";
                            SqlCommand cmdasig = new SqlCommand(consultaasig, f2conn);
                            cmdasig.ExecuteNonQuery();
                            //f2conn.Close();


                            String consulta2 = "insert into casino_valexusuarios(iduser, numvales, idserv) values (" + asigvalor + ", " +
                                           1 + ", " + valorturno + ")";
                            SqlCommand cmd2 = new SqlCommand(consulta2, f2conn);
                            cmd2.ExecuteNonQuery();
                            //f2conn.Close();
                        }
                    }
                    f2conn.Close();

                    MessageBox.Show("Servicio Asignado");
                    //checkedListBox2.Items.Clear();
                    checkedListBox1.Items.Clear();
                }
                catch (Exception ex4)
                {
                    DateTime dtex4 = DateTime.Now;
                    log(dtex4 + ": " + ex4.Message);
                    errorconnbd();
                }
                comboBox1.SelectedItem = 0;
            }
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

        private void button3_Click(object sender, EventArgs e)
        {
            validaidservicio();

            try
            {
                f2conectarbd();
                foreach (string item in checkedListBox2.Items)
                {
                    string[] valiritem = item.Split('-');
                    string insetasig = valiritem[0];
                    string asigvalor = insetasig.Trim();

                    //f2conectarbd();
                    String consultaparainsert = "select 1 from casino_servicioasig where iduser = '" + asigvalor + "' and idservicio = '" + valorturno + "'";
                    SqlCommand cmdparainsert = new SqlCommand(consultaparainsert, f2conn);
                    SqlDataReader readerparainsert = cmdparainsert.ExecuteReader();

                    if (readerparainsert.HasRows)
                    {
                        readerparainsert.Read();
                        valorselect = comboBox1.SelectedItem.ToString();
                        DateTime dt6 = DateTime.Now;
                        log(dt6 + ": Ya existe usuario " + asigvalor + " para servicio '" + valorselect + "' seleccionado");
                        readerparainsert.Close();
                    }
                    else
                    {
                        //f2conectarbd();
                        readerparainsert.Close();
                        string consultaasig = "insert into casino_servicioasig(iduser,idservicio) values (" + asigvalor + "," + valorturno + ");";
                        SqlCommand cmdasig = new SqlCommand(consultaasig, f2conn);
                        cmdasig.ExecuteNonQuery();
                        //f2conn.Close();
                    }
                }
                f2conn.Close();

                MessageBox.Show("Servicio Asignado");
                checkedListBox2.Items.Clear();
                checkedListBox1.Items.Clear();
            }
            catch (Exception ex4)
            {
                DateTime dtex4 = DateTime.Now;
                log(dtex4 + ": " + ex4.Message);
                errorconnbd();
            }
            comboBox1.SelectedItem = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkedListBox2.Items.Clear();
            validaidservicio();

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

                        checkedListBox2.Items.Add(bduseridcombo + " - " + bdidusercombo + " - " + bdnamecombo);
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            f2conectarbd();
            user_a_buscar = textBox1.Text;
            cargar_usuarios_text();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
            radioButton1.Checked = false;
            radioButton2.Checked = false;

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

            f2conectarbd();
            user_a_buscar = textBox2.Text;
            cargar_usuarios_text2();
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                checkedListBox2.SetItemChecked(i, false);
            }
            radioButton1.Checked = false;
            radioButton2.Checked = false;

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {


            if (valorturno > 0)
            {
                //f2conectarbd();
                //checkedListBox2.Items.Clear();
                //validaidservicio();

                //try
                //{
                //    f2conectarbd();
                //    String consultacombo = "select distinct bu.userid, bu.badgenumber, bu.Name " +
                //                       " from USERINFO bu " +
                //                       " where exists (select 1 " +
                //                                     " from casino_servicioasig cs " +
                //                                     " where bu.USERID = cs.iduser " +
                //                                     " and cs.idservicio = " + valorturno + ")";
                //    SqlCommand cmdcombo = new SqlCommand(consultacombo, f2conn);
                //    SqlDataReader readercombo = cmdcombo.ExecuteReader();

                //    if (readercombo.HasRows)
                //    {
                //        while (readercombo.Read())
                //        {
                //            int bduseridcombo = Convert.ToInt32(readercombo[0]);
                //            string bdidusercombo = Convert.ToString(readercombo[1]);
                //            string bdnamecombo = Convert.ToString(readercombo[2]);

                //            checkedListBox2.Items.Add(bduseridcombo + " - " + bdidusercombo + " - " + bdnamecombo);
                //        }
                //        readercombo.Close();
                //    }
                //    else
                //    {
                //        MessageBox.Show("No existen usuarios para servicio seleccionado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        readercombo.Close();
                //    }
                //    f2conn.Close();

                //}
                //catch (Exception ex5)
                //{
                //    DateTime dtex5 = DateTime.Now;
                //    log(dtex5 + ": " + ex5.Message);
                //    errorconnbd();
                //}
                //for (int i = 0; i < checkedListBox2.Items.Count; i++)
                //{
                //    checkedListBox2.SetItemChecked(i, true);
                //}
                //radioButton4.Checked = false;

                for (int i = 0; i < checkedListBox2.Items.Count; i++)
                {
                    checkedListBox2.SetItemChecked(i, true);
                }
                radioButton4.Checked = false;
            }
            else if (radioButton4.Checked == true)
            {
                MessageBox.Show("debe seleccionar primero un servicio");
                radioButton4.Checked = false;
            }

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            radioButton3.Checked = false;
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                checkedListBox2.SetItemChecked(i, false);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (comboBox1.SelectedIndex == -1)//Nothing selected
            {
                MessageBox.Show("Debes seleccionar un Servicio", "Error");
            }
            else
            {
                validaidservicio();
                f2conectarbd();


                for (int i = 0; i < checkedListBox2.Items.Count; i++)
                {
                    if (checkedListBox2.GetItemChecked(i))
                    {
                        try
                        {
                            string lineauser = checkedListBox2.Items[i].ToString();
                            string[] valiritem = lineauser.Split('-');
                            string insetasig = valiritem[0];
                            string asigvalor = insetasig.Trim();

                            //f2conectarbd();
                            string consultaasig = "delete casino_servicioasig where iduser = " + asigvalor + " and idservicio = " + valorturno;
                            SqlCommand cmdasig = new SqlCommand(consultaasig, f2conn);
                            cmdasig.ExecuteNonQuery();
                            checkedListBox2.Items.Remove(checkedListBox2.Items[i]);
                            i = i - 1;
                            //f2conn.Close();

                            String consulta2 = "delete from casino_valexusuarios where iduser = " + asigvalor + " and idserv = " + valorturno;
                            SqlCommand cmd2 = new SqlCommand(consulta2, f2conn);
                            cmd2.ExecuteNonQuery();

                        }
                        catch (Exception err)
                        {
                            DateTime dterr = DateTime.Now;
                            log(dterr + ": Error al eliminar usuario de un servicio - " + err.Message);
                            f2conn.Close();
                        }

                    }
                }
                f2conn.Close();
                checkedListBox1.Items.Clear();
                MessageBox.Show("Se eliminó servicio a usuarios seleccionados");
            }


        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
