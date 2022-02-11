using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Xunit;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

class Autotest
{
    //static IWebDriver driver = new ChromeDriver();
    static IWebDriver driver = new FirefoxDriver();
    static IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
    static void Main()
    {
        //Locators
        string CareersLink = "//div[@id='main-navbar']//a[contains(@class,'dropdown-toggle ga-track internal-link')and contains(text(),'CAREERS')]";
        string TechJobsLink = "//div[@id = 'main-navbar']//a[contains(text(),'Technologist Jobs')]";
        string SearchJobsButton = "//a[contains(@class,'career-tech-btn-search-jobs') and contains(text(),'SEARCH JOBS')]";
        string CitiesField = "3_188_3-search__field";
        string CitySelection = "//span[contains(@class,'select2-results')]";
        string SearchResultsButton = "//fieldset/div[5]/div/button";
        string ListJobOffers = "//div[contains(@class,'listJobs')]/ul/li";
        string ClearCityFieldIcon = "//span[contains(@class,'select2-selection__clear')]";
        string KeywordInputField = "tpt_search";
        string CountryDropDown = "3_56_3";
        string ExpertiseDropDown = "3_67_3";
        string RequirementText = "//div[contains(span/following-sibling::text(), 'Experience in testing both frontend and backend part of the system')]";


        //Navigate to the Avanade website
        string Url = "https://www.avanade.com";
        driver.Navigate().GoToUrl(Url);
        driver.Manage().Window.Maximize();

        //Click on the Technologist Jobs Link from Careers 
        IWebElement Careers = driver.FindElement(By.XPath(CareersLink));
        Actions action = new Actions(driver);
        action.MoveToElement(Careers).Perform();

        IWebElement TechnologistJobs = driver.FindElement(By.XPath(TechJobsLink));
        action.MoveToElement(TechnologistJobs).Perform();
        TechnologistJobs.Click();
        Thread.Sleep(3000);

        //Check if the page is correct and click on the Search Jobs button
        string actualTitle1 = driver.Title;
        string expectedTitle1 = "Technology Jobs | Avanade";

        if(actualTitle1.Contains(expectedTitle1))
        {
            Console.WriteLine("Title correct:" + "" + actualTitle1);
            Assert.True(true, "Passed");
        }
        else
        {
            Console.WriteLine("Incorrect page");
        }

        IWebElement SearchJobs = driver.FindElement(By.XPath(SearchJobsButton));
        SearchJobs.Click();
        Thread.Sleep(3000);

        //Check of the page is correct and search for job offers in Krakow
        string actualTitle2 = driver.Title;
        string expectedTitle2 = "Find Jobs - Avanade Jobs";

        if (actualTitle2.Contains(expectedTitle2))
        {
            Console.WriteLine("Title correct:" + "" + actualTitle2);
            Assert.True(true, "Passed");
        }
        else
        {
            Console.WriteLine("Incorrect page");
        }

        js.ExecuteScript("window.scrollBy(0,500)");
        IWebElement CityKrakow = driver.FindElement(By.Id(CitiesField));
        CityKrakow.Click();
        CityKrakow.SendKeys("Krakow");
        Thread.Sleep(3000);

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3000));
        IWebElement KrakowSelection = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(CitySelection)));
        KrakowSelection.Click();

        IWebElement KrakowSearchResults = driver.FindElement(By.XPath(SearchResultsButton));
        KrakowSearchResults.Click();
        js.ExecuteScript("window.scrollBy(0,500)");

        //Validate that there is a total of 8 results on the 1st page
        IList<IWebElement> JobOffersKrakow = new List <IWebElement>(driver.FindElements(By.XPath(ListJobOffers)));
        int totalkrakow = JobOffersKrakow.Count();

        string actualNumberofJobOffersKrakow = totalkrakow.ToString();
        string expectedNumberofJobOffersKrakow = "8";

        Assert.True(actualNumberofJobOffersKrakow.Contains(expectedNumberofJobOffersKrakow));
        if (true)           
        {
            Console.WriteLine("Total number of job offers in Krakow on the 1st page is:" + "" + actualNumberofJobOffersKrakow);
        }
        else
        {
            Console.WriteLine("Failed");
        }

        IWebElement ClearCityField = driver.FindElement(By.XPath(ClearCityFieldIcon));
        ClearCityField.Click();

        //Search for job offers in Warsaw
        IWebElement CityWarsaw = driver.FindElement(By.Id(CitiesField));
        CityWarsaw.Click();
        CityWarsaw.SendKeys("Warsaw");
        Thread.Sleep(3000);

        WebDriverWait wait1 = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        IWebElement WarsawSelection = wait1.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(CitySelection)));
        WarsawSelection.Click();

        IWebElement WarsawSearchResults = driver.FindElement(By.XPath(SearchResultsButton));
        WarsawSearchResults.Click();
        js.ExecuteScript("window.scrollBy(0,500)");

        //Validate that there is at least 1 result 
        IList<IWebElement> JobOffersWarsaw = new List<IWebElement>(driver.FindElements(By.XPath(ListJobOffers)));
        int totalwarsaw = JobOffersWarsaw.Count();

        Assert.True(totalwarsaw >= 1);
        if (true)
        {
            Assert.True(true, "Passed");
            Console.WriteLine("There is " + "" + totalwarsaw + "" + " job offers in Warsaw on the 1st page");
        }
        else
        {
            Console.WriteLine("Failed");
        }

        //Search for QA in Unites States, Expertise Software Engineering
        IWebElement KeywordField = driver.FindElement(By.Id(KeywordInputField));
        KeywordField.Click();
        KeywordField.SendKeys("QA");

        IWebElement Country = driver.FindElement(By.Id(CountryDropDown));
        Country.Click();
        SelectElement selectectlement = new SelectElement(Country);
        selectectlement.SelectByText("United States");

        IWebElement Expertise = driver.FindElement(By.Id(ExpertiseDropDown));
        Expertise.Click();
        SelectElement selectectlement1 = new SelectElement(Expertise);
        selectectlement1.SelectByText("Software Engineering");

        IWebElement SearchResults = driver.FindElement(By.XPath(SearchResultsButton));
        SearchResults.Click();
        js.ExecuteScript("window.scrollBy(0,500)");

        IWebElement SingleJobOffer = driver.FindElement(By.XPath(ListJobOffers));
        SingleJobOffer.Click();
        Thread.Sleep(3000);

        //Validate that one of the required experience is: Experience in testing both frontend and backend part of the system
        js.ExecuteScript("window.scrollBy(0,700)");
        IWebElement Requirement = driver.FindElement(By.XPath(RequirementText));

        string ActualRequirement = Requirement.Text;
        string ExpectedRequirement = "Experience in testing both frontend and backend part of the system";

        Assert.True(ActualRequirement.Contains(ExpectedRequirement));
        if(true)
        {
            Console.WriteLine("Experience in testing both frontend and backend part of the system is on of the requirement");
        }
        else
        {
            Console.WriteLine("Failed");
        }
        driver.Quit();
   }
}

