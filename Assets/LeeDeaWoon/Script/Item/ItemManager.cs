using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<GameObject> itemList = new List<GameObject>();
    public float[] spawnTime = new float[2];

    public int spawnXValue;
    public int spawnYValue;

    void Start()
    {
    }

    void Update()
    {
        StartCoroutine("Item_Spawn");
    }

    IEnumerator Item_Spawn()
    {
        float spawnValue = 12.5f;

        yield return new WaitForSeconds(spawnValue + Random.Range(spawnTime[0], spawnTime[1]));
        Instantiate(itemList[Random.Range(0, itemList.Count)], new Vector2(spawnXValue, spawnYValue), Quaternion.identity).transform.parent = gameObject.transform;
    }
}
