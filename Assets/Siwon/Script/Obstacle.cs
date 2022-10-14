using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Obstacle : PoolingObj
{
    public EObstacleType obstacleType;

    private Rigidbody2D rb;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        if(obstacleType == EObstacleType.Basic)
        {
            rb.velocity = Vector3.left * BackGroundSpawner.Instance.backgroundSpd;
        }
    }


}
