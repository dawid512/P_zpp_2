using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using P_zpp_2.Areas.Identity.Data;
using P_zpp_2.Data;
using System.Linq;

namespace NUnitTestProject
{
    public class WebTests
    {

        //public DbContextOptions<P_zpp_2DbContext> contextOptions { get; set }
        [SetUp]
        public void Setup()
        {


        }

        [Test]
        public void TestGoToMainPage()
        {
            IWebDriver webDriver = new FirefoxDriver();
            webDriver.Navigate().GoToUrl("http://localhost:28992");
            var txtConfirm = webDriver.FindElement(By.XPath("//button[.='Log in']"));
            Assert.That(txtConfirm.Displayed, Is.True);
            webDriver.Close();
            webDriver.Quit();
        }

        //<a class="nav-link" href="/Identity/Account/Register">Sing Up</a>
        [Test]
        public void TestGoToRegister()
        {
            IWebDriver webDriver = new FirefoxDriver();
            webDriver.Navigate().GoToUrl("http://localhost:28992");


            IWebElement butRegister = webDriver.FindElement(By.LinkText("Sing Up"));
            butRegister.Click();


            var txtConfirm = webDriver.FindElement(By.Id("Input_ConfirmPassword"));
            Assert.That(txtConfirm.Displayed, Is.True);
            webDriver.Close();
            webDriver.Quit();
        }

        [Test]
        public void TestRegisterCorrect()
        {
            IWebDriver webDriver = new FirefoxDriver();
            webDriver.Navigate().GoToUrl("http://localhost:28992");
            IWebElement butRegister = webDriver.FindElement(By.LinkText("Sing Up"));
            butRegister.Click();
            webDriver.FindElement(By.Id("Input_FirstName")).SendKeys("Kamil");
            webDriver.FindElement(By.Id("Input_LastName")).SendKeys("Nowak");
            webDriver.FindElement(By.Id("Input_Email")).SendKeys("kNowak@gmail.com");
            webDriver.FindElement(By.Id("Input_Password")).SendKeys("abc123.");
            webDriver.FindElement(By.Id("Input_ConfirmPassword")).SendKeys("abc123.");


            var register = webDriver.FindElement(By.XPath("//button[.='Register']"));
            register.Click();

            var registerSuccess = webDriver.FindElement(By.Id("manage"));
            Assert.That(registerSuccess.Displayed, Is.True);
            webDriver.Close();
            webDriver.Quit();
        }

        [Test]
        public void TestRegisterIncorrectPassword()
        {
            IWebDriver webDriver = new FirefoxDriver();
            webDriver.Navigate().GoToUrl("http://localhost:28992");
            IWebElement butRegister = webDriver.FindElement(By.LinkText("Sing Up"));
            butRegister.Click();
            webDriver.FindElement(By.Id("Input_FirstName")).SendKeys("Kamila");
            webDriver.FindElement(By.Id("Input_LastName")).SendKeys("Nowakowska");
            webDriver.FindElement(By.Id("Input_Email")).SendKeys("kNowakowska@gmail.com");
            webDriver.FindElement(By.Id("Input_Password")).SendKeys("abc123");
            webDriver.FindElement(By.Id("Input_ConfirmPassword")).SendKeys("abc123");


            var register = webDriver.FindElement(By.XPath("//button[.='Register']"));
            register.Click();

            Assert.That(register.Displayed, Is.True);
            webDriver.Close();
            webDriver.Quit();
        }

        [Test]
        public void TestLoginCorrect()
        {
            IWebDriver webDriver = new FirefoxDriver();
            webDriver.Navigate().GoToUrl("http://localhost:28992");
            webDriver.FindElement(By.Id("Input_Email")).SendKeys("kNowak@gmail.com");
            webDriver.FindElement(By.Id("Input_Password")).SendKeys("abc123.");

            var logIn = webDriver.FindElement(By.XPath("//button[.='Log in']"));
            logIn.Click();

            var registerSuccess = webDriver.FindElement(By.Id("manage"));
            Assert.That(registerSuccess.Displayed, Is.True);
            webDriver.Close();
            webDriver.Quit();
        }

        [Test]
        public void TestLoginIncorrect()
        {
            IWebDriver webDriver = new FirefoxDriver();
            webDriver.Navigate().GoToUrl("http://localhost:28992");
            webDriver.FindElement(By.Id("Input_Email")).SendKeys("kNowak@gmail.com");
            webDriver.FindElement(By.Id("Input_Password")).SendKeys("abcabc1111");

            var logIn = webDriver.FindElement(By.XPath("//button[.='Log in']"));
            logIn.Click();

            Assert.That(logIn.Displayed, Is.True);
            webDriver.Close();
            webDriver.Quit();
        }


    }
}