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

    void Update()
    {
        Coin_ColliderRange();
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
            this.gameObject.transform.DOKill();
            Destroy(this.gameObject);
            //Return();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.transform.DOKill();
            if (Player.Instance.vehicleType == EVehicleType.ProfitUFO)
            {
                GameManager.Instance.coin += 2;
            }
            else
            {
                GameManager.Instance.coin += 1;
            }
            Return();
        }
    }
}
