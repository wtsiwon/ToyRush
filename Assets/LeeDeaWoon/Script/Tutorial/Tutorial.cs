using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class Tutorial : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [SerializeField] GameObject obstacle;
    [SerializeField] GameObject enemyAttack;

    [SerializeField] GameObject descriptionBar;
    [SerializeField] int nextNum;

    [SerializeField] bool isNextCheck;
    [SerializeField] bool isNumberCheck;

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

    IEnumerator NextDescription()
    {
        if (isNumberCheck == false)
        {
            isNumberCheck = true;

            descriptionBar.transform.GetChild(nextNum).GetComponent<TextMeshProUGUI>().text = "잘하셨습니다.";

            yield return new WaitForSeconds(2);

            nextNum += 1;
            isNextCheck = true;
            isNumberCheck = false;

            switch (nextNum)
            {
                case 2:
                    Obstacle();
                    break;

                case 3:
                    StartCoroutine(EnemyAttack());
                    break;
            }
        }
    }

    void Obstacle()
    {
        obstacle.SetActive(true);
        obstacle.transform.DOMoveX(20, 0);
        Player.Instance.IsDie = false;

        obstacle.transform.DOMoveX(-14, 5).SetEase(Ease.Linear).OnComplete(() =>
        {
            if (Player.Instance.IsDie == true)
            {
                descriptionBar.transform.GetChild(nextNum).GetComponent<TextMeshProUGUI>().text = "다시 한 번 해봅시다.";
                Obstacle();
            }

            else
            {
                Destroy(obstacle);
                StartCoroutine(NextDescription());
            }
        });
    }

    IEnumerator EnemyAttack()
    {
        Player.Instance.IsDie = false;

        GameObject attack = Instantiate(enemyAttack, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(7);

        if (Player.Instance.IsDie == true)
        {
            descriptionBar.transform.GetChild(nextNum).GetComponent<TextMeshProUGUI>().text = "다시 한 번 해봅시다.";

            DOTween.KillAll();
            Destroy(attack);
            yield return new WaitForSeconds(1);

            StartCoroutine(EnemyAttack());
        }

        else
        {
            DOTween.KillAll();
            Destroy(attack);

            yield return new WaitForSeconds(1);

            StartCoroutine(NextDescription());
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (nextNum == 4)
            SceneManager.LoadScene("Main");

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
