using System;
namespace Backend.Models
{
    public class SearchResult
    {
        public long TimeSpan { get; set; }

        public decimal[] ResultList { get; set; }
    }
}
