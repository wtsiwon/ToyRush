using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum EITem
    {
        Transformation, // 변신
        Magnet,         // 자석
        Piggybank,      // 저금통
        Booster,        // 부스터
        Coinconverter,  // 코인변환기
        Sizecontrol,    // 크기조절
    }
    public EITem eITem;

    [Header("아이템 : 크기조절")]
    float invincibilityTimer;
    public int waitingTime;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (eITem)
        {
            case EITem.Transformation:
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    this.GetComponent<BoxCollider2D>().enabled = false;
                }
                break;

            case EITem.Magnet:
                break;

            case EITem.Piggybank:
                break;

            case EITem.Booster:
                break;

            case EITem.Coinconverter:
                break;

            case EITem.Sizecontrol:
                collision.transform.localScale *= 2.5f;

                invincibilityTimer += Time.deltaTime;
                if (invincibilityTimer < waitingTime)
                {
                    collision.GetComponent<CapsuleCollider2D>().enabled = false;
                }
                else
                {
                    collision.GetComponent<CapsuleCollider2D>().enabled = true;
                    invincibilityTimer = 0f;
                }
                break;
        }
    }
}
