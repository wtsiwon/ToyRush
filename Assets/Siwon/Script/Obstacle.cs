using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MovingElement
{
    public EObstacleType obstacleType;

    [Range(0f, 5f)]
    [Tooltip("돌아가는 속도")]
    public float spinSpd;

    [Tooltip("최대 각도")]
    public float maxAngle;

    [Tooltip("최소 각도")]
    public float minAngle;

    [Tooltip("SwingSpd")]
    private const float swingSpd = 100; 

    //나중에 좀 더 생각해서 해보자
    protected override void OnEnable()
    {
        //base.OnEnable();
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
                //Swing();
                break;
            case EObstacleType.Spin:
                Spin();
                break;
        }
    }

    private void Update()
    {
        if(obstacleType == EObstacleType.Swing)
        {
            Swing();
        }
    }
    private void Swing()
    {
        //왔다 갔다 하는 코드
        if (Mathf.Abs(transform.rotation.z) >= 50)
        {
            transform.Rotate(new Vector3(0, 0, swingSpd * -1));
        }
    }

    private IEnumerator CSwing()
    {
        yield return null;
    }

    private void Spin()
    {
        StartCoroutine(CSpin());
    }

    private IEnumerator CSpin()
    {
        transform.Rotate(new Vector3(0, 0, spinSpd));
        yield return new WaitForSeconds(0.02f);
        StartCoroutine(CSpin());
    }


}
