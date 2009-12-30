using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Google.API.Translate;

namespace BMG
{
    public partial class DictForm : Form
    {
        TranslateClient client = new TranslateClient("code.google.com/p/acrtranslation");

        public DictForm()
        {
            InitializeComponent();
        }

        private void DictForm_Load(object sender, EventArgs e)
        {
        }

        public void Translation(string text)
        {
            try
            {
                tbText.Text = client.Translate(text, Language.English, Language.Chinese);
            }
            catch (Exception ex)
            {
                tbText.Text = ex.Message;
            }
        }

        private void DictForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}
