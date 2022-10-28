using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    public static UIManager inst;
    private void Awake() => inst = this;

    [Header("타이틀")]
    public GameObject Title;
    public TextMeshProUGUI touchToStart;

    [Header("설정")]
    public GameObject settingWindow;
    public GameObject blackScreen;
    public GameObject gameruleWindow;
    public GameObject creditWindow;

    public Image blackScreenTarget;
    public Image bgmColor;
    public Image effectColor;

    public bool isBGMCheck;
    public bool isEffectCheck;
    public bool isRuleCheck;
    public bool isCreditCheck;


    void Start()
    {
        UI_Dot();

        isBGMCheck = true;
        isEffectCheck = true;
    }

    void Update()
    {

    }

    public void UI_Dot()
    {
        int touchWaitTime = 1;

        int move = 310;
        float titleWaitTime = 0.5f;

        touchToStart.DOFade(0, touchWaitTime).SetLoops(-1, LoopType.Yoyo);
        Title.transform.DOLocalMoveY(move, titleWaitTime).SetEase(Ease.OutQuad).SetLoops(-1, LoopType.Yoyo);
    }


    #region 설정 창
    public void Setting_Btn()
    {
        blackScreen.SetActive(true);
        blackScreenTarget.raycastTarget = true;

        settingWindow.transform.DOLocalMoveY(0, 0.5f);
    }

    public void Setting_Cancel()
    {
        int settingMovePos = 1570;
        int rightMovePos = 1723;
        float waitTime = 0.5f;

        blackScreen.SetActive(false);
        blackScreenTarget.raycastTarget = false;

        settingWindow.transform.DOLocalMoveY(settingMovePos, waitTime);
        gameruleWindow.transform.DOLocalMoveX(rightMovePos, waitTime);
        creditWindow.transform.DOLocalMoveX(rightMovePos, waitTime);
    }

    public void Setting_BGM()
    {
        if (isBGMCheck == true)
        {
            isBGMCheck = false;
            bgmColor.DOColor(Color.gray, 0);
            Debug.Log("BGM이 꺼졌습니다");
        }
        else
        {
            isBGMCheck = true;
            bgmColor.DOColor(Color.white, 0);
            Debug.Log("BGM이 켜졌습니다.");
        }
    }

    public void Setting_Effect()
    {
        if (isEffectCheck == true)
        {
            isEffectCheck = false;
            effectColor.DOColor(Color.gray, 0);
            Debug.Log("Effect가 꺼졌습니다");
        }
        else
        {
            isEffectCheck = true;
            effectColor.DOColor(Color.white, 0);
            Debug.Log("Effect가 켜졌습니다.");
        }
    }

    public void Setting_GameRule()
    {
        int MovePos = 1723;
        float waitTime = 0.5f;

        if (isRuleCheck == true)
        {
            gameruleWindow.transform.DOLocalMoveX(MovePos, waitTime);
            isRuleCheck = false;
        }
        
        else
        {
            gameruleWindow.transform.DOLocalMoveX(-MovePos, waitTime);
            isRuleCheck = true;
        }
    }

    public void Setting_Credit()
    {
        int MovePos = 1723;
        float waitTime = 0.5f;

        if (isCreditCheck == true)
        {
            isCreditCheck = false;
            creditWindow.transform.DOLocalMoveX(MovePos, waitTime);
        }

        else
        {
            isCreditCheck = true;
            creditWindow.transform.DOLocalMoveX(-MovePos, waitTime);
        }
    }
    #endregion

    public void Stop_Btn()
    {

    }
}
