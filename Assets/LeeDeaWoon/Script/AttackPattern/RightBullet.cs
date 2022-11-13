using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RightBullet : MonoBehaviour
{
    public int speed;
    private Rigidbody2D rb2D;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        SoundManager.instance.PlaySoundClip("Attack", SoundType.SFX, 1f);
    }

    void Update()
    {
        BulletMove();
    }

    void BulletMove()
    {
        rb2D.velocity = Vector2.left * speed * Time.deltaTime;

        if (transform.position.x < -11)
            Destroy(this.gameObject);
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Player.Instance.IsBig == true)
            {
                SoundManager.instance.PlaySoundClip("Fragments", SoundType.SFX, 1f);

                GameObject director = Instantiate(ItemManager.inst.piggybankDirector, Vector2.zero, Quaternion.identity);
                director.transform.SetParent(gameObject.transform, false);
                spriteRenderer.DOFade(0, 0);

                yield return new WaitForSeconds(1f);
            }

            transform.DOKill();
            Destroy(this.gameObject);
        }

    }
}
