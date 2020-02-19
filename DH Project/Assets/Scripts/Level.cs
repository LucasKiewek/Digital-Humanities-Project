using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level : MonoBehaviour
{
    public JobType jobType;
    public float width;

    [System.Serializable]
    public class Prefabs
    {
      public GameObject BlackManPrefab;
      public GameObject WhiteManPrefab;
      public GameObject BlackWomanPrefab;
      public GameObject WhiteWomanPrefab;

      public GameObject PrefabFromRacialEthnicGroup(RacialEthnicGroup group)
      {
        switch (group)
        {
          case RacialEthnicGroup.BlackMan:
            return BlackManPrefab;
          case RacialEthnicGroup.WhiteMan:
            return WhiteManPrefab;
          case RacialEthnicGroup.BlackWoman:
            return BlackWomanPrefab;
          case RacialEthnicGroup.WhiteWoman:
            return WhiteWomanPrefab;
          default:
            return BlackWomanPrefab;
        }
      }
    }

    public Prefabs prefabs;

    public TMP_Text LevelLabel;

    // Start is called before the first frame update
    void Start()
    {
      string levelName = jobType.ToString();
      LevelLabel.text = levelName;

      List<RacialEthnicGroupInfo> info = LoadData.GetData();

      int i = 0;
      foreach (RacialEthnicGroupInfo group in info)
      {
        StartCoroutine(SpawnPrefabs(group.racialGroup, group.employmentNumbers[jobType], i));
        i += 1;
      }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator SpawnPrefabs(RacialEthnicGroup group, int count, int interval = 0)
    {
      GameObject prefab = prefabs.PrefabFromRacialEthnicGroup(group);

      Vector3 position = transform.position;
      float intervalLength = width / 5;

      position.x += -width / 2;
      position.x += interval * intervalLength;
      position.x += (Random.value * 8) - 4;

      for (int i = 0; i < (count / 100000); i++)
      {
        Instantiate(
          prefab,
          position,
          Quaternion.Euler(
            new Vector3(0, 0, UnityEngine.Random.value * 360)
          )
        );
        yield return new WaitForSeconds(0.15f);
      }
    }
}
