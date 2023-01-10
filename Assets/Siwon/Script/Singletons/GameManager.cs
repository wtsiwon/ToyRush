using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

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

        }
    }

    public const float STARTSPD = 5f;

    [Space(10f)]
    public float spd;

    [Tooltip("죽을 때 나오는 조각들의 Sprite")]
    [Space(10f)]
    public List<GameObject> piecesList = new List<GameObject>();

    [SerializeField]
    private BoxCollider2D border;

    [Tooltip("시작확인")]
    [Space(10f)]
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

                if (SoundManager.isBGMCheck == true)
                    SoundManager.PlaySoundClip("MainScene", SoundType.BGM);
                else
                    SoundManager.StopSoundClip(SoundType.BGM);
            }
            else
            {
                print(isGameStart + "isGameStart");
                StartCoroutine(CSetGame());
            }
        }
    }

    private SoundManager SoundManager;

    private void Start()
    {
        SoundManager = SoundManager.instance;

        //StartCoroutine(UpDate());
        if (SoundManager.isBGMCheck == true)
            SoundManager.PlaySoundClip("MainScene", SoundType.BGM);
        else
            SoundManager.StopSoundClip(SoundType.BGM);
    }

    private void Update()
    {

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

    public void DestroyObstacleOnBorder()
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(Vector2.zero, border.size, 0);

        List<Obstacle> obstacles = new List<Obstacle>();

        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].TryGetComponent(out Obstacle obstacle))
            {
                obstacles.Add(obstacle);
            }
        }

        for (int i = 0; i < obstacles.Count; i++)
        {
            Destroy(obstacles[i].gameObject);
        }
    }



    private IEnumerator CToDropOnePieces()
    {
        yield return new WaitForSeconds(2f);
        UIManager.Instance.GameOver();
    }

    public void OnDie(Transform pos)
    {
        ToDropPieces();
        StartCoroutine(CToDropOnePieces());
    }

    private void ToDropPieces()
    {
        UIManager.Instance.stopBtn.transform.DOMoveY(500, 0);
        Instantiate(ItemManager.inst.whiteScreen, transform.position, Quaternion.identity);
        Player.Instance.gameObject.SetActive(false);

        for (int i = 0; i < piecesList.Count; i++)
        {
            Instantiate(piecesList[i], Player.Instance.transform.position, Quaternion.identity);
            piecesList[i].transform.DOLocalMoveX(transform.position.x + Random.Range(0, 3), 0.5f);
        }
    }

    private IEnumerator CAddDistance()
    {
        var wait = new WaitForSeconds(0.01f);

        while (true)
        {
            yield return wait;

            if (isGameStart == true)
            {
                distance += spd / 20;
                if (Player.Instance.IsBoosting == false && distance <= 3500f)
                {
                    spd = STARTSPD + (distance / 500);
                    Mathf.Clamp(spd, 1f, 50f);

                }
            }
        }
    }

    private IEnumerator CSetGame()
    {
        StopCoroutine(nameof(CAddDistance));
        spd = STARTSPD;
        distance = 0;
        yield return new WaitForSeconds(0.1f);

        MovingElementSpawner.Instance.isSpawn = true;
        StartCoroutine(nameof(CAddDistance));
        StartCoroutine(CCheckCoroutine());
        print(spd);
        print(distance);

        if (SoundManager.isBGMCheck == true)
        {
            SoundManager.StopSoundClip(SoundType.BGM);
            SoundManager.PlaySoundClip("DiamondRush", SoundType.BGM);
        }
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator CCheckCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            print("확인");
        }
    }
}
