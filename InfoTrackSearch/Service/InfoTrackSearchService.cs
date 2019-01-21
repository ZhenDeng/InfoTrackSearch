using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using InfoTrackSearch.Models;

namespace InfoTrackSearch.Service
{
    public class InfoTrackSearchService : IInfoTrackSearchService
    {
        //get html of result by calling google url with search keywords
        public ReturnString GetResult(string keywords) {
            string url = "https://www.google.com/search";
            keywords = keywords.Replace(" ", "+");
            Dictionary<string, string> inputModel = new Dictionary<string, string>();
            inputModel.Add("q", keywords);
            inputModel.Add("num", "100");
            var result = GetGoogleSearchResult.GetResult(url, WebMethods.GET, inputModel, null);
            return result;
        }

        //get url for each google search result
        public List<HtmlNode> nodes(string text) {
            var nodeList = new List<HtmlNode>();
            var tags = new string[] { "cite" };
            var document = new HtmlDocument();
            document.LoadHtml(text);
            foreach (var tag in tags)
            {
                nodeList.AddRange(
                    from searchText in document.DocumentNode.Descendants(tag)
                    select searchText
                );
            }
            return nodeList;
        }

        //get target url appearance index in result list
        public List<int> StringOccurrencesIndex(List<HtmlNode> nodes, string pattern) {

            List<int> index = new List<int>();

            for (int i=0; i<nodes.Count; i++) {
                if (nodes[i].InnerText.Contains(pattern)){
                    index.Add(i);
                }
            }
            return index;
        }
    }
}
