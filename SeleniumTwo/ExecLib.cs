
using ExcelDataReader;
using System.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTwo
{
    class ExecLib
    {

        /*
        public static DataTable ExcelToDataTable(string fileName)
        {
            //open file and returns as Stream
            FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
            //Createopenxmlreader via ExcelReaderFactory
            IExcelDataReader.IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream); //.xlsx
            //'IsFirstRowAsColumnNames' is not recognized anymore replaced the following code to store  DataSet Result in a variable.                                                                              //Set the First Row as Column Name
            //set the first row as Column name
            //excelReader.IsFirstRowAsColumnNames = true;
            //return as DataSet      
             DataSet result = excelReader.AsDataSet();
            //Get all the Tables
            DataTableCollection table = result.Tables;
            //Store it in DataTable
            DataTable resultTable = table["Sheet1"];

            //return
            return resultTable;
        }   */


        //Replaced the above lines of code as there were some new changes in ExcelDataReader package to read Data from Excel for automation
        //Excel to Data Table convertion which gets the date out of Excel sheet into a Data table
        private static DataTable ExcelToDataTable(string fileName)
        {
            //open file and return as a stream (instance of FileStream)

            // class File has a static method called Open and takes three parameters and returns an instance of FileStream
            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                //using a generic method CreateReader() which can detect the type of file and  read file of any type

                // variable reader is an instance of class FileStream
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    //read the data set
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (data) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });

                    //Get all the Tables
                    DataTableCollection table = result.Tables;
                    //Store it in DataTable
                    DataTable resultTable = table["Sheet1"];

                    //return
                    return resultTable;
                }


            }

        }

        static List<Datacollection> dataCol = new List<Datacollection>();

        //populate the data into a collection
        public static void PopulateInCollection(string fileName)
        {
            DataTable table = ExcelToDataTable(fileName);

            //Iterate through the rows and columns of the Table
            for (int row = 1; row <= table.Rows.Count; row++)
            {
                Console.WriteLine("Row No: " + row);
                Console.WriteLine("Row Count: " + table.Rows.Count);
                for (int col = 0; col < table.Columns.Count; col++)
                {
                    Console.WriteLine("Column No: " + col);
                    Console.WriteLine("Column Count: " + table.Columns.Count);
                    Datacollection dtTable = new Datacollection()
                    {
                        rowNumber = row,
                        colName = table.Columns[col].ColumnName,
                        colValue = table.Rows[row - 1][col].ToString()
                    };
                    //Add all the details for each row
                    dataCol.Add(dtTable);
                }
            }
        }

        //Reading data out from the Collection 
        public static string ReadData(int rowNumber, string columnName)
        {
            try
            {
                //Retriving Data using LINQ to reduce much of iterations
                string data = (from colData in dataCol
                               where colData.colName == columnName && colData.rowNumber == rowNumber
                               select colData.colValue).SingleOrDefault();

                //var datas = dataCol.Where(x => x.colName == columnName && x.rowNumber == rowNumber).SingleOrDefault().colValue;
                return data.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }

        //populate the above values into a collection
        public class Datacollection
        {
            public int rowNumber { get; set; }
            public string colName { get; set; }
            public string colValue { get; set; }
        }

}

