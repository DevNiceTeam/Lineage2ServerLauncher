using System;
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

        public void Run(LoginForm f)
        {
            Process proc = Process.Start(new ProcessStartInfo
            {
                FileName = Path.GetFullPath(@"java\bin\javaw.exe"),
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
                Exited(f);
                //Form1.ActiveForm.Controls["button7"].BeginInvoke(new Action(() =>
                //{
                //    Form1.ActiveForm.Controls["button7"].Enabled = true;
                //}));
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

        public bool Exited(LoginForm f)
        {
            f.closed = true;
            return f.closed;
        }
    }
}