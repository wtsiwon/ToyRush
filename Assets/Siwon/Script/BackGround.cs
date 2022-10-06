using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class BackGround : PoolingObj
{
    private const float SCROLLXPOS = -21f;

    private Rigidbody2D rb;

    private void Update()
    {
        //거리비례 속도조절
        if(transform.position.x < SCROLLXPOS)
        {
            Return();
        }
        MoveBackGround();
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void MoveBackGround()
    {
        rb.velocity = Vector3.left * BackGroundSpawner.Instance.backgroundSpd;
    }

    public override void Return()
    {
        base.Return();
    }

    private void OnDisable()
    {
        BackGroundSpawner.Instance.SpawnBackGround();
    }
}
