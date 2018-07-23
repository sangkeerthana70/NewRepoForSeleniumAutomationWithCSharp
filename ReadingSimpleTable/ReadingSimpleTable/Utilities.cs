using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingSimpleTable
{
    public class Utilities
    {
        static List<TableDatacollection> _tableDatacollections = new List<TableDatacollection>();

        //method to read table data and store it in the list of _tableDatacollections
        //dynamically read the table
        public static void ReadTable(IWebElement table)
        {
            //get all the columns from the table which is <th> tag  in html table
            //this method returns a read only collection og IWebElements
            var colums = table.FindElements(By.TagName("th"));

            //get all the rows
            var rows = table.FindElements(By.TagName("tr"));

            //create row index
            var rowIndex = 0;

            foreach (var row in rows)
            {
                //create column index
                var colIndex = 0;

                //store td's as collections by iterating through each table row
                //this gets all data within the particular row
                var colDatas = row.FindElements(By.TagName("td"));

                //put all the values into collections
                foreach(var colValue in colDatas)
                {
                    _tableDatacollections.Add(new TableDatacollection
                    {
                        RowNumber = rowIndex,
                        ColName = colums[colIndex].Text,
                        ColValue = colValue.Text
                    });

                    //move to next column index
                    colIndex++;
                }
                rowIndex++;

            }

        }

        //method to read the cell value from the table for a given column name and row number and returns a string value of the cell
        public static string ReadCellValue(string columnName, int rowNumber)
        {
            //linq query to get the first cell value
            var data = (from e in _tableDatacollections
                        where e.ColName == columnName && e.RowNumber == rowNumber
                        select e.ColValue).SingleOrDefault();

            return data;
        }
    }

    //custom class
    public class TableDatacollection
    {
        public int RowNumber { get; set; }
        public string ColName { get; set; }
        public string ColValue { get; set; }
    }
}
