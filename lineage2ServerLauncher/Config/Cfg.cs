using System.IO;

namespace lineage2ServerLauncher.Config
{
    internal class Cfg
    {
        public void read()
        {
            FileStream fs = new FileStream(@"\server\game\config\ipconfig.xml",FileMode.Open);
            StreamReader sr = new StreamReader(fs);
        }
    }
}
