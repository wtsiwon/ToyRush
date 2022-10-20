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
        GetBasicObstacle(spawnPoses[1]);

        yield return null;
    }

    private Obstacle GetBasicObstacle(Transform pos)
    {
        Obstacle obstacle = null;
        obstacle = ObjPool.Instance.GetObstacle(EObstacleType.Basic, pos.position);
        return obstacle;
    }

    private Obstacle GetSwingObstacle(Transform pos)
    {
        Obstacle obstacle = null;
        obstacle = ObjPool.Instance.GetObstacle(EObstacleType.Swing, pos.position);
        return obstacle;
    }

    private Obstacle GetSpinObstacle(Transform pos)
    {
        Obstacle obstacle = null;
        obstacle = ObjPool.Instance.GetObstacle(EObstacleType.Spin, pos.position);
        return obstacle;
    }





}
