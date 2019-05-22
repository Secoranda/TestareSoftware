using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using siteControl.Web;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        private ChromeDriver driver;
        private WebDriverWait wait;

        [TestMethod]
        public void Login()
        {
            try
            {
                string url = "http://localhost:49986/Home/Index";
                driver = new ChromeDriver();
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Id("login")).Click();
                driver.FindElement(By.Id("Email")).SendKeys("try@gmail.com");
                driver.FindElement(By.Id("Password")).SendKeys("try");
                driver.FindElement(By.Id("loginbutton")).Click();
                driver.Close();
                driver.Dispose();
            }

            catch
            {
                ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
                Screenshot screenshot = screenshotDriver.GetScreenshot();
                screenshot.SaveAsFile("C:/Users/Daniela/Desktop/WT-master/screens/test.png", ScreenshotImageFormat.Png);
                driver.Quit();
            }
        }
    }
}
