using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Bogus;

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
            string Username = Faker.Name.LastName(); 
            Console.WriteLine(Username); // for testing purposes

            // Tests only inputting a username
            // Works as intended despite my 1st test suggesting it wouldn't
            TestOnlyUsername(driver, Username);
            ScreenName.Clear();

            // This function fills every textbox
            // Feel free to copy it
            FillInfo(FirstName, LastName,Email,ScreenName,Password,PasswordConfirm,PhoneNum,Address,Province,PostalCode,Url,Description,Location,Username);
            
            //Thread.Sleep(10000);

        }
        static void FillInfo(IWebElement FirstName, IWebElement LastName, IWebElement Email, IWebElement ScreenName,
            IWebElement Password, IWebElement PasswordConfirm, IWebElement PhoneNum, IWebElement Address, IWebElement Province,
            IWebElement PostalCode, IWebElement Url, IWebElement Description, IWebElement Location, String Username)
        {
            var Faker = new Faker("en");
            FirstName.SendKeys(Faker.Name.FirstName());
            LastName.SendKeys(Faker.Name.LastName());
            Email.SendKeys("Email@Gmail.com");
            ScreenName.SendKeys(Username);
            Password.SendKeys("Password123");
            PasswordConfirm.SendKeys("Password123");
            PhoneNum.SendKeys(Faker.Phone.PhoneNumberFormat());
            Address.SendKeys(Faker.Address.StreetAddress());
            //Province I don't know how to select a drop down box
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
        {   // only inputs a randomly generated username
            IWebElement Username = driver.FindElement(By.Id("username"));
            Username.SendKeys(Name);
        }
    }
}
