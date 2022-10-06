using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class Background_TouchDot : MonoBehaviour, IPointerClickHandler
{
    public GameObject Main_Btn;
    public Ease EaseType;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Sequence MySequence = DOTween.Sequence();
        int distance = -1100;
        float Time = 0.5f;

        MySequence.Append(Main_Btn.transform.GetChild(0).DOLocalMoveY(distance, Time).SetEase(EaseType))
                  .Append(Main_Btn.transform.GetChild(1).DOLocalMoveY(distance, Time).SetEase(EaseType))
                  .Append(Main_Btn.transform.GetChild(2).DOLocalMoveY(distance, Time).SetEase(EaseType))
                  .Append(Main_Btn.transform.GetChild(3).DOLocalMoveY(distance, Time).SetEase(EaseType))
                  .Append(Main_Btn.transform.GetChild(4).DOLocalMoveY(distance, Time).SetEase(EaseType));
    }
}
