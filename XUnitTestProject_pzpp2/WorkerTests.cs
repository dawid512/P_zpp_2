using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using P_zpp_2;
using P_zpp_2.Data;
using System;
using System.Net.Http;
using Xunit;
using Xunit.Priority;

namespace XUnitTestProject_pzpp2
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class WorkerTests : IDisposable
    {
        private readonly IWebDriver _driver;
        public WorkerTests()
        {
            _driver = new ChromeDriver();
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        public void Login(string email, string password)
        {
            _driver.Navigate()
                .GoToUrl("https://localhost:44342/Identity/Account/Login?ReturnUrl=%2F");
            _driver.FindElement(By.Id("Input_Email"))
                .SendKeys(email);
            _driver.FindElement(By.Id("Input_Password"))
                .SendKeys(password);
            _driver.FindElement(By.Id("loginButton"))
                .Click();
        }

        public void Logout()
        {
            _driver.FindElement(By.Id("logout"))
                .Click();
        }

        [Fact, Priority(1)]
        public void RegisterTest()
        {
            _driver.Navigate()
                .GoToUrl("https://localhost:44342/Identity/Account/Register");
            _driver.FindElement(By.Id("Input_Rola"))
                .Click();
            _driver.FindElement(By.Id("Input_Rola"))
                .SendKeys("Pracownik");
            _driver.FindElement(By.Id("Input_FirstName"))
                .SendKeys("Karol");
            _driver.FindElement(By.Id("Input_LastName"))
                .SendKeys("Polak");
            _driver.FindElement(By.Id("Input_Email"))
                .SendKeys("karol123@gmail.com");
            _driver.FindElement(By.Id("Input_Password"))
                .SendKeys("abc123.");
            _driver.FindElement(By.Id("Input_ConfirmPassword"))
                .SendKeys("abc123.");
            _driver.FindElement(By.Id("registerButton"))
                .Click();
            Assert.Contains("Wyloguj", _driver.PageSource);
            Logout();
        }

        [Fact, Priority(2)]
        public void LoginTest()
        {
            _driver.Navigate()
                .GoToUrl("https://localhost:44342/Identity/Account/Login?ReturnUrl=%2F");
            _driver.FindElement(By.Id("Input_Email"))
                .SendKeys("maciej@gmail.com");
            _driver.FindElement(By.Id("Input_Password"))
                .SendKeys("abc123.");
            _driver.FindElement(By.Id("loginButton"))
                .Click();
            Assert.Contains("Wyloguj", _driver.PageSource);
            Logout();
        }

        [Fact, Priority(3)]
        public void CreateLeave()
        {
            _driver.Navigate()
                .GoToUrl("https://localhost:44342/Identity/Account/Login?ReturnUrl=%2F");
            Login("karol123@gmail.com", "abc123.");
            _driver.FindElement(By.Id("LeavesButton"))
                .Click();
            _driver.FindElement(By.Id("SendToModalBtn"))
                .Click();
            _driver.FindElement(By.Id("SendToModalBtn"))
                .Click();
            _driver.FindElement(By.Id("Leavesname"))
                .SendKeys("L4");
            _driver.FindElement(By.Id("AcceptBtn"))
                .Click();
            Assert.Contains("Status: Oczekuj¹ce", _driver.PageSource);
            Logout();
        }

        [Fact, Priority(4)]
        public void SetLeaveState()
        {
            _driver.Navigate()
                .GoToUrl("https://localhost:44342/Identity/Account/Login?ReturnUrl=%2F");
            Login("maciej@gmail.com", "abc123.");
            _driver.FindElement(By.Id("LeavesButton"))
                .Click();
            _driver.FindElement(By.Id("SendToModalBtn"))
                .Click();
            _driver.FindElement(By.Id("SendToModalBtn"))
                .Click();
            _driver.FindElement(By.Id("Leavesname"))
                .SendKeys("L4");
            _driver.FindElement(By.Id("AcceptBtn"))
                .Click();
            Assert.Contains("Status: Oczekuj¹ce", _driver.PageSource);
            Logout();
        }
    }
}
