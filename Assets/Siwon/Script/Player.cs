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

    private Rigidbody2D rb;

    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        
    }
}
