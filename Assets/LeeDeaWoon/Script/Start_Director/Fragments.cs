 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fragments : MonoBehaviour
{
    public float flyingSpeed;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine("Fragments_Transform");
    }

    void Update()
    {

    }

    IEnumerator Fragments_Transform()
    {
        int waitTime = 3;

        int rotMin = 0;
        int rotMax = 90;

        float posXMin = 5.0f;
        float posXMax = 10.0f;


        transform.Rotate(0, 0, Random.Range(rotMin, rotMax));
        transform.DOLocalMoveX(Random.Range(posXMin, posXMax), flyingSpeed);

        yield return new WaitForSeconds(waitTime);

        spriteRenderer.DOFade(0, 3);
    }
}