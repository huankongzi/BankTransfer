using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class DuePayReader
    {
        public LineItem[] Read(StreamReader sr)
        {
            List<LineItem> resultList=new List<LineItem>();
            while (sr.Peek() != -1)
            {
                string line = sr.ReadLine();
                if (string.IsNullOrEmpty(line))
                    continue;

                string[] parts = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 0)
                    continue;

                LineItem item;
                if (parts.Length >= 2)
                {
                    item = new LineItem
                    {
                        DuePay = parts[0],
                        BankTransfer = parts[1]
                    };
                }
                else
                {
                    item = new LineItem
                    {
                        DuePay = parts[0]
                    };
                }
                resultList.Add(item);
            }

            return resultList.ToArray();
        }
    }
}
