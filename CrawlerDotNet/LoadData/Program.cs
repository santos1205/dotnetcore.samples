using LoadData.DataAccess;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoadData
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver _webDriver = null;
            var _chromeDriverService = ChromeDriverService.CreateDefaultService();
            _webDriver = new ChromeDriver(_chromeDriverService);
            _webDriver.Navigate().GoToUrl("<url de destino>");
            _webDriver.Manage().Window.Maximize();

            IWebElement element = _webDriver.FindElement(By.XPath("XPATH ELEMENT"));
            FillSlow(element, "<login>");

            element = _webDriver.FindElement(By.XPath("XPATH ELEMENT"));
            FillSlow(element, "<senha>");
            //Thread.Sleep(1000);
            element = _webDriver.FindElement(By.XPath("XPATH ELEMENT"));
            //Thread.Sleep(1000);
            element.Click();

            Console.WriteLine("REGISTROS CARREGADOS COM SUCESSO!!");
        }


        static void FillSlow(IWebElement element, string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                string dig = value.Substring(i, 1);
                element.SendKeys(dig);
                //Força o cursor a permanecer a direita após a inserção do dígito. Em alguns inputs, o cursor não anda.
                element.SendKeys(Keys.Right);
                Thread.Sleep(200);
            }
        }
    }
}
