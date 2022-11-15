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

    [Tooltip("회전하는 가")]
    private bool isSpin;

    public bool IsSpin
    {
        get => isSpin;
        set
        {
            isSpin = value;
            if (value == true)
            {
                Spin();
            }
        }
    }

    private Vector3 spawnPoint;

    private const float DISTANCE = 50f;

    protected override void Start()
    {
        base.Start();
        TypeDefine();
    }

    private void TypeDefine()
    {
        switch (obstacleType)
        {
            case EObstacleType.BlowFish:
                StartCoroutine(nameof(CBlowFishAnim));
                break;
            default:

                break;
        }
    }

    //나중에 좀 더 생각해서 해보자
    protected override void OnEnable()
    {
        spawnPoint = Player.Instance.transform.position;

        base.OnEnable();
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

    private IEnumerator CBlowFishAnim()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            transform.DOScale(0.4f, 0.4f);
            yield return new WaitForSeconds(0.5f);
            transform.DOScale(0.2f, 0.2f);
        }
    }

    private void Spin()
    {
        StartCoroutine(nameof(CSpin));
    }

    private IEnumerator CSpin()
    {
        while (true)
        {
            transform.Rotate(new Vector3(0, 0, spinSpd));
            yield return new WaitForSeconds(0.02f);
        }
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Player.Instance.IsBig == true)
        {
            SoundManager.instance.PlaySoundClip("Fragments", SoundType.SFX, 1f);

            Camera.main.transform.DOShakePosition(0.5f, new Vector2(0.2f, 0.2f));
            gameObject.GetComponent<SpriteRenderer>().DOFade(0, 0);
            GameObject piggybankDirector = Instantiate(ItemManager.inst.piggybankDirector, Vector2.zero, Quaternion.identity);
            piggybankDirector.transform.SetParent(gameObject.transform, false);
            piggybankDirector.transform.DOScale(new Vector2(2, 2), 0);

            yield return new WaitForSeconds(1);

            Destroy(gameObject);
        }
    }
}
