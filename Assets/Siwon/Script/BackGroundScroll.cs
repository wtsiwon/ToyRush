using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MovingElement
{
    private Rigidbody2D rb;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(transform.position.x <= -41.4f)
        {
            transform.position = new Vector3(41.4f, 0, 0);
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
