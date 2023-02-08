using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    void Awake() => Instance = this;

    float waitTime = 0.5f;

    [Header("타이틀")]
    public GameObject title;
    public GameObject firstBackGround;
    public Sprite brokenBackGround;
    public TextMeshProUGUI touchToStart;
    [SerializeField] TextMeshProUGUI haveCoin;

    [Header("인게임")]
    public int coin;

    public RectTransform gadgetSlots;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI distanceText;

    [Header("체력")]
    public float maxHp;
    public float currentHp;
    public float hpReductionSpeed;
    [SerializeField] Image hpSlider;

    bool isHPCheck = false;

    [Header("상점 아이템")]
    public float currentCoolTime;
    public float maxCoolTime;

    public Image coolTimeSlot;
    [SerializeField] Image itemSlot;
    [SerializeField] TextMeshProUGUI itemCountText;

    int maxItemCount = 0;
    public bool isClickItemUse = false;

    #region 상점
    [Header("상점")]
    public List<Shop> itemShop = new List<Shop>();
    [SerializeField] GameObject shopWindow;
    [SerializeField] GameObject content;

    [SerializeField] Button shopBtn;
    [SerializeField] Button characterBtn;
    [SerializeField] Button gadgetBtn;
    [SerializeField] Button vehicleBtn;
    [SerializeField] Button shopsCancelBtn;
    [SerializeField] TextMeshProUGUI haveShopCoin;

    [Space(10)]
    [SerializeField] GameObject purchaseWindow;
    [SerializeField] GameObject addPurchas = null;
    [SerializeField] Image purchaseItemIcon;
    [SerializeField] Sprite buyBtn;
    [SerializeField] Sprite choiceBtn;
    [SerializeField] Sprite selecteBtn;

    [SerializeField] Button reductionBtn;
    [SerializeField] Button increaseBtn;
    [SerializeField] Button purchaseCancelBtn;
    [SerializeField] Button purchaseBtn;

    [SerializeField] TextMeshProUGUI quantityText;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] TextMeshProUGUI totalPriceText;

    ItemShop context;
    GameObject shopObj;
    Image buy;

    int shopQuantity;
    int shopPrice;
    public int shopItemNumber;

    bool isShopItemCheck = false;
    #endregion

    #region 설정
    [Header("설정")]
    [SerializeField] GameObject settingWindow;
    [SerializeField] GameObject blackScreen;
    [SerializeField] GameObject creditWindow;

    [SerializeField] Image bgmColor;
    [SerializeField] Image effectColor;

    [SerializeField] Button settingBtn;
    [SerializeField] Button settingCancelBtn;
    [SerializeField] Button bgmBtn;
    [SerializeField] Button effectBtn;
    [SerializeField] Button gameruleBtn;
    [SerializeField] Button creditBtn;

    public bool isCreditCheck;
    #endregion

    #region 일시정지
    [Header("일시정지")]
    [SerializeField] GameObject stopWindow;

    public Button stopBtn;
    [SerializeField] Button backBtn;
    [SerializeField] Button stopSettingBtn;
    [SerializeField] Button reGameBtn;

    public bool isStopCheck;
    #endregion

    #region 게임오버
    [Header("게임오버")]
    [SerializeField] GameObject gameOverWindow;

    [SerializeField] Button resurrectionBtn;
    [SerializeField] Button gameOverMenuBtn;

    [SerializeField] TextMeshProUGUI gameOverCoin;
    [SerializeField] TextMeshProUGUI gameOverDistance;
    #endregion

    void Start()
    {
        currentHp = maxHp;
        currentCoolTime = maxCoolTime;

        DOTween.PauseAll();
        Time.timeScale = 1;

        UI_Dot();
        UI_Btns();
        SoundSetting();
    }

    void Update()
    {
        UI_setting();
        ShopItem();
        hpBar();
        ClickItem();
    }

    void UI_Btns()
    {
        Stop_Btns();
        Main_Btns();
        Shop_Btns();
        Setting_Btns();
        GameOver_Btn();
    }

    void UI_Dot()
    {
        int move = 310;
        int touchWaitTime = 1;
        float titleWaitTime = 0.5f;

        touchToStart.DOFade(0, touchWaitTime).SetLoops(-1, LoopType.Yoyo);
        title.transform.DOLocalMoveY(move, titleWaitTime).SetEase(Ease.OutQuad).SetLoops(-1, LoopType.Yoyo);
    }

    void UI_setting()
    {
        context = itemSlot.GetComponent<ItemShop>();

        shopObj = GameObject.Find("Shop_Content");

        distanceText.text = $"{GameManager.Instance.Distance.ToString("F0")}m";

        coinText.text = coin.ToString();

        haveCoin.text = string.Format("{0:#,0}", GameManager.Instance.haveCoin);
        haveShopCoin.text = string.Format("{0:#,0}", GameManager.Instance.haveCoin);

        quantityText.text = shopQuantity.ToString();
        priceText.text = itemShop[shopItemNumber].itemPirce.ToString();
        totalPriceText.text = (itemShop[shopItemNumber].itemPirce * shopQuantity).ToString();

        purchaseItemIcon.sprite = itemShop[shopItemNumber].itemIcon;

    }

    void hpBar()
    {
        if (GameManager.Instance.IsGameStart)
        {
            hpSlider.fillAmount = Mathf.Lerp(hpSlider.fillAmount, currentHp / maxHp, Time.deltaTime * hpReductionSpeed);

            currentHp -= Time.deltaTime * hpReductionSpeed;

            if (currentHp > maxHp)
                currentHp = maxHp;
        }

        if (currentHp <= 0 && !isHPCheck)
        {
            isHPCheck = true;
            Player.Instance.IsDie = true;
        }
    }

    #region 메인버튼
    void Main_Btns()
    {
        // 상점 버튼을 눌렀을 때
        shopBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, SoundManager.instance.soundSFX);
            shopWindow.SetActive(true);
            content.transform.GetChild(0).gameObject.SetActive(true);
            isShopItemCheck = false;


            //GadgetManager.Instance.IsShopActive = true;
        });

        // 캐릭터 버튼을 눌렀을 때
        characterBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, SoundManager.instance.soundSFX);
            shopWindow.SetActive(true);
            content.transform.GetChild(1).gameObject.SetActive(true);
            //GadgetManager.Instance.IsShopActive = true;
        });

        // 가젯 버튼을 눌렀을 때
        gadgetBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, SoundManager.instance.soundSFX);
            gadgetSlots.DOAnchorPosY(-70, 0);
            shopWindow.SetActive(true);
            content.transform.GetChild(2).gameObject.SetActive(true);
            //GadgetManager.Instance.IsShopActive = true;
        });

        // 탈것 버튼을 눌렀을 때
        vehicleBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, SoundManager.instance.soundSFX);
            shopWindow.SetActive(true);
            content.transform.GetChild(3).gameObject.SetActive(true);
            //GadgetManager.Instance.IsShopActive = true;
        });
    }
    #endregion

    #region 설정 창
    void Setting_Btns()
    {
        // 설정 버튼을 눌렀을 때
        settingBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, SoundManager.instance.soundSFX);
            blackScreen.SetActive(true);

            settingWindow.transform.DOLocalMoveY(0, 0.5f).SetUpdate(true);
        });

        // 취소 버튼을 눌렀을 때
        settingCancelBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, SoundManager.instance.soundSFX);

            int settingMovePos = 1570;
            int rightMovePos = 1723;

            Sequence sequence = DOTween.Sequence();

            if (isStopCheck == false)
            {
                creditWindow.transform.DOLocalMoveX(rightMovePos, waitTime).SetUpdate(true);

                sequence.Append(settingWindow.transform.DOLocalMoveY(settingMovePos, waitTime).SetUpdate(true))
                        .OnComplete(() =>
                        {
                            blackScreen.SetActive(false);
                            settingWindow.transform.DOLocalMoveX(0, 0).SetUpdate(true);
                        });
            }

            else
            {
                settingWindow.transform.DOLocalMoveY(settingMovePos, waitTime).SetUpdate(true);
                stopWindow.transform.DOLocalMoveX(0, waitTime).SetUpdate(true);
            }
        });

        // BGM 버튼을 눌렀을 때
        bgmBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, SoundManager.instance.soundSFX);

            if (SoundManager.instance.isBGMCheck == true)
            {
                SoundManager.instance.isBGMCheck = false;

                SoundManager.instance.StopSoundClip(SoundType.BGM);
                bgmColor.DOColor(Color.gray, 0).SetUpdate(true);
                Debug.Log("BGM이 꺼졌습니다");
            }
            else
            {
                SoundManager.instance.isBGMCheck = true;

                if (GameManager.Instance.IsGameStart == false)
                    SoundManager.instance.PlaySoundClip("MainScene", SoundType.BGM);
                else
                    SoundManager.instance.PlaySoundClip("DiamondRush", SoundType.BGM);

                bgmColor.DOColor(Color.white, 0).SetUpdate(true);
                Debug.Log("BGM이 켜졌습니다.");
            }
        });

        // 효과음 버튼을 눌렀을 때
        effectBtn.onClick.AddListener(() =>
        {

            if (SoundManager.instance.isEffectCheck == true)
            {
                SoundManager.instance.isEffectCheck = false;

                SoundManager.instance.audioSourceClasses[SoundType.SFX].audioSource.volume = 0;
                effectColor.DOColor(Color.gray, 0).SetUpdate(true);
                Debug.Log("Effect가 꺼졌습니다");
            }
            else
            {
                SoundManager.instance.isEffectCheck = true;

                SoundManager.instance.audioSourceClasses[SoundType.SFX].audioSource.volume = 1;
                SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, SoundManager.instance.soundSFX);
                effectColor.DOColor(Color.white, 0).SetUpdate(true);
                Debug.Log("Effect가 켜졌습니다.");
            }
        });

        // 튜토리얼 버튼을 눌렀을 때
        gameruleBtn.onClick.AddListener(() =>
        {
            DOTween.KillAll();

            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, SoundManager.instance.soundSFX);
            SceneManager.LoadScene("Tutorial");
        });

        // 크레딧 버튼을 눌렀을 때
        creditBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, SoundManager.instance.soundSFX);

            int creditMovePos = 1723;
            int settingMovePos = -720;

            if (isCreditCheck == true)
            {
                isCreditCheck = false;
                creditWindow.transform.DOLocalMoveX(creditMovePos, waitTime).SetUpdate(true);
                settingWindow.transform.DOLocalMoveX(0, waitTime).SetUpdate(true);
            }

            else
            {
                isCreditCheck = true;
                creditWindow.transform.DOLocalMoveX(-creditMovePos, waitTime).SetUpdate(true);
                settingWindow.transform.DOLocalMoveX(settingMovePos, waitTime).SetUpdate(true);
            }
        });
    }

    void SoundSetting()
    {
        if (SoundManager.instance.isBGMCheck == false)
            bgmColor.DOColor(Color.gray, 0).SetUpdate(true);
        else
            bgmColor.DOColor(Color.white, 0).SetUpdate(true);

        if (SoundManager.instance.isEffectCheck == false)
            effectColor.DOColor(Color.gray, 0).SetUpdate(true);
        else
            effectColor.DOColor(Color.white, 0).SetUpdate(true);
    }
    #endregion

    #region 상점 아이템 쿨타임
    void ClickItem()
    {
        maxItemCount = itemShop[shopItemNumber].itemNum;
        itemCountText.text = maxItemCount.ToString();

        if (ItemShop.instance.eShopItem != EShopItem.None)
        {
            ItemShop.instance.gameObject.SetActive(true);

            if (GameManager.Instance.IsGameStart)
            {
                currentCoolTime -= Time.deltaTime * 0.1f;

                if (!isClickItemUse)
                {
                    isClickItemUse = true;
                    coolTimeSlot.fillAmount = 1;
                    currentCoolTime = maxCoolTime;

                    StartCoroutine(CoolTime());
                }
            }
        }
        else
            ItemShop.instance.gameObject.SetActive(false);
    }

    public IEnumerator CoolTime()
    {
        while (coolTimeSlot.fillAmount > 0)
        {
            coolTimeSlot.fillAmount = currentCoolTime / maxCoolTime;

            yield return null;
        }
        yield break;
    }
    #endregion

    #region 일시정지 창
    void Stop_Btns()
    {
        // 일시정지 버튼을 눌렀을 때
        stopBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, SoundManager.instance.soundSFX);

            int stopMovePos = 0;
            int settingMovePos = -720;
            int MusicMovePos = -45;
            Time.timeScale = 0;

            isStopCheck = true;
            blackScreen.SetActive(true);
            creditBtn.gameObject.SetActive(false);
            gameruleBtn.gameObject.SetActive(false);

            bgmBtn.transform.DOLocalMoveY(MusicMovePos, 0).SetUpdate(true);
            effectBtn.transform.DOLocalMoveY(MusicMovePos, 0).SetUpdate(true);

            stopWindow.transform.DOLocalMoveX(0, 0).SetUpdate(true);
            settingWindow.transform.DOLocalMoveX(settingMovePos, 0).SetUpdate(true);
            stopWindow.transform.DOLocalMoveY(stopMovePos, waitTime).SetUpdate(true);
        });

        // 돌아가기 버튼을 눌렀을 때
        backBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, SoundManager.instance.soundSFX);

            int stopMovePos = 1800;
            int settingMovePos = 1800;

            Time.timeScale = 1;

            blackScreen.SetActive(false);

            stopWindow.transform.DOLocalMoveY(stopMovePos, waitTime).SetUpdate(true);
            settingWindow.transform.DOLocalMoveY(settingMovePos, waitTime).SetUpdate(true);
        });

        // 설정 버튼을 눌렀을 때
        stopSettingBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, SoundManager.instance.soundSFX);

            int stopMovePos = 450;
            int settingMovePos = 0;

            stopWindow.transform.DOLocalMoveX(stopMovePos, waitTime).SetUpdate(true);
            settingWindow.transform.DOLocalMoveY(settingMovePos, waitTime).SetUpdate(true);
        });

        // 메뉴로 버튼을 눌렀을 때
        reGameBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, SoundManager.instance.soundSFX);

            DOTween.PauseAll();
            Time.timeScale = 1;

            GameManager.Instance.IsGameStart = false;

            SceneManager.LoadScene("Main");
        });
    }
    #endregion

    #region 상점 창
    void Shop_Btns()
    {
        //구매창 취소버튼을 눌렀을 때
        purchaseCancelBtn.onClick.AddListener(() =>
        {
            purchaseWindow.SetActive(false);
        });

        //구매확정 버튼을 눌렀을 때
        purchaseBtn.onClick.AddListener(() =>
        {
            var itemBar = shopObj.transform.GetChild(shopItemNumber).GetChild(3).GetComponent<TextMeshProUGUI>();

            if (shopQuantity > 0)
            {
                if (GameManager.Instance.haveCoin >= itemShop[shopItemNumber].itemPirce)
                {
                    Debug.Log(itemShop[shopItemNumber].itemName + "을 " + shopQuantity + "개 구매하셨습니다.");

                    GameManager.Instance.haveCoin -= itemShop[shopItemNumber].itemPirce; // 구매 금액만큼 소지금액을 차감한다.

                    // 적용버튼이 있는 상태에서 다른 아이템을 구매할 시 구매 아이템을 제외한 적용 버튼을 선택 버튼으로 변경한다.
                    for (int i = 0; i < shopObj.transform.childCount; i++)
                    {
                        var choice = shopObj.transform.GetChild(i).GetChild(2).GetComponent<Image>();

                        if (choice.sprite == selecteBtn)
                        {
                            addPurchas = shopObj.transform.GetChild(i).GetChild(0).gameObject;
                            addPurchas.SetActive(false);
                            choice.sprite = choiceBtn;
                            break;
                        }
                    }

                    context.eShopItem = itemShop[shopItemNumber].eShopItem;

                    itemSlot.sprite = itemShop[shopItemNumber].itemIcon; // 구매한 아이템을 아이템 슬롯에 넣어준다.
                    itemSlot.GetComponent<RectTransform>().DOSizeDelta(shopObj.transform.GetChild(shopItemNumber).GetChild(4).GetChild(2).GetComponent<RectTransform>().sizeDelta, 0); // 구매한 아이템의 크기와 동일하게 해준다.

                    buy.sprite = selecteBtn; // 구매 버튼을 적용 버튼으로 변경한다.

                    maxItemCount += shopQuantity;
                    itemShop[shopItemNumber].itemNum += shopQuantity;
                    itemBar.text = itemShop[shopItemNumber].itemNum.ToString();

                    // 추가 구매를 하기 위해 addPurchas를 활성화 한다.
                    addPurchas = shopObj.transform.GetChild(shopItemNumber).GetChild(0).gameObject;
                    addPurchas.SetActive(true);

                    // 구매 창을 비활성화 한다.
                    purchaseWindow.SetActive(false);
                }
                else
                    Debug.Log("해당 금액보다 돈이 없습니다.");
            }
            else
                Debug.Log("수량을 선택해주세요.");

        });

        //감소 버튼을 눌렀을 때
        reductionBtn.onClick.AddListener(() =>
        {
            int min = 0;

            if (shopQuantity > min)
                --shopQuantity;
            else
                Debug.Log("최소 수량입니다.");
        });

        //증가 버튼을 눌렀을 때
        increaseBtn.onClick.AddListener(() =>
        {
            int max = 99;
            if (shopQuantity < max)
                ++shopQuantity;
            else
                Debug.Log("최대 수량입니다.");
        });

        //상점취소 버튼을 눌렀을 때
        shopsCancelBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, SoundManager.instance.soundSFX);
            gadgetSlots.DOAnchorPosY(500, 0);
            shopWindow.SetActive(false);

            for (int i = 0; i < 4; i++)
                content.transform.GetChild(i).gameObject.SetActive(false);

        });
    }

    // 구매 버튼을 눌렀을 때
    public void PurchaseBtn(int number)
    {
        shopPrice = 0;
        shopQuantity = 0;

        shopItemNumber = number;

        buy = shopObj.transform.GetChild(shopItemNumber).GetChild(2).GetComponent<Image>();
        var selecteIcon = shopObj.transform.GetChild(shopItemNumber).GetChild(4).GetChild(2).GetComponent<Image>();
        var selecteIconSize = shopObj.transform.GetChild(shopItemNumber).GetChild(4).GetChild(2).GetComponent<RectTransform>();
        purchaseItemIcon.GetComponent<RectTransform>().DOSizeDelta(selecteIconSize.sizeDelta * 1.5f, 0);

        // 선택 이나 선택완료 버튼이 아닐 때
        if (buy.sprite != choiceBtn && buy.sprite != selecteBtn)
            purchaseWindow.SetActive(true);

        // 선텍 버튼을 눌렀을 경우 선택 완료 버튼으로 바꿔준다.
        if (buy.sprite == choiceBtn)
        {
            itemSlot.sprite = selecteIcon.sprite;
            itemSlot.GetComponent<RectTransform>().DOSizeDelta(selecteIconSize.sizeDelta, 0);

            context.eShopItem = itemShop[shopItemNumber].eShopItem;

            for (int i = 0; i < shopObj.transform.childCount; i++)
            {
                var selecte = shopObj.transform.GetChild(i).GetChild(2).GetComponent<Image>();
                addPurchas = shopObj.transform.GetChild(i).GetChild(0).gameObject;

                if (selecte.sprite == selecteBtn)
                {
                    addPurchas.SetActive(false);
                    selecte.sprite = choiceBtn;
                    break;
                }
            }
            addPurchas = shopObj.transform.GetChild(shopItemNumber).GetChild(0).gameObject;
            addPurchas.SetActive(true);

            buy.sprite = selecteBtn;
        }
    }

    public void AddPurchaseBtn()
    {
        shopPrice = 0;
        shopQuantity = 0;

        purchaseWindow.SetActive(true);
    }

    void ShopItem()
    {
        if (content.transform.GetChild(0).gameObject.activeSelf == true && isShopItemCheck == false)
        {
            isShopItemCheck = true;

            for (int i = 0; i < shopObj.transform.childCount; i++)
            {
                var shopChild = shopObj.transform.GetChild(i).GetChild(4);

                var itemName = shopChild.GetChild(0).GetComponent<TextMeshProUGUI>();
                var itemDescription = shopChild.GetChild(1).GetComponent<TextMeshProUGUI>();
                var itemIcon = shopChild.GetChild(2).GetComponent<Image>();
                var itemNum = shopObj.transform.GetChild(i).GetChild(3).GetComponent<TextMeshProUGUI>();

                itemName.text = itemShop[i].itemName.ToString();
                itemDescription.text = itemShop[i].itemDescription.ToString();
                itemIcon.sprite = itemShop[i].itemIcon;
                itemNum.text = itemShop[i].itemNum.ToString();

                if (itemShop[i].itemNum == 0)
                    shopObj.transform.GetChild(i).GetChild(2).GetComponent<Image>().sprite = buyBtn;
            }
        }
    }
    #endregion

    #region 게임오버 창
    public void GameOver()
    {
        SoundManager.instance.PlaySoundClip("GameOver", SoundType.SFX, SoundManager.instance.soundSFX + 0.5f);
        Time.timeScale = 0f;

        blackScreen.SetActive(true);

        gameOverCoin.text = $"코인 : {coin}";
        gameOverDistance.text = $"거리 : {GameManager.Instance.Distance.ToString("F0")}m";

        gameOverWindow.transform.DOLocalMoveY(0, waitTime).SetUpdate(true);
        SoundManager.instance.StopSoundClip(SoundType.BGM);
    }

    void GameOver_Btn()
    {
        gameOverMenuBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, SoundManager.instance.soundSFX);
            SoundManager.instance.StopSoundClip(SoundType.BGM);

            DOTween.PauseAll();
            Time.timeScale = 1;

            GameManager.Instance.IsGameStart = false;
            SceneManager.LoadScene("Main");
        });

        resurrectionBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, SoundManager.instance.soundSFX);
        });
    }
    #endregion

}
