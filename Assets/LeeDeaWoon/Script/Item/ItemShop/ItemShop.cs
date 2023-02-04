using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public enum EState
{
    SlowMove,
    SlowHP,
    EnhanceJetPack,
    WeakenJetPack,
    SlowCollisionDamage,
}

[System.Serializable]
public class State
{
    public string state;
    public EState eState;
}

public class ItemShop : MonoBehaviour
{
    public static ItemShop instance;
    void Awake() => instance = this;

    public EShopItem eShopItem;
    public List<State> stateList = new List<State>();

    public TextMeshProUGUI playerStateText;
    Button itemShopBtn;

    [Space(10)]
    [EnumType("eShopItem", (short)EShopItem.Shield)]
    public GameObject toyShield;

    [EnumType("eShopItem", (short)EShopItem.Slime)]
    public GameObject slime;
    float magnetTimer = 0;
    const int magnetWaitingTime = 7;

    [EnumType("eShopItem", (short)EShopItem.Clockwork)]
    public GameObject springAnim;
    GameObject springSummon;

    [EnumType("eShopItem", (short)EShopItem.TreasureBox)]
    public GameObject treasureBoxAnim;
    GameObject treasureBoxSummon;

    UIManager uiManager;
    Player player;

    void Start()
    {
        gameObject.SetActive(true);

        uiManager = UIManager.Instance;
        player = Player.Instance;

        itemShopBtn = GetComponent<Button>();

        ItemBtn();
    }

    void Update()
    {
        if (magnetTimer < magnetWaitingTime && player.IsMagneting)
            magnetTimer += Time.deltaTime;

        if (treasureBoxSummon != null)
            treasureBoxSummon.transform.DOLocalMove(new Vector2(player.transform.position.x, player.transform.position.y + 2), 0).SetEase(Ease.Linear);

        if (springSummon != null)
            springSummon.transform.DOLocalMove(new Vector2(player.transform.position.x, player.transform.position.y + 2), 0).SetEase(Ease.Linear);

    }

    void ItemBtn()
    {
        itemShopBtn.onClick.AddListener(() =>
        {
            int itemCount = uiManager.itemShop[uiManager.shopItemNumber].itemNum;

            if (itemCount > 0 && uiManager.coolTimeSlot.fillAmount == 0)
            {
                uiManager.isClickItemUse = false;
                --uiManager.itemShop[uiManager.shopItemNumber].itemNum;

                switch (eShopItem)
                {
                    // 장난감 방패
                    case EShopItem.Shield:
                        Shield();
                        break;

                    // 슬라임
                    case EShopItem.Slime:
                        StartCoroutine(Slime());
                        break;

                    // 낡은 태엽
                    case EShopItem.Clockwork:
                        StartCoroutine(Clockwork());
                        break;

                    // 해적 룰렛
                    case EShopItem.PirateRoulette:
                        PirateRoulette();
                        break;

                    // 보물 상자
                    case EShopItem.TreasureBox:
                        StartCoroutine(TreasureBox());
                        break;
                }
            }
        });
    }

    void Shield()
    {
        Vector2 shildSummonPos = new Vector2(11, 0);
        Instantiate(toyShield, shildSummonPos, Quaternion.identity);
    }

    IEnumerator Slime()
    {
        player.IsMagneting = true;

        #region 슬라임 연출
        var slimeScaleObj = Instantiate(slime.gameObject, Vector2.zero, Quaternion.identity);
        slimeScaleObj.transform.SetParent(player.transform, false);

        var spriteRenderer = slimeScaleObj.GetComponent<SpriteRenderer>();
        slimeScaleObj.transform.DOScale(new Vector2(10, 10), 0.8f).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        spriteRenderer.DOFade(0, 0.8f).SetLoops(-1, LoopType.Restart);
        #endregion

        yield return new WaitForSeconds(magnetWaitingTime);
        player.IsMagneting = false;

        slimeScaleObj.transform.DOKill();
        spriteRenderer.DOKill();

        magnetTimer = 0;

        Destroy(slimeScaleObj);
    }

    IEnumerator Clockwork()
    {
        springSummon = Instantiate(springAnim, new Vector2(player.transform.position.x, player.transform.position.y + 2), Quaternion.identity);
        yield return new WaitForSeconds(1);
        springSummon.GetComponent<SpriteRenderer>().DOFade(0, 3).SetEase(Ease.Linear).OnComplete(() =>
        {
            springSummon.transform.DOKill();
            Destroy(springSummon);
        });

        uiManager.currentHp += 20;
    }

    void PirateRoulette()
    {
        Instantiate(ItemManager.inst.itemList[Random.Range(0, ItemManager.inst.itemList.Count)], new Vector2(11, 2),
        Quaternion.identity).transform.parent = gameObject.transform;
    }

    IEnumerator TreasureBox()
    {
        float waitTime = 0.5f;

        float currentPlayerSpd = player.force;

        treasureBoxSummon = Instantiate(treasureBoxAnim, new Vector2(player.transform.position.x, player.transform.position.y + 2), Quaternion.identity);

        yield return new WaitForSeconds(1.5f);

        int stateRandom = Random.Range(0, stateList.Count);

        playerStateText.transform.DOLocalMoveY(treasureBoxSummon.transform.localPosition.y, 0).SetEase(Ease.Linear);
        playerStateText.DOFade(1, 0);

        playerStateText.text = stateList[stateRandom].state;

        playerStateText.transform.DOLocalMoveY(treasureBoxSummon.transform.localPosition.y + 200, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
        {
            playerStateText.DOFade(0, waitTime).SetEase(Ease.Linear);
        });

        treasureBoxSummon.GetComponent<SpriteRenderer>().DOFade(0, 1).SetEase(Ease.Linear).OnComplete(() =>
        {
            treasureBoxSummon.transform.DOKill();
            Destroy(treasureBoxSummon);
        });

        switch (stateList[stateRandom].eState)
        {
            // 이동속도 저하
            case EState.SlowMove:
                float currentBackGroundSpd = GameManager.Instance.spd;
                GameManager.Instance.spd -= 4;
                yield return new WaitForSeconds(5);
                GameManager.Instance.spd = currentBackGroundSpd;
                break;

            // 체력감소 저하
            case EState.SlowHP:
                float currentHpSpeed = UIManager.Instance.hpReductionSpeed;
                uiManager.hpReductionSpeed /= 2;
                yield return new WaitForSeconds(5);
                uiManager.hpReductionSpeed = currentHpSpeed;
                break;

            // 제트팩 강화
            case EState.EnhanceJetPack:
                player.force += 2000;
                yield return new WaitForSeconds(5);
                player.force = currentPlayerSpd;
                break;

            // 제트팩 약화
            case EState.WeakenJetPack:
                player.force -= 500;
                yield return new WaitForSeconds(5);
                player.force = currentPlayerSpd;
                break;

            // 충돌 데미지 감소
            case EState.SlowCollisionDamage:
                break;
        }
    }
}
