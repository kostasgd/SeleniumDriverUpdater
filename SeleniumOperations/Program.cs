using OpenQA.Selenium.Chrome;
using SeleniumDriverUpdater.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            var driver = new SeleniumDriverUpdater.Drivers.EdgeDriver();
            var chromeDriver = driver.GetDriver();
            string link = @"https://www.google.com/";
            driver.Scrap(link,chromeDriver);
        }
    }
}
