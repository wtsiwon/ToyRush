using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class BackgroundTouchDot : MonoBehaviour, IPointerClickHandler
{
    public GameObject mainBtn;
    public Ease easeType;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Sequence mySequence = DOTween.Sequence();
        int distance = -1100;
        float time = 0.5f;


        mySequence.Append(mainBtn.transform.GetChild(0).DOLocalMoveY(distance, time).SetEase(easeType))
                  .Append(mainBtn.transform.GetChild(1).DOLocalMoveY(distance, time).SetEase(easeType))
                  .Append(mainBtn.transform.GetChild(2).DOLocalMoveY(distance, time).SetEase(easeType))
                  .Append(mainBtn.transform.GetChild(3).DOLocalMoveY(distance, time).SetEase(easeType));


    }
}
