using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace brfnt
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public int ImageWidth
        {
            get
            {
                return int.Parse(tbWidth.Text);
            }
            set
            {
                tbWidth.Text = value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int.Parse(tbWidth.Text);

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
