using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake() => Instance = this;

    [Tooltip("거리")]
    public int distance;

    public int coin;

    public const float STARTSPD = 8f;

    [Tooltip("시작확인")]
    private bool isGameStart;
    public bool IsGameStart
    {
        get
        {
            return isGameStart;
        }
        set
        {
            isGameStart = value;
            StartCoroutine(CSetGame());
        }
    }

    

    private IEnumerator CSetGame()
    {
        yield return new WaitForSeconds(1f);
        MovingElementManager.Instance.BackGroundSpeedSet(STARTSPD);
        BackGroundSpawner.Instance.backgroundSpd = STARTSPD;
        yield return new WaitForSeconds(3f);
        MovingElementManager.Instance.ObstacleSpeedSet(STARTSPD);
    }

    private void Start()
    {
        //StartCoroutine(UpDate());
    }

    private IEnumerator UpDate()
    {
        if(isGameStart == true)
        {
            //속도가 빨라지면 점수도 빨리 오름
            distance += (int)BackGroundSpawner.Instance.backgroundSpd / 10;
        }
        yield return StartCoroutine(UpDate());
    }

}
