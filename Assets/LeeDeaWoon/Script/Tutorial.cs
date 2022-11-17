using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour, IPointerClickHandler
{
    public GameObject canvas;
    public int nextNum;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData) => TutorialNext();

    void TutorialNext()
    {
        if (nextNum < 4)
        {
            nextNum += 1;

            canvas.transform.GetChild(nextNum-1).gameObject.SetActive(false);
            canvas.transform.GetChild(nextNum).gameObject.SetActive(true);
        }

        else
            SceneManager.LoadScene("Main");
    }


}
