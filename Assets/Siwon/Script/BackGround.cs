using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class BackGround : MovingElement
{
    public bool isfirstBack;

    public int backgroundNum;
    protected override void Update()
    {
        base.Update();
        if (transform.position.x <= -82.8f)
        {
            if (isfirstBack == false)
            {
                transform.position = new Vector3(0, 0, 0);
            }
            else
            {
                Return();
            }
            //BackGroundSpawner.Instance.SpawnBackGround();
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    protected override void OnEnable()
    {

        base.OnEnable();
        //StartCoroutine(CUpdate());
    }

    private IEnumerator CUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            print(rb.velocity);
        }
    }


    public override void Return()
    {
        base.Return();
    }

}
