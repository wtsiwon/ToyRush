using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterItem : AbstractItem
{
    [Header("아이템 : 부스터")]
    public float boosterDuration; // 지속시간
    public float boosterSpeed; // 속력
    public Ease ease;

    protected override IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (Player.Instance.IsBoosting == false)
        {
            float playerXValue = collision.transform.position.x;

            Player.Instance.IsBoosting = true;
            ItemManager.inst.isItemTouch = true;
            Player.Instance.tag = "Invincibility";

            #region 연출시간
            Instantiate(ItemManager.inst.smallDirector, Vector2.zero, Quaternion.identity).transform.SetParent(Player.Instance.transform, false);
            GameObject director = Instantiate(ItemManager.inst.bigDirector, Vector2.zero, Quaternion.identity);
            director.transform.SetParent(Player.Instance.transform, false);

            director.transform.DOScale(Vector2.zero, 2f);

            SpriteRenderer boosterSprite = director.GetComponent<SpriteRenderer>();
            boosterSprite.DOFade(0, 2f);
            #endregion

            SoundManager.instance.PlaySoundClip("ChangeBooster", SoundType.SFX, SoundManager.instance.soundSFX);
            Camera.main.transform.DOMoveX(3, 2)
                      .OnComplete(() =>
                      {
                          director.transform.DOKill();
                          boosterSprite.DOKill();

                          Destroy(director);

                          SoundManager.instance.PlaySoundClip("IsBooster", SoundType.SFX, SoundManager.instance.soundSFX);
                          Camera.main.transform.DOMoveX(0, 0.4f).OnComplete(() =>
                          {
                              Camera.main.transform.DOShakePosition(3, new Vector2(0.3f, 0.3f));
                          });

                          ItemManager.inst.boosterNumber = 3;

                          Instantiate(ItemManager.inst.whiteScreen, Vector2.zero, Quaternion.identity);
                          collision.transform.DOLocalMoveX(-3.5f, boosterSpeed);
                      });
            yield return new WaitForSeconds(boosterDuration); // 지속시간

            Player.Instance.transform.DOLocalMoveX(5.5f, 0);
            collision.transform.DOLocalMoveX(playerXValue, boosterSpeed);

            ItemManager.inst.isItemTouch = false;
            Player.Instance.IsBoosting = false;

            #region 무적시간
            Player.Instance.GetComponent<SpriteRenderer>().DOFade(0.5f, 0.5f).SetLoops(-1, LoopType.Yoyo);

            yield return new WaitForSeconds(ItemManager.inst.invincibilityTimer);

            Player.Instance.tag = "Player";
            Player.Instance.GetComponent<SpriteRenderer>().DOKill();
            Player.Instance.GetComponent<SpriteRenderer>().DOFade(1, 0);
            #endregion


            yield return new WaitForSeconds(boosterSpeed);

            Destroy(this.gameObject);
        }
    }
}
