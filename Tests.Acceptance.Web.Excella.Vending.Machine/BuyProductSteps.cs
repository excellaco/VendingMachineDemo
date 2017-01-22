﻿using System;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;
using TechTalk.SpecFlow;
// ReSharper disable UnusedMember.Global -- test methods are said to be unused which isn't correct. -SK

namespace Tests.Acceptance.Web.Excella.Vending.Machine
{
    [Binding]
    public class BuyProductSteps
    {
        private const int IIS_PORT = 8484;
        private const string APPLICATION_NAME = "Excella.Vending.Web.UI";
        private HomePage _homePage;
        private int _previousBalance;

        [BeforeFeature]
        public static void BeforeFeature()
        {
            StartIIS();
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            StopIIS();
            //TODO: Release change to put the value back for the sake of other tests.
        }

        private static void StopIIS()
        {
            var localIISExpressProcesses = Process.GetProcessesByName("iisexpress");
            foreach (var iisExpressProcess in localIISExpressProcesses)
            {
                if (!iisExpressProcess.HasExited)
                {
                    iisExpressProcess.Kill();
                }
            }
        }

        private static void StartIIS()
        {
            var applicationPath = GetApplicationPath(APPLICATION_NAME);
            var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            var startInfoFileName = programFiles + @"\IIS Express\iisexpress.exe";
            var startInfoArguments = $"/path:\"{applicationPath}\" /port:{IIS_PORT}";

            var iisProcess = new Process
            {
                StartInfo =
                {
                    FileName = startInfoFileName,
                    Arguments = startInfoArguments
                }
            };
            iisProcess.Start();
        }

        private static string GetApplicationPath(string applicationName)
        {
            var solutionFolder = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            // ReSharper disable once AssignNullToNotNullAttribute -- test will fail if it's null.
            return Path.Combine(solutionFolder, applicationName);
        }

        [BeforeScenario]
        public void Setup()
        {
            _homePage = new HomePage();
            _homePage.Go();
        }

        [AfterScenario]
        public void Teardown()
        {
            _homePage.Close();
        }

        [When(@"I insert a Quarter")]
        public void WhenIInsertAQuarter()
        {
            InsertQuarter();
        }

        [Then(@"The balance should increase by 25 cents")]
        public void TheBalanceShouldIncreaseBy25Cents()
        {
            var balance = _homePage.Balance();

            Assert.That(balance, Is.GreaterThan(_previousBalance));
            Assert.That(balance, Is.EqualTo(_previousBalance + 25));
        }
        private void InsertQuarter()
        {
            _previousBalance = _homePage.Balance();
            _homePage.InsertCoinButton().Click();
        }

        [Given(@"I have inserted a quarter")]
        public void GivenIHaveInsertedAQuarter()
        {
            InsertQuarter();
        }

        [When(@"I purchase a product")]
        public void WhenIPurchaseAProduct()
        {
            _homePage.PurchaseProductButton().Click();
        }

        [Then(@"I should receive the product")]
        public void ThenIShouldReceiveTheProduct()
        {

            //Assert.IsNotNull(product);
        }

        [Given(@"I have not inserted a quarter")]
        public void GivenIHaveNotInsertedAQuarter()
        {
            // Not calling insert coin
        }

        [Then(@"I should not receive a product")]
        public void ThenIShouldNotReceiveAProduct()
        {
            //Assert.IsNull(product);
        }
    }
}
