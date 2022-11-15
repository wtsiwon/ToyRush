using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : MovingElement
{
    SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Invincibility"))
        {
            SoundManager.instance.PlaySoundClip("Coin", SoundType.SFX, SoundManager.instance.soundSFX - 0.8f);

            GameObject director = Instantiate(ItemManager.inst.piggybankDirector, Vector2.zero, Quaternion.identity);
            director.transform.SetParent(gameObject.transform, false);
            director.transform.position = new Vector3(director.transform.position.x,
                director.transform.position.y, 0);

            spriteRenderer.DOFade(0, 0);

            if (Player.Instance.vehicleType == EVehicleType.ProfitUFO)
            {
                UIManager.Instance.coin += 2;
                GameManager.Instance.haveCoin += 2;
            }
            else
            {
                UIManager.Instance.coin += 1;
                GameManager.Instance.haveCoin += 1;
            }

            yield return new WaitForSeconds(1f);

            transform.DOKill();
            Destroy(gameObject);

            Return();
        }

    }
}
