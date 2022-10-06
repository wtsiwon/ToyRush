using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundSpawner : Singleton<BackGroundSpawner>
{
    //[Tooltip("현재 보여지고 있는 배경을 저장하는 queue")]
    //public Queue<BackGround> backgroundQueue = new Queue<BackGround>();

    [Tooltip("배경 속도")]
    public float backgroundSpd;

    private void Start()
    {
        SpawnBackGround();
    }

    public void SpawnBackGround()
    {
        int rand = Random.Range(0, (int)EPoolType.ShippingBack2 + 1);

        ObjPool.Instance.Get((EPoolType)rand, transform.position);
    }
}
