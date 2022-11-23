using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class Tutorial : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public TextMeshProUGUI descriptionText;


    void Start()
    {
        GameManager.Instance.IsGameStart = true;
    }

    void Update()
    {

    }

    void Description_Box()
    {

    }

    #region 플레이어 이동
    public void OnPointerDown(PointerEventData eventData) => Player.Instance.isPressing = true;

    public void OnPointerUp(PointerEventData eventData) => Player.Instance.isPressing = false;
    #endregion
}
