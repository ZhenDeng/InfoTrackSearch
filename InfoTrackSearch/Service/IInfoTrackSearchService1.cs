using System.Collections.Generic;
using InfoTrackSearch.Models;

namespace InfoTrackSearch.Service
{
    public interface IInfoTrackSearchService1
    {
        int CountStringOccurrences(string text, string pattern);
        ReturnString GetResult(string keywords);
        List<int> StringOccurrencesIndex(string text, string pattern);
    }
}