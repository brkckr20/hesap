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
        Encoding en;
        string strXLST = "";
        private OpenFileDialog openFileDialog1;
        public FrmIrsaliyeFaturaGoruntuleyici()
        {
            InitializeComponent();
            en = Encoding.UTF8;
            string startup_path = Application.StartupPath;
            string xlst_path = startup_path + "\\e-Irsaliye\\Main.xslt";
            string xml_path = startup_path + "\\e-Irsaliye\\Giden\\001.xml"; // bu kısımda hata veriyor düzeltilecek
            var sr = new StreamReader(xml_path);
            string strFaturaXML = sr.ReadToEnd();
            string ErrMsg = "";
            string strResult = Transform(strFaturaXML, strXLST, out ErrMsg);
            if (!File.Exists(xlst_path))
            {
                MessageBox.Show(xlst_path + " dosyası bulunamadı");
                return;
            }
            else
            {
                strXLST = File.ReadAllText(xlst_path, en);
            }

            if (ErrMsg != "")
                MessageBox.Show(ErrMsg);
            else
            {
                webBrowser1.DocumentText = strResult;
            }
        }
        private string Transform(string XMLPage, string XSLStylesheet, out string ErrorMessage)
        {

            string result = "";
            ErrorMessage = "";
            try
            {
                // Reading XML
                TextReader textReader1 = new StringReader(XMLPage);
                XmlTextReader xmlTextReader1 = new XmlTextReader(textReader1);
                XPathDocument xPathDocument = new XPathDocument(xmlTextReader1);

                //Reading XSLT
                TextReader textReader2 = new StringReader(XSLStylesheet);
                XmlTextReader xmlTextReader2 = new XmlTextReader(textReader2);

                //Define XslCompiledTransform
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(xmlTextReader2);


                StringBuilder sb = new StringBuilder();
                TextWriter tw = new StringWriter(sb);

                xslt.Transform(xPathDocument, null, tw);

                result = sb.ToString();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return result;
        }
        void Onizle()
        {
            //try
            //{
            //    // XML belgesini yükle
            //    XmlDocument xmlDoc = new XmlDocument();
            //    xmlDoc.Load(xmlverisi);

            //    // XSLT stil dosyasını yükle
            //    XslCompiledTransform xslt = new XslCompiledTransform();
            //    xslt.Load(xsltVerisi);

            //    // HTML'ye dönüştür
            //    using (System.IO.StringWriter sw = new System.IO.StringWriter())
            //    using (XmlTextWriter writer = new XmlTextWriter(sw))
            //    {
            //        xslt.Transform(xmlDoc, null, writer);
            //        string htmlOutput = sw.ToString();

            //        // WebBrowser ile HTML içeriğini göster
            //        webBrowser1.DocumentText = htmlOutput;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Bir hata oluştu: {ex.Message}");
            //}
        }
        private void FrmIrsaliyeFaturaGoruntuleyici_Load(object sender, EventArgs e)
        {
            Onizle();
        }
    }
}