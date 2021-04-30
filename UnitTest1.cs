using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

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
        string name = "Printed Chiffon Dress";



        [Test]
        public void SignInTest()
        {
            Login(email, password);

            IWebElement accountText = driver.FindElement(By.XPath("//*[@id='center_column']/h1"));
            Assert.AreEqual("MY ACCOUNT", accountText.Text, "Account info is incorrect");

        }
        [Test]
        public void CorrectSignInTest()
        {
            Login(email, password);

            IWebElement accountInfo = driver.FindElement(By.CssSelector("#header > div.nav > div > div > nav > div:nth-child(1) > a > span"));
            Assert.AreEqual("T G", accountInfo.Text, "Account name is incorrect");
        }

        [Test]
        public void SearchItemTest()
        {
            Login(email, password);

            Search(name);

            IWebElement searchItemText = driver.FindElement(By.CssSelector("#center_column > h1 > span.lighter"));
            StringAssert.Contains(name.ToUpper(), searchItemText.Text, "Search item name is incorrect");
        }
        [Test]
        public void ValidateFoundItemTest()
        {
            Login(email, password);
            Search(name);
            IWebElement correctSearchItemName = driver.FindElement(By.XPath("//*[@id='center_column']/ul/li[1]/div/div[2]/h5/a"));
            Assert.AreEqual(name, correctSearchItemName.Text, "Found item name is incorrect");
        }
        [Test]
        public void BuyingItemTest()
        {
            Login(email, password);
            Search(name);
            FinishBuying();
            IWebElement orderConfirmationText = driver.FindElement(By.CssSelector("#center_column > h1"));
            Assert.AreEqual("ORDER CONFIRMATION", orderConfirmationText.Text, "Order confirmation text is incorrect");
        }
        [Test]
        public void ValidateOrderCompleteTest()
        {
            Login(email, password);
            Search(name);
            FinishBuying();
            IWebElement succesfulOrderText = driver.FindElement(By.CssSelector("#center_column > p.alert.alert-successn"));
            Assert.AreEqual("Your order on My Store is complete.", succesfulOrderText.Text, "Succesful order text is incorrect");
        }
        public void Login(string email, string password)
        {
            System.Threading.Thread.Sleep(6000);

            driver.FindElement(By.CssSelector("#header > div.nav > div > div > nav > div.header_user_info > a")).Click();
            driver.FindElement(By.Id("email")).SendKeys(email);
            driver.FindElement(By.Id("passwd")).SendKeys(password);
            driver.FindElement(By.Id("SubmitLogin")).Click();

        }

        public void Search(string name)
        {
            System.Threading.Thread.Sleep(6000);
            driver.FindElement(By.Id("search_query_top")).SendKeys(name);
            driver.FindElement(By.Name("submit_search")).Click();

        }

        public void FinishBuying()
        {
            driver.FindElement(By.XPath("//*[@id='center_column']/ul/li[1]/div/div[1]/div/a[1]/img")).Click();

            System.Threading.Thread.Sleep(6000);

            //IWebElement sizeDropdown = driver.FindElement(By.Name("group_1"));
            //SelectElement selectElement = new SelectElement(sizeDropdown);
            //selectElement.SelectByText("L");
            driver.Manage().Timeouts().ImplicitWait = System.TimeSpan.FromSeconds(5);
            driver.FindElement(By.XPath("//*[@id='add_to_cart']/button/span")).Click();
            System.Threading.Thread.Sleep(6000);
            driver.FindElement(By.XPath("//*[@id='layer_cart']/div[1]/div[2]/div[4]/a/span")).Click();
            driver.FindElement(By.XPath("//*[@id='center_column']/p[2]/a[1]/span")).Click();
            driver.FindElement(By.Name("processAddress")).Click();
            driver.FindElement(By.Id("cgv")).Click();
            driver.FindElement(By.Name("processCarrier")).Click();
            driver.FindElement(By.XPath("//*[@id='HOOK_PAYMENT']/div[2]/div/p/a")).Click();
            driver.FindElement(By.XPath("//*[@id='cart_navigation']/button/span")).Click();

        }
        [TearDown]
        public void CloseBrowser()
        {

            driver.Close();
        }
    }
}