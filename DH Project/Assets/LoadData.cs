using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    public List<JobTypes> types = new List<JobTypes>();

    // Start is called before the first frame update
    void Start()
    {
      TextAsset DH_Data = Resources.Load<TextAsset>("2018_Data");
      string[] data = DH_Data.text.Split(new char[] { '\n' });

      for (int i = 1; i < data.Length; i++)
      {
        string[] row = data[i].Split(new char[] { ',' });

        JobTypes t = new JobTypes();

        t.Racial_Ethnic_Group_and_Sex = row[0];
        int.TryParse(row[1], out t.Executive_Senior_Level_Officials_and_Managers);
        int.TryParse(row[2], out t.First_Mid_Level_Officials_and_Managers);
        int.TryParse(row[3], out t.Professionals);
        int.TryParse(row[4], out t.Technicians);
        int.TryParse(row[5], out t.Sales_Workers);
        int.TryParse(row[6], out t.Office_and_Clerical_Workers);
        int.TryParse(row[7], out t.Craft_Workers);
        int.TryParse(row[8], out t.Operatives);
        int.TryParse(row[9], out t.Laborers);
        int.TryParse(row[10], out t.Service_Workers);

        types.Add(t);
      }
    }

    public List<JobTypes> GetList()
    {
        return types;
    }

    // // Update is called once per frame
    // void Update()
    // {
    //
    // }
}
