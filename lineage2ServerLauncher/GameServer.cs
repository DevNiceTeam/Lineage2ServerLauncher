using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace lineage2ServerLauncher
{
    internal class GameServer : IServer
    {       
        public string getPath()
        {    
            return Path.GetFullPath(@"java\bin\java.exe"); 
        }

        public String run()
        {
            String txt = "";

            Process proc = Process.Start(new ProcessStartInfo
            {
                FileName = getPath(),
                Arguments = " -version",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true,
            });

            proc.BeginErrorReadLine();
            proc.ErrorDataReceived += (s, a) =>
            {
                txt += a.Data + Environment.NewLine;
            };  
            
            proc.WaitForExit();

            return txt;
        }
    }
}