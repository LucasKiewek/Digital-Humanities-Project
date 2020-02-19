using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_Text))]
public class YearLabel : MonoBehaviour
{
    private TMP_Text _text;
    
    // Year label also changes the year for every level
    private List<Level> _levels;

    // Event function
    void Start()
    {
        _text = GetComponent<TMP_Text>();
        _levels = GameObject.FindObjectsOfType<Level>().ToList();

        _text.text = LoadData.DEFAULT_YEAR.ToString();
    }

    public void SetYear(Slider slider)
    {
        int[] availableYears = Enumerable.Range(LoadData.MIN_YEAR, (LoadData.MAX_YEAR - LoadData.MIN_YEAR) + 1).ToArray();
        int index = ((int) (slider.value * availableYears.Length)) - 1;
        
        // Clamp index
        index = Mathf.Clamp(index, 0, availableYears.Length);

        int year = availableYears[index];

        _text.text = year.ToString();

        foreach (Level level in _levels)
        {
            level.ChangeYear(year);
        }
    }
}
