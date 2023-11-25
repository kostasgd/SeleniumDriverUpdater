using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumDriverUpdater.Interface
{
    public interface ISeleniumDriver
    {
        void Scrap(string link, OpenQA.Selenium.Chromium.ChromiumDriver driver);

    }
}
