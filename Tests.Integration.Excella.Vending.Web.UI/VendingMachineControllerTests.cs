﻿using Excella.Vending.DAL;
using Excella.Vending.Domain;
using Excella.Vending.Machine;
using Excella.Vending.Web.UI.Controllers;
using NUnit.Framework;
using System.Transactions;
using System.Web.Mvc;
using Excella.Vending.Web.UI.Models;
using Tests.Integration.Excella.Vending.Machine;
using Xania.AspNet.Simulator;

namespace Tests.Integration.Excella.Vending.Web.UI
{
    [TestFixtureSource(typeof(PaymentDaoTestCases), "TestCases")]
    public class VendingMachineControllerTests
    {
        private TransactionScope _transactionScope;
        private VendingMachineController _controller;
        private IPaymentDAO _injectedPaymentDao;

        public VendingMachineControllerTests(IPaymentDAO paymentDao)
        {
            _injectedPaymentDao = paymentDao;
        }

        [OneTimeSetUp]
        public void FixtureSetup()
        {
        }

        [SetUp]
        public void Setup()
        {
            _transactionScope = new TransactionScope();
            var paymentProcessor = new CoinPaymentProcessor(_injectedPaymentDao);
            var vendingMachine = new VendingMachine(paymentProcessor);
            _controller = new VendingMachineController(vendingMachine);
        }

        [TearDown]
        public void Teardown()
        {
            _transactionScope.Dispose();
        }

        [Test]
        public void Index_WhenFirstLoad_ExpectNoBalance()
        {
            // Act
            _controller.Index();

            // Assert
            var vm = _controller.ViewData.Model as VendingMachineViewModel;
            Assert.That(vm.Balance, Is.EqualTo(0));
        }

        [Test]
        public void InsertCoin_WhenCalledOnce_Expect25Balance()
        {
            // Act
            _controller.InsertCoin();
            _controller.Index();
            // Assert
            var result = _controller.ViewData.Model as VendingMachineViewModel;
            Assert.AreEqual(25, result.Balance);
        }

        [Test]
        public void ReleaseChange_WithMoneyEntered_ReturnsChange()
        {
            // Arrange
            _controller.InsertCoin();

            // Act
            var result = _controller.ReleaseChange() as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("IndexWithChange", result.RouteValues["action"]);
            Assert.AreEqual(25, result.RouteValues["ReleasedChange"]);
        }

        [Test]
        public void ReleaseChange_WithMoneyEntered_SetsBalanceToZero()
        {
            // Arrange
            _controller.InsertCoin();

            // Act
            _controller.ReleaseChange();
            _controller.Index();

            var vm = _controller.ViewData.Model as VendingMachineViewModel;
            Assert.AreEqual(0, vm.Balance);
        }
    }
}
