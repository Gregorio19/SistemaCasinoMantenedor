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
    public partial class Form7 : Form
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

        System.Data.SqlClient.SqlConnection f7conn;

        private void errorconnbd()
        {
            MessageBox.Show("No se pudo establecer conexión con la base de datos.\rAplicación se cerrará.\rRevise su configuración de Base de Datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }

        public Form7()
        {
            InitializeComponent();

            button2.Enabled = false;
            button4.Enabled = false;

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

                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
                textBox1.Enabled = false;
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = comboBox1.SelectedIndex;

            if (idx != -1)
            {
                dateTimePicker2.Enabled = false;
                textBox1.Enabled = false;
                button2.Enabled = false;
                button1.Enabled = true;
                button4.Enabled = true;

                dateTimePicker1.Format = DateTimePickerFormat.Short;
                dateTimePicker1.Value = new DateTime(2015, 01, 01);
                validanulo = 0;
                dateTimePicker2.Format = DateTimePickerFormat.Short;
                dateTimePicker2.Value = new DateTime(2015, 01, 01);
                valormodif = "";
                textBox1.Text = "";

                string comboselect = comboBox1.SelectedItem.ToString();
                codserv = Convert.ToInt32(comboselect.Substring(0, 1));

                try
                {
                    conectarbd();

                    String consulta7 = "select costoservicio from casino_costos where idcosto = " + codserv;
                    SqlCommand cmd7 = new SqlCommand(consulta7, f7conn);
                    SqlDataReader reader7 = cmd7.ExecuteReader();
                    reader7.Read();

                    if (reader7.HasRows)
                    {
                        try
                        {
                            conectarbd();

                            String consulta8 = "select max(ultreg) from casino_costos where idcosto = " + codserv;
                            SqlCommand cmd8 = new SqlCommand(consulta8, f7conn);
                            SqlDataReader reader8 = cmd8.ExecuteReader();
                            reader8.Read();

                            ultreg = Convert.ToInt32(reader8[0]);

                            reader7.Close();
                            f7conn.Close();
                        }
                        catch (Exception)
                        {
                            errorconnbd();
                        }

                        if (ultreg == 1)  //Existe sólo una tarifa configurada, desplegar los datos con controles bloqueados
                        {
                            try
                            {
                                conectarbd();

                                String consulta9 = "select distinct costoservicio, fecinival, fecfinval from casino_costos where idcosto = " + codserv;
                                SqlCommand cmd9 = new SqlCommand(consulta9, f7conn);
                                SqlDataReader reader9 = cmd9.ExecuteReader();
                                reader9.Read();

                                valormodif = Convert.ToString(reader9[0]);
                                fecinival2 = Convert.ToDateTime(reader9[1]);
                                fecfinval2 = Convert.ToDateTime(reader9[2]);

                                textBox1.Text = valormodif;
                                dateTimePicker1.Value = fecfinval2;
                                dateTimePicker2.Value = fecfinval2;
                                dateTimePicker1.Enabled = false;
                                dateTimePicker2.Enabled = true;
                                textBox1.Enabled = true;
                                button2.Enabled = true;

                                reader9.Close();
                                f7conn.Close();
                            }
                            catch (Exception)
                            {
                                errorconnbd();
                            }
                        }
                        else
                        {
                            try
                            {
                                conectarbd();

                                String consulta10 = "select distinct costoservicio, fecinival, fecfinval from casino_costos where idcosto = " + codserv + "and ultreg = " + ultreg;
                                SqlCommand cmd10 = new SqlCommand(consulta10, f7conn);
                                SqlDataReader reader10 = cmd10.ExecuteReader();
                                reader10.Read();

                                valormodif = Convert.ToString(reader10[0]);
                                fecinival2 = Convert.ToDateTime(reader10[1]);
                                fecfinval2 = Convert.ToDateTime(reader10[2]);

                                textBox1.Text = valormodif;
                                dateTimePicker1.Value = fecfinval2;
                                dateTimePicker2.Value = fecfinval2;
                                dateTimePicker1.Enabled = false;
                                dateTimePicker2.Enabled = true;
                                textBox1.Enabled = true;
                                button2.Enabled = true;

                                reader10.Close();
                                f7conn.Close();
                            }
                            catch (Exception)
                            {
                                errorconnbd();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No existen Fechas Configuradas del Servicio.\r Designelas y luego Presione botón 'Crear Nuevo' para Asignar un nuevo costo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        validanulo = 1;

                        //NUEVO, Cuando un servicio no tiene fecha ni costo configurado
                        dateTimePicker1.Value = DateTime.Now;
                        dateTimePicker2.Value = DateTime.Now.AddDays(1);
                        ultreg = 0;

                        dateTimePicker1.Enabled = true;
                        dateTimePicker2.Enabled = true;
                        textBox1.Enabled = true;
                        button2.Enabled = true;

                        button1.Enabled = false;

                        textBox1.Select();
                    }
                }
                catch (Exception)
                {
                    errorconnbd();
                }
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (validanulo == 1)
            {
                DateTime validafec = dateTimePicker2.Value;
                DateTime validahoy = DateTime.Now;

                if (validafec == validahoy || validafec < validahoy)
                {
                    //MessageBox.Show("La fecha fin sólo puede ser a partir de mañana", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dateTimePicker2.Value = DateTime.Now.AddDays(1);
                }
            }
            else
            {
                validanulo = 1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string obtieneindex = comboBox1.SelectedIndex.ToString();
            int validaselect = Convert.ToInt32(obtieneindex);

            if (validaselect != -1)
            {
                Form15 frm15 = new Form15();
                frm15.ShowDialog();

                seleccionopcion = frm15.opcion;

                if (seleccionopcion == 1)
                {
                    if (string.IsNullOrEmpty(valormodif))
                    {
                        dateTimePicker1.Value = DateTime.Now;
                        dateTimePicker2.Value = DateTime.Now.AddDays(1);
                        ultreg = 0;

                        dateTimePicker2.Enabled = true;
                        textBox1.Enabled = true;
                        button2.Enabled = true;
                        textBox1.Select();
                    }
                    else
                    {
                        validanulo = 0;
                        dateTimePicker1.Value = Convert.ToDateTime(dateTimePicker2.Value).AddDays(1);
                        dateTimePicker2.Value = Convert.ToDateTime(dateTimePicker1.Value).AddDays(1);
                        dateTimePicker2.Enabled = true;
                        textBox1.Enabled = true;
                        button2.Enabled = true;
                        textBox1.Select();
                    }
                    button1.Enabled = false;
                    seleccionopcion = 0;
                    frm15.opcion = 0;
                }

                if (seleccionopcion == 2)
                {
                    button1.Enabled = false;
                    button2.Enabled = true;
                    textBox1.Enabled = true;
                    seleccionopcion = 0;
                    frm15.opcion = 0;
                }
            }
            else
            {
                MessageBox.Show("Primero debe seleccionar un servicio");
                seleccionopcion = 0;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Debe ingresar costo del servicio", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Select();
            }
            else
            {
                if (Convert.ToInt32(textBox1.Text) <= 9999999)
                {
                    try
                    {
                        conectarbd();
                        String consultaidioma = "select @@LANGUAGE";
                        SqlCommand cmdidioma = new SqlCommand(consultaidioma, f7conn);
                        SqlDataReader readeridioma = cmdidioma.ExecuteReader();
                        readeridioma.Read();
                        string vidioma = Convert.ToString(readeridioma[0]);
                        f7conn.Close();

                        ultreg = ultreg + 1;

                        string pasoinieng = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                        string pasofineng = dateTimePicker2.Value.ToString("yyyy-MM-dd");

                        string pasoiniesp = dateTimePicker1.Value.ToString("dd-MM-yyyy");
                        string pasofinesp = dateTimePicker2.Value.ToString("dd-MM-yyyy");

                        conectarbd();

                        if (vidioma == "us_english")
                        {
                            consulta12 = "insert into casino_costos(idcosto, costoservicio, fecinival, fecfinval, ultreg) " +
                                         "values (" + codserv + "," + textBox1.Text + ", convert(char(10),'" + pasoinieng + "',103), convert(char(10),'" + pasofineng + "',103)," + ultreg + ")";
                        }
                        else
                        {
                            consulta12 = "insert into casino_costos(idcosto, costoservicio, fecinival, fecfinval, ultreg) " +
                                         "values (" + codserv + "," + textBox1.Text + ", convert(char(10),'" + pasoiniesp + "',103), convert(char(10),'" + pasofinesp + "',103)," + ultreg + ")";
                        }

                        SqlCommand cmd12 = new SqlCommand(consulta12, f7conn);
                        cmd12.ExecuteNonQuery();
                        f7conn.Close();
                        MessageBox.Show("Configuración de servicio registrado exitosamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dateTimePicker2.Enabled = false;
                        textBox1.Enabled = false;
                        button2.Enabled = false;

                        textBox1.Text = "";
                        comboBox1.SelectedIndex = -1;
                    }
                    catch (Exception ex)
                    {
                        string err = ex.Message;
                        MessageBox.Show("Error al insertar registro: " + err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("El costo del servicio no puede ser mayor a $9.999.999", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Text = "";
                    textBox1.Select();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                string Linea;
                Linea = comboBox1.SelectedItem.ToString();
                string[] campo1 = Linea.Split('-');
                string intserid2 = campo1[0];
                string intsernam2 = campo1[1];

                Form8 frm8 = new Form8(intserid2, intsernam2);
                frm8.ShowDialog();
            }

            else
            {
                MessageBox.Show("debe seleccionar un servicio");
            }
            
        }

    }
}
