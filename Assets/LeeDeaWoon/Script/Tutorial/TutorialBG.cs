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
        //if (transform.position.x <= -82.8f)
        //{
        if (isfirstBack == false)
            transform.position = new Vector3(standardObj.transform.position.x + (20.7f * backgroundNum), 0, 0);

        else if (isfirstBack == true)
        {
            if (transform.position.x <= -20.7f)
                Destroy(gameObject);
        }
    }

    private IEnumerator CWait()
    {
        yield return new WaitForSeconds(5f);
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

    private IEnumerator CUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            print(rb.velocity);
        }
    }
}
