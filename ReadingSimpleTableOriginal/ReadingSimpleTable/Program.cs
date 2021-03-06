﻿using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingSimpleTable
{
    //add reference for base class
    public class Program : Base
    {

        //public Program()
        //{
        //    Driver = new ChromeDriver();
        //    Driver.Navigate().GoToUrl("file:///C:/Users/anuradha/Desktop/Selenium/SimpleTable.html");
        //}
        static void Main(string[] args)
        {
            //Program p = new Program();

            Driver = new ChromeDriver();
            Driver.Navigate().GoToUrl("file:///C:/Users/anuradha/Desktop/Selenium/SimpleTable.html");

            //instantiate a new TablePage
            TablePage page = new TablePage();

            //reads the Html table
            Utilities.ReadTable(page.table);

            /*
            //get the cell value from the table
            Console.WriteLine(Utilities.ReadCellValue("Firstname", 1));
            */

            Console.WriteLine("****************************************************************");

            //string formatting for the cell values outputted in the console
            Console.WriteLine("The  Firstname {0}    LastName {1}  and is Age {2} years old and is a  Gender {3}", 
                Utilities.ReadCellValue("Firstname", 2), Utilities.ReadCellValue("Lastname", 2), Utilities.ReadCellValue("Age", 2), Utilities.ReadCellValue("Gender", 2));

            Console.WriteLine("****************************************************************");

            Console.Read();
        }
    }
}
