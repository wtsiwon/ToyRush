using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackgroundTouchDot : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public Ease easeType;

    [Header("화면 클릭")]
    [SerializeField] Image screenClick;
    [SerializeField] Image screenPrevent;

    [Header("타이틀")]
    [SerializeField] GameObject title;
    [SerializeField] GameObject haveCoin;
    [SerializeField] GameObject stopBtn;
    [SerializeField] GameObject mainBtn;
    [SerializeField] GameObject settingBtn;

    [Header("인게임")]
    [SerializeField] GameObject ingame;
    [SerializeField] GameObject itemSlot;

    [Header("시작 연출")]
    [SerializeField] GameObject smokeBoomb;

    const float playerDistance = 5;
    const float playerWaitTime = 2;
    bool isStartClick = false;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(BtnMove());
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameManager.Instance.IsGameStart == true)
            Player.Instance.isPressing = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (GameManager.Instance.IsGameStart == true)
            Player.Instance.isPressing = false;
    }

    public IEnumerator BtnMove()
    {
        if (!isStartClick)
        {
            int titleDistance = 1500;
            int mainBtnDistance = -1100;

            Vector2 settingPos = settingBtn.transform.localPosition;
            Vector2 stopPos = stopBtn.transform.localPosition;

            float time = 0.5f;
            float waitTime = 0.2f;

            isStartClick = true;
            screenPrevent.raycastTarget = true;
            screenClick.raycastTarget = false;

            UIManager.Instance.touchToStart.DOKill();
            UIManager.Instance.title.transform.DOKill();

            UIManager.Instance.touchToStart.DOFade(0, time).SetEase(Ease.Linear);

            for (int i = 1; i <= 4; i++)
            {
                mainBtn.transform.GetChild(i).DOLocalMoveY(mainBtnDistance, time).SetEase(easeType);
                yield return new WaitForSeconds(waitTime);

                switch (i)
                {
                    case 1:
                        title.transform.DOLocalMoveY(titleDistance, time).SetEase(easeType);
                        settingBtn.transform.DOLocalMove(stopPos, time).SetEase(easeType);
                        haveCoin.transform.DOLocalMoveY(stopPos.y, time).SetEase(easeType);
                        break;

                    case 4:
                        stopBtn.transform.DOLocalMove(settingPos, time).SetEase(easeType);
                        ingame.transform.DOLocalMoveY(-100, time).SetEase(easeType);
                        itemSlot.transform.DOLocalMoveY(-500, time).SetEase(easeType);
                        mainBtn.transform.GetChild(0).DOLocalMoveY(mainBtnDistance, time).SetEase(easeType);
                        UIManager.Instance.gadgetSlots.DOAnchorPosY(-70, time).SetEase(easeType);
                        break;
                }
            }

            yield return new WaitForSeconds(1);

            smokeBoomb.SetActive(true);
            UIManager.Instance.firstBackGround.GetComponent<SpriteRenderer>().sprite
                = UIManager.Instance.brokenBackGround;
            Player.Instance.transform.DOLocalMoveX(-playerDistance, playerWaitTime).SetEase(Ease.Linear);

            yield return new WaitForSeconds(playerWaitTime);
            screenPrevent.raycastTarget = false;
            screenClick.raycastTarget = true;
            GameManager.Instance.IsGameStart = true;
        }
    }
}
