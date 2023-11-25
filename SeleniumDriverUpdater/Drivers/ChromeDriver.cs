using OpenQA.Selenium;
using OpenQA.Selenium.Chromium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using System.Text.RegularExpressions;
using System.IO;
using System.IO.Compression;

namespace SeleniumDriverUpdater.Drivers
{
    public class ChromeDriver : AbstractDriver
    {
        private static string CHROMEBASELINK = "https://chromedriver.storage.googleapis.com/";
        private static string CHROMEDRIVERZIPNAME = "/chromedriver_win32.zip";
        private static string CHROMEDRIVERPATH = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
        private string CHROMEDOWNLOADLINK = "https://chromedriver.chromium.org/downloads";
        public ChromeDriver()
        {
            DriverType = DriverType.chromedriver;
        }
        public override void UnzipFile(string driverName, string zipFileName)
        {
            string path = Directory.GetCurrentDirectory() + driverName;
            if (File.Exists(Directory.GetCurrentDirectory() + driverName))
            {
                File.Delete(Directory.GetCurrentDirectory() + driverName);
                if (File.Exists(Directory.GetCurrentDirectory() + EXTRACTEDFOLDER.TrimEnd('\\') + CHROMEDRIVEREXENAME))
                    File.Delete(Directory.GetCurrentDirectory() + EXTRACTEDFOLDER.TrimEnd('\\') + CHROMEDRIVEREXENAME);
                ZipFile.ExtractToDirectory(Directory.GetCurrentDirectory() + CHROMEDOWNLOADEDDRIVERZIPNAME
                , Directory.GetCurrentDirectory() + EXTRACTEDFOLDER.TrimEnd('\\'));
                MoveDriver(driverName);
            }
        }
        public override string CreateDownloadLink(string latestVersionLink)
        {
            string installedVersionFromPath = GetInstalledDriverVersionFromPath(CHROMEDRIVERPATH);
            int lastIndex = installedVersionFromPath.LastIndexOf('.');
            String regex = installedVersionFromPath.Substring(0, lastIndex) + REGEXPATTERN;
            MatchCollection coll = Regex.Matches(latestVersionLink, regex);
            String regResult = coll[0].Groups[0].Value;
            string result = CHROMEBASELINK + regResult + CHROMEDRIVERZIPNAME;
            return result;
        }

        public override OpenQA.Selenium.Chromium.ChromiumDriver GetDriver()
        {
            try
            {
                return new OpenQA.Selenium.Chrome.ChromeDriver((ChromeOptions)ChromiumOptions());
            }
            catch (System.InvalidOperationException ex)
            {
                base.KillBrowserDriverProcesses();
                DownloadZip(CHROMEDOWNLOADLINK, CHROMEDOWNLOADEDDRIVERZIPNAME, CHROMEDRIVEREXENAME);
            }
            return new OpenQA.Selenium.Chrome.ChromeDriver((ChromeOptions)ChromiumOptions());
        }
    }
}

