using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Camera Cam
    {
        get
        {
            return Camera.main;
        }
    }

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
            if (!value)
                distance = 0;
            else
                StartCoroutine(CSetGame());
        }
    }

    private SoundManager SoundManager;
    public int currentCoin = 0;

    private void Start()
    {
        //StartCoroutine(UpDate());
        //if (SoundManager.isBGMCheck == true)
        //    SoundManager.PlaySoundClip("MainScene", SoundType.BGM);
        //else
        //    SoundManager.StopSoundClip(SoundType.BGM);
    }

    private void Update()
    {
        Cheat();
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }
    
    /// <summary>
    /// 현재 스폰 되어있는 장애물을 파괴하는 함수
    /// </summary>
    public void DestroyObstacleOnBorder()
    {
        if(border == null)
        {
            border = GetComponentInChildren<BoxCollider2D>();
        }

        Collider2D[] cols = Physics2D.OverlapBoxAll(border.offset, border.size, 0);

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
            GameObject effect = Instantiate(EffectManager.Instance.effectList[1]);
            effect.transform.position = obstacles[i].transform.position;
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

        yield return new WaitForSeconds(1f);
    }

    private void ApplyGadgetAbility()
    {
        GadgetData[] datas = new GadgetData[2];
        for (int i = 0; i < datas.Length; i++)
        {
            datas[i] = GadgetManager.Instance.gadgetSlotList[i].Data;
            switch (datas[i].gadgetType)
            {
                case EGadgetType.None:
                    Player.Instance.GetComponent<Rigidbody2D>().gravityScale = Player.Instance.defaultGravityScale;
                    break;
                case EGadgetType.GravityBelt:
                    Player.Instance.GetComponent<Rigidbody2D>().gravityScale = Player.Instance.gravityBeltGravityScale;
                    break;
                case EGadgetType.Magnet:

                    break;
                case EGadgetType.SlowRocket:

                    break;
                case EGadgetType.XrayGoggles:

                    break;
                case EGadgetType.LifeRing:
                    
                    break;
            }
        }

        
    }

    private IEnumerator CCheckCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            //print("확인");
        }
    }

    private void Cheat()
    {
        if (Input.GetKeyDown(KeyCode.G))
            haveCoin += 10000000;
    }
}
