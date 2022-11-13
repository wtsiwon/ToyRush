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
    public SpriteRenderer magnetScale;
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

        if (magnetTimer < magnetWaitingTime && Player.Instance.IsMagneting == true)
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

                    Player.Instance.IsMagneting = true;

                    #region 자석 연출
                    GameObject magnetScaleObj = Instantiate(magnetScale.gameObject, Vector2.zero, Quaternion.identity);
                    magnetScaleObj.transform.SetParent(Player.Instance.transform, false);

                    SpriteRenderer spriteRenderer = magnetScaleObj.GetComponent<SpriteRenderer>();
                    magnetScaleObj.transform.DOScale(new Vector2(10,10), 0.8f).SetLoops(-1, LoopType.Restart);
                    spriteRenderer.DOFade(0, 0.8f).SetLoops(-1, LoopType.Restart);
                    #endregion

                    yield return new WaitForSeconds(magnetWaitingTime);
                    Player.Instance.IsMagneting = false;

                    magnetScaleObj.transform.DOKill();
                    spriteRenderer.DOKill();

                    magnetTimer = 0;

                    Destroy(magnetScaleObj);
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

                    Player.Instance.IsBoosting = true;
                    ItemManager.inst.isItemTouch = true;

                    #region 연출시간
                    Instantiate(ItemManager.inst.smallDirector, Vector2.zero, Quaternion.identity).transform.SetParent(Player.Instance.transform, false);
                    GameObject director = Instantiate(ItemManager.inst.bigDirector, Vector2.zero, Quaternion.identity);
                    director.transform.SetParent(Player.Instance.transform, false);

                    director.transform.DOScale(Vector2.zero, 2f);

                    SpriteRenderer boosterSprite = director.GetComponent<SpriteRenderer>();
                    boosterSprite.DOFade(0, 2f);
                    #endregion

                    mySequence.Append(collision.transform.DOLocalMoveX(-8, 2f))
                              .OnComplete(() =>
                              {
                                  director.transform.DOKill();
                                  boosterSprite.DOKill();

                                  Destroy(director);

                                  ItemManager.inst.boosterNumber = 3;

                                  Instantiate(ItemManager.inst.whiteScreen, Vector2.zero, Quaternion.identity);
                                  collision.transform.DOLocalMoveX(-3.5f, boosterSpeed);
                              });
                    yield return new WaitForSeconds(boosterDuration); // 지속시간
                    collision.transform.DOLocalMoveX(playerXValue, boosterSpeed);

                    Player.Instance.IsBoosting = false;
                    ItemManager.inst.isItemTouch = false;

                    #region 무적시간
                    Player.Instance.tag = "Invincibility";
                    Player.Instance.GetComponent<SpriteRenderer>().DOFade(0.5f, 0.5f).SetLoops(-1, LoopType.Yoyo);

                    yield return new WaitForSeconds(2);

                    Player.Instance.tag = "Player";
                    Player.Instance.GetComponent<SpriteRenderer>().DOKill();
                    #endregion

                    yield return new WaitForSeconds(boosterSpeed);

                    Destroy(this.gameObject);
                    break;


                case EItemType.Coinconverter:
                    break;


                case EItemType.Sizecontrol: // 크기 조절

                    Player.Instance.IsBig = true;
                    collision.transform.DOScale(new Vector2(playerSize.x + 0.2f, playerSize.y + 0.2f), sizeTime);

                    yield return new WaitForSeconds(sizeWaitingTime);
                    collision.transform.DOScale(playerSize, sizeTime);

                    #region 무적시간
                    Player.Instance.tag = "Invincibility";
                    Player.Instance.GetComponent<SpriteRenderer>().DOFade(0.5f, 0.5f).SetLoops(-1, LoopType.Yoyo);

                    yield return new WaitForSeconds(2);

                    Player.Instance.tag = "Player";
                    Player.Instance.GetComponent<SpriteRenderer>().DOKill();
                    #endregion

                    Player.Instance.IsBig = false;

                    Destroy(this.gameObject);
                    sizeTimer = 0;

                    break;
            }

        }
    }

}
