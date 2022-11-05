using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class BackGround : MovingElement
{
    private void Update()
    {
        //거리비례 속도조절
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DestroyZone"))
        {
            Return();
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    private void MoveBackGround()
    {
        rb.velocity = Vector3.left * BackGroundSpawner.Instance.backgroundSpd;
    }

    public override void Return()
    {
        base.Return();
        MovingElementManager.Instance.movingElementList.Remove(this);
    }

}
