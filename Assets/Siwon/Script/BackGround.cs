using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class BackGround : MovingElement
{
    public bool isfirstBack;

    public int backgroundNum;

    public GameObject standardObj;
    protected override void Update()
    {
        base.Update();
        //if (transform.position.x <= -82.8f)
        //{
        if (isfirstBack == false && GameManager.Instance.IsGameStart == true)
        {
            transform.position = new Vector3(standardObj.transform.position.x + (20.7f * backgroundNum), 0, 0);
        }
        else if(isfirstBack == true && GameManager.Instance.IsGameStart == true)
        {
            if(transform.position.x <= -20.7f)
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator CWait()
    {
        yield return new WaitForSeconds(5f);
    }

    protected override void FixedUpdate()
    {
        //if(isfirstBack == true && GameManager.Instance.IsGameStart == true)
        //{
        //    rb.velocity = Vector3.left * GameManager.Instance.pd * Time.fixedDeltaTime;
        //}
        //base.FixedUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    protected override void OnEnable()
    {

        base.OnEnable();
    }
}
