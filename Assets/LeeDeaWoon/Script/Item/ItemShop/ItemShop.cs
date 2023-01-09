using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ItemShop : MonoBehaviour
{
    public EShopItem eShopItem;
    public TextMeshProUGUI playerStateText;

    public List<string> state = new List<string>();
    Button itemShopBtn;
    

    [Header("슬라임")]
    public GameObject slime;
    float magnetTimer = 0;

    const int magnetWaitingTime = 7;

    void Start()
    {
        itemShopBtn = GetComponent<Button>();

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
            if (UIManager.Instance.itemCount > 0)
            {
                --UIManager.Instance.itemCount;
                switch (eShopItem)
                {
                    case EShopItem.Shield:
                        Shield();
                        break;

                    case EShopItem.Slime:
                        StartCoroutine(Slime());
                        break;

                    case EShopItem.Clockwork:
                        Clockwork();
                        break;

                    case EShopItem.TreasureBox:
                        TreasureBox();
                        break;

                    case EShopItem.PirateRoulette:
                        PirateRoulette();
                        break;
                }
            }
        });
    }

    void Shield()
    {
        SpriteRenderer playerSprite = Player.Instance.GetComponent<SpriteRenderer>();
        Player.Instance.tag = "Invincibility";
        playerSprite.DOFade(0.3f, 0.5f).SetEase(Ease.Linear).SetLoops(4, LoopType.Yoyo).OnComplete(() =>
        {
            Player.Instance.tag = "Player";
        });
    }

    IEnumerator Slime()
    {
        Player.Instance.IsMagneting = true;

        #region 슬라임 연출
        var slimeScaleObj = Instantiate(slime.gameObject, Vector2.zero, Quaternion.identity);
        slimeScaleObj.transform.SetParent(Player.Instance.transform, false);

        var spriteRenderer = slimeScaleObj.GetComponent<SpriteRenderer>();
        slimeScaleObj.transform.DOScale(new Vector2(10, 10), 0.8f).SetLoops(-1, LoopType.Restart);
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

    void TreasureBox()
    {
        Instantiate(ItemManager.inst.itemList[Random.Range(0, ItemManager.inst.itemList.Count)], new Vector2(11, 2),
        Quaternion.identity).transform.parent = gameObject.transform;
    }

    void PirateRoulette()
    {
        int stateRandom = Random.Range(0, state.Count);
        playerStateText.text = state[stateRandom];

        Debug.Log(playerStateText);
    }
}
