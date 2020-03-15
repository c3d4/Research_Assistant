using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace FinalResearchAssistant
{
    public class ApiHelper
    {
        // Properties of the API 
        public string url;

        // Result returned from the web service
        public string result;                   // Raw result from web service
        public string[,] titleAndLink_XML;          // Dirty (Multidimensional array) for XML
        public string[,] titleAndLink_JSON;          // Dirty (Multidimensional array) for JSON 
        
        #region Requests
        // GET Request to server, returns content of server
        public async void GetRequest()
        {
            // Create an instance of a HTTP client and it's properties
            HttpClient _client = new HttpClient();
            HttpResponseMessage _response = await _client.GetAsync(url);

            // Get the response from the web service
            HttpContent _content = _response.Content;
            HttpContentHeaders _headers = _content.Headers;

            // Set raw results to the server response
            result = await _content.ReadAsStringAsync();

            // Match the web service to its appropriate parser
            if (url.Contains("arxiv"))
            {
                readXML(result);
            }
            else if (url.Contains("core"))
            {
                readJSON(result);
            }
        }

        // POST Request to server, returns content of server
        public async void PostRequest(string query1, string query2, string field1, string field2)
        {
            IEnumerable<KeyValuePair<string, string>> _queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(field1, query1),
                new KeyValuePair<string, string>(field2, query2)
            };

            HttpContent _query = new FormUrlEncodedContent(_queries);
            HttpClient _client = new HttpClient();
            HttpResponseMessage _response = await _client.PostAsync(url, _query);
            HttpContent _content = _response.Content;
            HttpContentHeaders _headers = _content.Headers;

            result = await _content.ReadAsStringAsync();
        }

        #endregion

        #region File Parsers/Readers
        public void readXML(string xml)
        {
            string _fileName = "Research_Assistant_Result.xml";

            try
            {
                // Delete the file if it exists
                if (File.Exists(_fileName))
                {
                    File.Delete(_fileName);
                }

                // Create a new file
                using (FileStream fs = File.Create(_fileName))
                {
                    //Add some text to the file
                    byte[] title = new UTF8Encoding(true).GetBytes(result);
                    fs.Write(title, 0, title.Length);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.ToString());
            }

            // Create the xml document 
            XmlDocument doc = new XmlDocument();
            doc.Load("Research_Assistant_Result.xml");

            // Display all of the titles to the works in the xml 
            XmlNodeList titleList = doc.GetElementsByTagName("title");
            titleAndLink_XML = new string[titleList.Count, 2];

            for (int i = 1; i < titleList.Count; i++)
            {
                // Append titles to multiDimensional Array
                if (titleList[i].InnerXml != "")
                {
                    string fixedTitle = Regex.Replace(titleList[i].InnerXml, @"\t|\n|\r", "");
                    titleAndLink_XML[i, 0] = fixedTitle;
                }
            }

            // Append links to multiDimensional Array
            XmlNodeList linkList = doc.GetElementsByTagName("id");
            for (int i = 1; i < linkList.Count; i++)
            {
                if (linkList[i].InnerXml != "")
                {
                    titleAndLink_XML[i, 1] = linkList[i].InnerXml;
                }
            }

            writeToFile("Research_Results_XML", titleAndLink_XML);
            clearSearch();
        }

        public void readJSON(string json)
        {
            // Convert the JSON String to a series of objects
            Rootobject root = JsonConvert.DeserializeObject<Rootobject>(json);

            if (root.data != null)
            {
                titleAndLink_JSON = new string[root.data.Length, 2];

                // Get the title and link of each journal
                for (int i = 0; i < root.data.Length; i++)
                {
                    string link = root.data[i].identifiers[2];
                    if (link.Contains("url:"))
                    {
                        titleAndLink_JSON[i, 0] = root.data[i].title;
                        link.Replace("url:", "");
                        titleAndLink_JSON[i, 1] = link;
                    }
                }
                writeToFile("Research_Results_JSON", titleAndLink_JSON);
            }
        }

        #endregion

        #region JSON Serialization

        public class Rootobject
        {
            public string status { get; set; }
            public int totalHits { get; set; }
            public Datum[] data { get; set; }
        }

        public class Datum
        {
            public string title { get; set; }
            public string[] identifiers { get; set; }
            public string[] subjects { get; set; }
            public string language { get; set; }
            public string publisher { get; set; }
            public string rights { get; set; }
        }

        #endregion

        #region File and Variable Manipulation
        public void writeToFile(string fileName, string[,] list)
        {
            // Delete the file if it already exists
            if (File.Exists(fileName + ".txt"))
            {
                File.Delete(fileName + ".txt");
            }

            foreach (var entry in list)
            {
                File.AppendAllText(fileName + ".txt", entry + Environment.NewLine);
            }
        }

        public void clearSearch()
        {
            result = "";
        }
        #endregion

    }
}
