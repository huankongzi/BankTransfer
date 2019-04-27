using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Models
{
    public class DuePayFinder
    {
        public decimal[] Find(decimal[] duePayList, decimal bankTransfer)
        {
            List<decimal> resultList = new List<decimal>();
            duePayList = duePayList.OrderBy(r => r).ToArray();
            Find(resultList, duePayList, bankTransfer, 0);
            return resultList.ToArray();
        }

        private bool Find(List<decimal> resultList, decimal[] duePayList, decimal bankTransfer, int start)
        {
            if (start >= duePayList.Length)
                return false;

            if (bankTransfer == 0)
                return true;

            if (bankTransfer < duePayList[start])
                return false;

            for (int i = start; i < duePayList.Length; i++)
            {
                resultList.Add(duePayList[i]);
                bool result = Find(resultList, duePayList, bankTransfer - duePayList[i], i + 1);
                if (result)
                {
                    return true;
                }
                else
                {
                    resultList.RemoveAt(resultList.Count - 1);
                }
            }

            return false;
        }
    }
}
