using System.Collections.Generic;
using HtmlAgilityPack;
using InfoTrackSearch.Models;

namespace InfoTrackSearch.Service
{
    public interface IInfoTrackSearchService
    {
        ReturnString GetResult(string keywords);
        List<HtmlNode> nodes(string text);
        List<int> StringOccurrencesIndex(List<HtmlNode> nodes, string pattern);
    }
}