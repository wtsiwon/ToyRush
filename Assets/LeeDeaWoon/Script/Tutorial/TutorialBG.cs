using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class TutorialBG : MovingElement
{
    public bool isfirstBack;

    public int backgroundNum;

    public GameObject standardObj;
    protected override void Update()
    {
        base.Update();

        if (isfirstBack == false)
            transform.position = new Vector3(standardObj.transform.position.x + (20.7f * backgroundNum), 0, 0);

        else if (isfirstBack == true)
        {
            if (transform.position.x <= -20.7f)
                Destroy(gameObject);
        }
    }

    protected override void FixedUpdate()
    {
        if (isfirstBack == true)
            rb.velocity = Vector3.left * 5;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }
}
