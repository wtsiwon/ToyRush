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

    public new Collider2D collider2D;
    public SpriteRenderer spriteRenderer;
    private Vector2 playerDistance;

    [Header("아이템 : 부스터")]
    public float boosterDuration; // 지속시간
    public float boosterDistance; // 거리
    public float boosterSpeed; // 연출속도
    public Ease ease;

    [Header("아이템 : 자석")]
    public int magnetWaitingTime; // 기다릴 시간

    float magnetTimer;

    [Header("아이템 : 저금통")]
    public GameObject piggybankCoin; // 소환될 프리팹 코인
    public int coinCountMax; // 최대 갯수

    [Header("아이템 : 크기조절")]
    public int sizeTime; //커지는 시간
    public int sizeWaitingTime; //기다릴 시간
    public Vector2 size = new Vector2(); //원하는 사이즈

    float sizeTimer;

    void Start()
    {
        collider2D = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerDistance.x = Player.Instance.transform.position.x;
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
            collider2D.enabled = false;
            spriteRenderer.DOFade(0, 0);

            int randX = Random.Range(0, 2);
            int randY = Random.Range(0, 2);

            switch (eItem)
            {
                case EItem.Transformation:

                    break;


                case EItem.Magnet: // 자석

                    Player.Instance.isMagneting = true;

                    yield return new WaitForSeconds(magnetWaitingTime);
                    Destroy(this.gameObject);
                    Player.Instance.isMagneting = false;
                    magnetTimer = 0;

                    break;


                case EItem.Piggybank: // 저금통
                    for (int i = 0; i < coinCountMax; i++)
                        Instantiate(piggybankCoin, new Vector2(transform.position.x + randX, transform.position.y + randY), Quaternion.identity);
                    break;


                case EItem.Booster: // 부스터

                    Player.Instance.isBoosting = true;
                    collision.transform.DOLocalMoveX(boosterDistance, boosterSpeed).SetEase(ease);
                    yield return new WaitForSeconds(boosterDuration);
                    collision.transform.DOLocalMoveX(playerDistance.x, boosterSpeed).SetEase(ease);
                    yield return new WaitForSeconds(boosterSpeed);
                    Destroy(this.gameObject);
                    break;


                case EItem.Coinconverter:
                    break;


                case EItem.Sizecontrol: // 크기 조절

                    Player.Instance.isBig = true;
                    collision.transform.DOScale(size, sizeTime);
                    // 장애물의 콜라이더를 꺼주기

                    yield return new WaitForSeconds(sizeWaitingTime);
                    Destroy(this.gameObject);
                    Player.Instance.isBig = false;
                    sizeTimer = 0;

                    collision.transform.DOScale(new Vector2(1, 1), sizeTime);
                    // 장애물의 콜라이더를 켜주기
                    break;
            }

        }
    }
}
