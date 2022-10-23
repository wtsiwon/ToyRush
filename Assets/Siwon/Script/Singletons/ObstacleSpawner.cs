using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObstacleSpawner : Singleton<ObstacleSpawner>
{
    [SerializeField]
    [Tooltip("장애물을 소환할 위치들")]
    private List<Transform> spawnPoses = new List<Transform>();

    [Tooltip("장애물 패턴 함수들")]
    public List<Action> obstaclePatterns = new List<Action>();

    [Tooltip("방향 값을 담는 Dictionary")]
    private Dictionary<EDir, Quaternion> rotatesDic = new Dictionary<EDir, Quaternion>();

    [Tooltip("장애물 Sprite를 enum타입에 따라 분류한 dictionary")]
    private Dictionary<EObstacleType, Sprite> obstacleSpriteDic = new Dictionary<EObstacleType, Sprite>();

    [Tooltip("장애물 Sprite들List")]
    [Space(15f)]
    public List<Sprite> obstacleSpriteList = new List<Sprite>();

    private void Start()
    {
        AddRotates();
        AddObstaclePattern();
        AddObstacleSprite();
    }
    /// <summary>
    /// 장애물 SpriteDictionary에 추가
    /// </summary>
    private void AddObstacleSprite()
    {
        for (int i = 0; i < (int)EObstacleType.End; i++)
        {
            obstacleSpriteDic.Add((EObstacleType)i, obstacleSpriteList[i]);
        }
    }

    private void AddRotates()
    {
        rotatesDic.Add(EDir.Up, new Quaternion(0, 0, (float)EDir.Up, 0));
        rotatesDic.Add(EDir.Down, new Quaternion(0, 0, (float)EDir.Down, 0));
        rotatesDic.Add(EDir.Left, new Quaternion(0, 0, (float)EDir.Left, 0));
        rotatesDic.Add(EDir.Right, new Quaternion(0, 0, (float)EDir.Right, 0));
    }

    /// <summary>
    /// Action에 장애물 패턴 함수 추가하는 함수
    /// </summary>
    private void AddObstaclePattern()
    {
        obstaclePatterns.Add(SpawnPattern1);
    }

    #region 패턴 함수들

    private void SpawnPattern1()
    {
        StartCoroutine(CSpawnPattern1());
    }

    private void SpawnPattern2()
    {
        StartCoroutine(CSpawnPattern2());
    }

    private void SpawnPattern3()
    {
        StartCoroutine(CSpawnPattern3());
    }

    private void SpawnPattern4()
    {
        //StartCoroutine(CSpawnPattern3());
    }

    private IEnumerator CSpawnPattern1()
    {
        yield return new WaitForSeconds(1f);
        Obstacle obstacle1 = GetBasicObstacle(spawnPoses[1]);
        obstacle1.transform.rotation = rotatesDic[EDir.Up];


        yield return new WaitForSeconds(1f);
        Obstacle obstacle2 = GetSwingObstacle(spawnPoses[5]);
    }

    private IEnumerator CSpawnPattern2()
    {
        yield return new WaitForSeconds(0.5f);
        Obstacle obstacle1 = GetSpinObstacle(spawnPoses[2]);

        yield return new WaitForSeconds(0.5f);
        Obstacle obstacle2 = GetBasicObstacle(spawnPoses[2]);

        yield return new WaitForSeconds(1f);
        Obstacle obstacle3 = GetBasicObstacle(spawnPoses[4]);
        obstacle3.transform.rotation = rotatesDic[EDir.Down];
    }

    private IEnumerator CSpawnPattern3()
    {
        yield return new WaitForSeconds(0.7f);
        Obstacle obstacle1 = GetSwingObstacle(spawnPoses[5]);

        yield return new WaitForSeconds(0.5f);
        Obstacle obstacle2 = GetSpinObstacle(spawnPoses[1]);

        yield return new WaitForSeconds(0.8f);
        Obstacle obstacle3 = GetSwingObstacle(spawnPoses[1]);
    }

    private IEnumerator CSpawnPattern4()
    {
        yield return new WaitForSeconds(1f);
        Obstacle obstacle1 = GetBasicObstacle(spawnPoses[2]);
        obstacle1.transform.rotation = rotatesDic[EDir.Left];

        yield return new WaitForSeconds(0.5f);
        Obstacle obstacle2 = GetSwingObstacle(spawnPoses[1]);
        obstacle2.transform.rotation = rotatesDic[EDir.Down];

    }


    private IEnumerator CSpawnPattern5()
    {
        yield return null;
    }
    #endregion

    private Obstacle GetBasicObstacle(Transform pos)
    {
        Obstacle obstacle = null;
        obstacle = ObjPool.Instance.GetObstacle(EObstacleType.Basic, pos.position);
        obstacle.spriterenderer.sprite = obstacleSpriteDic[EObstacleType.Basic];
        return obstacle;
    }

    /// <summary>
    /// Swing
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    private Obstacle GetSwingObstacle(Transform pos)
    {
        Obstacle obstacle = null;
        obstacle = ObjPool.Instance.GetObstacle(EObstacleType.Swing, pos.position);
        obstacle.spriterenderer.sprite = obstacleSpriteDic[EObstacleType.Swing];
        return obstacle;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    private Obstacle GetSpinObstacle(Transform pos)
    {
        Obstacle obstacle = null;
        obstacle = ObjPool.Instance.GetObstacle(EObstacleType.Spin, pos.position);
        obstacle.spriterenderer.sprite = obstacleSpriteDic[EObstacleType.Spin];
        return obstacle;
    }





}
