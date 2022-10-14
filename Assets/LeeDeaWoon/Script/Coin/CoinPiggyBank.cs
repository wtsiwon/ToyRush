using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPiggyBank : MonoBehaviour
{
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        int SpeedRnage = Random.Range(10, 100);
        rb.AddForce(new Vector2(SpeedRnage, 0));
    }

    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
