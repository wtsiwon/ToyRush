using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class Tutorial : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject descriptionBar;
    public bool isNextCheck;
    public int nextNum;

    void Start()
    {

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
        descriptionBar.transform.GetChild(nextNum).GetComponent<TextMeshProUGUI>().text = "잘하셨습니다";

        yield return new WaitForSeconds(2);

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
