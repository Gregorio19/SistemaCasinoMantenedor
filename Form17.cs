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
    public partial class Form17 : Form
    {
        public string ReturnNumVales { get; set; }
        string pasonomserv;

        public Form17(string nomserv)
        {
            pasonomserv = nomserv;
            InitializeComponent();
        }

        private void Form17_Load(object sender, EventArgs e)
        {
            label1.Text = pasonomserv;
            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                ReturnNumVales = textBox1.Text;
                //MessageBox.Show("Se incorporaron " + textBox1.Text + " Vales");
                this.Close();
            }
            else
            {
                MessageBox.Show("Debe ingresar un número de vales");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReturnNumVales = "0";
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Para obligar a que sólo se introduzcan números 
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan 
                    e.Handled = true;
                }
        }
    }
}
