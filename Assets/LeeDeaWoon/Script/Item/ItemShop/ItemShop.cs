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
    float statePosY = 0;
    Button itemShopBtn;

    [Space(10)]
    [EnumType("eShopItem", (short)EShopItem.Shield)]
    public GameObject toyShield;

    [EnumType("eShopItem", (short)EShopItem.Slime)]
    public GameObject slime;
    float magnetTimer = 0;

    const int magnetWaitingTime = 7;

    UIManager uiManager;

    void Start()
    {
        uiManager = UIManager.Instance;
        itemShopBtn = GetComponent<Button>();
        statePosY = playerStateText.transform.localPosition.y;

        ItemBtn();
    }

    void Update()
    {
        if (magnetTimer < magnetWaitingTime && Player.Instance.IsMagneting)
            magnetTimer += Time.deltaTime;
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
                        Clockwork();
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
        Player.Instance.IsMagneting = true;

        #region 슬라임 연출
        var slimeScaleObj = Instantiate(slime.gameObject, Vector2.zero, Quaternion.identity);
        slimeScaleObj.transform.SetParent(Player.Instance.transform, false);

        var spriteRenderer = slimeScaleObj.GetComponent<SpriteRenderer>();
        slimeScaleObj.transform.DOScale(new Vector2(10, 10), 0.8f).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        spriteRenderer.DOFade(0, 0.8f).SetLoops(-1, LoopType.Restart);
        #endregion

        yield return new WaitForSeconds(magnetWaitingTime);
        Player.Instance.IsMagneting = false;

        slimeScaleObj.transform.DOKill();
        spriteRenderer.DOKill();

        magnetTimer = 0;

        Destroy(slimeScaleObj);
    }

    void Clockwork()
    {
        UIManager.Instance.currentHp += 20;
    }

    void PirateRoulette()
    {
        Instantiate(ItemManager.inst.itemList[Random.Range(0, ItemManager.inst.itemList.Count)], new Vector2(11, 2),
        Quaternion.identity).transform.parent = gameObject.transform;
    }

    IEnumerator TreasureBox()
    {
        float waitTime = 0.5f;

        int stateRandom = Random.Range(0, stateList.Count);
        float currentPlayerSpd = Player.Instance.force;

        playerStateText.transform.DOLocalMoveY(statePosY, 0).SetEase(Ease.Linear);
        playerStateText.DOFade(1, 0);

        playerStateText.text = stateList[stateRandom].state;

        playerStateText.transform.DOLocalMoveY(statePosY + 800, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
        {
            playerStateText.DOFade(0, waitTime).SetEase(Ease.Linear);
        });

        switch (stateList[stateRandom].eState)
        {
            // 이동속도 저하
            case EState.SlowMove:
                float currentBackGroundSpd = GameManager.Instance.spd;
                GameManager.Instance.spd -= 4;
                yield return new WaitForSeconds(3);
                GameManager.Instance.spd = currentBackGroundSpd;
                break;

            // 체력감소 저하
            case EState.SlowHP:
                float currentHpSpeed = UIManager.Instance.hpReductionSpeed;
                UIManager.Instance.hpReductionSpeed /= 2;
                yield return new WaitForSeconds(3);
                UIManager.Instance.hpReductionSpeed = currentHpSpeed;
                break;

            // 제트팩 강화
            case EState.EnhanceJetPack:
                Player.Instance.force += 2000;
                yield return new WaitForSeconds(3);
                Player.Instance.force = currentPlayerSpd;
                break;

            // 제트팩 약화
            case EState.WeakenJetPack:
                Player.Instance.force -= 2000;
                yield return new WaitForSeconds(3);
                Player.Instance.force = currentPlayerSpd;
                break;

            // 충돌 데미지 감소
            case EState.SlowCollisionDamage:
                break;
        }
    }
}
