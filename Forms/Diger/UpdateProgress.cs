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
            progressBar1.Value = progress;
            labelStatus.Text = status;
        }
    }
}
