using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : Singleton<CoinSpawner>
{
    public List<GameObject> coinPatternList = new List<GameObject>();


    public void SpawnCoinPattern()
    { 
        GameObject coin = Instantiate(coinPatternList[Random.Range(0,4)], ObstacleSpawner.Instance.spawnPoses[Random.Range(1,3)]);
        coin.GetComponent<Rigidbody2D>().velocity = Vector3.left * 
            BackGroundSpawner.Instance.backgroundSpd * Time.deltaTime;
    }
}
