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
    public partial class Form8 : Form
    {
        string path = Application.StartupPath;
        string f8vfipbdsoft;
        string f8vfbdsoft;
        string f8vfusersoft;
        string f8vfclavesoft;
        int f8check;
        string recid;
        string recname;
        int ulterg;

        System.Data.SqlClient.SqlConnection f8conn;

        private void errorconnbd()
        {
            MessageBox.Show("No se pudo establecer conexión con la base de datos.\rAplicación se cerrará.\rRevise su configuración de Base de Datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }

        public Form8(string srvid, string srvname)
        {
            InitializeComponent();
            this.KeyUp += new KeyEventHandler(cerrar_form);

            recid = srvid;
            recname = srvname;

            using (StreamReader Lee = new StreamReader(path + @"\casino.out"))
            {
                string Linea;
                Linea = Lee.ReadLine();
                f8check = Convert.ToInt32(Linea);

                Linea = Lee.ReadLine();
                f8vfipbdsoft = Linea;

                Linea = Lee.ReadLine();
                f8vfbdsoft = Linea;

                Linea = Lee.ReadLine();
                f8vfusersoft = Linea;

                Linea = Lee.ReadLine();
                f8vfclavesoft = Linea;
            }
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            try
            {
                conectarbd();

                String consultafr8 = "select idcosto, costoservicio, fecinival, fecfinval from casino_costos where idcosto = " + recid + " order by ultreg asc";
                SqlCommand cmdfr8 = new SqlCommand(consultafr8, f8conn);
                SqlDataReader readerfr8 = cmdfr8.ExecuteReader();
                //readerfr8.Read();

                if (readerfr8.HasRows)
                {
                    while (readerfr8.Read())
                    {
                        string bdidserv = Convert.ToString(readerfr8[0]);
                        int bdcostser = Convert.ToInt32(readerfr8[1]);
                        DateTime bdfecini = Convert.ToDateTime(readerfr8[2]);
                        DateTime bdfecfin = Convert.ToDateTime(readerfr8[3]);
                        CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
                        string pasomiles = bdcostser.ToString();
                        dataGridView1.Rows.Add(recname, pasomiles, bdfecini, bdfecfin);
                    }
                }
                else
                {
                    MessageBox.Show("No existen costos configurados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                errorconnbd();
            }

        }

        void cerrar_form(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
            {
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void conectarbd()
        {
            if (f8check == 0)
            {
                try
                {
                    f8conn = new System.Data.SqlClient.SqlConnection();
                    f8conn.ConnectionString = "Server=" + f8vfipbdsoft + ";initial catalog=" + f8vfbdsoft + ";user=" + f8vfusersoft + ";password=" + f8vfclavesoft + ";Trusted_Connection=FALSE";
                    f8conn.Open();
                }
                catch (Exception)
                {
                    MessageBox.Show("No se pudo establecer conexión con la base de datos");
                    this.Close();
                }
            }

            if (f8check == 1)
            {
                try
                {
                    f8conn = new System.Data.SqlClient.SqlConnection();
                    f8conn.ConnectionString = "Server=" + f8vfipbdsoft + ";initial catalog=" + f8vfbdsoft + ";user=" + f8vfusersoft + ";password=" + f8vfclavesoft + ";Trusted_Connection=FALSE";
                    f8conn.Open();
                }
                catch (Exception)
                {
                    MessageBox.Show("No se pudo establecer conexión con la base de datos");
                    this.Close();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

           // MessageBox.Show("numero " + e.ColumnIndex);
            if (e.RowIndex >-1 && e.ColumnIndex >0)
            {
                panel_edit_item.Visible = true;
                edit_elemenet.Visible = true;
                edit_item_win.Text = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                itemtoedit.Text = dataGridView1.Columns[e.ColumnIndex].HeaderText;
                ulterg = (e.RowIndex + 1);
            }
            
            // MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
        }

        private void volver_win_Click(object sender, EventArgs e)
        {
            panel_edit_item.Visible = false;
            edit_elemenet.Visible = false;
        }

        private void save_edit_Click(object sender, EventArgs e)
        {
            panel_edit_item.Visible = false;
            edit_elemenet.Visible = false;
            String updateserv = "";
            try
            {
                conectarbd();
                if (itemtoedit.Text == "Costo Servicio")
                {
                    updateserv = "update casino_costos " +
                    "set costoservicio = '" + edit_item_win.Text +  "'" +
                    "where idcosto = '" + recid + "'" + "AND ultreg = '" + ulterg + "'";
                    dataGridView1.Rows[ulterg - 1].Cells[1].Value = edit_item_win.Text;
                }

                if (itemtoedit.Text == "Fecha Inicio Validez")
                {
                    updateserv = "update casino_costos " +
                    "set fecinival = '" + edit_item_win.Text + "'" +
                    "where idcosto = '" + recid + "'" + "AND ultreg = '" + ulterg + "'";
                    dataGridView1.Rows[ulterg - 1].Cells[2].Value = edit_item_win.Text;
                }

                if (itemtoedit.Text == "Fecha Fin Validez")
                {
                    updateserv = "update casino_costos " +
                    "set fecfinval = '" + edit_item_win.Text + "'" +
                    "where idcosto = '" + recid + "'" + "AND ultreg = '" + ulterg + "'";
                    dataGridView1.Rows[ulterg - 1].Cells[3].Value = edit_item_win.Text;
                }


                SqlCommand cmdudps = new SqlCommand(updateserv, f8conn);
                cmdudps.ExecuteNonQuery();
                f8conn.Close();

                MessageBox.Show(itemtoedit.Text + " modificado correctamente");

            }
            catch (Exception)
            {
                errorconnbd();
            }
        }
    }
}
