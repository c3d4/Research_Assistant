using System;
using System.Collections.Generic;
using System.IO;

namespace FinalResearchAssistant
{
    class LocalGatherer
    {
        public static void mergeFiles()
        {
            Queue<string> finalResults = new Queue<string>();
            
            File.AppendAllText("Research_Results_History.txt", "");  // Append nothing to a blank file everytime this is called
            
            #region Enqueue the data in a queue data structure

            string[] lines = File.ReadAllLines("Research_Results_XML.txt");

            foreach (var line in lines)
            {
                if (line != "")
                {
                    finalResults.Enqueue(line);
                }
            }

            lines = File.ReadAllLines("Research_Results_JSON.txt");
            
            foreach (var line in lines)
            {
                if (line != "")
                {
                    finalResults.Enqueue(line);
                }
            }

            #endregion

            #region Create Merged and History Folders

            try
            {
                File.WriteAllText("Research_Results_Merged.txt", "");
                int count = finalResults.Count;

                for (int i=0; i<count; i++)
                {
                    File.AppendAllText("Research_Results_Merged" + ".txt", finalResults.Peek() + Environment.NewLine);
                    if (File.ReadAllText("Research_Results_History.txt").Contains(finalResults.Peek()) == false)
                    {
                        File.AppendAllText("Research_Results_History.txt", finalResults.Peek() + Environment.NewLine);
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
    }
}
