using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class BackGround : MovingElement
{
    public bool isfirstBack;
    protected override void Update()
    {
        base.Update();
        if (transform.position.x <= -20.6f)
        {
            //transform.position = new Vector3(41f, 0, 0);
            BackGroundSpawner.Instance.SpawnBackGround();
            Return();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    protected override void OnEnable()
    {
        
        base.OnEnable();
        StartCoroutine(CUpdate());
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
