using System;
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

            // refresh the variables because leaving the page breaks them
            FirstName = driver.FindElement(By.Id("firstname"));
            LastName = driver.FindElement(By.Id("lastname"));
            Email = driver.FindElement(By.Id("email"));
            ScreenName = driver.FindElement(By.Id("username"));
            Password = driver.FindElement(By.Id("password"));
            PasswordConfirm = driver.FindElement(By.Id("confirm"));
            PhoneNum = driver.FindElement(By.Id("phone"));
            Address = driver.FindElement(By.Id("address"));
            Province = driver.FindElement(By.Id("province"));
            PostalCode = driver.FindElement(By.Id("postalCode"));
            Url = driver.FindElement(By.Id("url"));
            Description = driver.FindElement(By.Id("desc"));
            Location = driver.FindElement(By.Id("location"));

            // This function fills every textbox in the signup
            FillInfo(FirstName, LastName, Email, ScreenName, Password, PasswordConfirm, PhoneNum, Address, Province, PostalCode, Url, Description, Location, Username);

            TestNoFirstName(driver, FirstName);
            FirstName = driver.FindElement(By.Id("firstname"));
            LastName = driver.FindElement(By.Id("lastname"));
            Email = driver.FindElement(By.Id("email"));
            ScreenName = driver.FindElement(By.Id("username"));
            Password = driver.FindElement(By.Id("password"));
            PasswordConfirm = driver.FindElement(By.Id("confirm"));
            PhoneNum = driver.FindElement(By.Id("phone"));
            Address = driver.FindElement(By.Id("address"));
            Province = driver.FindElement(By.Id("province"));
            PostalCode = driver.FindElement(By.Id("postalCode"));
            Url = driver.FindElement(By.Id("url"));
            Description = driver.FindElement(By.Id("desc"));
            Location = driver.FindElement(By.Id("location"));
            FillInfo(FirstName, LastName, Email, ScreenName, Password, PasswordConfirm, PhoneNum, Address, Province, PostalCode, Url, Description, Location, Username);

            TestNoLastName(driver, LastName);
            FirstName = driver.FindElement(By.Id("firstname"));
            LastName = driver.FindElement(By.Id("lastname"));
            Email = driver.FindElement(By.Id("email"));
            ScreenName = driver.FindElement(By.Id("username"));
            Password = driver.FindElement(By.Id("password"));
            PasswordConfirm = driver.FindElement(By.Id("confirm"));
            PhoneNum = driver.FindElement(By.Id("phone"));
            Address = driver.FindElement(By.Id("address"));
            Province = driver.FindElement(By.Id("province"));
            PostalCode = driver.FindElement(By.Id("postalCode"));
            Url = driver.FindElement(By.Id("url"));
            Description = driver.FindElement(By.Id("desc"));
            Location = driver.FindElement(By.Id("location"));
            FillInfo(FirstName, LastName, Email, ScreenName, Password, PasswordConfirm, PhoneNum, Address, Province, PostalCode, Url, Description, Location, Username);
            driver.FindElement(By.Id("button")).Click();
            driver.SwitchTo().Alert().Accept();

            Thread.Sleep(10000);
        }
        static void FillInfo(IWebElement FirstName, IWebElement LastName, IWebElement Email, IWebElement ScreenName,
            IWebElement Password, IWebElement PasswordConfirm, IWebElement PhoneNum, IWebElement Address, IWebElement Province,
            IWebElement PostalCode, IWebElement Url, IWebElement Description, IWebElement Location, String Username)
        {
            Random random = new Random();
            var Faker = new Faker("en");
            FirstName.SendKeys(Faker.Name.FirstName());
            LastName.SendKeys(Faker.Name.LastName());
            Email.SendKeys("Email@Gmail.com");
            string RandomName = new Random().Next(1,100000).ToString();
            ScreenName.SendKeys(RandomName);
            Password.SendKeys("Password123");
            PasswordConfirm.SendKeys("Password123");
            PhoneNum.SendKeys(Faker.Phone.PhoneNumberFormat());
            Address.SendKeys(Faker.Address.StreetAddress());
            int randomIndex = random.Next(1, 13);
            Province.FindElement(By.XPath("//option[" + randomIndex + "]")).Click();
            PostalCode.SendKeys(Faker.Address.ZipCode());
            Url.SendKeys(Faker.Internet.Url());
            Description.SendKeys(Faker.Rant.Review());
            Location.SendKeys(Faker.Address.State());
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
        static void TestNoFirstName(IWebDriver driver, IWebElement FirstName)
        {   // Test ID: TestNoName1
            // Removes First name and it doesn't go throught
            FirstName.Clear();
            driver.FindElement(By.Id("button")).Click();
        }
         static void TestNoLastName(IWebDriver driver, IWebElement LastName)
        {   // Test ID: TestNoName2
            // Removes last name and it doesn't go throught
            LastName.Clear();
            driver.FindElement(By.Id("button")).Click();
        }
    }
}
