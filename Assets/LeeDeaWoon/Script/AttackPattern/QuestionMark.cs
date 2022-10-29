using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class QuestionMark : MonoBehaviour
{
    public GameObject questionMarkPrefab;
    public SpriteRenderer questionMarkFade;

    public GameObject bulletPrefab;

    void Start()
    {
        StartCoroutine(BulletSummon());
    }

    void Update()
    {
        
    }

    IEnumerator BulletSummon()
    {
        questionMarkPrefab.transform.DOScale(new Vector2(0.4f, 0.4f), 0.2f).SetLoops(-1, LoopType.Yoyo);

        yield return new WaitForSeconds(3f);

        questionMarkPrefab.transform.DOKill();
        questionMarkFade.DOFade(0, 0.5f);

        Instantiate(bulletPrefab, new Vector2(12, transform.position.y), Quaternion.identity);
    }
}
