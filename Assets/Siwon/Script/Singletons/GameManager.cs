using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake() => Instance = this;

    [Tooltip("거리")]
    public int distance;

    public bool isBoosting;
    public bool isBig;

    [Tooltip("시작확인")]
    public bool isGameStart;

    [SerializeField]
    private bool gameStart;
    public bool GameStart
    {
        get
        {
            return gameStart;
        }
        set
        {
            GameStart = value;
            if (GameStart == true)
            {

            }
        }
    }

    private IEnumerator CSetGame()
    {
        yield return new WaitForSeconds(1f);

    }

    private void Start()
    {
        //StartCoroutine(UpDate());
    }

    private IEnumerator UpDate()
    {
        if(GameStart == true)
        {
            //속도가 빨라지면 점수도 빨리 오름
            distance += (int)BackGroundSpawner.Instance.backgroundSpd / 10;
        }
        yield return StartCoroutine(UpDate());
    }

}
