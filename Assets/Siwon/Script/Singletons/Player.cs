using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using DG.Tweening;
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
                StartCoroutine(CWaitChangeBoosterSpd());
            }
            else
            {
                //BackGroundSpawner.Instance.backgroundSpd = GameManager.Instance.spd;
            }
        }
    }
    private bool isMagneting;
    public bool IsMagneting
    {
        get => isMagneting;
        set
        {
            isMagneting = value;
            if(value == true)
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

    public bool isUseItem =>
     IsMagneting || IsBig || isBoosting;

    private bool isDie;
    public bool IsDie
    {
        get => isDie;

        set
        {
            isDie = value;
            if(isDie == true)
            {
                GameManager.Instance.OnDie(transform);
            }
        }
    }

    [Tooltip("탈것의 종류")]
    public EVehicleType vehicleType;

    [Tooltip("힘")]
    [Range(100, 5000)]
    public float force;

    #region GetComponent한 Component
    private Rigidbody2D rb;
    private SpriteRenderer spriterenderer;

    [SerializeField]
    private MagnetRange magnetRange;

    #endregion

    [Tooltip("눌렀음?")]
    public bool isPressing;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
        
        IsDie = false;
    }

    private void Update()
    {
        InputKey(vehicleType);
        CurrentVehicle(vehicleType);
    }

    private IEnumerator CWaitChangeBoosterSpd()
    {
        yield return new WaitForSeconds(2f);
        BackGroundSpawner.Instance.backgroundSpd = 4000f;
    }


    /// <summary>
    /// ���ư��� Ű �Է�(PC)
    /// </summary>
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
            case EVehicleType.BusterMachine:
            case EVehicleType.ProfitUFO:
            case EVehicleType.GravitySuit:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isPressing = true;
                    Debug.Log("asdfasdf");
                }
                else
                {
                    isPressing = false;
                }
                break;
        }
    }

    /// <summary>
    /// ���� Ż�Ϳ� ���� Input
    /// </summary>
    /// <param name="type"></param>
    private void CurrentVehicle(EVehicleType type)
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
                case EVehicleType.BusterMachine:

                    break;
                case EVehicleType.Frog:

                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            print(collision.gameObject);
            if (vehicleType == EVehicleType.None && isUseItem == false)
            {
                IsDie = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (IsBig == true)
            {
                Camera.main.transform.DOShakePosition(0.2f, new Vector2(0, 0.3f));
            }
        }
    }

    

    #region 탈것

    /// <summary>
    /// ���� ������~
    /// </summary>
    private void Flying()
    {
        if (isPressing == true)
        {
            rb.AddForce(Vector2.up * force * Time.deltaTime);
        }
    }

    /// <summary>
    /// UFO����
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