using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Number_of_words
{
    class ExcelParser
    {
        private string trainNumber { get; set; }
        private DateTime departureTime { get; set; }
        private int platform { get; set; }

        public ExcelParser(List<string> excelData, DateTime departure)
        {
            for (int i = 0; i < excelData.Count; i++)
            {
                if ((i + 1) != excelData.Count)
                {
                    // Проверка на соответствие формату номера поезда
                    if (excelData[i] == "endOfRow" && Regex.Match(excelData[i + 1], @"\d{3,4}[А-Я]{0,1}").Success)
                    {
                        // Проверка подходит ли время
                        if (DateTime.Compare(DateTime.Now, Convert.ToDateTime(excelData[i + 2]).AddDays(1)) == 0 
                            || DateTime.Compare(DateTime.Now, Convert.ToDateTime(excelData[i + 2]).AddMinutes(15)) == 0
                            || DateTime.Compare(DateTime.Now.Date, Convert.ToDateTime(excelData[i + 2]).Date) == 0)
                        {
                            trainNumber = excelData[i + 1];
                            departureTime = Convert.ToDateTime(excelData[i + 2]);

                            if (excelData[i + 3].Length<3)
                                platform = Convert.ToInt32(excelData[i + 3]);
                            else platform = Convert.ToInt32(excelData[i + 4]);
                        }
                        //else
                        // Эту ветку написал просто в целях тестирования
/*                        {
                            Console.WriteLine("No trains found");
                        }*/
                    }
                }
            }
        }


        public string GetTrainNumber()
        {
            return trainNumber;
        }

        public DateTime GetDepartureTime()
        {
            return Convert.ToDateTime(departureTime);
        }

        public int GetPlatform()
        {
            return platform;
        }
    }
}