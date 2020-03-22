using System;
using System.Collections.Generic;
using System.IO;

namespace FinalResearchAssistant
{
    class LocalGatherer
    {
        // Username and history
        static string user;
        static bool enableHistory;

        // Merge the XML and JSON result files
        public static void mergeFiles()
        {
            // Create a queue as a stream for the data
            Queue<string> finalResults = new Queue<string>();

            // Reset the Research_Results_History file 
            File.AppendAllText(@"" + user + @"\Research_Results_History.txt", "");  

            #region Enqueue the data in a queue data structure

            if (File.Exists(@"" + user + @"\Research_Results_XML.txt"))
            {
                // Create an array of all of the lines in XML File
                string[] lines = File.ReadAllLines(@"" + user + @"\Research_Results_XML.txt");

                // Go through each line in the XML file 
                foreach (var line in lines)
                {
                    // Make sure that the line has content
                    if (line != "")
                    {
                        // Add the line to the queue
                        finalResults.Enqueue(line);
                    }
                }
            }
            
            if (File.Exists(@"" + user + @"\Research_Results_JSON.txt"))
            {
                // Create an array of all of the lines in JSON File
                string [] Jlines = File.ReadAllLines(@"" + user + @"\Research_Results_JSON.txt");

                // Go through each line in the JSON file 
                foreach (var line in Jlines)
                {
                    // Make sure that the line has content
                    if (line != "")
                    {
                        // Add the line to the queue
                        finalResults.Enqueue(line);
                    }
                }
            }

            #endregion

            #region Create Merged and History Folders

            // Catch any errors if either file doesnt exist (Null return from GET/POST Request)
            try
            {
                // Reset the Merged File
                File.Create(@"" + user + @"\Research_Results_Merged.txt").Close();
                
                // Transfer the merged data via a dequeue
                int count = finalResults.Count;

                for (int i = 0; i < count; i++)
                {
                    File.AppendAllText(@"" + user + @"\Research_Results_Merged" + ".txt", finalResults.Peek() + Environment.NewLine);

                    // Make sure that the history doesn't contain any duplicate results
                    if (enableHistory == true)
                    {
                        if (File.ReadAllText(@"" + user + @"\Research_Results_History.txt").Contains(finalResults.Peek()) == false)
                        {
                            File.AppendAllText(@"" + user + @"\Research_Results_History.txt", finalResults.Peek() + Environment.NewLine);
                        }
                    }

                    finalResults.Dequeue();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            #endregion
        }

        // Get the username from the form 
        public static void getUser(string username, bool history)
        {
            user = username;
            enableHistory = history;
        }
    }
}
