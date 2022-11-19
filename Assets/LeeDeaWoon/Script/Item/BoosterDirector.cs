using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoosterDirector : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.DOMove(Player.Instance.transform.position, 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Invincibility"))
        {
            transform.DOKill();
            Destroy(gameObject);
        }
    }
}
