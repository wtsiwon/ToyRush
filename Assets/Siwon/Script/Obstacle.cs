using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MovingElement
{
    public EObstacleType obstacleType;


    protected override void OnEnable()
    {
        base.OnEnable();
        TypeofDefine();
    }

    /// <summary>
    /// enum타입의 따른 정의
    /// obstacleType에 따라 장애물 
    /// </summary>
    private void TypeofDefine()
    {
        switch (obstacleType)
        {
            case EObstacleType.Basic:
                //없음
                break;
            case EObstacleType.Swing:
                Swing();
                break;
            case EObstacleType.Spin:
                Spin();
                break;
        }
    }

    private void Swing()
    {

    }

    private void Spin()
    {
        StartCoroutine(ISpin());
    }

    private IEnumerator ISpin()
    {
        transform.rotation = new Quaternion(0, 0, 1, 0);
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(ISpin());
    }


}
