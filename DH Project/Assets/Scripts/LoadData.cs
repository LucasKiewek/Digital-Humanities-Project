using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public static class LoadData
{
    public static int DEFAULT_YEAR = 2018;
    
    public static int MIN_YEAR = 2008;
    public static int MAX_YEAR = 2018;
    
    private static Dictionary<int, List<RacialEthnicGroupInfo>> _racialEthnicGroupsByYear = new Dictionary<int, List<RacialEthnicGroupInfo>>();

    public static List<RacialEthnicGroupInfo> GetData(int year)
    {
        // If not already loaded, load text
        if (!_racialEthnicGroupsByYear.ContainsKey(year))
        {
            _racialEthnicGroupsByYear[year] = new List<RacialEthnicGroupInfo>();
            
            // Load the data as text
            TextAsset DH_Data = Resources.Load<TextAsset>($"{year}_Data");
            string[] data = DH_Data.text.Split(new char[] { '\n' });

            for (int i = 1; i < data.Length; i++)
            {
                string line = data[i];
                
                // Split string for line by comma
                string[] row = line.Split(new char[] { ',' });
                
                // Only execute if the row is full; should have as many sections as jobTypes + 1
                if (row.Length == Enum.GetValues(typeof(JobType)).Length + 1)
                {
                    // Create a new RacialEthnicGroupInfo object for this row's racial ethnic group
                    RacialEthnicGroupInfo info = new RacialEthnicGroupInfo(
                        RacialEthnicGroupInfo.RacialEthnicGroupFromString(row[0])
                    );

                    // Iterate through columns and set the numbers
                    for (int j = 1; j < row.Length; j++)
                    {
                        int count;
                        int.TryParse(row[j], out count);

                        // Casting an int to an enum value is sort of like accessing an array by index;
                        // As long as the enum's declaration is in the same order as the columns in the csv, this code should work
                        info.AddJobType((JobType) j-1, count);
                    }

                    // Add the info to a list
                    _racialEthnicGroupsByYear[year].Add(info);
                }
            }
        }

        // Return the list
        return _racialEthnicGroupsByYear[year];
    }

    public static int GetTotalEmployment(JobType jobType, int year)
    {
        int total = 0;
        List<RacialEthnicGroupInfo> info = GetData(year);

        foreach (RacialEthnicGroupInfo group in info)
        {
            total += group.employmentNumbers[jobType];
        }
        
        return total;
    }
}
