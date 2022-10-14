using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//인게임 움직이는 요소들
[RequireComponent(typeof(Rigidbody2D))]
public class MovingElement : PoolingObj
{

    protected Rigidbody2D rb;

    protected virtual void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.left * BackGroundSpawner.Instance.backgroundSpd;
    }

    
}
