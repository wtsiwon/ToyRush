using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Runtime.CompilerServices;

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

    private Animator animator;

    [Tooltip("장애물이 파괴될 X좌표")]
    public float destroyPointx;

    private const float DISTANCE = 50f;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();

        StartCoroutine(nameof(Wait));
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.05f);
        TypeDefine();
    }

    private void TypeDefine()
    {
        switch (obstacleType)
        {
            case EObstacleType.BlowFish:
                StartCoroutine(nameof(CBlowFishAnim));
                break;
            case EObstacleType.Gear:
                SetGearObstacle();
                RandColor(obstacleType);
                break;
            case EObstacleType.Drill:
                SetDrillObstacle();
                RandColor(obstacleType);
                break;
            case EObstacleType.Fist:

                break;
            default:
                Debug.Assert(false, "없는 Type입니다");
                break;
        }
    }

    /// <summary>
    /// SetGearObstacle
    /// </summary>
    private void SetGearObstacle()
    {
        IsSpin = Random.Range(0, 2) == 1;

        if (IsSpin == false)
        {
            RandRotate();
        }
    }

    /// <summary>
    /// SetDrillObstacle
    /// </summary>
    private void SetDrillObstacle()
    {
        RandRotate();
    }

    /// <summary>
    /// 현재 장애물에 랜덤 Rotation적용
    /// </summary>
    private void RandRotate()
    {
        int randRotate = Random.Range(0, (int)EDir.End);
        Vector3 rotate = MovingElementSpawner.Instance.rotatesDic[(EDir)randRotate];

        transform.rotation = Quaternion.Euler(rotate);
    }

    /// <summary>
    /// 장애물 랜덤Color
    /// </summary>
    /// <param name="type"></param>
    private void RandColor(EObstacleType type)
    {
        int randColor = Random.Range(0, (int)EObstacleColorType.End);
        RuntimeAnimatorController animatorController 
            = MovingElementSpawner.Instance.obstacleAnimation[(int)type].list[randColor];

        animator.runtimeAnimatorController = animatorController;
    }

    //나중에 좀 더 생각해서 해보자
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Update()
    {
        base.Update();
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
        if (collision.CompareTag("Player"))
        {
            if (Player.Instance.IsBoosting == true)
            {
                SoundManager.instance.PlaySoundClip("Fragments", SoundType.SFX, 1f);
                gameObject.GetComponent<SpriteRenderer>().DOFade(0, 0);

                GameObject piggybankDirector = Instantiate(ItemManager.inst.piggybankDirector, Vector2.zero, Quaternion.identity);
                piggybankDirector.transform.SetParent(gameObject.transform, false);
                piggybankDirector.transform.DOScale(new Vector2(2, 2), 0);
            }
            else if (Player.Instance.IsBig == true)
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

    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Player.Instance.IsBoosting == true)
            {
                SoundManager.instance.PlaySoundClip("Fragments", SoundType.SFX, 1f);
                gameObject.GetComponent<SpriteRenderer>().DOFade(0, 0);

                GameObject piggybankDirector = Instantiate(ItemManager.inst.piggybankDirector, Vector2.zero, Quaternion.identity);
                piggybankDirector.transform.SetParent(gameObject.transform, false);
                piggybankDirector.transform.DOScale(new Vector2(2, 2), 0);
            }
            else if (Player.Instance.IsBig == true)
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
}
