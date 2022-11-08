using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Item : MovingElement
{
    public EItemType itemType;

    public new Collider2D collider2D;

    [Header("아이템 : 부스터")]
    public float boosterDuration; // 지속시간
    public float boosterSpeed; // 속력
    public Ease ease;

    [Header("아이템 : 자석")]
    public int magnetWaitingTime; // 기다릴 시간

    float magnetTimer;

    [Header("아이템 : 저금통")]
    public int getCoin;
    //public GameObject piggybankCoin; // 소환될 프리팹 코인

    [Header("아이템 : 크기조절")]
    public int sizeTime; //커지는 시간
    public int sizeWaitingTime; //기다릴 시간
    public Vector2 playerSize;

    float sizeTimer;

    protected override void Start()
    {
        base.Start();
        collider2D = GetComponent<Collider2D>();

        playerSize = Player.Instance.transform.localScale;
    }

    protected override void Update()
    {
        base.Update();
        Item_Delay();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    // 아이템 딜레이
    void Item_Delay()
    {
        if (sizeTimer < sizeWaitingTime && Player.Instance.IsBig == true)
            sizeTimer += Time.deltaTime;

        if (magnetTimer < magnetWaitingTime && Player.Instance.isMagneting == true)
            magnetTimer += Time.deltaTime;
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collider2D.enabled = false;
            spriterenderer.DOFade(0, 0);

            switch (itemType)
            {
                case EItemType.Transformation:

                    break;


                case EItemType.Magnet: // 자석

                    Player.Instance.isMagneting = true;
                    yield return new WaitForSeconds(magnetWaitingTime);
                    Destroy(this.gameObject);
                    Player.Instance.isMagneting = false;
                    magnetTimer = 0;

                    break;


                case EItemType.Piggybank: // 저금통
                    Instantiate(ItemManager.inst.piggybankDirector, Vector2.zero, Quaternion.identity).transform.SetParent(gameObject.transform, false);
                    UIManager.Instance.coin += getCoin;
                    GameManager.Instance.haveCoin += getCoin;

                    //GameObject bankCoinPattern = Instantiate(piggybankCoin, new Vector2(transform.position.x + Random.Range(posMinX, posMaxX), transform.position.y + Random.Range(posMinY, posMaxY)), Quaternion.identity);
                    //bankCoinPattern.transform.parent = gameObject.transform;
                    //bankCoinPattern.GetComponent<Rigidbody2D>().velocity = Vector3.left *
                    //    BackGroundSpawner.Instance.backgroundSpd * Time.deltaTime;

                    break;


                case EItemType.Booster: // 부스터
                    Sequence mySequence = DOTween.Sequence();
                    float playerXValue = collision.transform.position.x;

                    Player.Instance.isBoosting = true;
                    mySequence.Append(collision.transform.DOLocalMoveX(-8, 2f))
                              .OnComplete(() =>
                              {
                                  Player.Instance.boosterType = EBoosterType.BoosterItem;

                                  Instantiate(ItemManager.inst.whiteScreen, Vector2.zero, Quaternion.identity);
                                  collision.transform.DOLocalMoveX(3, boosterSpeed);
                              });
                    yield return new WaitForSeconds(boosterDuration); // 지속시간

                    collision.transform.DOLocalMoveX(playerXValue, boosterSpeed);

                    Player.Instance.isBoosting = false;
                    yield return new WaitForSeconds(boosterSpeed);

                    Destroy(this.gameObject);
                    break;


                case EItemType.Coinconverter:
                    break;


                case EItemType.Sizecontrol: // 크기 조절

                    Player.Instance.IsBig = true;
                    collision.transform.DOScale(new Vector2(playerSize.x + 0.2f, playerSize.y + 0.2f), sizeTime);
                    // 장애물의 콜라이더를 꺼주기

                    yield return new WaitForSeconds(sizeWaitingTime);
                    Destroy(this.gameObject);
                    Player.Instance.IsBig = false;
                    sizeTimer = 0;

                    collision.transform.DOScale(playerSize, sizeTime);
                    // 장애물의 콜라이더를 켜주기
                    break;
            }

        }
    }
}
