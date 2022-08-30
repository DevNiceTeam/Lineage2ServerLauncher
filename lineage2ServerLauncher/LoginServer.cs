using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace lineage2ServerLauncher
{
    internal class LoginServer
    {
        Process proc;

        public string getPath()
        {
            return Path.GetFullPath(@"java\bin\javaw.exe");
        }

        public void run(Form3 f)
        {
            if (!proc.Start())
            {
                proc = Process.Start(new ProcessStartInfo
                {
                    FileName = getPath(),
                    WorkingDirectory = @"server\login",
                    Arguments = @"-server -Xms1024m -Xmx1024m -jar ../libs/LoginServer.jar",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                });
                proc.EnableRaisingEvents = true;
                proc.Exited += (sae, sea) =>
                {
                    proc.Dispose();
                    proc.Close();
                };
                proc.OutputDataReceived += (sa, ea) =>
                {
                    f.textBox1.Text += ea.Data + Environment.NewLine;
                    Console.WriteLine(ea.Data + Environment.NewLine);
                };
                proc.ErrorDataReceived += (s, a) =>
                {
                    f.textBox1.Text += a.Data + Environment.NewLine;
                    Console.WriteLine(a.Data + Environment.NewLine);
                };

                proc.BeginOutputReadLine();
                proc.BeginErrorReadLine();
            }
            
        }

        public void Stop()
        {            
            
        }
    }
}