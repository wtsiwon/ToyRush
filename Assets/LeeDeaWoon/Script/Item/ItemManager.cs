using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager inst;
    private void Awake() => inst = this;

    public List<GameObject> itemList = new List<GameObject>();
    public float spawnInterval;

    public const int spawnXValue = 11;
    public const int spawnYValue = 2;

    public float spawnValue = 12.5f;

    public bool isItemSummon;

    void Start()
    {
        StartCoroutine(Item_Spawn());
    }

    void Update()
    {
    }

    IEnumerator Item_Spawn()
    {
        while (true)
        {
            if (GameManager.Instance.IsGameStart == true)
            {
                yield return new WaitForSeconds(spawnValue + Random.Range(-spawnInterval, spawnInterval));
                Instantiate(itemList[Random.Range(0, itemList.Count)], new Vector2(spawnXValue, spawnYValue), Quaternion.identity).transform.parent = gameObject.transform;
            }
        }
    }
}
