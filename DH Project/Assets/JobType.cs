using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum RacialEthnicGroup
{
  BlackMan,
  WhiteMan,
  BlackWoman,
  WhiteWoman
}

public enum JobType
{
  ExecutiveSeniorLevelOfficials,
  FirstMidLevelOfficials,
  Professionals,
  Technicians,
  SalesWorkers,
  OfficeClericalWorkers,
  CraftWorkers,
  Operatives,
  Laborers,
  ServiceWorkers
}

public class RacialEthnicGroupInfo
{
  public RacialEthnicGroup racialGroup;
  public Dictionary<JobType, int> employmentNumbers = new Dictionary<JobType, int>();

  public RacialEthnicGroupInfo(RacialEthnicGroup group)
  {
    racialGroup = group;
  }
  
  public RacialEthnicGroupInfo(RacialEthnicGroup group, Dictionary<JobType, int> numbers)
  {
    racialGroup = group;
    employmentNumbers = numbers;
  }

  public void AddJobType(JobType jobType, int count)
  {
    employmentNumbers[jobType] = count;
  }

  public static RacialEthnicGroup RacialEthnicGroupFromString(string group)
  {
    switch (group)
    {
      case "WHITEMen":
        return RacialEthnicGroup.WhiteMan;
      case "BLACKMen":
        return RacialEthnicGroup.BlackMan;
      case "WHITEWomen":
        return RacialEthnicGroup.WhiteWoman;
      case "BLACKWomen":
        return RacialEthnicGroup.BlackWoman;
      default:
        // Why not have the black woman be default
        
        return RacialEthnicGroup.BlackWoman;
    }
  }
}
