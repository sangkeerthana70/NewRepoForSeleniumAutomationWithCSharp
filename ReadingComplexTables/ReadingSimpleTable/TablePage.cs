using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingComplexTables
{
    //add reference for base class
    public class TablePage : Base
    {
        [Obsolete("Use newMethod instead", false)]//modify the code to use the Obsolete atrribute as PageFactory was deprecated
        //initialize page object using constructor
        public TablePage()
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//table")]
        //add the table in the page as a property of IWebElement data type
        public IWebElement table { get; set; }
    }
}
