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
    public partial class Form15 : Form
    {
        public int opcion { get; set; }

        public Form15()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            opcion = 1;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            opcion = 2;
            this.Close();
        }
    }
}
