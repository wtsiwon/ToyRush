using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using DG.Tweening;
public class Player : Singleton<Player>
{
    #region Condition
    public bool isBoosting;
    public bool isMagneting;
    public bool IsBig { get; set; }
    private bool shouldObstacleBreak;

    public EBoosterType boosterType;
    #endregion

    public bool isUseItem =>
     isMagneting || IsBig || isBoosting;

    [Tooltip("탈것의 종류")]
    public EVehicleType vehicleType;

    public float force;

    private Rigidbody2D rb;
    private SpriteRenderer spriterenderer;

    [Tooltip("눌렀음?")]
    public bool isPressing;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        InputKey(vehicleType);
        CurrentVehicle(vehicleType);
        if(isBoosting == true)
        {
            BackGroundSpawner.Instance.backgroundSpd = 500;
        }
        else
        {
            BackGroundSpawner.Instance.backgroundSpd = GameManager.STARTSPD;
        }
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
            OnDie(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if(IsBig == true)
            {
                Camera.main.transform.DOShakePosition(0.2f, new Vector2(0, 0.3f));
            }
        }
    }

    private void OnDie(GameObject obj)
    {
        if (vehicleType == EVehicleType.None && isUseItem == false)
        {
            MovingElementManager.Instance.MovingElementSpeedSet(0);
            UIManager.Instance.GameOver();
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