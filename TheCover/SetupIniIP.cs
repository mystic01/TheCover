using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace TheCover
{
    public class SetupIniIP
    {
        public string path;
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        public void IniWriteValue(string Section, string Key, string Value, string iniPath)
        {
            WritePrivateProfileString(Section, Key, Value, Application.StartupPath + "\\" + iniPath);
        }

        public string IniReadValue(string Section, string Key, string iniPath)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, Application.StartupPath + "\\" + iniPath);
            return temp.ToString();
        }
    }
}