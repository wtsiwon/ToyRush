using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WhiteScreen : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeInOut());
    }

    void Update()
    {
        
    }

    IEnumerator FadeInOut()
    {
        spriteRenderer.DOFade(1, 0.1f).OnComplete(() =>
        {
            spriteRenderer.DOFade(0, 0.1f);
        });

        yield return new WaitForSeconds(0.5f);

        Destroy(this.gameObject);
    }
}
