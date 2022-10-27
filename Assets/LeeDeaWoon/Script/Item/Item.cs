using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Item : MovingElement
{
    public EItemType itemType;

    public new Collider2D collider2D;
    public SpriteRenderer spriteRenderer;
    private Vector2 playerDistance;

    [Header("������ : �ν���")]
    public float boosterDuration; // ���ӽð�
    public float boosterSpeed; // �ӷ�
    public Ease ease;

    [Header("������ : �ڼ�")]
    public int magnetWaitingTime; // ��ٸ� �ð�

    float magnetTimer;

    [Header("������ : ������")]
    public GameObject piggybankCoin; // ��ȯ�� ������ ����

    [Header("������ : ũ������")]
    public int sizeTime; //Ŀ���� �ð�
    public int sizeWaitingTime; //��ٸ� �ð�
    public Vector2 size = new Vector2(); //���ϴ� ������

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


    // ������ ������
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

            switch (itemType)
            {
                case EItemType.Transformation:

                    break;


                case EItemType.Magnet: // �ڼ�

                    Player.Instance.isMagneting = true;
                    yield return new WaitForSeconds(magnetWaitingTime);
                    Destroy(this.gameObject);
                    Player.Instance.isMagneting = false;
                    magnetTimer = 0;

                    break;


                case EItemType.Piggybank: // ������

                    Instantiate(piggybankCoin, transform.position, Quaternion.identity);
                    Destroy(this.gameObject);
                    break;


                case EItemType.Booster: // �ν���
                    Sequence mySequence = DOTween.Sequence();
                    float playerXValue = collision.transform.position.x;

                    Player.Instance.isBoosting = true;
                    mySequence.Append(collision.transform.DOLocalMoveX(-8, 2f))
                              .Append(collision.transform.DOLocalMoveX(5, boosterSpeed));

                    yield return new WaitForSeconds(boosterDuration); // ���ӽð�

                    collision.transform.DOLocalMoveX(playerXValue, boosterSpeed);

                    yield return new WaitForSeconds(boosterSpeed);

                    Destroy(this.gameObject);
                    Player.Instance.isBoosting = false;
                    break;


                case EItemType.Coinconverter:
                    break;


                case EItemType.Sizecontrol: // ũ�� ����

                    Player.Instance.isBig = true;
                    collision.transform.DOScale(size, sizeTime);
                    // ��ֹ��� �ݶ��̴��� ���ֱ�

                    yield return new WaitForSeconds(sizeWaitingTime);
                    Destroy(this.gameObject);
                    Player.Instance.isBig = false;
                    sizeTimer = 0;

                    collision.transform.DOScale(new Vector2(1, 1), sizeTime);
                    // ��ֹ��� �ݶ��̴��� ���ֱ�
                    break;
            }

        }
    }
}