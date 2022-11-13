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
    private void Awake() => Instance = this;

    private float waitTime = 0.5f;

    [Header("타이틀")]
    public GameObject title;
    public TextMeshProUGUI touchToStart;
    public TextMeshProUGUI haveCoin;
    public GameObject firstBackGround;
    public Sprite brokenBackGround;

    [Header("인게임")]
    public int coin;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI distanceText;

    #region 상점
    [Header("상점")]
    public GameObject shopWindow;
    public GameObject content;

    public Button shopBtn;
    public Button characterBtn;
    public Button gadgetBtn;
    public Button vehicleBtn;
    public Button shopsCancelBtn;
    #endregion

    #region 설정
    [Header("설정")]
    public GameObject settingWindow;
    public GameObject blackScreen;
    public GameObject gameruleWindow;
    public GameObject creditWindow;

    public Image bgmColor;
    public Image effectColor;

    public Button settingBtn;
    public Button settingCancelBtn;
    public Button bgmBtn;
    public Button effectBtn;
    public Button gameruleBtn;
    public Button creditBtn;

    public bool isRuleCheck;
    public bool isCreditCheck;
    #endregion

    #region 일시정지
    [Header("일시정지")]
    public GameObject stopWindow;

    public Button stopBtn;
    public Button backBtn;
    public Button stopSettingBtn;
    public Button reGameBtn;

    public bool isStopCheck;
    #endregion

    #region 게임오버
    [Header("게임오버")]
    public GameObject gameOverWindow;

    public Button gameOverMenuBtn;

    public TextMeshProUGUI gameOverCoin;
    public TextMeshProUGUI gameOverDistance;
    #endregion

    void Start()
    {
        UI_Dot();
        Stop_Btns();
        Main_Btns();
        Setting_Btns();
        GameOver_Btn();

        if (SoundManager.instance.isBGMCheck == false)
            bgmColor.DOColor(Color.gray, 0).SetUpdate(true);
        else
            bgmColor.DOColor(Color.white, 0).SetUpdate(true);

        if (SoundManager.instance.isEffectCheck == false)
            effectColor.DOColor(Color.gray, 0).SetUpdate(true);
        else
            effectColor.DOColor(Color.white, 0).SetUpdate(true);
    }

    void Update()
    {
        distanceText.text = $"{GameManager.Instance.Distance.ToString("F0")}m";

        coinText.text = coin.ToString();
        haveCoin.text = GameManager.Instance.haveCoin.ToString();

    }

    public void UI_Dot()
    {
        int move = 310;
        int touchWaitTime = 1;
        float titleWaitTime = 0.5f;

        touchToStart.DOFade(0, touchWaitTime).SetLoops(-1, LoopType.Yoyo);
        title.transform.DOLocalMoveY(move, titleWaitTime).SetEase(Ease.OutQuad).SetLoops(-1, LoopType.Yoyo);
    }

    #region 메인버튼
    public void Main_Btns()
    {
        // 상점 버튼을 눌렀을 때
        shopBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, 0.5f);
            shopWindow.SetActive(true);
            content.transform.GetChild(0).gameObject.SetActive(true);
        });

        // 캐릭터 버튼을 눌렀을 때
        characterBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, 1);
            shopWindow.SetActive(true);
            content.transform.GetChild(1).gameObject.SetActive(true);
        });

        // 가젯 버튼을 눌렀을 때
        gadgetBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, 1);
            shopWindow.SetActive(true);
            content.transform.GetChild(2).gameObject.SetActive(true);
        });

        // 탈것 버튼을 눌렀을 때
        vehicleBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, 1);
            shopWindow.SetActive(true);
            content.transform.GetChild(3).gameObject.SetActive(true);
        });

        //상점취소 버튼을 눌렀을 때
        shopsCancelBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, 1);
            shopWindow.SetActive(false);

            for (int i = 0; i < 4; i++)
                content.transform.GetChild(i).gameObject.SetActive(false);

        });
    }


    #endregion

    #region 설정 창
    public void Setting_Btns()
    {
        // 설정 버튼을 눌렀을 때
        settingBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, 1);
            blackScreen.SetActive(true);

            settingWindow.transform.DOLocalMoveY(0, 0.5f).SetUpdate(true);
        });

        // 취소 버튼을 눌렀을 때
        settingCancelBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, 1);

            int settingMovePos = 1570;
            int rightMovePos = 1723;

            Sequence sequence = DOTween.Sequence();

            if (isStopCheck == false)
            {
                gameruleWindow.transform.DOLocalMoveX(rightMovePos, waitTime).SetUpdate(true);
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
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, 1);

            if (SoundManager.instance.isBGMCheck == true)
            {
                SoundManager.instance.isBGMCheck = false;

                SoundManager.instance.audioSourceClasses[SoundType.BGM].audioSource.volume = 0;
                bgmColor.DOColor(Color.gray, 0).SetUpdate(true);
                Debug.Log("BGM이 꺼졌습니다");
            }
            else
            {
                SoundManager.instance.isBGMCheck = true;

                SoundManager.instance.audioSourceClasses[SoundType.BGM].audioSource.volume = 1;
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
                SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, 1);
                effectColor.DOColor(Color.white, 0).SetUpdate(true);
                Debug.Log("Effect가 켜졌습니다.");
            }
        });

        // 게임규칙 버튼을 눌렀을 때
        gameruleBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, 1);

            int gameRuleMovePos = 1723;
            int settingMovePos = -720;

            if (isRuleCheck == true)
            {
                if (isCreditCheck == false)
                    settingWindow.transform.DOLocalMoveX(0, waitTime).SetUpdate(true);

                isRuleCheck = false;
                gameruleWindow.transform.DOLocalMoveX(gameRuleMovePos, waitTime).SetUpdate(true);
            }

            else
            {
                settingWindow.transform.DOLocalMoveX(settingMovePos, waitTime).SetUpdate(true);
                gameruleWindow.transform.DOLocalMoveX(-gameRuleMovePos, waitTime).SetUpdate(true);
                isRuleCheck = true;
            }
        });

        // 크레딧 버튼을 눌렀을 때
        creditBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, 1);

            int creditMovePos = 1723;
            int settingMovePos = -720;

            if (isCreditCheck == true)
            {
                if (isRuleCheck == false)
                    settingWindow.transform.DOLocalMoveX(0, waitTime).SetUpdate(true);

                isCreditCheck = false;
                creditWindow.transform.DOLocalMoveX(creditMovePos, waitTime).SetUpdate(true);
            }

            else
            {
                isCreditCheck = true;
                creditWindow.transform.DOLocalMoveX(-creditMovePos, waitTime).SetUpdate(true);
                settingWindow.transform.DOLocalMoveX(settingMovePos, waitTime).SetUpdate(true);
            }
        });
    }
    #endregion

    #region 일시정지 창
    public void Stop_Btns()
    {
        // 일시정지 버튼을 눌렀을 때
        stopBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, 1);

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
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, 1);

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
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, 1);

            int stopMovePos = 450;
            int settingMovePos = 0;

            stopWindow.transform.DOLocalMoveX(stopMovePos, waitTime).SetUpdate(true);
            settingWindow.transform.DOLocalMoveY(settingMovePos, waitTime).SetUpdate(true);
        });

        // 메뉴로 버튼을 눌렀을 때
        reGameBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, 1);

            DOTween.PauseAll();
            Time.timeScale = 1;

            GameManager.Instance.IsGameStart = false;

            SceneManager.LoadScene("Main");
        });
    }
    #endregion

    #region 게임오버 창
    public void GameOver()
    {
        SoundManager.instance.PlaySoundClip("GameOver", SoundType.SFX, 1.5f);
        Time.timeScale = 0f;

        blackScreen.SetActive(true);

        gameOverCoin.text = $"코인 : {coin}";
        gameOverDistance.text = $"거리 : {GameManager.Instance.Distance.ToString("F0")}m";

        gameOverWindow.transform.DOLocalMoveY(0, waitTime).SetUpdate(true);
        GameManager.Instance.IsGameStart = false;
    }

    void GameOver_Btn()
    {
        gameOverMenuBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySoundClip("ButtonClick", SoundType.SFX, 1);

            DOTween.PauseAll();
            Time.timeScale = 1;

            GameManager.Instance.IsGameStart = false;
            SceneManager.LoadScene("Main");
        });
    }
    #endregion
}
