using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundSpawner : Singleton<BackGroundSpawner>
{
    [Tooltip("배경 속도")]
    public float backgroundSpd;

    private void Start()
    {
        SpawnBackGround();
    }

    public void SpawnBackGround()
    {
        ObjPool.Instance.Get(EPoolType.BackGround, transform.position);
        print(transform.position);
    }
}
