using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SeleniumDriverUpdater.Drivers
{
    public class EdgeDriver : AbstractDriver
    {
        private static string EDGEBASELINK = "https://developer.microsoft.com/en-us/microsoft-edge/tools/webdriver/";
        private static string EDGEDRIVERZIPNAME = "/edgedriver_win64.zip";
        private static string EDGEDRIVERPATH = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";
        private string EDGEDOWNLOADLINK = "https://msedgedriver.azureedge.net/";

        public EdgeDriver()
        {
            DriverType = DriverType.edgedriver;
        }

        public override string CreateDownloadLink(string latestVersionLink)
        {
            string installedVersionFromPath = GetInstalledDriverVersionFromPath(EDGEDRIVERPATH);
            int lastIndex = installedVersionFromPath.LastIndexOf('.');
            String regex = installedVersionFromPath.Substring(0, lastIndex) + REGEXPATTERN;
            MatchCollection coll = Regex.Matches(latestVersionLink, regex);
            String regResult = coll[0].Groups[0].Value;
            string result = EDGEBASELINK + regResult + EDGEDRIVERZIPNAME;
            return result;
        }

        public override ChromiumDriver GetDriver()
        {
            try
            {
                return new OpenQA.Selenium.Edge.EdgeDriver((EdgeOptions)ChromiumOptions());
            }
            catch (System.InvalidOperationException ex)
            {
                base.KillBrowserDriverProcesses();
                DownloadZip(EDGEDOWNLOADLINK, EDGEDRIVERZIPNAME, EDGEDRIVEREXENAME);
            }
            return new OpenQA.Selenium.Edge.EdgeDriver((EdgeOptions)ChromiumOptions());
        }

        public override void UnzipFile(string driverName, string zipFileName)
        {
            string path = Directory.GetCurrentDirectory() + driverName;
            if (File.Exists(Directory.GetCurrentDirectory() + driverName))
            {
                File.Delete(Directory.GetCurrentDirectory() + driverName);
                if (File.Exists(Directory.GetCurrentDirectory() + EXTRACTEDFOLDER.TrimEnd('\\') + EDGEDRIVEREXENAME))
                    File.Delete(Directory.GetCurrentDirectory() + EXTRACTEDFOLDER.TrimEnd('\\') + EDGEDRIVEREXENAME);
                ZipFile.ExtractToDirectory(Directory.GetCurrentDirectory() + CHROMEDOWNLOADEDDRIVERZIPNAME
                , Directory.GetCurrentDirectory() + EXTRACTEDFOLDER.TrimEnd('\\'));
                MoveDriver(driverName);
            }
        }
    }
}
