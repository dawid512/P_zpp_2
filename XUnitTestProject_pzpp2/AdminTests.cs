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


namespace XUnitTestProject_pzpp2
{
    public class AdminTests : IDisposable
    {
        private readonly IWebDriver _driver;

        public AdminTests()
        {
            _driver = new ChromeDriver();


        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
