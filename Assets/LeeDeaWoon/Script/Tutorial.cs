using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Tutorial : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isNextCheck;
    public int nextNum;
    public GameObject descriptionBar;

    void Start()
    {
        Time.timeScale = 0;
        GameManager.Instance.IsGameStart = true;
    }

    void Update()
    {
        Description_Box();
    }

    void Description_Box()
    {
        if (isNextCheck == true)
        {
            isNextCheck = false;
            descriptionBar.transform.GetChild(nextNum - 1).gameObject.SetActive(false);
            descriptionBar.transform.GetChild(nextNum).gameObject.SetActive(true);
        }
    }

    public IEnumerator NextDescription()
    {
        Time.timeScale = 1;

        yield return new WaitForSeconds(0.5f);

        if (nextNum == 0)
            Time.timeScale = 0;

        nextNum += 1;
        isNextCheck = true;

    }

    #region 플레이어 이동
    public void OnPointerDown(PointerEventData eventData)
    {
        if (nextNum == 0)
            StartCoroutine(NextDescription());

        Player.Instance.isPressing = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (nextNum == 1)
            StartCoroutine(NextDescription());

        Player.Instance.isPressing = false;
    }
    #endregion
}
