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
    public List<Action> obstaclePattern = new List<Action>();


    private void Start()
    {
        obstaclePattern[1] = SpawnPattern1;
    }

    private void SpawnPattern1()
    {
        StartCoroutine(CSpawnPattern1());
    }

    private IEnumerator CSpawnPattern1()
    {
        yield return new WaitForSeconds(1f);
        Obstacle obstacle1 = GetBasicObstacle(spawnPoses[1]);
        obstacle1.transform.rotation = new Quaternion(0, 0, 90, 0);


        yield return new WaitForSeconds(1f);
        Obstacle obstacle2 = GetSwingObstacle(spawnPoses[5]);


    }

    private Obstacle GetBasicObstacle(Transform pos)
    {
        Obstacle obstacle = null;
        obstacle = ObjPool.Instance.GetObstacle(EObstacleType.Basic, pos.position);
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
        return obstacle;
    }





}
