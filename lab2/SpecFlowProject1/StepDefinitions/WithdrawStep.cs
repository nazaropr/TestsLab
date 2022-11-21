using Lab_2;
using System;
using TechTalk.SpecFlow;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public class WithdrawlStepDefinitions : BaseSteps
    {
        private OpenAccountPage openAccountPage;

        [Given(@"XYZ Bank page")]
        public void GivenXYZBankPage()
        {
            driver.Url = "https://www.globalsqa.com/angularJs-protractor/BankingProject";
            openAccountPage = new OpenAccountPage(driver);
        }

        [When(@"click Customer Login, choose Your Name Hermoine Granger, click Login, choose Account Number (.*), click Withdrawl, type (.*), click Withdraw")]
        public void WhenClickCustomerLoginChooseYourNameHermoineGrangerClickLoginChooseAccountNumberClickWithdrawlTypeClickWithdraw(int p0, int p1)
        {
            openAccountPage.OpenANewAccount();
        }

        
    }
}
