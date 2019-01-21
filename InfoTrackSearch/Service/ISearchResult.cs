using InfoTrackSearch.Models;

namespace InfoTrackSearch.Service
{
    public interface ISearchResult
    {
        ReturnString GetResult(string keywords);
    }
}