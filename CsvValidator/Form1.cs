using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsvValidator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void lbLinkFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                lbLinkFile.Text = openFileDialog.FileName;
            }
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            if (!lbLinkFile.Text.Equals("Browse", StringComparison.CurrentCultureIgnoreCase))
            {
                var validation = new XeroxValidator(lbLinkFile.Text);
                validation.Validate();
                MessageBox.Show("Done");
            }
        }
    }
}
