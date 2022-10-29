using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RightBullet : MonoBehaviour
{
    public int speed;
    private Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        BulletMove();
    }

    void BulletMove()
    {
        rb2D.velocity = Vector2.left * speed;

        if (transform.position.x < -11)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.DOKill();
            Destroy(this.gameObject);
        }
    }
}
