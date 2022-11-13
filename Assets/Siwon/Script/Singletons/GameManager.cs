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

    [Tooltip("죽을 때 나오는 조각들의 Sprite")]
    public List<Sprite> piecesList = new List<Sprite>();

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
            if (value == false)
            {
                distance = 0;
            }
            else
            {
                StartCoroutine(CSetGame());
            }
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

    private IEnumerator CToDropOnePieces()
    {
        yield return new WaitForSeconds(2f);
        UIManager.Instance.GameOver();
    }

    public void OnDie(Transform pos)
    {
        ToDropPieces(pos);
        StartCoroutine(CToDropOnePieces());
    }

    private void ToDropPieces(Transform pos)
    {
        Instantiate(ItemManager.inst.whiteScreen, transform.position, Quaternion.identity);
        Player.Instance.gameObject.SetActive(false);

        for (int i = 0; i < piecesList.Count; i++)
        {
            GameObject piece = new GameObject();
            piece.transform.position = new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y, 0);
            piece.AddComponent<SpriteRenderer>();
            piece.AddComponent<CircleCollider2D>();
            piece.AddComponent<Rigidbody2D>().AddForce(new Vector2(1, 0));
            piece.GetComponent<SpriteRenderer>().sprite = piecesList[i];
            piece.transform.localScale = new Vector3(0.1f, 0.1f, 1);
        }
    }

    private IEnumerator CAddDistance()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.02f);
            if (isGameStart == true)
            {
                distance += BackGroundSpawner.Instance.backgroundSpd / 1000;
            }
        }
    }

    private IEnumerator CAddSpd()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (IsGameStart == true && distance <= 5000 && Player.Instance.IsBoosting == false)
            {
                BackGroundSpawner.Instance.backgroundSpd = STARTSPD + distance / 100;
            }
        }
    }

    private IEnumerator CSetGame()
    {
        BackGroundSpawner.Instance.backgroundSpd = STARTSPD;
        yield return new WaitForSeconds(1f);
        ObstacleSpawner.Instance.canSpawn = true;
        StartCoroutine(CAddDistance());
        StartCoroutine(CAddSpd());
    }
}
