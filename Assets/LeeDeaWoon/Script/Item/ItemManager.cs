using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ItemManager : MonoBehaviour
{
    public static ItemManager inst;
    private void Awake() => inst = this;

    public List<GameObject> itemList = new List<GameObject>();
    public GameObject player;

    public float spawnInterval;

    public const int spawnXValue = 11;
    public const int spawnYValue = 2;

    public float spawnValue = 12.5f;

    [Header("시작 아이템")]
    public GameObject whiteScreen;
    public GameObject startItem;
    public GameObject piggybankDirector;

    public Image boosterRayCast500;
    public Image boosterRayCast1500;

    public Button booster500Btn;
    public Button booster1500Btn;

    public bool isItemTouch;
    public bool isStartItemClick;
    public bool isStartItemSummon;
    public bool isStartItemCheck;

    public int boosterNumber;
    void Start()
    {
        StartItem_Btn();
        StartCoroutine(Item_Spawn());

    }

    void Update()
    {
        StartItem_Summon();
        StartCoroutine(StartItem());
    }

    IEnumerator Item_Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnValue + Random.Range(-spawnInterval, spawnInterval));
            if (GameManager.Instance.IsGameStart == true)
            {
                Instantiate(itemList[Random.Range(0, itemList.Count)], new Vector2(spawnXValue, spawnYValue),
                    Quaternion.identity).transform.parent = gameObject.transform;
            }
        }
    }


    #region 시작 아이템
    public void StartItem_Btn()
    {
        int movePos = 1250;
        float waitTime = 0.5f;

        // 500원 부스터 버튼을 눌렀을 때
        booster500Btn.onClick.AddListener(() =>
        {
            //if (GameManager.Instance.haveCoin > 500)
            //{

            //}
            isStartItemClick = true;

            StartItem_DoKill();
            boosterRayCast500.raycastTarget = false;
            boosterRayCast1500.raycastTarget = false;

            booster1500Btn.transform.DOLocalMoveY(movePos, waitTime);
            booster500Btn.transform.DOScale(new Vector2(0, 0), waitTime);

            boosterNumber = 1;
            StartCoroutine(Start_Booster());
        });

        // 1500원 부스터 버튼을 눌렀을 때
        booster1500Btn.onClick.AddListener(() =>
        {
            //if (GameManager.Instance.haveCoin >= 1500)
            //{

            //}
            isStartItemClick = true;

            StartItem_DoKill();
            boosterRayCast500.raycastTarget = false;
            boosterRayCast1500.raycastTarget = false;

            booster500Btn.transform.DOLocalMoveY(movePos, waitTime);
            booster1500Btn.transform.DOScale(new Vector2(0, 0), waitTime);

            boosterNumber = 2;
            StartCoroutine(Start_Booster());
        });
    }

    void StartItem_Summon()
    {
        if (GameManager.Instance.IsGameStart == true && isStartItemSummon == false)
        {
            isStartItemSummon = true;
            Instantiate(startItem, transform.position, Quaternion.identity).transform.parent = gameObject.transform;
        }
    }

    void StartItem_DoKill()
    {
        booster500Btn.transform.DOKill();
        booster1500Btn.transform.DOKill();
    }

    IEnumerator StartItem()
    {
        if (GameManager.Instance.IsGameStart == true && isStartItemCheck == false)
        {
            isStartItemCheck = true;

            startItem.SetActive(true);

            booster500Btn.transform.DOLocalMoveY(100, 0.5f).SetLoops(-1, LoopType.Yoyo);
            yield return new WaitForSeconds(0.2f);
            booster1500Btn.transform.DOLocalMoveY(100, 0.5f).SetLoops(-1, LoopType.Yoyo);

            yield return new WaitForSeconds(4f);

            if (isStartItemClick == false)
            {
                StartItem_DoKill();

                boosterRayCast500.raycastTarget = false;
                boosterRayCast1500.raycastTarget = false;

                booster500Btn.transform.DOLocalMoveY(1250, 0.5f);
                booster1500Btn.transform.DOLocalMoveY(1250, 0.5f);

                yield return new WaitForSeconds(1);

                StartItem_DoKill();

                Destroy(startItem);
            }

        }
    }

    IEnumerator Start_Booster()
    {
        Sequence mySequence = DOTween.Sequence();
        float playerXValue = player.transform.position.x;

        Player.Instance.IsBoosting = true;
        mySequence.Append(player.transform.DOLocalMoveX(-8, 2f))
                  .OnComplete(() =>
                  {
                      Instantiate(whiteScreen, Vector2.zero, Quaternion.identity);
                      player.transform.DOLocalMoveX(-11, 0.5f);
                  });

        yield return new WaitForSeconds(5); // 지속시간

        player.transform.DOLocalMoveX(playerXValue, 0.5f);
        Player.Instance.IsBoosting = false;

        Player.Instance.tag = "Invincibility";
        Player.Instance.GetComponent<SpriteRenderer>().DOFade(0.5f, 0.5f).SetLoops(-1, LoopType.Yoyo);

        yield return new WaitForSeconds(2);

        Player.Instance.tag = "Player";
        Player.Instance.GetComponent<SpriteRenderer>().DOKill();

        yield return new WaitForSeconds(0.5f);
    }

    #endregion
}
