using System.Collections;
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
        rb.velocity = Vector3.left * BackGroundSpawner.Instance.backgroundSpd;

        MovingElementManager.Instance.movingElementList.Add(this);
    }

    /// <summary>
    /// Spd바꿔줌
    /// </summary>
    /// <param name="spd"></param>
    public void SetMovingSpd(float spd)
    {
        rb.velocity = Vector3.left * spd;
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
