using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Item : PoolingObj
{
    public enum EItem
    {
        Transformation, //변신
        Magnet,         //자석
        Piggybank,      //저금통
        Booster,        //부스터
        Coinconverter,  //코인변환기
        Sizecontrol,    //크기조절
    }
    public EItem eItem;

    public SpriteRenderer spriteRenderer;

    [Header("아이템 : 자석")]
    public int magnetWaitingTime; // 기다릴 시간

    float magnetTimer;


    [Header("아이템 : 크기조절")]
    public int sizeTime; //커지는 시간
    public int sizeWaitingTime; //기다릴 시간
    public Vector3 size = new Vector3(); //원하는 사이즈

    float sizeTimer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Item_Delay();
    }


    // 아이템 딜레이
    public void Item_Delay()
    {
        if (sizeTimer < sizeWaitingTime && Player.Instance.isBig == true)
            sizeTimer += Time.deltaTime;

        if (magnetTimer < magnetWaitingTime && Player.Instance.isMagneting == true)
            magnetTimer += Time.deltaTime;
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (eItem)
            {
                case EItem.Transformation:

                    break;


                case EItem.Magnet: // 자석

                    Player.Instance.isMagneting = true;
                    spriteRenderer.DOFade(0, 0);

                    yield return new WaitForSeconds(magnetWaitingTime);
                    Destroy(this.gameObject);
                    Player.Instance.isMagneting = false;
                    magnetTimer = 0;

                    break;


                case EItem.Piggybank:


                    Destroy(this.gameObject);

                    break;


                case EItem.Booster:
                    break;


                case EItem.Coinconverter:
                    break;


                case EItem.Sizecontrol: // 크기 조절

                    Player.Instance.isBig = true;
                    collision.transform.DOScale(size, sizeTime);
                    spriteRenderer.DOFade(0, 0);
                    // 장애물의 콜라이더를 꺼주기

                    yield return new WaitForSeconds(sizeWaitingTime);
                    Destroy(this.gameObject);
                    Player.Instance.isBig = false;
                    sizeTimer = 0;

                    collision.transform.DOScale(new Vector3(1, 1, 1), sizeTime);
                    // 장애물의 콜라이더를 켜주기
                    break;
            }

        }
    }
}
