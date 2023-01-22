using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ToyItem : MonoBehaviour
{
    public EShopItem eShopItem;

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        StartCoroutine(Director());
    }

    void Update()
    {
        switch (eShopItem)
        {
            case EShopItem.Shield:
                transform.position = new Vector2(-3, Player.Instance.transform.position.y);
                break;
        }
    }

    IEnumerator Director()
    {
        switch (eShopItem)
        {
            case EShopItem.Shield:
                yield return new WaitForSeconds(3);

                var playerFade = Player.Instance.GetComponent<SpriteRenderer>();
                spriteRenderer.DOFade(0, 0);
                boxCollider2D.enabled = false;

                Player.Instance.tag = "Invincibility";
                playerFade.DOFade(0.5f, 0.5f).SetEase(Ease.Linear).SetLoops(4, LoopType.Yoyo).OnComplete(() =>
                {
                    Player.Instance.tag = "Player";

                    Destroy(gameObject);
                });
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            switch (eShopItem)
            {
                case EShopItem.Shield:
                    var director = Instantiate(ItemManager.inst.piggybankDirector, collision.transform.position, Quaternion.identity);
                    director.transform.localScale = collision.transform.localScale * 2;

                    Destroy(collision.gameObject);
                    break;
            }
        }
    }
}
