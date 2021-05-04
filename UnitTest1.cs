using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;

namespace WebAutomation_Homework
{
    public class Tests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Url = "http://automationpractice.com/index.php";
            driver.Manage().Window.Maximize();
        }

        string email = "tatjanasg@123.com";
        string password = "12345";
        string name = "Blouse";

        [Test]
        public void A_SignInTest()
        {
            Login(email, password);

            IWebElement accountText = driver.FindElement(By.XPath("//*[@id='center_column']/h1"));
            Assert.AreEqual("MY ACCOUNT", accountText.Text, "Account info is incorrect");
        }

        [Test]
        public void B_CorrectSignInTest()
        {
            Login(email, password);

            IWebElement accountInfo = driver.FindElement(By.CssSelector("#header > div.nav > div > div > nav > div:nth-child(1) > a > span"));
            Assert.AreEqual("T G", accountInfo.Text, "Account name is incorrect");
        }

        [Test]
        public void C_SearchItemTest()
        {
            Login(email, password);
            Search(name);

            IWebElement searchItemText = driver.FindElement(By.CssSelector("#center_column > h1 > span.lighter"));
            StringAssert.Contains(name.ToUpper(), searchItemText.Text, "Search item name is incorrect");
        }

        [Test]
        public void D_ValidateFoundItemTest()
        {
            Login(email, password);
            Search(name);

            IWebElement correctSearchItemName = driver.FindElement(By.XPath("//*[@id='center_column']/ul/li[1]/div/div[2]/h5/a"));
            Assert.AreEqual(name, correctSearchItemName.Text, "Found item name is incorrect");
        }

        [Test]
        public void E_BuyingItemTest()
        {
            Login(email, password);
            Search(name);
            FinishBuying();

            IWebElement orderConfirmationText = driver.FindElement(By.CssSelector("#center_column > h1"));
            Assert.AreEqual("ORDER CONFIRMATION", orderConfirmationText.Text, "Order confirmation text is incorrect");
        }

        [Test]
        public void F_ValidateOrderCompleteTest()
        {
            Login(email, password);
            Search(name);
            FinishBuying();

            IWebElement succesfulOrderText = driver.FindElement(By.XPath("//*[@id='center_column']/p[1]"));
            Assert.AreEqual("Your order on My Store is complete.", succesfulOrderText.Text, "Succesful order text is incorrect");
        }

        public void Login(string email, string password)
        {
            driver.FindElement(By.CssSelector("#header > div.nav > div > div > nav > div.header_user_info > a")).Click();
            driver.FindElement(By.Id("email")).SendKeys(email);
            driver.FindElement(By.Id("passwd")).SendKeys(password);
            driver.FindElement(By.Id("SubmitLogin")).Click();
        }

        public void Search(string name)
        {
            driver.FindElement(By.Id("search_query_top")).SendKeys(name);
            driver.FindElement(By.Name("submit_search")).Click();
        }

        public void FinishBuying()
        {
            driver.FindElement(By.XPath("//*[@id='list']/a/i")).Click();
            driver.FindElement(By.XPath("//*[@id='center_column']/ul/li/div/div/div[3]/div/div[2]/a[1]/span")).Click();
            driver.FindElement(By.XPath("//*[@id='header']/div[3]/div/div/div[3]/div/a")).Click();

           Actions actions = new Actions(driver);
           actions.SendKeys(Keys.PageDown).Build().Perform();

            driver.FindElement(By.CssSelector("#center_column > p.cart_navigation.clearfix > a.button.btn.btn-default.standard-checkout.button-medium")).Click();
            driver.FindElement(By.CssSelector("#center_column > form > p > button")).Click();
            driver.FindElement(By.CssSelector("#uniform-cgv")).Click();
            driver.FindElement(By.CssSelector("#form > p > button")).Click();
            driver.FindElement(By.CssSelector("#HOOK_PAYMENT > div:nth-child(2) > div > p > a")).Click();
            driver.FindElement(By.CssSelector("#cart_navigation > button")).Click();
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Close();
        }
    }
}