using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using SeleniumExtras.WaitHelpers;

namespace ConsoleSelenium2
{
    class Program
    {
        public static List<IWebElement> allnames = new List<IWebElement>();
        public static List<IWebElement> allprices = new List<IWebElement>();
        public static List<IWebElement> allpricesformin = new List<IWebElement>();
        public static List<Product> lstprod;
        public class Product
        {
            public string Name { get; set; }
            public double Price { get; set; }
        }

        static void Main(string[] args)
        {

           // string url = "https://bomba.md/";
            string url = "https://bomba.md/ro/category/all-notebook/";
            IWebDriver driver = new ChromeDriver();

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            driver.Navigate().GoToUrl(url);

           // driver.FindElement(By.XPath("/html/body/div[2]/div/div[8]/div[3]/div[3]/a")).Click();
             //driver.FindElement(By.XPath("/html/body/div[5]/div/div[2]/div[2]/div/div[1]/div/a/div[2]")).Click();
             //driver.FindElement(By.XPath("/html/body/div[6]/div/div[2]/div/div/div[1]/div/a/div[2]")).Click();

            //search and parse all elements with name and price
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            Func<IWebDriver, bool> all = new Func<IWebDriver, bool>((IWebDriver Web) =>
            {
                lstprod = new List<Product>(); ;

                allprices = Web.FindElements(By.XPath("//div[@class='aac-price']/span")).ToList();
                //allpricesformin = Web.FindElements(By.XPath("//div[@class='aac-price']/span")).ToList();
                allnames = Web.FindElements(By.XPath("//div[@class='product-name']/a")).ToList();

                foreach (var a in allnames.Zip(allprices, (t, w) => new { t, w }))
                {
                    //Console.WriteLine(a.t.Text + ' ' + a.w.Text);
                    var txt = Int32.Parse( a.w.Text.Replace(" ", "").Replace(".00", "").Replace(".25", ""));


                    var prod = new Product { Name = a.t.Text, Price = txt };
                        lstprod.Add(prod);
                    
                }

                foreach (var a in lstprod)
                {
                    Console.WriteLine($"{a.Name} : {a.Price}");
                }
             
                return true;
            });

            //page numbers parsing into list, splitting to array
            IList<IWebElement> pages = driver.FindElements(By.XPath("/html/body/div[6]/div/div[3]/div[3]/div/div/div/div/div")).ToList();

            string[] separator = { "\r\n" };
            string[] lines_page = Listtostringaaray(separator, 10, pages);
            string[] stringminsep = Listtostringaaray(separator, 100, allpricesformin);

            //find all elements into each page
            int pagenum = 1;

            while (true)
            {
                wait.Until(all);

                pagenum++;
                if (pagenum > lines_page.Length)
                { break; }
                var pagechange = driver.FindElement(By.XPath("/html/body/div[6]/div/div[3]/div[3]/div/div/div/div/div/a[" + pagenum + "]/div"));
         
                pagechange.Click();
                wait.Until(ExpectedConditions.StalenessOf(pagechange));

            }
           // var min = lstprod.SelectMany(Product => Product.Price).Min(item => item.Price);


        }

        static string[] Listtostringaaray(string[] var, int nr, IList<IWebElement> list)
        {
            string[] stringseparator = new string[] { "\r\n" };
            string[] lines_page = new string[nr];
            foreach (var p in list)
            {
                stringseparator = new string[] { "\r\n" };
                lines_page = p.Text.Split(stringseparator, StringSplitOptions.None);
            }
            return lines_page;
        }
    }
    }

