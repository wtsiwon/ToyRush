using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingElementSpawner : Singleton<MovingElementSpawner>
{
    public Item[] items;

    [Tooltip("ObstacleCoinPattern Prefab")]
    public GameObject[] obstaclePatterns;

    public ObstacleData obstacleData;

    public bool isSpawn;

    public ECurrentSpawnType spawnType;

    [Tooltip("전에 소환된 패턴")]
    private GameObject beforeSpawnPattern;


    private void Start()
    {
        StartCoroutine(nameof(CUpdate));
        
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
                switch (spawnType)
                {
                    case ECurrentSpawnType.Obstacle:

                        break;
                }
            }
            //스폰 함수호출등
        }
    }

    private void GetRandomObstaclePattern()
    {
        int randPattern = Random.Range(0, obstaclePatterns.Length);


    }



    




}
