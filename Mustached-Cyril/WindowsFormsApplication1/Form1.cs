using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HSR_Helper.DomainLibrary.Domain;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HSR_Helper.DomainLibrary.Helper.DomainLibraryHelper.GetLunchtable(Cb);
        }

        private void Cb(Lunchtable lunchtable)
        {
            Console.WriteLine(lunchtable.ToString());
        }
    }
}
