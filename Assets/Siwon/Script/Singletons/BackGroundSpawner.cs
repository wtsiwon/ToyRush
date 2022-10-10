using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundSpawner : Singleton<BackGroundSpawner>
{

    [Tooltip("현재 있는 배경들을 담는다")]
    public List<BackGround> backgroundList = new List<BackGround>();

    [Tooltip("배경 속도")]
    public float backgroundSpd;

    private void Start()
    {
        SpawnBackGround();
    }

    public void SpawnBackGround()
    {
        ObjPool.Instance.Get(EPoolType.BackGround, transform.position);
    }
}
