using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Hesap.Forms
{
    public partial class FrmIrsaliyeFaturaGoruntuleyici : DevExpress.XtraEditors.XtraForm
    {
        public FrmIrsaliyeFaturaGoruntuleyici()
        {
            InitializeComponent();
        }
        void Onizle()
        {
            string appPath = Application.StartupPath;
            string xmlPath = appPath+ "\\e-Irsaliye\\Invoice.xml";
            string xsltPath = appPath + "\\e-Irsaliye\\Invoice.xslt";

            string transformedHtml = TransformXmlWithXslt(xmlPath, xsltPath);

            webBrowser1.DocumentText = transformedHtml;
        }
        private string TransformXmlWithXslt(string xmlPath, string xsltPath)
        {
            try
            {
                var xslt = new XslCompiledTransform();
                xslt.Load(xsltPath);

                using (var writer = new System.IO.StringWriter())
                {
                    xslt.Transform(xmlPath, null, writer);
                    return writer.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
                return string.Empty;
            }
        }
        private void FrmIrsaliyeFaturaGoruntuleyici_Load(object sender, EventArgs e)
        {
            Onizle();
        }
    }
}