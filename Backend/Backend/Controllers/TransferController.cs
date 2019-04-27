using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [EnableCors("transferPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private static LineItem[] dataList;
        private static decimal[] duePayList;

        [HttpGet]
        public ActionResult Get()
        {
            if (dataList != null)
                return new JsonResult(dataList);

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.csv");

            List<LineItem> resultList = new List<LineItem>();
            using(StreamReader sr=new StreamReader(path)) 
            { 
                while(sr.Peek()!=-1)
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
            }

            dataList = resultList.ToArray();
            duePayList = resultList.Select(r => decimal.Parse(r.DuePay)).ToArray();
            return new JsonResult(dataList);
        }

        [HttpGet]
        [Route("Search/{number}")]
        public ActionResult Search(decimal number)
        {
            Stopwatch watch = Stopwatch.StartNew();
            DuePayFinder finder = new DuePayFinder();
            decimal[] resultList = finder.Find(duePayList, number);
            watch.Stop();

            SearchResult result = new SearchResult
            {
                ResultList = resultList,
                TimeSpan = watch.ElapsedMilliseconds
            };
            return new JsonResult(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
