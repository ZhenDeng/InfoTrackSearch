using System;
using System.Collections.Generic;
using InfoTrackSearch.Infrastructure;
using InfoTrackSearch.Models;
using InfoTrackSearch.Service;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrackSearch.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class InfoSearchController : ControllerBase
    {
        private readonly IInfoTrackSearchService _service;

        public InfoSearchController(IInfoTrackSearchService service)
        {
            //DI service
            _service = service;
        }

        //create an api for result model of indexs and urls
        [HttpGet("ResultList")]
        [NoCache]
        public IActionResult ResultList(string keywords, string url)
        {
            try
            {
                ReturnString result = _service.GetResult(keywords);
                var nodes = _service.nodes(result.result);
                List<int> index = _service.StringOccurrencesIndex(nodes, url);
                List<string> urls = new List<string>();
                foreach (int i in index)
                {
                    urls.Add(nodes[i].InnerText);
                }
                ResultModel resultModel = new ResultModel() {
                    IndexList = index,
                    UrlList = urls
                };
                return Ok(resultModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}