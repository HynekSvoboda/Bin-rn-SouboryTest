using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;

namespace TestBinary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();
            numericUpDown1.Visible = false;
            numericUpDown2.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button5.Visible = false;
        }

        int pocetznamek;
        int pocet;
        string znamky;
        private void button1_Click(object sender, EventArgs e)
        {
            pocetznamek = (int)numericUpDown3.Value;
            pocet = 0;
            string jmeno = textBox1.Text;
            if (!string.IsNullOrEmpty(jmeno)&&jmeno!="")
            {

            numericUpDown1.Visible = true;
            numericUpDown2.Visible = true;
            label1.Visible = true;
            znamky= "";
            label2.Visible = true;
            button2.Visible = true;
            listBox1.Items.Clear();
            }
            else MessageBox.Show("Nezadal jsi jméno", "ERROR");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pocet++;
            if (pocet <= pocetznamek)
            {
                znamky += numericUpDown2.Value.ToString() + " " + numericUpDown1.Value.ToString() + " ";
                if(pocet==pocetznamek)
                {
                    listBox1.Items.Add(znamky);
                    button2.Visible = false;
                    button3.Visible = true;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FileStream tok = new FileStream("seznam.dat", FileMode.OpenOrCreate, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(tok);
            bw.BaseStream.Position = bw.BaseStream.Length;
            bw.Write(pocetznamek);
            string jmeno = textBox1.Text;
            foreach (string c in listBox1.Items)
            {
                bw.Write(c);
                bw.Write(jmeno);
            }
            tok.Close();
            textBox1.Text = "";
            listBox1.Items.Clear();
            button5.Visible = true;
            button3.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FileStream tok = new FileStream("seznam.dat", FileMode.Open, FileAccess.ReadWrite);
            BinaryReader br = new BinaryReader(tok);
            double pocetcis = 0;
            double soucet = 0;
            double prumer = 0;
            double pocitadlo = 1;
            double pomocnacis = 0;
            double pomocnanas = 0;
            br.BaseStream.Position = 0;
            listBox2.Items.Clear();
            textBox2.Text = "";

            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                int pocet = br.ReadInt32();
                string cteniznamky = br.ReadString();
                string jmeno =br.ReadString();
                char[] znamkychar = cteniznamky.ToCharArray();
                pocetcis = 0;
                soucet = 0;
                prumer = 0;
                pocitadlo = 1;
                pomocnacis = 0;
                pomocnanas = 0;
                for (int i=0;i<znamkychar.Count()-1;i++)
                    {
                        if (znamkychar[i]!=' ')
                        {
                           
                            int cislo = Convert.ToInt32(znamkychar[i]) - 48;
                            if (pocitadlo % 2 == 0)
                            {
                                pomocnacis =Convert.ToDouble(cislo);
                            }
                            else
                            {
                                pomocnanas = Convert.ToDouble(cislo);
                                pocetcis += cislo;
                            }
                            if(pomocnacis!=0&&pomocnanas!=0)
                            {
                                soucet += pomocnanas * pomocnacis;
                                pomocnacis = 0;
                                pomocnanas = 0;
                            }
                            pocitadlo++;
                        }
                }
                prumer = soucet / (pocetcis);
                if (prumer >=1&&prumer<=1.8) MessageBox.Show("Vychází ti 1", "GRATULACE");
                else if(prumer>=3.6&&prumer<=4.2) MessageBox.Show("Vychází ti 4", "UPOZORNĚNÍ");
                else if(prumer>4.2)
                {
                    BinaryWriter bw = new BinaryWriter(tok);
                    bw.BaseStream.Position = br.BaseStream.Position;
                    int delkajmena = jmeno.Length;
                    string john = "John Doe";
                    bw.BaseStream.Seek(-delkajmena, SeekOrigin.Current);
                    jmeno = john;
                    bw.Write(john);
                    MessageBox.Show("Vychází ti 5", "POZOR");
                }

                listBox2.Items.Add(cteniznamky);
                textBox2.Text += jmeno + Environment.NewLine;
            }
            tok.Close();
        }
    }
}
