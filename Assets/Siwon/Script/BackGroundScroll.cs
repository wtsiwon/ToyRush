using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MovingElement
{

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
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
