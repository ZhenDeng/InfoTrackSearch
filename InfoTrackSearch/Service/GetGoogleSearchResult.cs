using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using InfoTrackSearch.Models;
using Newtonsoft.Json;

namespace InfoTrackSearch.Service
{
    public class GetGoogleSearchResult
    {
        //Get method: queryParameters, Post method: bodyContent
        //queryParameters: using dictionary and set key to query name, value to query value
        public static ReturnString GetResult(string baseUrl, WebMethods method, dynamic queryParameters = null, dynamic bodyContent = null)
        {
            string url = string.Empty;
            if (queryParameters != null)
            {
                url = BuildUrl(baseUrl, queryParameters);
            }
            else
            {
                url = baseUrl;
            }
            var request = (HttpWebRequest)HttpWebRequest.Create(url);

            request.Method = method.ToString();
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                if (bodyContent != null)
                {
                    string contentString = string.Empty;
                    if (bodyContent is String)
                    {
                        contentString = bodyContent;
                    }
                    else
                    {
                        contentString = JsonConvert.SerializeObject(bodyContent);
                    }
                    using (var writer = new StreamWriter(request.GetRequestStream()))
                    {
                        writer.Write(contentString);
                    }
                }
                var response = request.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseContent = reader.ReadToEnd();

                    var result = new ReturnString();

                    try
                    {
                        if (!string.IsNullOrWhiteSpace(responseContent))
                            result.result = responseContent;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    return result;

                }
            }
            catch (WebException webException)
            {
                throw webException;
            }
        }

        private static string BuildUrl(string baseUrl, dynamic queryParameters)
        {
            string url = string.Empty, parameterList = string.Empty;
            if (queryParameters != null)
            {
                try
                {
                    // the parameters could be dictionary or list

                    Type type = queryParameters.GetType();
                    if (type.Name == "Dictionary`2" || type.Name == "List`1") // dictionary/list
                    {
                        foreach (KeyValuePair<string, string> param in queryParameters)
                        {
                            if (parameterList.Length > 0) parameterList += "&";
                            parameterList += string.Format("{0}={1}", param.Key, param.Value);
                        }
                    }
                    else // class with properties
                    {
                        foreach (PropertyInfo propertyInfo in type.GetProperties())
                        {
                            if (propertyInfo.CanRead)
                            {
                                if (parameterList.Length > 0) parameterList += "&";
                                parameterList += string.Format("{0}={1}", propertyInfo.Name, propertyInfo.GetValue(queryParameters, null));
                            }
                        }
                    }
                }
                catch (JsonException jex)
                {
                    throw jex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            if (!string.IsNullOrWhiteSpace(parameterList))
            {
                url = string.Format("{0}?{1}", baseUrl, parameterList);
            }
            else
            {
                url = baseUrl;
            }

            return url;
        }
    }
}
