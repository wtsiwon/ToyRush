using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class BackGround : MovingElement
{
    private void Update()
    {
        if (transform.position.x <= -20.7f)
        {
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
            print(BackGroundSpawner.Instance.backgroundSpd);
        }
    }


    public override void Return()
    {
        base.Return();
    }

}
