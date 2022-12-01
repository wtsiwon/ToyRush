using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingElementSpawner : Singleton<MovingElementSpawner>
{
    [Header("ItemInfos")]
    [Tooltip("Items")]
    public Item[] items;

    [Header("ObstaclePattern prefabs")]
    [Tooltip("ObstacleCoinPattern Prefab")]
    public GameObject[] obstaclePatterns;

    [Tooltip("장애물 회전방향 값을 담는 Dictionary")]
    public Dictionary<EDir, Vector3> rotatesDic = new Dictionary<EDir, Vector3>();

    [Header("obstacleAnimationInfos")]
    [Tooltip("장애물 Animation Infos")]
    public List<Array<RuntimeAnimatorController>> obstacleAnimation = new List<Array<RuntimeAnimatorController>>();

    public bool isSpawn;

    public ECurrentSpawnType spawnType;

    [Tooltip("전에 소환된 패턴")]
    public GameObject beforeSpawnPattern;

    private Vector3 defaultPos = Vector3.zero;

    private void Start()
    {
        StartCoroutine(nameof(CUpdate));
        
    }

    private void AddRotates()
    {
        rotatesDic.Add(EDir.Up, new Vector3(0, 0, 0));
        rotatesDic.Add(EDir.Down, new Vector3(0, 0, 180));
        rotatesDic.Add(EDir.Left, new Vector3(0, 0, 90));
        rotatesDic.Add(EDir.Right, new Vector3(0, 0, -90));
        rotatesDic.Add(EDir.Cross1, new Vector3(0, 0, 45));
        rotatesDic.Add(EDir.Cross2, new Vector3(0, 0, 135));
    }

    /// <summary>
    /// 스포너
    /// </summary>
    /// <returns></returns>
    private IEnumerator CUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            
            if(isSpawn == true)
            {
                if(beforeSpawnPattern.transform.position.x <= defaultPos.x)
                {
                    if (beforeSpawnPattern == null) yield return null;

                    GetRandomObstaclePattern();
                }
            }
            //스폰 함수호출등
        }
    }

    private void GetRandomObstaclePattern()
    {
        int randPattern = Random.Range(0, obstaclePatterns.Length);
        GameObject obj = Instantiate(obstaclePatterns[randPattern]);
        obj.transform.position = transform.position;

        beforeSpawnPattern = obj;
    }

    private void GetRandomItem()
    {
        int randPattern = Random.Range(0, items.Length);

        Item item = Instantiate(items[randPattern]);
        item.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
    }
    
}
