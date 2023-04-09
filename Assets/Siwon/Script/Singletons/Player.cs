using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Player : Singleton<Player>
{
    #region Condition
    private bool isBoosting;
    public bool IsBoosting
    {
        get
        {
            return isBoosting;
        }
        set
        {
            isBoosting = value;
            if (value == true)
            {
                StartCoroutine(nameof(CWaitChangeBoosterSpd));
            }
            else
            {

            }
        }
    }
    [SerializeField]
    private bool isMagneting;
    public bool IsMagneting
    {
        get => isMagneting;
        set
        {
            isMagneting = value;

            //자석 가젯이 장착중이라면
            if (GadgetManager.Instance.CheckGadgetSlot(EGadgetType.Magnet) == true)
            {
                value = true;
            }

            if (value == true)
            {
                magnetRange.gameObject.SetActive(true);
            }
            else
            {
                magnetRange.gameObject.SetActive(false);
            }
        }
    }
    public bool IsBig { get; set; }
    private bool shouldObstacleBreak;

    public EBoosterType boosterType;
    #endregion

    #region GadgetStat
    [Space(10f)]
    public float gravitybeltGravity;
    public float attackPatternSpd;
    public bool isUseXray;
    #endregion

    #region VehicleAnimationConroller
    [Header("Animation")]
    public RuntimeAnimatorController characterAnimator;

    public RuntimeAnimatorController birdAnimator;

    public RuntimeAnimatorController gravityAnimator;

    public RuntimeAnimatorController frogAnimator;

    public RuntimeAnimatorController wyvernAnimator;

    public RuntimeAnimatorController ufoAnimator;
    #endregion

    #region MaxPos
    [Space(10f)]
    public float maxYpos;
    #endregion

    public bool isUseItem =>
     IsBig || isBoosting;

    #region 변수s
    [Tooltip("바닥에 닿아 있는가")]
    [SerializeField]
    private bool isGround;
    public bool IsGround
    {
        get
        {
            return isGround;
        }
        set
        {
            isGround = value;

            
        }
    }

    [SerializeField]
    private bool isCelling;
    
    public bool IsCelling
    {
        get
        {
            return isCelling;
        }
        set
        {
            isCelling = value;
        }
    }

    public float boostingSpd;

    public float gravityBeltGravityScale;
    public float defaultGravityScale;

    private bool isDie;
    public bool IsDie
    {
        get => isDie;

        set
        {
            isDie = value;
            if (isDie == true)
            {
                if (SceneManager.GetActiveScene().name == "Main")
                    GameManager.Instance.OnDie(transform);

                else
                    Camera.main.transform.DOShakePosition(2.5f, new Vector2(0.3f, 0.3f));
            }
        }
    }

    [SerializeField]
    private float hp;

    public float Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;

            if (value <= 0)
            {
                isDie = true;
            }
            else
            {

            }
        }
    }
    #endregion

    #region 탈것 변수
    [Tooltip("탈것의 종류")]
    private EVehicleType vehicleType;
    public EVehicleType VehicleType
    {
        get
        {
            return vehicleType;
        }
        set
        {
            vehicleType = value;

            ChangeVehicle();
        }

    }

    [SerializeField]
    private Vehicle vehicle;

    #endregion

    [Tooltip("힘")]
    [Range(100, 5000)]
    public float force;

    #region GetComponent한 Component
    public Rigidbody2D rb;

    [HideInInspector]
    public SpriteRenderer spriterenderer;

    [SerializeField]
    private MagnetRange magnetRange;

    #endregion

    [Tooltip("눌렀음?")]
    public bool isPressing;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(nameof(CUpdate));

        IsDie = false;
    }

    private void Update()
    {
        CurrentVehicleKeyInput(vehicleType);
    }

    /// <summary>
    /// 플레이어 움직임 제한하는 함수
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    private Vector3 RestrictMovePos(Vector3 pos)
    {
        return new Vector3
            (pos.x
            , Mathf.Clamp(pos.y, -maxYpos, maxYpos)
            , 0);
    }

    private IEnumerator CUpdate()
    {
        while (true)
            yield return new WaitForSeconds(1f);
    }

    public float shakeDuration;

    public void OnDamaged(float dmg)
    {
        GameManager.Instance.Cam.transform.DOShakePosition(shakeDuration, 2);
        Hp -= dmg;
    }

    private IEnumerator CWaitChangeBoosterSpd()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.spd = boostingSpd;
    }


    /// <summary>
    /// 키입력
    /// </summary>
    /// <param name="type"></param>
    private void InputKey(EVehicleType type)
    {
        switch (type)
        {
            case EVehicleType.None:
            case EVehicleType.Wyvern:
            case EVehicleType.Frog:
                //if (Input.GetKey(KeyCode.Space))
                //{
                //    isPressing = true;
                //}
                //else
                //{
                //    isPressing = false;
                //}
                break;
            case EVehicleType.BirdMachine:
            case EVehicleType.ProfitUFO:
            case EVehicleType.GravitySuit:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isPressing = true;
                }
                else
                {
                    isPressing = false;
                }
                break;
            default:

                break;
        }
    }

    /// <summary>
    /// Input
    /// </summary>
    /// <param name="type"></param>
    private void CurrentVehicleKeyInput(EVehicleType type)
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            if (GameManager.Instance.IsGameStart == true)
            {
                switch (type)
                {
                    case EVehicleType.None:
                        Flying();
                        break;
                    case EVehicleType.GravitySuit:
                        ChangeGravity();
                        break;
                    case EVehicleType.Wyvern:
                        FlyingWyvern();
                        break;
                    case EVehicleType.ProfitUFO:
                        MoveUFO();
                        break;
                    case EVehicleType.BirdMachine:

                        break;
                    case EVehicleType.Frog:

                        break;
                }
            }
        }
        else
            Flying();
    }


    private void ChangeVehicle()
    {
        switch (vehicleType)
        {
            case EVehicleType.None:
                vehicle = null;
                break;
            case EVehicleType.GravitySuit:
                //vehicle = new GravitySuit(EVehicleState.Levitation);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") && CompareTag("Player"))
        {
            print(collision.gameObject);
            tag = "Invincibility";
            UIManager.Instance.currentHp -= 10;
            Camera.main.transform.DOShakePosition(2.5f, new Vector2(0.3f, 0.3f));
            spriterenderer.DOFade(0.5f, 0.5f).SetEase(Ease.Linear).SetLoops(6, LoopType.Yoyo).OnComplete(() =>
            {
                tag = "Player";
            });

            if (vehicleType == EVehicleType.None && isUseItem == false && UIManager.Instance.currentHp <= 0)
            {
                IsDie = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            if (IsBig)
            {
                Camera.main.transform.DOShakePosition(0.2f, new Vector2(0, 0.3f)).OnComplete(()
                => Camera.main.transform.DOMove(new Vector3(0, 0, -10), 0.1f));
            }
        }
    }

    #region 탈것

    /// <summary>
    /// 
    /// </summary>
    private void Flying()
    {
        if (isPressing == true)
        {
            rb.AddForce(Vector2.up * force * Time.deltaTime);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void MoveUFO()
    {
        if (isPressing == true)
        {
            rb.AddForce(Vector2.up * force);
        }
    }


    private void BusterJump()
    {
        if (isPressing == true)
        {

        }
    }

    /// <summary>
    /// �߷� �ٲٱ�
    /// </summary>
    private void ChangeGravity()
    {
        if (isPressing == true)
        {
            rb.gravityScale *= -1;
        }
    }

    /// <summary>
    /// ���̹����� ����
    /// </summary>
    private void FlyingWyvern()
    {
        if (isPressing == true)
        {
            rb.AddForce(Vector3.down * force);
        }
    }

    #endregion
}