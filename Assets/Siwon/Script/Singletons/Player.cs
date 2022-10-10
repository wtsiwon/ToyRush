using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : Singleton<Player>
{
    #region Condition
    public bool isBoosting;
    public bool isMagneting;
    public bool isBig;
    #endregion

    private const float FORCE = 12f;

    private Rigidbody2D rb;

    [Tooltip("누르고 있나")]
    public bool isPressing;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        InputKey();
        Flying();
    }

    /// <summary>
    /// 날아가는 키 입력(PC)
    /// </summary>
    private void InputKey()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            isPressing = true;
        }
        else
        {
            isPressing = false;
        }
    }

    /// <summary>
    /// 날아 가보자~
    /// </summary>
    private void Flying()
    {
        if (isPressing == true)
        {
            rb.AddForce(Vector3.up * FORCE);
        }
    }
}