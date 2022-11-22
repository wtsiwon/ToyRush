using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Obstacle : MovingElement
{
    [SerializeField]
    private EObstacleType obstacleType;
    public EObstacleType ObstacleType
    {
        get
        {
            return obstacleType;
        }
        set
        {
            obstacleType = value;
            switch (value)
            {
                case EObstacleType.Gear:

                    break;
            }
            
        }
    }

    [Range(0f, 5f)]
    [Tooltip("주먹 장애물 돌아가는 속도")]
    public float spinSpd;

    [SerializeField]
    [Tooltip("회전하는 가")]
    private bool isSpin;
    
    public bool IsSpin
    {
        get
        {
            if (isSpin == true)
            {
                StartCoroutine(nameof(CSpin));
            }
            return isSpin;
        }
        set
        {
            isSpin = value;
            
        }
    }

    private Vector3 spawnPoint;

    private const float DISTANCE = 50f;

    private DOTween domove;

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
            transform.DOScale(0.35f, 0.35f).SetEase(Ease.InCubic);
            yield return new WaitForSeconds(0.5f);
            transform.DOScale(0.25f, 0.25f).SetEase(Ease.OutCubic);
        }
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

            transform.DOKill();
            Destroy(gameObject);
        }
    }
}
