﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lineage2ServerLauncher
{
    internal class LoginServer
    {
        int p;      

        

        public string getPath()
        {
            return Path.GetFullPath(@"java\bin\javaw.exe");
        }

        public void Run(Form3 f)
        {
            Process proc = Process.Start(new ProcessStartInfo
            {
                FileName = getPath(),
                WorkingDirectory = @"server/login",
                Arguments = @"-server -Xms1024m -Xmx1024m -jar ../libs/LoginServer.jar",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            });
            proc.EnableRaisingEvents = true;
            p = proc.Id;
            proc.Exited += (sae, sea) =>
            {
                Form1.ActiveForm.Controls["button7"].Invoke(new Action(() =>
                {
                    Form1.ActiveForm.Controls["button7"].Enabled = true;
                }));
            };
            proc.OutputDataReceived += (sa, ea) =>
            {
                f.textBox1.BeginInvoke(new Action(() =>
                {
                    f.textBox1.Text += ea.Data + Environment.NewLine;
                }));
            };
            proc.ErrorDataReceived += (s, a) =>
            {
                f.textBox1.BeginInvoke(new Action(() =>
                {
                    f.textBox1.Text += a.Data + Environment.NewLine;
                }));
            };


            proc.BeginOutputReadLine();
            proc.BeginErrorReadLine();
        }

        public void Stop()
        {
            Process.GetProcessById(p).Kill();
        }
    }
}