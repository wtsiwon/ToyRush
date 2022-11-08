﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//인게임 움직이는 요소들
[RequireComponent(typeof(Rigidbody2D))]
public class MovingElement : PoolingObj
{
    protected Rigidbody2D rb;

    [HideInInspector]
    public SpriteRenderer spriterenderer;


    protected virtual void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();


        MovingElementManager.Instance.movingElementList.Add(this);
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        if (GameManager.Instance.IsGameStart == true)
        {
            rb.velocity = Vector3.left * BackGroundSpawner.Instance.backgroundSpd * Time.fixedDeltaTime;
        }
    }


    /// <summary>
    /// Spd바꿔줌
    /// </summary>
    /// <param name="spd"></param>
    public void SetMovingSpd(float spd)
    {
        //rb.velocity = Vector3.left * spd * Time.deltaTime;
    }
    protected virtual void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    public override void Return()
    {
        base.Return();
        MovingElementManager.Instance.movingElementList.Remove(this);
    }
}
