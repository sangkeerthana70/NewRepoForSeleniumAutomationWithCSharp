using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTwo
{
    public static class SeleniumSetMethods
    {
        //generic Enter Text method to support all text box controls for the application
        //
        //Extension method for entering text in the control by supplying element and values to be set
        public static void EnterText(this IWebElement element, string value)
        {
            //if (elementtype == PropertyType.Id)
            //    PropertiesCollection.driver.FindElement(By.Id(element)).SendKeys(value);
            //if (elementtype == PropertyType.Name)
            //    PropertiesCollection.driver.FindElement(By.Name(element)).SendKeys(value);

            element.SendKeys(value);
        }

        //custom method to perform a click operation on button, checkbox, option etc
        //Extension method for clicking on a button, checkbox, options etc.
        public static void Clicks(this IWebElement element)
        {
            //if (elementtype == PropertyType.Id)
            //    PropertiesCollection.driver.FindElement(By.Id(element)).Click();
            //if (elementtype == PropertyType.Name)
            //    PropertiesCollection.driver.FindElement(By.Name(element)).Click();
            element.Click();

        }

        //selecting a dropdown control
        //extended method to select a dropdown control
        public static void SelectDropDown(this IWebElement element, string value)
        {

            new SelectElement(element).SelectByText(value);

        }

        public static void ClickSubmit(this IWebElement element)
        {
            element.Submit();
        }

    }
}
