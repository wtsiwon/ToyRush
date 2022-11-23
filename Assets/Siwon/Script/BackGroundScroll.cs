using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(transform.position.x <= -103.5f)
        {
            transform.position = new Vector3(-20.7f, 0, 0);
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.IsGameStart == true)
        {
            rb.velocity = Vector3.left * 300;
        }
    }
}
