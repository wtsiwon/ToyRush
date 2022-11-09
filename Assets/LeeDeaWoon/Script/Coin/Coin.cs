using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : MovingElement
{
    SpriteRenderer SpriteRenderer;

    [Header("범위")]
    public float flySpeed; // 날아가는 속도
    public int coinRange; // 코인범위

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

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject director = Instantiate(ItemManager.inst.piggybankDirector, Vector2.zero, Quaternion.identity);
            director.transform.SetParent(gameObject.transform, false);
            director.transform.position = new Vector3(director.transform.position.x,
                director.transform.position.y, 0);


            spriterenderer.DOFade(0, 0);

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

            yield return new WaitForSeconds(1f);
            transform.DOKill();
            Destroy(gameObject);

            Return();
        }

    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("MagnetRange") && Player.Instance.isMagneting == true)
            transform.DOMove(Player.Instance.transform.position, flySpeed);
    }
}
