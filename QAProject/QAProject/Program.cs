﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Bogus;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;

namespace QAProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            IWebDriver driver = new ChromeDriver(@"C:/Selenium");
            // opens signup site
            TestOpenSignup(driver);

            // Test with nothing input this worked the first time which is a bug
            // now it doesn't work claiming the username is taken
            TestNoInput(driver);

            // variables refering to elements on the page
                IWebElement FirstName = driver.FindElement(By.Id("firstname"));
                IWebElement LastName = driver.FindElement(By.Id("lastname"));
                IWebElement Email = driver.FindElement(By.Id("email"));
                IWebElement ScreenName = driver.FindElement(By.Id("username"));
                IWebElement Password = driver.FindElement(By.Id("password"));
                IWebElement PasswordConfirm = driver.FindElement(By.Id("confirm"));
                IWebElement PhoneNum = driver.FindElement(By.Id("phone"));
                IWebElement Address = driver.FindElement(By.Id("address"));
                IWebElement Province = driver.FindElement(By.Id("province"));
                IWebElement PostalCode = driver.FindElement(By.Id("postalCode"));
                IWebElement Url = driver.FindElement(By.Id("url"));
                IWebElement Description = driver.FindElement(By.Id("desc"));
                IWebElement Location = driver.FindElement(By.Id("location"));

            // Random last name to use as a username
            // stored as a string for later use
            var Faker = new Faker("en");
            string Username = Faker.Name.FullName();
            Console.WriteLine(Username); // for testing purposes

            // Tests only inputting a username
            // Works as intended despite my 1st test suggesting it wouldn't
            TestOnlyUsername(driver, Username);
            driver.SwitchTo().Alert().Accept();
            TestOpenSignup(driver);

            driver.Close();

            IWebDriver driver1 = new ChromeDriver("C:/Selenium");
            driver1.Navigate().GoToUrl("http://10.157.123.12/site8/login.php");

            TestNoPasswordLogin(driver1);
            TestNoUsername(driver1);
            TestNoAccount(driver1);
            driver1.SwitchTo().Alert().Accept();
            TestLogin(driver1);

            Thread.Sleep(10000);
        }
        static void TestOpenSignup(IWebDriver driver)
        {   // Test ID: TestOpen1
            // according to the instructions this counts as a test
            driver.Navigate().GoToUrl("http://10.157.123.12/site8/login.php");
            driver.FindElement(By.LinkText("Click Here")).Click();
        }
        static void TestNoInput(IWebDriver driver)
        {   // Test ID: TestEmpty
            driver.FindElement(By.Id("button")).Click();
        }
        static void TestOnlyUsername(IWebDriver driver, String Name)
        {   // Test ID :TestOnlyUser
            // only inputs a randomly generated username
            IWebElement Username = driver.FindElement(By.Id("username"));
            Username.SendKeys(Name);
            driver.FindElement(By.Id("button")).Click();
        }
        static void TestNoPasswordLogin(IWebDriver driver)
        {   // Test ID: LoginNoPassword
            // I made this account prior to this
            IWebElement Username = driver.FindElement(By.Id("username"));
            Username.SendKeys("Username");
            driver.FindElement(By.Id("button")).Click();
            Username.Clear();
        }
        static void TestNoUsername(IWebDriver driver)
        {   // Test ID: LoginNoUsername
            IWebElement Password = driver.FindElement(By.Id("password"));
            Password.SendKeys("password");
            driver.FindElement(By.Id("button")).Click();
            Password.Clear();
        }
        static void TestNoAccount(IWebDriver driver)
        {   // Test ID NoAccount
            IWebElement Username = driver.FindElement(By.Id("username"));
            Username.SendKeys("RealUsername");
            IWebElement Password = driver.FindElement(By.Id("password"));
            Password.SendKeys("RealPassword");
            driver.FindElement(By.Id("button")).Click();
        }
        static void TestLogin(IWebDriver driver)
        {   // Test ID TestLogin
            IWebElement Username = driver.FindElement(By.Id("username"));
            Username.SendKeys("Example");
            IWebElement Password = driver.FindElement(By.Id("password"));
            Password.SendKeys("Password");
            driver.FindElement(By.Id("button")).Click();
        }
    }
}
