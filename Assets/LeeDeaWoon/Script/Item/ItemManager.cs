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
    public GameObject startItem;

    public Image boosterRayCast500;
    public Image boosterRayCast1500;

    public Button booster500Btn;
    public Button booster1500Btn;

    bool isStartItemClick = false;

    void Start()
    {
        StartItem();
        StartCoroutine(Item_Spawn());
    }

    void Update()
    {

    }

    IEnumerator Item_Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnValue + Random.Range(-spawnInterval, spawnInterval));
            if (GameManager.Instance.IsGameStart == true)
            {
                Instantiate(itemList[Random.Range(0, itemList.Count)], new Vector2(spawnXValue, spawnYValue), Quaternion.identity).transform.parent = gameObject.transform;
            }
        }
    }

    public void StartItem()
    {
        booster500Btn.onClick.AddListener(() =>
        {
            isStartItemClick = true;


            booster500Btn.transform.DOScale(new Vector2(0, 0), 0.5f);
            booster1500Btn.transform.DOLocalMoveY(1250, 0.5f);

            StartCoroutine(Start_Booster());
        });

        booster1500Btn.onClick.AddListener(() =>
        {
            boosterRayCast500.raycastTarget = false;
            boosterRayCast1500.raycastTarget = false;
        });
    }

    IEnumerator Start_Booster()
    {
        Sequence mySequence = DOTween.Sequence();

        float playerXValue = player.transform.position.x;

        Player.Instance.isBoosting = true;
        mySequence.Append(player.transform.DOLocalMoveX(-8, 2f))
                  .Append(player.transform.DOLocalMoveX(5, 0.5f));

        yield return new WaitForSeconds(5); // 지속시간

        player.transform.DOLocalMoveX(playerXValue, 0.5f);

        yield return new WaitForSeconds(0.5f);

        Destroy(this.gameObject);
        Player.Instance.isBoosting = false;
    }
}
