using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Tooltip("거리")]
    [SerializeField]
    private float distance;

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
            //BackGroundSpawner.Instance.backgroundSpd = distance / 10 + STARTSPD;
        }
    }

    [SerializeField]
    [Tooltip("거리 Text")]
    private TextMeshProUGUI distanceText;

    [SerializeField]
    private TextMeshProUGUI coinText;

    public const float STARTSPD = 200f;

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



    private void Start()
    {
        //StartCoroutine(UpDate());
    }

    private void Update()
    {
        //distanceText.text = $"{distance.ToString("F0")}m";
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator CAddDistance()
    {
        while (true)
        {
            if (isGameStart == true)
            {
                yield return new WaitForSeconds(0.01f);
                distance += BackGroundSpawner.Instance.backgroundSpd / 1000;
            }
        }
    }

    private IEnumerator CSetGame()
    {
        //MovingElementManager.Instance.BackGroundSpeedSet(STARTSPD);
        BackGroundSpawner.Instance.backgroundSpd = STARTSPD;
        yield return new WaitForSeconds(1f);
        //MovingElementManager.Instance.ObstacleSpeedSet(STARTSPD);
        ObstacleSpawner.Instance.canSpawn = true;
        StartCoroutine(CAddDistance());
    }
}
