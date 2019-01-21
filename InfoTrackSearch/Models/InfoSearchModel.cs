using System;
using System.Collections.Generic;

namespace InfoTrackSearch.Models
{
    public enum WebMethods
    {
        GET,
        POST
    }

    public class ReturnString {
        public string result { get; set; }
    }

    public class ResultModel {
        public List<int> IndexList { get; set; }
        public List<string> UrlList { get; set; }
    }

}
