using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
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
        private IHttpContextAccessor httpContextAccessor;

        public TransferController(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public ActionResult Get()
        {
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

        [HttpPost]
        [Route("UploadFile")]
        public ActionResult Post()
        {
            var file = httpContextAccessor.HttpContext.Request.Form.Files[0];
            using (StreamReader sr = new StreamReader(file.OpenReadStream()))
            {
                dataList = new DuePayReader().Read(sr);
            }

            duePayList = dataList.Select(r => decimal.Parse(r.DuePay)).ToArray();
            return new OkResult();
        }
    }
}
