using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : MovingElement
{
    SpriteRenderer SpriteRenderer;

    protected override void Start()
    {
        base.Start();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Invincibility"))
        {
            GameObject director = Instantiate(ItemManager.inst.piggybankDirector, Vector2.zero, Quaternion.identity);
            director.transform.SetParent(gameObject.transform, false);
            director.transform.position = new Vector3(director.transform.position.x,
                director.transform.position.y, 0);

            if (Player.Instance.vehicleType == EVehicleType.ProfitUFO)
            {
                UIManager.Instance.coin += 2;
                GameManager.Instance.haveCoin += 2;
            }
            else
            {
                UIManager.Instance.coin += 1;
                GameManager.Instance.haveCoin += 1;
            }

            transform.DOKill();
            Destroy(gameObject);

            Return();
        }

    }
}
