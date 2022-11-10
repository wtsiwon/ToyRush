using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    private Vector3 spawnPoint;

    private const float DISTANCE = 50f;

    protected override void Start()
    {
        base.Start();
    }

    //나중에 좀 더 생각해서 해보자
    protected override void OnEnable()
    {
        spawnPoint = Player.Instance.transform.position;

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
            case EObstacleType.Spin:
                Spin();
                break;
        }
    }

    protected override void Update()
    {
        base.Update();
        if (spawnPoint.x - transform.position.x > DISTANCE)
        {
            base.Return();
        }


    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
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

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Player.Instance.IsBig == true)
        {
            gameObject.GetComponent<SpriteRenderer>().DOFade(0, 0);
            Instantiate(ItemManager.inst.piggybankDirector, Vector2.zero, Quaternion.identity).transform.SetParent(gameObject.transform, false);

            yield return new WaitForSeconds(1);

            Destroy(gameObject);
        }
    }
}
