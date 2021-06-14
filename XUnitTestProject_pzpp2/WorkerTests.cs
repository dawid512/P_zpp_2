using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using P_zpp_2;
using P_zpp_2.Data;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using Xunit;
using Xunit.Priority;

namespace XUnitTestProject_pzpp2
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class WorkerTests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly P_zpp_2DbContext _context;
        public WorkerTests()
        {
            _driver = new ChromeDriver();
            _context = new P_zpp_2DbContext();
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
        public void CreateCommpany()
        {
            _driver.Navigate()
                .GoToUrl("https://localhost:44342/Identity/Account/Login?ReturnUrl=%2F");
            Login("maciej@gmail.com", "abc123.");
            _driver.FindElement(By.Id("companiesBtn"))
                .Click();
            _driver.FindElement(By.Id("createCompanyBtn"))
                .Click();
            _driver.FindElement(By.Id("CompanyName"))
                .SendKeys("newCompany");
            _driver.FindElement(By.Id("acceptButton"))
                .Click();
            Assert.Contains("newCompany", _driver.PageSource);
            Logout();
        }
        [Fact, Priority(4)]
        public void CreateDepartment()
        {
            _driver.Navigate()
                .GoToUrl("https://localhost:44342/Identity/Account/Login?ReturnUrl=%2F");
            Login("maciej@gmail.com", "abc123.");
            _driver.FindElement(By.Id("departuresBtn"))
                .Click();
            _driver.FindElement(By.Id("createNewDep"))
                .Click();
            _driver.FindElement(By.Id("DepartureName"))
                .SendKeys("newDep");
            _driver.FindElement(By.Id("Companies"))
                .SendKeys("newCompany");
            _driver.FindElement(By.Id("acceptButton"))
                .Click();
            Assert.Contains("newDep", _driver.PageSource);
            Logout();
        }
        /*[Fact, Priority(5)]
        public void SetDepartment()
        {
            _driver.Navigate()
                .GoToUrl("https://localhost:44342/Identity/Account/Login?ReturnUrl=%2F");
            Login("karol123@gmail.com", "abc123.");
            string id = _context.Users.Where(x => x.Email == "karol123@gmail.com").Select(x => x.Id).ToString();
            _driver.Navigate()
                .GoToUrl(String.Format("https://localhost:44342/Identity/Admin/Pracownicy/Edit?id={0}", id));
            _driver.FindElement(By.Id("Departures"))
                .SendKeys("newCompany");
            _driver.FindElement(By.Id("acceptButton"))
                .Click();
            Assert.Contains("newDep", _driver.PageSource);
            Logout();
        }*/
        [Fact, Priority(6)]
        public void CreateLeave()
        {
            _driver.Navigate()
                .GoToUrl("https://localhost:44342/Identity/Account/Login?ReturnUrl=%2F");
            Login("karol123@gmail.com", "abc123.");
            _driver.FindElement(By.Id("LeavesButton"))
                .Click();
            Thread.Sleep(500);
            _driver.FindElement(By.Id("SendToModalBtn"))
                .Click();
            Thread.Sleep(500);
            _driver.FindElement(By.Id("AcceptBtn"))
                .Click();
            Thread.Sleep(500);
            Assert.Contains("Status: Oczekuj¹ce", _driver.PageSource);
            Logout();
          
        }


    }
}
