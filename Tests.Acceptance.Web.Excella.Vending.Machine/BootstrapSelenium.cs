using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace Tests.Acceptance.Web.Excella.Vending.Machine
{
    [Binding]
    public class BootstrapSelenium : IDisposable
    {
        private readonly IObjectContainer _objectContainer;
        private IWebDriver _webDriver = null;

        public BootstrapSelenium(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void LoadDriverAndDefaultWait()
        {
                    _webDriver = new ChromeDriver();
            _objectContainer.RegisterInstanceAs(_webDriver, typeof(IWebDriver));
        }

        [AfterScenario]
        public void Dispose()
        {
            if (_webDriver != null)
            {
                _webDriver.Quit();
                _webDriver = null;
            }
        }
    }
}
