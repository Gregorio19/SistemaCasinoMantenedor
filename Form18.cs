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
    public partial class Form18 : Form
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

        int procesaiduser;
        string procesanombre;

        string valor;
        string[] valoresserv;
        string idservasig;
        int inserto = 0;

        public int insertopaso { get; set; }

        public Form18(int recibeiduser, string recibenombre)
        {
            InitializeComponent();
            procesaiduser = recibeiduser;
            procesanombre = recibenombre;
            label1.Text = procesanombre;
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

        private void cargaservdisponibles()
        {
            try
            {
                cargadatosbd();
                f2conectarbd();
                String consulta = "select distinct act.TimeZoneID, act.name " +
                                   " from ACTimeZones act " +
                                   " where not exists (select 1 " +
                                                     " from casino_servicioasig cs " +
                                                     " where cs.iduser = " + procesaiduser +
                                                     " and cs.idservicio = act.TimeZoneID)";

                SqlCommand cmd = new SqlCommand(consulta, f2conn);
                SqlDataReader reader = cmd.ExecuteReader();

                checkedListBox1.Items.Clear();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string idservdisponible = Convert.ToString(reader[0]);
                        string servdisponible = Convert.ToString(reader[1]);

                        checkedListBox1.Items.Add(idservdisponible + " - " + servdisponible, CheckState.Unchecked);
                    }
                    reader.Close();
                }
                else
                {
                    MessageBox.Show("Usuario tiene todos los servicios asignados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reader.Close();
                    f2conn.Close();
                    this.Close();
                }
                f2conn.Close();
            }
            catch (Exception ex1)
            {
                DateTime dtex1 = DateTime.Now;
                msgerror = ex1.Message;
                admerrores();
                errorconnbd();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            insertopaso = inserto;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            inserto = 0;

            if (checkedListBox1.CheckedItems.Count != 0)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        valor = (string)checkedListBox1.Items[i];
                        valoresserv = valor.Split();
                        idservasig = Convert.ToString(valoresserv[0]);

                        try
                        {
                            cargadatosbd();
                            f2conectarbd();
                            String consulta2 = "insert into casino_servicioasig(iduser, idservicio) values (" +
                                                procesaiduser + "," + idservasig + ")";
                            SqlCommand cmd2 = new SqlCommand(consulta2, f2conn);
                            cmd2.ExecuteNonQuery();
                            f2conn.Close();
                        }
                        catch (Exception msins)
                        {
                            msgerror = msins.Message;
                            admerrores();
                            f2conn.Close();
                        }

                        inserto = 1;
                        insertopaso = inserto;
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Servicio");
                inserto = 0;
                insertopaso = inserto;
            }

            if (inserto == 1)
            {
                MessageBox.Show("Servicios Asignados Correctamente");
                inserto = 0;
                this.Close();
            }
        }

        private void Form18_Load(object sender, EventArgs e)
        {
            cargaservdisponibles();
        }
    }
}
