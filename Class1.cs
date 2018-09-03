using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Management;
using System.IO;
using System.Collections;
using System.Threading;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Net.NetworkInformation;

namespace Casino
{
    class Generador_clave
    {
        public string generar_key()
        {
            //serial();
            string serie1 = serial(), serieA = "", serieB, serieC, serieD;

            for (int i = 0; i < serie1.Length; i++)
            {
                serieA += parse_char_to_int(serie1[i]);
            }
            serieB = serieC = serieD = serieA.ToString();
            serieA = serieA.ToString();
            serieA = rewrite_the_serie(serie1, serieA, 80, 401, 1402);
            serieB = rewrite_the_serie(serie1, serieB, 4, 763, 7546545);
            serieC = rewrite_the_serie(serie1, serieC, 673, 26645, 9835);
            serieD = rewrite_the_serie(serie1, serieD, 723, 8245, 402);
            string serial_enviar = serieA + "-" + serieB + "-" + serieC + "-" + serieD;
            return serial_enviar;
        }

        private string serial()
        {
            string serialmadre = "";
            //MessageBox.Show("SerialNumber:");
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    //Console.WriteLine("-----------------------------------");
                    //Console.WriteLine("Win32_BaseBoard instance");
                    //Console.WriteLine("-----------------------------------");
                    //Console.WriteLine("SerialNumber: {0}", queryObj["SerialNumber"]);
                    //MessageBox.Show("hola " + queryObj["SerialNumber"].ToString());
                    serialmadre = queryObj["SerialNumber"].ToString();
                }
            }
            catch (ManagementException e)
            {
                MessageBox.Show("An error occurred while querying for WMI data: " + e.Message);
            }

            return serialmadre;
        }

        private int parse_char_to_int(char letra)
        {

            switch (letra)
            {
                case 'A':
                    return 1;
                case 'B':
                    return 2;
                case 'C':
                    return 3;
                case 'D':
                    return 4;
                case 'E':
                    return 5;
                case 'F':
                    return 6;
                case 'G':
                    return 7;
                case 'H':
                    return 8;
                case 'I':
                    return 9;
                case 'J':
                    return 10;
                case 'K':
                    return 11;
                case 'L':
                    return 12;
                case 'M':
                    return 13;
                case 'N':
                    return 14;
                case 'O':
                    return 15;
                case 'P':
                    return 16;
                case 'Q':
                    return 17;
                case 'R':
                    return 18;
                case 'S':
                    return 19;
                case 'T':
                    return 20;
                case 'U':
                    return 21;
                case 'V':
                    return 22;
                case 'W':
                    return 23;
                case 'X':
                    return 24;
                case 'Y':
                    return 25;
                case 'Z':
                    return 26;
                default:
                    return Int32.Parse("" + letra);
            }

        }

        public char parse_nume_to_char(int nume)
        {
            if (nume > 26)
            {
                nume = (int)(nume / 4);
            }
            switch (nume)
            {
                case 1:
                    return 'A';

                case 2:
                    return 'B';

                case 3:
                    return 'C';

                case 4:
                    return 'D';

                case 5:
                    return 'E';

                case 6:
                    return 'F';

                case 7:
                    return 'G';

                case 8:
                    return 'H';

                case 9:
                    return 'I';

                case 10:
                    return 'J';

                case 11:
                    return 'K';

                case 12:
                    return 'L';

                case 13:
                    return 'M';

                case 14:
                    return 'N';

                case 15:
                    return 'O';

                case 16:
                    return 'P';

                case 17:
                    return 'Q';

                case 18:
                    return 'R';

                case 19:
                    return 'S';

                case 20:
                    return 'T';

                case 21:
                    return 'U';

                case 22:
                    return 'V';

                case 23:
                    return 'W';

                case 24:
                    return 'X';

                case 25:
                    return 'Y';

                case 26:
                    return 'Z';

                default:
                    return 'W';

            }
        }

        public string rewrite_the_serie(string serie1, string serieA, int num1, int num2, int num3)
        {
            string auxcadena;
            long aux = 0;
            long x = 0;
            long.TryParse(serieA, out x);
            aux = aux + x;

            aux = ((((aux * serie1.Length) * serieA.Length) / num1) * num2) / num3;
            serieA = "" + aux;

            while (aux.ToString().Length != 10)
            {
                if (aux.ToString().Length > 10)
                {
                    aux = aux / (aux.ToString().Length / 2);
                }
                else if (serieA.Length < 10)
                {
                    aux = aux / (aux.ToString().Length / 3);
                }
            }
            auxcadena = aux.ToString();
            serieA = "";

            for (int i = 0; i < aux.ToString().Length; i++)
            {
                int paux = int.Parse("" + auxcadena[i] + "" + auxcadena[i + 1]);
                serieA += parse_nume_to_char(paux);
                i = i + 1;
            }
            return serieA;

        }
    }


}
