using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : MovingElement
{
    [Header("범위")]
    public float flySpeed; // 날아가는 속도
    public int coinRange; // 코인범위

    void Start()
    {

    }

    protected override void Update()
    {
        base.Update();
        Coin_ColliderRange();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    //자석 아이템을 먹은 후 
    public void Coin_ColliderRange()
    {
        if (Player.Instance.isMagneting == true)
        {

            if (Player.Instance.transform.position.x <= this.transform.position.x + coinRange && Player.Instance.transform.position.y <= this.transform.position.y + coinRange)
                this.gameObject.transform.DOLocalMove(Player.Instance.transform.position, flySpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(ItemManager.inst.piggybankDirector, transform.position, Quaternion.identity)/*.transform.SetParent(gameObject.transform, false)*/;
            //Destroy(this.gameObject);

            gameObject.transform.DOKill();
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
            Return();
        }
    }
}
