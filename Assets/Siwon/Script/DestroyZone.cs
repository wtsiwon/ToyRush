using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;
        if (tag == "Obstacle" || tag == "Item" || tag == "Coin")
        {
            Destroy(collision.gameObject);
        }
    }
}
