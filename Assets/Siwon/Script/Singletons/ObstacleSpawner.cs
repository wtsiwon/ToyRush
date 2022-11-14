using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

using System;

public class ObstacleSpawner : Singleton<ObstacleSpawner>
{
    [Tooltip("장애물을 소환할 위치들")]
    public List<Transform> spawnPoses = new List<Transform>();

    [Tooltip("방향 값을 담는 Dictionary")]
    private Dictionary<EDir, Quaternion> rotatesDic = new Dictionary<EDir, Quaternion>();

    [Tooltip("장애물 Sprite를 enum타입에 따라 분류한 dictionary")]
    private Dictionary<EObstacleType, Sprite> obstacleSpriteDic = new Dictionary<EObstacleType, Sprite>();

    [Tooltip("장애물 List")]
    [Space(15f)]
    public List<Obstacle> obstacleList = new List<Obstacle>();

    [Tooltip("코인 패턴들")]
    public List<GameObject> coinPatterns = new List<GameObject>();

    [Tooltip("장애물 소환 간격")]
    public float obstacleSpawnInterval;

    public float obstacleSpawnDelay;

    public bool canSpawn;

    private void Start()
    {
        AddRotates();
        AddObstacleSprite();
        StartCoroutine(SpawnObstacle());
    }

    private IEnumerator SpawnObstacle()
    {
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            if (canSpawn && GameManager.Instance.IsGameStart == true)
            {
                int obstacleRand = Random.Range(1, 16);
                SpawnObstaclePattern(obstacleRand);

                yield return new WaitForSeconds(obstacleSpawnDelay
                    + Random.Range(-obstacleSpawnInterval, obstacleSpawnInterval));
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    /// <summary>
    /// 장애물 SpriteDictionary에 추가
    /// </summary>
    private void AddObstacleSprite()
    {
        for (int i = 0; i < (int)EObstacleType.End; i++)
        {
            //obstacleSpriteDic.Add((EObstacleType)i, obstacleSpriteList[i]);
        }
    }

    private void AddRotates()
    {
        rotatesDic.Add(EDir.Up, new Quaternion(0, 0, (float)EDir.Up, 0));
        rotatesDic.Add(EDir.Down, new Quaternion(0, 0, (float)EDir.Down, 0));
        rotatesDic.Add(EDir.Left, new Quaternion(0, 0, (float)EDir.Left, 0));
        rotatesDic.Add(EDir.Right, new Quaternion(0, 0, (float)EDir.Right, 0));
        rotatesDic.Add(EDir.Cross1, new Quaternion(0, 0, (float)EDir.Cross1, 0));
        rotatesDic.Add(EDir.Cross2, new Quaternion(0, 0, (float)EDir.Cross2, 0));
    }

    /// <summary>
    /// 패턴소환!
    /// </summary>
    /// <param name="index"></param>
    public void SpawnObstaclePattern(int index)
    {
        StartCoroutine($"CSpawnPattern{index}");
    }

    public void SpawnCoinPattern(int index)
    {
        StartCoroutine($"CCoinPattern{index}");
    }

    #region 장애물 패턴 함수들
    private IEnumerator CSpawnPattern1()
    {
        Obstacle obstacle1 = GetGearObstacle(spawnPoses[1]);
        obstacle1.transform.rotation = rotatesDic[EDir.Up];
        yield return new WaitForSeconds(1.2f);

        Obstacle obstacle2 = GetDrillObstacle(spawnPoses[4]);
        yield return new WaitForSeconds(1.2f);
        CoinSpawner.Instance.SpawnCoinPattern();
        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator CSpawnPattern2()
    {
        Obstacle obstacle1 = GetDrillObstacle(spawnPoses[2]);
        yield return new WaitForSeconds(1.2f);

        Obstacle obstacle2 = GetGearObstacle(spawnPoses[2]);
        yield return new WaitForSeconds(1.2f);

        Obstacle obstacle3 = GetGearObstacle(spawnPoses[4]);
        obstacle3.transform.rotation = rotatesDic[EDir.Down];
        yield return new WaitForSeconds(1.1f);
    }

    private IEnumerator CSpawnPattern3()
    {
        Obstacle obstacle1 = GetGearObstacle(spawnPoses[4]);
        transform.rotation = rotatesDic[EDir.Up];
        yield return new WaitForSeconds(1.3f);

        Obstacle obstacle2 = GetDrillObstacle(spawnPoses[1]);
        yield return new WaitForSeconds(1.2f);
    }

    private IEnumerator CSpawnPattern4()
    {
        Obstacle obstacle1 = GetGearObstacle(spawnPoses[2]);
        obstacle1.transform.rotation = rotatesDic[EDir.Left];
        yield return new WaitForSeconds(1f);

        Obstacle obstacle2 = GetDrillObstacle(spawnPoses[1]);
        yield return new WaitForSeconds(1.2f);

    }

    private IEnumerator CSpawnPattern5()
    {
        Obstacle obstacle1 = GetDrillObstacle(spawnPoses[0]);
        yield return new WaitForSeconds(1.2f);

        Obstacle obstacle2 = GetGearObstacle(spawnPoses[4]);
        obstacle2.transform.rotation = rotatesDic[EDir.Left];
        yield return new WaitForSeconds(1.3f);
    }

    private IEnumerator CSpawnPattern6()
    {
        Obstacle obstacle1 = GetGearObstacle(spawnPoses[2]);
        yield return new WaitForSeconds(1.2f);

        CoinSpawner.Instance.SpawnCoinPattern();
        yield return new WaitForSeconds(1f);

        Obstacle obstacle2 = GetDrillObstacle(spawnPoses[4]);
        yield return new WaitForSeconds(1.2f);
    }

    private IEnumerator CSpawnPattern7()
    {
        Obstacle obstacle1 = GetDrillObstacle(spawnPoses[0]);
        yield return new WaitForSeconds(1.4f);

        Obstacle obstacle2 = GetGearObstacle(spawnPoses[4]);
        obstacle2.transform.rotation = rotatesDic[EDir.Left];
        yield return new WaitForSeconds(1.1f);

        CoinSpawner.Instance.SpawnCoinPattern();
        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator CSpawnPattern8()
    {
        Obstacle obstacle1 = GetGearObstacle(spawnPoses[1]);
        obstacle1.transform.rotation = rotatesDic[EDir.Right];
        yield return new WaitForSeconds(1.3f);

        Obstacle obstacle2 = GetDrillObstacle(spawnPoses[3]);
        yield return new WaitForSeconds(1.2f);
    }

    private IEnumerator CSpawnPattern9()
    {
        Obstacle obstacle1 = GetDrillObstacle(spawnPoses[4]);
        yield return new WaitForSeconds(1.5f);

        Obstacle obstacle2 = GetGearObstacle(spawnPoses[1]);
        obstacle2.transform.rotation = rotatesDic[EDir.Down];
        yield return new WaitForSeconds(1.4f);
    }

    private IEnumerator CSpawnPattern10()
    {
        CoinSpawner.Instance.SpawnCoinPattern();
        yield return new WaitForSeconds(0.8f);

        Obstacle obstacle1 = GetDrillObstacle(spawnPoses[3]);
        yield return new WaitForSeconds(1.3f);

        Obstacle obstacle2 = GetGearObstacle(spawnPoses[1]);
        obstacle2.transform.rotation = rotatesDic[EDir.Left];
        yield return new WaitForSeconds(1.4f);
    }

    private IEnumerator CSpawnPattern11()
    {
        Obstacle obstacle2 = GetGearObstacle(spawnPoses[4]);
        obstacle2.transform.rotation = rotatesDic[EDir.Right];
        yield return new WaitForSeconds(1.2f);

        CoinSpawner.Instance.SpawnCoinPattern();
        yield return new WaitForSeconds(0.5f);

        Obstacle obstacle3 = GetDrillObstacle(spawnPoses[1]);
        yield return new WaitForSeconds(1.3f);
    }

    private IEnumerator CSpawnPattern12()
    {
        Obstacle obstacle1 = GetGearObstacle(spawnPoses[2]);
        yield return new WaitForSeconds(1.2f);

        Obstacle obstacle2 = GetDrillObstacle(spawnPoses[4]);
        yield return new WaitForSeconds(1.5f);
    }

    private IEnumerator CSpawnPattern13()
    {
        Obstacle obstacle1 = GetDrillObstacle(spawnPoses[3]);
        yield return new WaitForSeconds(1.5f);

        CoinSpawner.Instance.SpawnCoinPattern();
        yield return new WaitForSeconds(0.5f);

        Obstacle obstacle2 = GetGearObstacle(spawnPoses[0]);
        yield return new WaitForSeconds(1.2f);
    }

    private IEnumerator CSpawnPattern14()
    {
        Obstacle obstacle1 = GetGearObstacle(spawnPoses[1]);
        obstacle1.transform.rotation = rotatesDic[EDir.Left];
        yield return new WaitForSeconds(1.2f);

        Obstacle obstacle2 = GetDrillObstacle(spawnPoses[4]);
        yield return new WaitForSeconds(1.1f);

        CoinSpawner.Instance.SpawnCoinPattern();
        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator CSpawnPattern15()
    {
        Obstacle obstacle1 = GetDrillObstacle(spawnPoses[2]);
        yield return new WaitForSeconds(1.5f);

        Obstacle obstacle = GetGearObstacle(spawnPoses[3]);
        yield return new WaitForSeconds(1.3f);

        CoinSpawner.Instance.SpawnCoinPattern();
        yield return new WaitForSeconds(0.8f);
    }

    //20까지
    #endregion

    #region 장애물 불러오는 함수
    private Obstacle GetGearObstacle(Transform pos)
    {
        Obstacle obstacle = null;
        obstacle = Instantiate(obstacleList[(int)EObstacleType.Gear]);
        obstacle.transform.position = pos.position;

        if (obstacle.GetComponent<PolygonCollider2D>() == null)
        {
            obstacle.gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;
        }
        return obstacle;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    private Obstacle GetDrillObstacle(Transform pos)
    {
        Obstacle obstacle = null;
        obstacle = Instantiate(obstacleList[(int)EObstacleType.Drill]);
        obstacle.transform.position = pos.position;

        if (obstacle.GetComponent<PolygonCollider2D>() == null)
        {
            obstacle.gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;
        }
        return obstacle;
    }
    #endregion

    private IEnumerator CCoinPattern1()
    {
        Instantiate(coinPatterns[0], spawnPoses[2]);
        coinPatterns[0].GetComponent<Rigidbody2D>().velocity
            = Vector2.left * BackGroundSpawner.Instance.backgroundSpd * Time.deltaTime;
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator CCoinPattern2()
    {
        Instantiate(coinPatterns[1], spawnPoses[1].position, Quaternion.identity);
        coinPatterns[1].GetComponent<Rigidbody2D>().velocity
            = Vector2.left * BackGroundSpawner.Instance.backgroundSpd * Time.deltaTime;
        yield return new WaitForSeconds(1f);

    }

}
