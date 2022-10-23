using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class BackgroundTouchDot : MonoBehaviour, IPointerClickHandler
{
    public Ease easeType;

    [Header("UI 버튼")]
    public GameObject mainBtn;

    [Header("타이틀")]
    public GameObject Title;

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
        int btnDistance = -1100;
        int titleDistance = 1250;

        float time = 0.5f;
        float waitTime = 0.2f;

        UIManager.inst.touchToStart.DOKill();
        UIManager.inst.Title.transform.DOKill();

        UIManager.inst.touchToStart.DOFade(0, time);

        for (int i = 1; i <= 4; i++)
        {

            mainBtn.transform.GetChild(i).DOLocalMoveY(btnDistance, time).SetEase(easeType);
            yield return new WaitForSeconds(waitTime);

            switch (i)
            {
                case 1:
                    Title.transform.DOLocalMoveY(titleDistance, time).SetEase(easeType);
                    break;

                case 4:
                    mainBtn.transform.GetChild(0).DOLocalMoveY(btnDistance, time).SetEase(easeType);
                    break;
            }

        }
    }
}
