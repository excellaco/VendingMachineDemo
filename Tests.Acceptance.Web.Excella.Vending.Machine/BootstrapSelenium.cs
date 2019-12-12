using System;
using BoDi;
using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace Tests.Acceptance.Web.Excella.Vending.Machine
{
    //[Binding]
    //public class BootstrapSelenium : IDisposable
    //{
    //    private readonly IObjectContainer _objectContainer;
    //    private IWebDriver _webDriver = null;

    //    public BootstrapSelenium(IObjectContainer objectContainer)
    //    {
    //        _objectContainer = objectContainer;
    //    }

    //    [BeforeScenario]
    //    public void LoadDriverAndDefaultWait()
    //    {
    //        _webDriver = new ChromeDriver();
    //        _objectContainer.RegisterInstanceAs(_webDriver, typeof(IWebDriver));
    //    }

    //    [AfterScenario]
    //    public void Dispose()
    //    {
    //        _webDriver?.Quit();
    //        //if (_webDriver != null)
    //        //{
    //        //    _webDriver.Dispose();
    //        //    _webDriver = null;
    //        //}
    //    }
    //}

    [Binding]
    public class WebDriverSupport
    {
        private readonly IObjectContainer objectContainer;

        public WebDriverSupport(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }

        private void RegisterWebDriver()
        {
            var webDriver = new ChromeDriver();
            objectContainer.RegisterInstanceAs<IWebDriver>(webDriver);
        }
        [BeforeScenario]
        public void InitializeWebDriver()
        {
            if (!objectContainer.IsRegistered<IWebDriver>())
            {
                RegisterWebDriver();
                return;
            }

            if (objectContainer.Resolve<IWebDriver>() == null)
            {
                RegisterWebDriver();
            }
        }

        [AfterScenario()]
        public void AfterScenario()
        {
            objectContainer.Resolve<IWebDriver>().Close();
        }
    }
}