 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fragments : MonoBehaviour
{
    public float flyingSpeed;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Fragments_Transform();
    }

    void Update()
    {

    }

    void Fragments_Transform()
    {
        int rotMin = 0;
        int rotMax = 90;

        float posXMin = -5.0f;
        float posXMax = 8.0f;


        transform.Rotate(0, 0, Random.Range(rotMin, rotMax));
        transform.DOLocalMoveX(Random.Range(posXMin, posXMax), flyingSpeed);

    }

    private void ByeBye()
    {
        rb.velocity = Vector3.left * BackGroundSpawner.Instance.backgroundSpd;
    }
}
