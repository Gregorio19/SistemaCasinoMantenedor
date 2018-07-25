using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Casino
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*if (!LicenciaValida())
            {
                MessageBox.Show("Software No Licenciado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Application.Exit();
            }
            else
            {*/
                timer1.Start();
            //}
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Hide();
            timer1.Stop();
            Form19 frm19 = new Form19();
            frm19.ShowDialog();
        }
    }
}
