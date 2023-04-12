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

namespace TestBinary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FileStream tok = new FileStream("seznam.dat", FileMode.Open, FileAccess.Read);
            listBox1.Items.Clear();
            BinaryReader cteni = new BinaryReader(tok);
            cteni.BaseStream.Position = 0;
            while (cteni.BaseStream.Position<cteni.BaseStream.Length)
            {
                //cteni.ReadInt32();
                string znamky = cteni.ReadString();
                listBox1.Items.Add(znamky);
                textBox1.Text+= cteni.ReadString()+Environment.NewLine;
            }
            tok.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FileStream tok = new FileStream("seznam.dat", FileMode.OpenOrCreate, FileAccess.Write);
            BinaryWriter zapis = new BinaryWriter(tok, Encoding.UTF8);
            int i = 0;
            foreach (string s in textBox2.Lines)
            {
                i++;
            }
            i = 0;
           // numericUpDown1.Value = i;
           // zapis.Write(i);
            foreach (string s in textBox2.Lines)
            {
                zapis.Write(textBox3.Text);
                zapis.Write(textBox2.Lines[i]);
                i++;
            }
            tok.Close();
        }
    }
}
