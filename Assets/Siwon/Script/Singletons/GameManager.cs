using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake() => Instance = this;

    [Tooltip("거리")]
    [SerializeField]
    private float distance;

    public int coin;
    public int haveCoin;

    public float Distance
    {
        get
        {
            return distance;
        }
        set
        {
            distance = value;
            BackGroundSpawner.Instance.backgroundSpd = distance / 10 + STARTSPD;
        }
    }

    [SerializeField]
    [Tooltip("거리 Text")]
    private TextMeshProUGUI distanceText;

    [SerializeField]
    private TextMeshProUGUI coinText;

    public const float STARTSPD = 1000f;

    [Tooltip("시작확인")]
    [SerializeField]
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

    private void Update()
    {
        distanceText.text = $"{distance}m";

    }
    private void Start()
    {
        //StartCoroutine(UpDate());
    }

    private IEnumerator CAddDistance()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            distance += BackGroundSpawner.Instance.backgroundSpd / 1000;
        }
    }

    private IEnumerator CSetGame()
    {
        MovingElementManager.Instance.BackGroundSpeedSet(STARTSPD);
        BackGroundSpawner.Instance.backgroundSpd = STARTSPD;
        yield return new WaitForSeconds(1f);
        MovingElementManager.Instance.ObstacleSpeedSet(STARTSPD);
        ObstacleSpawner.Instance.canSpawn = true;
        StartCoroutine(CAddDistance());
    }


    private IEnumerator UpDate()
    {
        if (isGameStart == true)
        {
            //속도가 빨라지면 점수도 빨리 오름
            distance += (int)BackGroundSpawner.Instance.backgroundSpd / 10;
        }
        yield return StartCoroutine(UpDate());
    }

}
