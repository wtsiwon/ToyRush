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

    [Header("Ÿ��Ʋ")]
    public GameObject title;
    public GameObject firstBackGround;
    public Sprite brokenBackGround;
    public TextMeshProUGUI touchToStart;
    [SerializeField] TextMeshProUGUI haveCoin;

    [Header("�ΰ���")]
    public int coin;

    public RectTransform gadgetSlots;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI distanceText;

    [Header("ü��")]
    public float maxHp;
    public float currentHp;
    public float hpReductionSpeed;
    [SerializeField] Image hpSlider;

    bool isHPCheck = false;

    [Header("���� ������")]
    public float currentCoolTime;
    public float maxCoolTime;

    public Image coolTimeSlot;
    [SerializeField] Image itemSlot;
    [SerializeField] TextMeshProUGUI itemCountText;

    int maxItemCount = 0;
    public bool isClickItemUse = false;

    #region ����
    [Header("����")]
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

    #region ����
    [Header("����")]
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

    #region �Ͻ�����
    [Header("�Ͻ�����")]
    [SerializeField] GameObject stopWindow;

    public Button stopBtn;
    [SerializeField] Button backBtn;
    [SerializeField] Button stopSettingBtn;
    [SerializeField] Button reGameBtn;

    public bool isStopCheck;
    #endregion

    #region ���ӿ���
    [Header("���ӿ���")]
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

    #region ���ι�ư
    void Main_Btns()
    {
        // ���� ��ư�� ������ ��
        shopBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, SoundManager.instance.soundSFX);
            shopWindow.SetActive(true);
            content.transform.GetChild(0).gameObject.SetActive(true);
            isShopItemCheck = false;


            //GadgetManager.Instance.IsShopActive = true;
        });

        // ĳ���� ��ư�� ������ ��
        characterBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, SoundManager.instance.soundSFX);
            shopWindow.SetActive(true);
            content.transform.GetChild(1).gameObject.SetActive(true);
            //GadgetManager.Instance.IsShopActive = true;
        });

        // ���� ��ư�� ������ ��
        gadgetBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, SoundManager.instance.soundSFX);
            gadgetSlots.DOAnchorPosY(-70, 0);
            shopWindow.SetActive(true);
            content.transform.GetChild(2).gameObject.SetActive(true);
            //GadgetManager.Instance.IsShopActive = true;
        });

        // Ż�� ��ư�� ������ ��
        vehicleBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, SoundManager.instance.soundSFX);
            shopWindow.SetActive(true);
            content.transform.GetChild(3).gameObject.SetActive(true);
            //GadgetManager.Instance.IsShopActive = true;
        });
    }
    #endregion

    #region ���� â
    void Setting_Btns()
    {
        // ���� ��ư�� ������ ��
        settingBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, SoundManager.instance.soundSFX);
            blackScreen.SetActive(true);

            settingWindow.transform.DOLocalMoveY(0, 0.5f).SetUpdate(true);
        });

        // ��� ��ư�� ������ ��
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

        // BGM ��ư�� ������ ��
        bgmBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, SoundManager.instance.soundSFX);

            if (SoundManager.instance.isBGMCheck == true)
            {
                SoundManager.instance.isBGMCheck = false;

                SoundManager.instance.StopSoundClip(SoundType.BGM);
                bgmColor.DOColor(Color.gray, 0).SetUpdate(true);
                Debug.Log("BGM�� �������ϴ�");
            }
            else
            {
                SoundManager.instance.isBGMCheck = true;

                if (GameManager.Instance.IsGameStart == false)
                    SoundManager.instance.PlaySoundClip("MainScene", SoundType.BGM);
                else
                    SoundManager.instance.PlaySoundClip("DiamondRush", SoundType.BGM);

                bgmColor.DOColor(Color.white, 0).SetUpdate(true);
                Debug.Log("BGM�� �������ϴ�.");
            }
        });

        // ȿ���� ��ư�� ������ ��
        effectBtn.onClick.AddListener(() =>
        {

            if (SoundManager.instance.isEffectCheck == true)
            {
                SoundManager.instance.isEffectCheck = false;

                SoundManager.instance.audioSourceClasses[SoundType.SFX].audioSource.volume = 0;
                effectColor.DOColor(Color.gray, 0).SetUpdate(true);
                Debug.Log("Effect�� �������ϴ�");
            }
            else
            {
                SoundManager.instance.isEffectCheck = true;

                SoundManager.instance.audioSourceClasses[SoundType.SFX].audioSource.volume = 1;
                SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, SoundManager.instance.soundSFX);
                effectColor.DOColor(Color.white, 0).SetUpdate(true);
                Debug.Log("Effect�� �������ϴ�.");
            }
        });

        // Ʃ�丮�� ��ư�� ������ ��
        gameruleBtn.onClick.AddListener(() =>
        {
            DOTween.KillAll();

            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, SoundManager.instance.soundSFX);
            SceneManager.LoadScene("Tutorial");
        });

        // ũ���� ��ư�� ������ ��
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

    #region ���� ������ ��Ÿ��
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

    #region �Ͻ����� â
    void Stop_Btns()
    {
        // �Ͻ����� ��ư�� ������ ��
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

        // ���ư��� ��ư�� ������ ��
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

        // ���� ��ư�� ������ ��
        stopSettingBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, SoundManager.instance.soundSFX);

            int stopMovePos = 450;
            int settingMovePos = 0;

            stopWindow.transform.DOLocalMoveX(stopMovePos, waitTime).SetUpdate(true);
            settingWindow.transform.DOLocalMoveY(settingMovePos, waitTime).SetUpdate(true);
        });

        // �޴��� ��ư�� ������ ��
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

    #region ���� â
    void Shop_Btns()
    {
        //����â ��ҹ�ư�� ������ ��
        purchaseCancelBtn.onClick.AddListener(() =>
        {
            purchaseWindow.SetActive(false);
        });

        //����Ȯ�� ��ư�� ������ ��
        purchaseBtn.onClick.AddListener(() =>
        {
            var itemBar = shopObj.transform.GetChild(shopItemNumber).GetChild(3).GetComponent<TextMeshProUGUI>();

            if (shopQuantity > 0)
            {
                if (GameManager.Instance.haveCoin >= itemShop[shopItemNumber].itemPirce)
                {
                    Debug.Log(itemShop[shopItemNumber].itemName + "�� " + shopQuantity + "�� �����ϼ̽��ϴ�.");

                    GameManager.Instance.haveCoin -= itemShop[shopItemNumber].itemPirce; // ���� �ݾ׸�ŭ �����ݾ��� �����Ѵ�.

                    // �����ư�� �ִ� ���¿��� �ٸ� �������� ������ �� ���� �������� ������ ���� ��ư�� ���� ��ư���� �����Ѵ�.
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

                    itemSlot.sprite = itemShop[shopItemNumber].itemIcon; // ������ �������� ������ ���Կ� �־��ش�.
                    itemSlot.GetComponent<RectTransform>().DOSizeDelta(shopObj.transform.GetChild(shopItemNumber).GetChild(4).GetChild(2).GetComponent<RectTransform>().sizeDelta, 0); // ������ �������� ũ��� �����ϰ� ���ش�.

                    buy.sprite = selecteBtn; // ���� ��ư�� ���� ��ư���� �����Ѵ�.

                    maxItemCount += shopQuantity;
                    itemShop[shopItemNumber].itemNum += shopQuantity;
                    itemBar.text = itemShop[shopItemNumber].itemNum.ToString();

                    // �߰� ���Ÿ� �ϱ� ���� addPurchas�� Ȱ��ȭ �Ѵ�.
                    addPurchas = shopObj.transform.GetChild(shopItemNumber).GetChild(0).gameObject;
                    addPurchas.SetActive(true);

                    // ���� â�� ��Ȱ��ȭ �Ѵ�.
                    purchaseWindow.SetActive(false);
                }
                else
                    Debug.Log("�ش� �ݾ׺��� ���� �����ϴ�.");
            }
            else
                Debug.Log("������ �������ּ���.");

        });

        //���� ��ư�� ������ ��
        reductionBtn.onClick.AddListener(() =>
        {
            int min = 0;

            if (shopQuantity > min)
                --shopQuantity;
            else
                Debug.Log("�ּ� �����Դϴ�.");
        });

        //���� ��ư�� ������ ��
        increaseBtn.onClick.AddListener(() =>
        {
            int max = 99;
            if (shopQuantity < max)
                ++shopQuantity;
            else
                Debug.Log("�ִ� �����Դϴ�.");
        });

        //������� ��ư�� ������ ��
        shopsCancelBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, SoundManager.instance.soundSFX);
            gadgetSlots.DOAnchorPosY(500, 0);
            shopWindow.SetActive(false);

            for (int i = 0; i < 4; i++)
                content.transform.GetChild(i).gameObject.SetActive(false);

        });
    }

    // ���� ��ư�� ������ ��
    public void PurchaseBtn(int number)
    {
        shopPrice = 0;
        shopQuantity = 0;

        shopItemNumber = number;

        buy = shopObj.transform.GetChild(shopItemNumber).GetChild(2).GetComponent<Image>();
        var selecteIcon = shopObj.transform.GetChild(shopItemNumber).GetChild(4).GetChild(2).GetComponent<Image>();
        var selecteIconSize = shopObj.transform.GetChild(shopItemNumber).GetChild(4).GetChild(2).GetComponent<RectTransform>();
        purchaseItemIcon.GetComponent<RectTransform>().DOSizeDelta(selecteIconSize.sizeDelta * 1.5f, 0);

        // ���� �̳� ���ÿϷ� ��ư�� �ƴ� ��
        if (buy.sprite != choiceBtn && buy.sprite != selecteBtn)
            purchaseWindow.SetActive(true);

        // ���� ��ư�� ������ ��� ���� �Ϸ� ��ư���� �ٲ��ش�.
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

    #region ���ӿ��� â
    public void GameOver()
    {
        SoundManager.instance.PlaySoundClip("GameOver", SoundType.SFX, SoundManager.instance.soundSFX + 0.5f);
        Time.timeScale = 0f;

        blackScreen.SetActive(true);

        gameOverCoin.text = $"���� : {coin}";
        gameOverDistance.text = $"�Ÿ� : {GameManager.Instance.Distance.ToString("F0")}m";

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
