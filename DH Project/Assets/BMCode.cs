using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BMCode : MonoBehaviour
{

  public List<LoadData> types = new List<LoadData>();
  public GameObject BlackManPrefab;


  // Start is called before the first frame update
  void Start()
  {
    addBlackMen(3);
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void addBlackMen(int amount)
  {
    for(int i =0; i < amount; i++){
        Instantiate(BlackManPrefab, Vector3.zero, Quaternion.identity); ///will spawn amount of times given
    }
  }
}
