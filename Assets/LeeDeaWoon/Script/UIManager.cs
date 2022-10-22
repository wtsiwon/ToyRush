using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager inst;
    private void Awake() => inst = this;

    public GameObject Title;
    public TextMeshProUGUI touchToStart;

    void Start()
    {
        UI_Dot();
    }

    void Update()
    {

    }

    public void UI_Dot()
    {
        int touchWaitTime = 1;

        int Move = 310;
        float titleWaitTime = 0.5f;

        touchToStart.DOFade(0, touchWaitTime).SetLoops(-1, LoopType.Yoyo);
        Title.transform.DOLocalMoveY(Move, titleWaitTime).SetEase(Ease.OutQuad).SetLoops(-1, LoopType.Yoyo);
    }
}
