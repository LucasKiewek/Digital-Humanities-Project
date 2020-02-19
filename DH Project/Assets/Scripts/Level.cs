using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public JobType jobType;
    public float width;
    public float spawnInterval;
    
    public DataPanel DataPanel;

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

    [SerializeField] public Prefabs prefabs;
    
    // Keep track of spawned prefabs to destroy them on year change
    private List<GameObject> _spawnedPrefabs = new List<GameObject>();
    
    // Keep track of spawn coroutines to destroy them on year change as well; otherwise prefabs keep spawning
    private List<IEnumerator> _spawnCoroutines = new List<IEnumerator>();
    
    // Keep track of current year for convenience
    private int _currentYear;

    // Start is called before the first frame update
    void Start()
    {
      string levelName = AddSpaces(jobType.ToString(), true);
      DataPanel.LevelLabel.text = levelName;

      // Default year
      _currentYear = LoadData.DEFAULT_YEAR;
      DisplayData();
    }

    public void DisplayData()
    {
      StopAllSpawnCoroutines();
      DestroyAllSpawnedPrefabs();

      List<RacialEthnicGroupInfo> info = LoadData.GetData(_currentYear);

      int i = 1;
      foreach (RacialEthnicGroupInfo group in info)
      {
        // Set the text for each label
        DataPanel.GroupLabelFromRacialEthnicGroup(group.racialGroup).Count.text = group.employmentNumbers[jobType].ToString("#,##0");
        
        // Calculate & label percent
        float percent = (float) group.employmentNumbers[jobType] / LoadData.GetTotalEmployment(jobType, _currentYear);
        TMP_Text percentLabel = DataPanel.GroupLabelFromRacialEthnicGroup(group.racialGroup).Percent;
        percentLabel.text = $"({(int) (percent * 100)}%)";
        
        // Set color by percent
        percentLabel.color = Color.Lerp(Color.red, Color.green, percent);

        // Start the spawning coroutine
        IEnumerator spawnCoroutine = SpawnPrefabs(group.racialGroup, group.employmentNumbers[jobType], i);
        StartCoroutine(spawnCoroutine);
        _spawnCoroutines.Add(spawnCoroutine);
        
        i += 1;
      }
    }

    public void ChangeYear(int year)
    {
      // Cancel previous invocations
      CancelInvoke(nameof(DisplayData));
      
      // Wait to start displaying data to avoid jitter
      _currentYear = year;
      Invoke(nameof(DisplayData), 0.25f);
    }

    private IEnumerator SpawnPrefabs(RacialEthnicGroup group, int count, int interval = 0)
    {
      // Get prefab
      GameObject prefab = prefabs.PrefabFromRacialEthnicGroup(group);

      // Get correct position
      Vector3 position = transform.position;
      float intervalLength = width / 5;

      position.x += -width / 2;
      position.x += interval * intervalLength;

      for (int i = 0; i < (count / 100000); i++)
      {
        // Slightly randomize position
        Vector3 uniquePosition = position;
        uniquePosition.x += (Random.value * 3.0f) - 1.5f;
        
        // Spawn object
        GameObject spawnedPrefab = Instantiate(
          prefab,
          uniquePosition,
          Quaternion.Euler(
            new Vector3(0, 0, Random.value * 360)
          )
        );
        
        _spawnedPrefabs.Add(spawnedPrefab);
        
        // Wait before spawning next
        yield return new WaitForSeconds(spawnInterval);
      }
    }

    private void DestroyAllSpawnedPrefabs()
    {
      // Destroy all spawned prefabs
      foreach (GameObject spawnedPrefab in _spawnedPrefabs)
      {
        Destroy(spawnedPrefab);
      }
      
      // Reset list
      _spawnedPrefabs = new List<GameObject>();
    }

    private void StopAllSpawnCoroutines()
    {
      // Stop all spawn coroutines
      foreach (IEnumerator coroutine in _spawnCoroutines)
      {
        if (coroutine != null)
        {
          StopCoroutine(coroutine);
        }
      }
      
      // Reset list
      _spawnCoroutines = new List<IEnumerator>();
    }
    
    private string AddSpaces(string text, bool preserveAcronyms)
    {
      if (string.IsNullOrWhiteSpace(text))
        return string.Empty;
      StringBuilder newText = new StringBuilder(text.Length * 2);
      newText.Append(text[0]);
      for (int i = 1; i < text.Length; i++)
      {
        if (char.IsUpper(text[i]))
          if ((text[i - 1] != ' ' && !char.IsUpper(text[i - 1])) ||
              (preserveAcronyms && char.IsUpper(text[i - 1]) && 
               i < text.Length - 1 && !char.IsUpper(text[i + 1])))
            newText.Append(' ');
        newText.Append(text[i]);
      }
      return newText.ToString();
    }
}
