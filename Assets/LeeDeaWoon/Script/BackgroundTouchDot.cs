using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackgroundTouchDot : MonoBehaviour, IPointerClickHandler
{
    public Ease easeType;

    [Header("화면 클릭")]
    public Image screenClick;
    public Image screenPrevent;

    [Header("타이틀")]
    public GameObject title;
    public GameObject haveCoin;
    public GameObject stopBtn;
    public GameObject mainBtn;
    public GameObject settingBtn;

    [Header("인게임")]
    public GameObject coinDistance;

    [Header("시작 연출")]
    public GameObject smokeBoomb;

    [Header("플레이어")]
    public GameObject player;
    public float playerDistance;
    public float playerWaitTime;

    bool isStartClick = false;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine("BtnMove");
    }

    public IEnumerator BtnMove()
    {
        if (isStartClick == false)
        {
            int titleDistance = 1250;
            int mainBtnDistance = -1100;

            Vector2 settingPos = settingBtn.transform.localPosition;
            Vector2 stopPos = stopBtn.transform.localPosition;

            float time = 0.5f;
            float waitTime = 0.2f;

            isStartClick = true;
            screenPrevent.raycastTarget = true;
            screenClick.raycastTarget = false;

            UIManager.inst.touchToStart.DOKill();
            UIManager.inst.title.transform.DOKill();

            UIManager.inst.touchToStart.DOFade(0, time);

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
                        coinDistance.transform.DOLocalMoveY(0, time).SetEase(easeType);
                        mainBtn.transform.GetChild(0).DOLocalMoveY(mainBtnDistance, time).SetEase(easeType);
                        break;
                }
            }

            yield return new WaitForSeconds(1);
            smokeBoomb.SetActive(true);
            player.transform.DOLocalMoveX(playerDistance, playerWaitTime);
            yield return new WaitForSeconds(playerWaitTime);
            GameManager.Instance.IsGameStart = true;
        }
    }
}
