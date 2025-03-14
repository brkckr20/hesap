// UpdateChecker.cs
using System;
using System.Net;
using System.Text.Json;
using System.Windows.Forms;
using System.Diagnostics;
using DevExpress.XtraEditors;
using System.Web.UI;
using Hesap.Forms.Diger;
using System.Threading.Tasks;

public class UpdaterHelper
{
    private const string repoUrl = "https://api.github.com/repos/brkckr20/maliyet-hesaplama/releases/latest";
    private const string currentVersion = "1.0.0"; // Mevcut versiyonun

    public void CheckForUpdate(Form form)
    {
        try
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("User-Agent", "request");

                string json = client.DownloadString(repoUrl);
                var release = JsonSerializer.Deserialize<GitHubRelease>(json);

                if (release != null && release.tag_name != currentVersion)
                {
                    var result = XtraMessageBox.Show($"Yeni versiyon {release.tag_name} mevcut! Güncellemek ister misiniz?",
                                                  "Güncelleme Var",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        string downloadUrl = release.assets[0].browser_download_url;
                        string tempPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "MaliyetProgramiSetup.exe");

                        Hesap.Forms.Diger.UpdateProgress updateProgress = new Hesap.Forms.Diger.UpdateProgress();
                        Task.Run(() =>
                        {
                            updateProgress.ShowDialog();  // Modal olarak aç
                        });

                        client.DownloadFileAsync(new Uri(downloadUrl), tempPath);

                        client.DownloadProgressChanged += (sender, e) =>
                        {
                            updateProgress.UpdateProgressF(e.ProgressPercentage, $"İndiriliyor... {e.ProgressPercentage}%");
                        }; 
                        client.DownloadFileCompleted += (sender, e) =>
                        {
                            //Process.Start(tempPath);
                            var restartResult = MessageBox.Show("Güncellemelerin geçerli olabilmesi için lütfen programı yeninden başlatınız?",
                                                                "Yeniden Başlat",
                                                                MessageBoxButtons.YesNo,
                                                                MessageBoxIcon.Information);
                            if (restartResult == DialogResult.OK)
                            {
                                Application.Exit();  // Programı kapat
                            }
                        };
                    }
                }
            }// uygulamayı indirme ve güncelleme işleminden devam edilecek!!!!
        }
        catch (Exception ex)
        {
            MessageBox.Show("Güncelleme kontrol edilirken hata oluştu: " + ex.Message);
        }
    }
}

public class GitHubRelease
{
    public string tag_name { get; set; }
    public GitHubAsset[] assets { get; set; }
}

public class GitHubAsset
{
    public string browser_download_url { get; set; }
}
