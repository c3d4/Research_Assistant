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

            //string fileName = "Research_Results_Merged";

            try
            {
                File.WriteAllText("Research_Results_Merged.txt", "");
                
                foreach (var entity in finalResults)
                {
                    File.AppendAllText("Research_Results_Merged" + ".txt", entity + Environment.NewLine);
                    if (File.ReadAllText("Research_Results_History.txt").Contains(entity) == false)
                    {
                        File.AppendAllText("Research_Results_History.txt", entity + Environment.NewLine);
                    }   
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
