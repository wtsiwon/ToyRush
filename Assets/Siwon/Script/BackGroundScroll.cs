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
        if(transform.position.x <= -41.4f)
        {
            transform.position = new Vector3(41.4f, 0, 0);
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.IsGameStart == true)
        {
            rb.velocity = Vector3.left * BackGroundSpawner.Instance.backgroundSpd * Time.fixedDeltaTime;
        }
    }
}
