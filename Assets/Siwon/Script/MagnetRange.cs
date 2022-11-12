using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MagnetRange : MonoBehaviour
{
    [Tooltip("날아오는 속도")]
    public float spd;

    private void Update()
    {
        transform.position = Player.Instance.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Coin coin = collision.GetComponent<Coin>();
        if (collision.CompareTag("Coin"))
        {
            coin.transform.DOMove(Player.Instance.transform.position,spd);
        }
    }
}
