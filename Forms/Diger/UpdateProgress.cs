using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hesap.Forms.Diger
{
    public partial class UpdateProgress : Form
    {
        public UpdateProgress()
        {
            InitializeComponent();
        }
        public void UpdateProgressF(int progress, string status)
        {
            if (progressBar1.InvokeRequired || labelStatus.InvokeRequired)
            {
                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Value = progress;
                    labelStatus.Text = status;
                }));
            }
            else
            {
                progressBar1.Value = progress;
                labelStatus.Text = status;
            }
        }

        private void UpdateProgress_Load(object sender, EventArgs e)
        {
            lblNameSurname.Text += " - " + DateTime.Now.Year.ToString();
        }
    }
}
