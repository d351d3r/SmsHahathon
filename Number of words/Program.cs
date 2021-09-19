using System;
using System.IO;
using System.Collections.Generic;
using OfficeOpenXml;

namespace Number_of_words
{
    class Program
    {
        static void Main(string[] args)
        {
            //create a list to hold all the values
            List<string> excelData = new List<string>();

            //read the Excel file as byte array
            byte[] bin = File.ReadAllBytes(@"../../../test_data.xlsx");

            //or if you use asp.net, get the relative path
            //       byte[] bin = File.ReadAllBytes(Server.MapPath("ExcelDemo.xlsx"));

            //create a new Excel package in a memorystream
            using (MemoryStream stream = new MemoryStream(bin))
            using (ExcelPackage excelPackage = new ExcelPackage(stream))
            {
                //loop all worksheets
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                foreach (ExcelWorksheet worksheet in excelPackage.Workbook.Worksheets)
                {
                    //loop all rows
                    for (int i = worksheet.Dimension.Start.Row; i <= worksheet.Dimension.End.Row; i++)
                    {
                        //loop all columns in a row
                        for (int j = worksheet.Dimension.Start.Column; j <= worksheet.Dimension.End.Column; j++)
                        {
                            //add the cell data to the List
                            if (worksheet.Cells[i, j].Value != null)
                            {
                                excelData.Add(worksheet.Cells[i, j].Value.ToString());
                            }
                        }
                        excelData.Add("endOfRow");
                    }
                }
            }
            ExcelParser parser = new ExcelParser(excelData,DateTime.Now.AddDays(-1));
            /*            Console.WriteLine(parser.GetTrainNumber());
            Console.WriteLine(parser.GetDepartureTime());
            Console.WriteLine(parser.GetPlatform());*/
            SmsSender.Send("+79142167272", parser.GetDepartureTime(), parser.GetTrainNumber(), parser.GetPlatform());
            Console.ReadKey();
        }
    }
}
