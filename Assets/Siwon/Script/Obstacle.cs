using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MovingElement
{
    public EObstacleType obstacleType;


    protected override void OnEnable()
    {
        base.OnEnable();
        //obstacleType = GetComponent<EObstacleType>();
        switch (obstacleType)
        {
            case EObstacleType.Basic:
                
                break;
        }
    }


}
