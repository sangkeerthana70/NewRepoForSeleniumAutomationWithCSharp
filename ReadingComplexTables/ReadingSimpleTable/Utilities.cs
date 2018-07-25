using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingComplexTables
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
            Console.WriteLine(table);
            var colums = table.FindElements(By.TagName("th"));
            Console.WriteLine(colums);
            //get all the rows
            var rows = table.FindElements(By.TagName("tr"));
            Console.WriteLine(rows);
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
                        //ColName = colums[colIndex].Text,
                        ColName = colums[colIndex].Text != "" ? 
                                  colums[colIndex].Text : colIndex.ToString(),

                        ColValue = colValue.Text,
                        ColumnSpecialValues = colValue.Text != "" ? null :
                                              colValue.FindElements(By.TagName("input"))
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
        
        //method to perform an operation on the cell
        public static void PerformActionOnCell(string columnIndex, string refColumnName, string refColumnValue, string controlToOperate = null)
        {
            //Console.WriteLine("Inside PerformActionOnCell");
            //Console.WriteLine(columnIndex);
            //Console.WriteLine(refColumnName);
            //Console.WriteLine(refColumnValue);
            Console.WriteLine(controlToOperate);

            foreach (int rowNumber in GetDynamicRowNumber(refColumnName, refColumnValue))
            {

                var cell = (from e in _tableDatacollections
                            where e.ColName == columnIndex && e.RowNumber == rowNumber
                            select e.ColumnSpecialValues).SingleOrDefault();

                //operate on a particular control
                if (controlToOperate != null && cell != null)
                {
                    Console.WriteLine("Debug: inside if");
                    var returnedControl = (from c in cell
                                           where c.GetAttribute("value") == controlToOperate
                                           select c).SingleOrDefault();
                    //ToDo:Currently only the Click operation is supported, future operations like dropdown not implemented yet.
                    returnedControl?.Click();
                }
                else
                {
                    //if (cell != null) cell.First().Click();
                    Console.WriteLine("Debug: inside else");
                    cell?.First().Click();

                }
            }

            

        }

        private static IEnumerable GetDynamicRowNumber(string columnName, string columnValue)
        {
            //Console.WriteLine("Inside GetDynamicRowNumber");
            //Console.WriteLine(columnName);
            //Console.WriteLine(columnValue);

            foreach (var table in _tableDatacollections)
            {
                //return a dynamic row number
                //Console.WriteLine(table.ColName);
                //Console.WriteLine(table.ColValue);

                if (table.ColName == columnName && table.ColValue == columnValue)
                    yield return table.RowNumber;
            }
        }

    }

    //custom class
    public class TableDatacollection
    {
        public int RowNumber { get; set; }
        public string ColName { get; set; }
        public string ColValue { get; set; }

        public IEnumerable<IWebElement> ColumnSpecialValues { get; set; }
    }
}
