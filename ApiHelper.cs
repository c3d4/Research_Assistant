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
            // Set the field properties and queries
            IEnumerable<KeyValuePair<string, string>> _queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(field1, query1),
                new KeyValuePair<string, string>(field2, query2)
            };

            // Create an instance of a HTTP client and it's properties
            HttpContent _query = new FormUrlEncodedContent(_queries);
            HttpClient _client = new HttpClient();

            // Get the response from the web service
            HttpResponseMessage _response = await _client.PostAsync(url, _query);
            HttpContent _content = _response.Content;
            HttpContentHeaders _headers = _content.Headers;

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

        #endregion

        #region File Parsers/Readers

        // Parse XML elements from the result of the web service
        public void readXML(string xml)
        {
            // Create an XML File using the results of the web service
            string _fileName = "Research_Assistant_Result.xml";

            try
            {
                // Delete overriding/leftover results
                if (File.Exists(_fileName))
                {
                    File.Delete(_fileName);
                }

                // Create the XML file 
                using (FileStream fs = File.Create(_fileName))
                {
                    //Add the results to the XML file 
                    byte[] title = new UTF8Encoding(true).GetBytes(result);
                    fs.Write(title, 0, title.Length);
                }
            }
            catch (Exception ex)
            {
                // If an error occurs, catch it, display the error on the back-end console, and skip over 
                System.Diagnostics.Debug.Write(ex.ToString());
            }

            // Load the XML Document for parsing
            XmlDocument doc = new XmlDocument();
            doc.Load("Research_Assistant_Result.xml");

            // Get a list of all of the journal titles in the XML
            XmlNodeList titleList = doc.GetElementsByTagName("title");

            // Fix the size of the multi-dimensional array dynamically, according to the size of titleList
            titleAndLink_XML = new string[titleList.Count, 2];

            for (int i = 1; i < titleList.Count; i++)
            {
                // Append titles to the multi dimensional array
                if (titleList[i].InnerXml != "")
                {
                    // Use regex to remove any unwanted characters in the title
                    string fixedTitle = Regex.Replace(titleList[i].InnerXml, @"\t|\n|\r", "");

                    // Assign the title to a position in the multi dimensional array
                    titleAndLink_XML[i, 0] = fixedTitle;
                }
            }

            // Get a list of all of the journal links in the XML
            XmlNodeList linkList = doc.GetElementsByTagName("id");

            for (int i = 1; i < linkList.Count; i++)
            {
                // Make sure that the value isn't null
                if (linkList[i].InnerXml != "")
                {
                    // Assign the link to a position in the multi dimensional array
                    titleAndLink_XML[i, 1] = linkList[i].InnerXml;
                }
            }

            // Write the elements of the multi dimensional array in a text file
            writeToFile("Research_Results_XML", titleAndLink_XML);

            // Clear overriding info for next use
            clearSearch();
        }

        public void readJSON(string json)
        {
            // Convert the JSON String (from the web service) to a series of objects
            Rootobject root = JsonConvert.DeserializeObject<Rootobject>(json);

            // Make sure that the JSON has data in the objects
            if (root.data != null)
            {
                // Dynamically set the size of the multi dimensional array based on the number of results
                titleAndLink_JSON = new string[root.data.Length, 2];

                // Get the title and link of each journal
                for (int i = 0; i < root.data.Length; i++)
                {
                    string link = root.data[i].identifiers[2]; // 2 is a constant from the results of the web service

                    // Make sure that the data being parsed is the URL 
                    if (link.Contains("url:"))
                    {
                        titleAndLink_JSON[i, 0] = root.data[i].title;

                        // Remove unwanted characters from the link and add it to the multi dimensional array 
                        link = link.Remove(0, 4);
                        titleAndLink_JSON[i, 1] = link;
                    }
                }

                // Write the elements of the multi dimensional array in a text file
                writeToFile("Research_Results_JSON", titleAndLink_JSON);

                // Clear overriding info for next use
                clearSearch();
            }
        }

        #endregion

        #region JSON Serialization

        /// JSON PARSER NOTICE:
        /// Some of this data wont be returned to the main form, however must be set up correctly as such
        /// to mimick the architecture of the JSON and the output of the web service. This is needed for 
        /// the parser to function correctly. 

        // Set object properties for the JSON from the web service
        public class Rootobject
        {
            public string status { get; set; }  // Status of the request
            public int totalHits { get; set; }  // Total hits
            public Datum[] data { get; set; }   // Second object property for data inside the root object property
        }

        // Second object property to parse the useful data out of the JSON    
        public class Datum
        {
            public string title { get; set; }   // Title of journal
            public string[] identifiers { get; set; }   // Unique Identifier - contains the URL 
            public string[] subjects { get; set; } // Subject of interest
            public string language { get; set; }   // Language of the paper
            public string publisher { get; set; }  // Publisher
            public string rights { get; set; }  // Rights of the user in regards to the paper
        }

        #endregion

        #region File and Variable Manipulation

        // Write results of each scraper to a text (.txt) file 
        public void writeToFile(string fileName, string[,] list)
        {
            // Delete the file if it already exists
            if (File.Exists(fileName + ".txt"))
            {
                File.Delete(fileName + ".txt");
            }

            // Add the data from the entries provided
            foreach (var entry in list)
            {
                File.AppendAllText(fileName + ".txt", entry + Environment.NewLine);
            }
        }

        // Function to clear the client-side for future use 
        public void clearSearch()
        {
            result = "";
        }

        #endregion

    }
}
