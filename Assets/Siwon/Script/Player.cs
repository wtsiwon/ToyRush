using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : Singleton<Player>
{
    public float force;

    public bool isPressing;

    #region Condition
    public bool isBoosting;
    public bool isMagneting;
    public bool isBig;
    #endregion

    private Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        InputKey();
        Flying();
    }

    private void Flying()
    {
        if (isPressing == true)
        {
            rb.AddForce(Vector2.up * force);
        }
    }

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
}
