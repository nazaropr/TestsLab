using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumExtras.PageObjects;

namespace Lab_2
{
    public class OpenAccountPage
    {
        public IWebDriver Driver;
        public static WebDriverWait? wait;

        public OpenAccountPage(IWebDriver driver)
        {
            Driver = driver;
            PageFactory.InitElements(driver, this);
            wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        }

        [FindsBy(How = How.XPath, Using = "/html/body/div/div/div[2]/div/div[1]/div[1]/button")]
        private IWebElement customerLoginBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div/div/div[2]/div/form/div/select")]
        private IWebElement customerName { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div/div/div[2]/div/form/button")]
        private IWebElement LoginBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div/div/div[2]/div/div/select")]
        private IWebElement AccountNumberBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div/div/div[2]/div/div[3]/button[3]")]
        private IWebElement withdrawlBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div/div/div[2]/div/div[4]/div/form/div/input")]
        private IWebElement amountBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div/div/div[2]/div/div[4]/div/form/button")]
        private IWebElement withdrawBtn { get; set; }

        private string alertText;
        public void CheckThatAlertContainsText(string message)
        {
            Assert.That(alertText.Contains(message));
        }

        public OpenAccountPage OpenANewAccount()
        {
            customerLoginBtn.Click();

            var selectFirstDropDown = new SelectElement(customerName);
            selectFirstDropDown.SelectByText("Hermoine Granger");

            LoginBtn.Click();

             var selectSecondDropDown = new SelectElement(AccountNumberBtn);
             selectSecondDropDown.SelectByText("1002");

            withdrawlBtn.Click();

            amountBtn.SendKeys("5095");
            withdrawBtn.Click();

            return new OpenAccountPage(Driver);
        }
    }
}
