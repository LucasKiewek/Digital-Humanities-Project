using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public static class LoadData
{
    private static List<RacialEthnicGroupInfo> _racialEthnicGroups = new List<RacialEthnicGroupInfo>();

    public static List<RacialEthnicGroupInfo> GetData()
    {
        // If not already loaded, load text
        if (_racialEthnicGroups.Count == 0)
        {
            // Load the data as text
            TextAsset DH_Data = Resources.Load<TextAsset>("2018_Data");
            string[] data = DH_Data.text.Split(new char[] { '\n' });

            foreach (string line in data)
            {
                // Split string for line by comma
                string[] row = line.Split(new char[] { ',' });
                
                // Create a new RacialEthnicGroupInfo object for this row's racial ethnic group
                RacialEthnicGroupInfo info = new RacialEthnicGroupInfo(
                    RacialEthnicGroupInfo.RacialEthnicGroupFromString(row[0])
                );

                // Iterate through columns and set the numbers
                for (int i = 1; i < row.Length; i++)
                {
                    int count;
                    int.TryParse(row[i], out count);
                    
                    // Casting an int to an enum value is sort of like accessing an array by index;
                    // As long as the enum's declaration is in the same order as the columns in the csv, this code should work
                    info.AddJobType((JobType) i-1, count);
                }
                
                // Add the info to a list
                _racialEthnicGroups.Add(info);
            }
        }

        // Return the list
        return _racialEthnicGroups;
    }
}
