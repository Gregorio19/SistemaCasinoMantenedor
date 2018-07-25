using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Casino
{
    public partial class Form5 : Form
    {

        string folderName;
        public int varf2;
        public string varf3;


        public Form5()
        {
            InitializeComponent();
        }

        interface IAddItem
        {
            void AddNewItem(DataGridViewRow row);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                DialogResult result = folderBrowserDialog1.ShowDialog();
                folderName = folderBrowserDialog1.SelectedPath;
                textBox1.Text = folderName;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Debe ingresar ruta para generar reporte");
            }
            else
            {
                varf2 = 1;
                varf3 = folderName;
                textBox1.Text = "";
                this.Close();
            }
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Debe ingresar ruta para generar reporte");
            }
            else
            {
                varf2 = 2;
                varf3 = folderName;
                textBox1.Text = "";
                this.Close();
            }
        }


    }
}
