using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataPanel : MonoBehaviour
{
    public TMP_Text LevelLabel;

    [System.Serializable]
    public class RacialGroupInfoLabels
    {
        public TMP_Text Count;
        public TMP_Text Percent;
    }
      
    public RacialGroupInfoLabels BlackMenLabels;
    public RacialGroupInfoLabels WhiteMenLabels;
    public RacialGroupInfoLabels BlackWomenLabels;
    public RacialGroupInfoLabels WhiteWomenLabels;

    public RacialGroupInfoLabels GroupLabelFromRacialEthnicGroup(RacialEthnicGroup group)
    {
        switch (group)
        {
            case RacialEthnicGroup.BlackMan:
                return BlackMenLabels;
            case RacialEthnicGroup.WhiteMan:
                return WhiteMenLabels;
            case RacialEthnicGroup.BlackWoman:
                return BlackWomenLabels;
            case RacialEthnicGroup.WhiteWoman:
                return WhiteWomenLabels;
            default:
                return BlackWomenLabels;
        }
    }
}