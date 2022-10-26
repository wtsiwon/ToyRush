using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Singleton<Player>
{
    #region Condition
    public bool isBoosting;
    public bool isMagneting;
    public bool isBig;
    #endregion

    [Tooltip("현재 무엇을 타고 있는가")]
    public EVehicleType vehicleType;

    public float force;

    private Rigidbody2D rb;
    private SpriteRenderer spriterenderer;

    [Tooltip("누르고 있나")]
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
    }



    /// <summary>
    /// 날아가는 키 입력(PC)
    /// </summary>
    private void InputKey(EVehicleType type)
    {
        switch (type)
        {
            case EVehicleType.None:
            case EVehicleType.Wyvern:
            case EVehicleType.Frog:
                if (Input.GetKey(KeyCode.Space))
                {
                    isPressing = true;
                }
                else
                {
                    isPressing = false;
                }
                break;
            case EVehicleType.BusterMachine:
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
        }
    }

    /// <summary>
    /// 현재 탈것에 따라 Input
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
                    //점프 뛰로 누를시 천천히 활공
                    break;
                case EVehicleType.Frog:
                    //길게 누를수록 높게 점프
                    break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    /// <summary>
    /// 날아 가보자~
    /// </summary>
    private void Flying()
    {
        if (isPressing == true)
        {
            rb.AddForce(Vector2.up * force);
        }
    }

    /// <summary>
    /// UFO조작
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
    /// 중력 바꾸기
    /// </summary>
    private void ChangeGravity()
    {
        if (isPressing == true)
        {
            rb.gravityScale *= -1;
        }
    }

    /// <summary>
    /// 와이번으로 날기
    /// </summary>
    private void FlyingWyvern()
    {
        if (isPressing == true)
        {
            rb.AddForce(Vector3.down * force);
        }
    }

    private void GameOver()
    {

    }
}