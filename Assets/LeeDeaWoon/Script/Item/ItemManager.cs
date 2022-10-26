using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<GameObject> itemList = new List<GameObject>();
    public float spawnInterval;

    public const int spawnXValue = 11;
    public const int spawnYValue = 2;

    public float spawnValue = 12.5f;
    void Start()
    {
    }

    void Update()
    {
        StartCoroutine(Item_Spawn());
    }

    IEnumerator Item_Spawn()
    {
        yield return new WaitForSeconds(spawnValue + Random.Range(-spawnInterval, spawnInterval));
        Instantiate(itemList[Random.Range(0, itemList.Count)], new Vector2(spawnXValue, spawnYValue), Quaternion.identity).transform.parent = gameObject.transform;
    }
}
